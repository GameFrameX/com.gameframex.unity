using System;
using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace GameFrameX.Tests
{
    /// <summary>
    /// C28 静默贡献者发现测试：Options 贡献者（发现 / 应用 / 手动优先双顺序 / 幂等 / Reset / 无参构造缺失诊断）
    /// 与 Override Provider 扫描发现（免手动注册即生效）。
    /// </summary>
    /// <remarks>
    /// Tests 程序集被扫描器结构性跳过（与 Manager 发现同门），因此发现后的行为经 internal 注入口
    /// （Apply / CollectOverrides(scanResult)）以反射驱动——与仓内既有 EditMode 反射驱动先例一致。
    /// </remarks>
    [TestFixture]
    public sealed class GameFrameXRuntimeContributorTests
    {
        [SetUp]
        public void SetUp()
        {
            GameFrameworkEntry.Shutdown();
            GameFrameXOptions.Reset();
            GameFrameXRuntimeHost.Reset();
            GameFrameXRuntimeContributorHost.Reset();
            GameFrameXRuntimeOverrideProvider.ClearProviders();
        }

        [TearDown]
        public void TearDown()
        {
            GameFrameworkEntry.Shutdown();
            GameFrameXOptions.Reset();
            GameFrameXRuntimeHost.Reset();
            GameFrameXRuntimeContributorHost.Reset();
            GameFrameXRuntimeOverrideProvider.ClearProviders();
        }

        [Test]
        public void Apply_DiscoveredContributor_WritesOptions()
        {
            ApplyContributors(typeof(ProbeContributor));

            Assert.IsTrue(GameFrameXOptions.TryGet<ProbeOptions>(out var options));
            Assert.IsNotNull(options);
            Assert.AreEqual(7, options.Value, "裸贡献者（无属性 / 无注册代码）的 Options 应被应用");
        }

        [Test]
        public void Apply_ManualConfigureFirst_ContributorSkipped_ManualWins()
        {
            // BeforeSceneLoad 阶段顺序一：游戏手动 Configure 先执行 → 贡献者按类型粒度跳过
            GameFrameXOptions.Configure(new ProbeOptions { Value = 42 });

            LogAssert.Expect(LogType.Warning, new System.Text.RegularExpressions.Regex(@"\[GameFrameX\].*手动值优先"));
            ApplyContributors(typeof(ProbeContributor));

            Assert.IsTrue(GameFrameXOptions.TryGet<ProbeOptions>(out var options));
            Assert.AreEqual(42, options.Value, "手动 Configure 过的类型，贡献者写入被跳过且值保持手动值");
        }

        [Test]
        public void Apply_ContributorFirst_ManualOverwriteWins()
        {
            // BeforeSceneLoad 阶段顺序二：贡献者先应用 → 游戏后写覆盖（未被消费前后写胜出）
            ApplyContributors(typeof(ProbeContributor));
            GameFrameXOptions.Configure(new ProbeOptions { Value = 42 });

            Assert.IsTrue(GameFrameXOptions.TryGet<ProbeOptions>(out var options));
            Assert.AreEqual(42, options.Value, "两种执行顺序下手动值均必胜：后写覆盖贡献者值");
        }

        [Test]
        public void EnsureApplied_OncePerDomain_ResetAllowsReapply()
        {
            GameFrameXRuntimeContributorHost.EnsureApplied();
            GameFrameXRuntimeContributorHost.EnsureApplied(); // 幂等：重复调用不应抛异常

            var appliedField = typeof(GameFrameXRuntimeContributorHost).GetField("s_Applied",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            Assert.IsNotNull(appliedField);
            Assert.IsTrue((bool)appliedField.GetValue(null), "应用后应标记已应用（每 domain 一次）");

            GameFrameXRuntimeContributorHost.Reset();
            Assert.IsFalse((bool)appliedField.GetValue(null), "Reset 后应允许下个 domain 重新应用");
        }

        [Test]
        public void Bootstrap_BeforeSceneLoad_EntryAppliesContributors_WithoutThrowing()
        {
            // 双入口之一：BeforeSceneLoad 接线后不应抛异常，且 Options 仍可继续手动 Configure（未被消费）
            var beforeSceneLoad = typeof(GameFrameXRuntimeBootstrap).GetMethod("BeforeSceneLoad",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            Assert.IsNotNull(beforeSceneLoad, "Bootstrap 应保留 BeforeSceneLoad 入口");
            Assert.DoesNotThrow(() => beforeSceneLoad.Invoke(null, null));
            Assert.DoesNotThrow(() => GameFrameXOptions.Configure(new ProbeOptions { Value = 1 }));
        }

        [Test]
        public void Apply_MissingParameterlessConstructor_WarnsAndSkips()
        {
            LogAssert.Expect(LogType.Warning, new System.Text.RegularExpressions.Regex(@"\[GameFrameX\].*NoDefaultCtorContributor.*无参构造"));
            ApplyContributors(typeof(NoDefaultCtorContributor));

            Assert.IsFalse(GameFrameXOptions.TryGet<ProbeOptions>(out _), "缺无参构造的贡献者应被跳过（不产生 Options）");
        }

        [Test]
        public void Apply_ContributorThrows_WarnsAndContinues()
        {
            LogAssert.Expect(LogType.Warning, new System.Text.RegularExpressions.Regex(@"\[GameFrameX\].*ThrowingContributor.*异常"));
            ApplyContributors(typeof(ThrowingContributor), typeof(ProbeContributor));

            Assert.IsTrue(GameFrameXOptions.TryGet<ProbeOptions>(out var options), "单个贡献者抛异常不应中断其余贡献者应用");
            Assert.AreEqual(7, options.Value);
        }

        [Test]
        public void CollectOverrides_DiscoversUnregisteredProvider()
        {
            var context = CollectOverridesWithScan(typeof(ProbeOverrideProvider));

            Assert.AreEqual(1, context.GetManagerOverrides().Count, "Provider 不调 RegisterProvider 即应被扫描发现生效");
        }

        [Test]
        public void CollectOverrides_ManuallyRegisteredType_NotDuplicatedByScan()
        {
            GameFrameXRuntimeOverrideProvider.RegisterProvider(new ProbeOverrideProvider());

            var context = CollectOverridesWithScan(typeof(ProbeOverrideProvider));

            Assert.AreEqual(1, context.GetManagerOverrides().Count, "手动注册过的同类型实例优先，扫描不应重复收集覆盖");
        }

        [Test]
        public void CollectOverrides_MissingParameterlessConstructor_WarnsAndSkips()
        {
            LogAssert.Expect(LogType.Warning, new System.Text.RegularExpressions.Regex(@"\[GameFrameX\].*NoDefaultCtorProvider.*无参构造"));
            var context = CollectOverridesWithScan(typeof(NoDefaultCtorProvider));

            Assert.AreEqual(0, context.GetManagerOverrides().Count);
        }

        [Test]
        public void Scan_IgnoresTestAssemblyContributorAndProviderTypes()
        {
            var scanResult = GameFrameXRuntimeScanner.Scan();

            Assert.IsFalse(scanResult.OptionsContributorTypes.Contains(typeof(ProbeContributor)), "Tests 程序集的贡献者不应被发现");
            Assert.IsFalse(scanResult.OverrideProviderTypes.Contains(typeof(ProbeOverrideProvider)), "Tests 程序集的 Provider 不应被发现");
        }

        private static void ApplyContributors(params Type[] contributorTypes)
        {
            var scanResult = new GameFrameXRuntimeScanResult();
            foreach (var contributorType in contributorTypes)
            {
                scanResult.OptionsContributorTypes.Add(contributorType);
            }

            typeof(GameFrameXRuntimeContributorHost)
                .GetMethod("Apply", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .Invoke(null, new object[] { scanResult });
        }

        private static GameFrameXRuntimeOverrideContext CollectOverridesWithScan(params Type[] providerTypes)
        {
            var scanResult = new GameFrameXRuntimeScanResult();
            foreach (var providerType in providerTypes)
            {
                scanResult.OverrideProviderTypes.Add(providerType);
            }

            var method = typeof(GameFrameXRuntimeOverrideProvider).GetMethod(
                "CollectOverrides",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic,
                null,
                new[] { typeof(GameFrameXRuntimeScanResult) },
                null);
            Assert.IsNotNull(method);
            return (GameFrameXRuntimeOverrideContext)method.Invoke(null, new object[] { scanResult });
        }

        private sealed class ProbeOptions
        {
            public int Value;
        }

        private sealed class ProbeContributor : IGameFrameXOptionsContributor
        {
            public void ConfigureOptions(GameFrameXOptionsContributorContext context)
            {
                context.Configure(new ProbeOptions { Value = 7 });
            }
        }

        private sealed class NoDefaultCtorContributor : IGameFrameXOptionsContributor
        {
            public NoDefaultCtorContributor(int value)
            {
                _ = value;
            }

            public void ConfigureOptions(GameFrameXOptionsContributorContext context)
            {
                context.Configure(new ProbeOptions());
            }
        }

        private sealed class ThrowingContributor : IGameFrameXOptionsContributor
        {
            public void ConfigureOptions(GameFrameXOptionsContributorContext context)
            {
                throw new InvalidOperationException("contributor failure probe");
            }
        }

        private interface IProbeOverrideManager
        {
        }

        private sealed class ProbeOverrideManager : IProbeOverrideManager
        {
        }

        private sealed class ProbeOverrideProvider : IGameFrameXRuntimeOverrideProvider
        {
            public void CollectOverrides(GameFrameXRuntimeOverrideContext context)
            {
                context.SetManagerImplementation(typeof(IProbeOverrideManager), typeof(ProbeOverrideManager));
            }
        }

        private sealed class NoDefaultCtorProvider : IGameFrameXRuntimeOverrideProvider
        {
            public NoDefaultCtorProvider(int value)
            {
                _ = value;
            }

            public void CollectOverrides(GameFrameXRuntimeOverrideContext context)
            {
                context.SetManagerImplementation(typeof(IProbeOverrideManager), typeof(ProbeOverrideManager));
            }
        }
    }
}
