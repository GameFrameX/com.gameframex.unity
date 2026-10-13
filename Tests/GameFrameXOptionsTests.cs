using System;
using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace GameFrameX.Tests
{
    /// <summary>
    /// C9 配置终态契约测试：GameFrameXOptions 注册表（时机响亮失败 / 未消费诊断）、
    /// 模块启动期全量实例化（eager + LazyModule 逃生口）。
    /// </summary>
    [TestFixture]
    public sealed class GameFrameXOptionsTests
    {
        [SetUp]
        public void SetUp()
        {
            GameFrameworkEntry.Shutdown();
            GameFrameXOptions.Reset();
            GameFrameXRuntimeHost.Reset();
            ProbeModule.Constructions = 0;
            LazyProbeModule.Constructions = 0;
        }

        [TearDown]
        public void TearDown()
        {
            GameFrameworkEntry.Shutdown();
            GameFrameXOptions.Reset();
            GameFrameXRuntimeHost.Reset();
        }

        [Test]
        public void Configure_ThenTryGet_ReturnsValue()
        {
            GameFrameXOptions.Configure(new ProbeOptions { Value = 42 });
            Assert.IsTrue(GameFrameXOptions.TryGet<ProbeOptions>(out var options));
            Assert.IsNotNull(options);
            Assert.AreEqual(42, options.Value);
        }

        [Test]
        public void TryGet_Unconfigured_ReturnsFalse_And_MarksDemanded()
        {
            Assert.IsFalse(GameFrameXOptions.TryGet<ProbeOptions>(out var options), "未注册时应返回 false");
            Assert.IsNull(options);

            // 模块已消费（demand 已标记）后再 Configure → 时机违例响亮失败
            Assert.Throws<GameFrameworkException>(() => GameFrameXOptions.Configure(new ProbeOptions()));
        }

        [Test]
        public void Configure_AfterModuleConsumed_Throws()
        {
            GameFrameXOptions.Configure(new ProbeOptions { Value = 1 });
            GameFrameXOptions.TryGet<ProbeOptions>(out _); // 模块消费

            Assert.Throws<GameFrameworkException>(() => GameFrameXOptions.Configure(new ProbeOptions { Value = 2 }),
                "模块已消费 Options 后重复 Configure 应抛异常");
        }

        [Test]
        public void Configure_OverwriteBeforeDemand_LastWins()
        {
            GameFrameXOptions.Configure(new ProbeOptions { Value = 1 });
            GameFrameXOptions.Configure(new ProbeOptions { Value = 2 }); // 未被消费前允许覆盖

            GameFrameXOptions.TryGet<ProbeOptions>(out var options);
            Assert.AreEqual(2, options.Value);
        }

        [Test]
        public void Reset_WarnsForUnconsumedOptions()
        {
            GameFrameXOptions.Configure(new ProbeOptions()); // 从未被任何模块消费

            LogAssert.Expect(LogType.Warning, new System.Text.RegularExpressions.Regex("从未被消费"));
            GameFrameXOptions.Reset();

            // Reset 清理后可重新 Configure
            Assert.DoesNotThrow(() => GameFrameXOptions.Configure(new ProbeOptions()));
        }

        [Test]
        public void InstantiateAllModules_CreatesAllRegisteredModules()
        {
            GameFrameworkEntry.RegisterModule(typeof(IProbeManager), typeof(ProbeModule));
            GameFrameworkEntry.RegisterModule(typeof(IProbeOtherManager), typeof(ProbeOtherModule));

            GameFrameworkEntry.InstantiateAllModules();

            Assert.AreEqual(1, ProbeModule.Constructions, "eager 应创建 ProbeModule 一次");
            Assert.AreEqual(1, ProbeOtherModule.Constructions);
        }

        [Test]
        public void InstantiateAllModules_SkipsLazyModule()
        {
            GameFrameworkEntry.RegisterModule(typeof(IProbeManager), typeof(ProbeModule));
            GameFrameworkEntry.RegisterModule(typeof(ILazyProbeManager), typeof(LazyProbeModule));

            GameFrameworkEntry.InstantiateAllModules();

            Assert.AreEqual(1, ProbeModule.Constructions, "普通模块应被 eager 创建");
            Assert.AreEqual(0, LazyProbeModule.Constructions, "[GameFrameXLazyModule] 模块应跳过 eager，保持惰性");

            var lazy = GameFrameworkEntry.GetModule(typeof(LazyProbeModule)); // 惰性兜底仍可创建
            Assert.AreEqual(1, LazyProbeModule.Constructions);
        }

        [Test]
        public void InstantiateAllModules_IsIdempotent()
        {
            GameFrameworkEntry.RegisterModule(typeof(IProbeManager), typeof(ProbeModule));

            GameFrameworkEntry.InstantiateAllModules();
            GameFrameworkEntry.InstantiateAllModules();

            Assert.AreEqual(1, ProbeModule.Constructions, "重复调用不应重复创建");
        }

        [Test]
        public void Shutdown_ClearsOptionsAndWarnsUnconsumed()
        {
            GameFrameXOptions.Configure(new ProbeOptions()); // 未消费

            LogAssert.Expect(LogType.Warning, new System.Text.RegularExpressions.Regex("从未被消费"));
            GameFrameworkEntry.Shutdown();

            // Shutdown 清理后可重新 Configure
            Assert.DoesNotThrow(() => GameFrameXOptions.Configure(new ProbeOptions()));
        }

        private sealed class ProbeOptions
        {
            public int Value;
        }

        private interface IProbeManager
        {
        }

        private interface IProbeOtherManager
        {
        }

        private interface ILazyProbeManager
        {
        }

        private sealed class ProbeModule : IGameFrameworkModule, IProbeManager
        {
            public static int Constructions;

            public ProbeModule()
            {
                Constructions++;
            }
        }

        private sealed class ProbeOtherModule : IGameFrameworkModule, IProbeOtherManager
        {
            public static int Constructions;

            public ProbeOtherModule()
            {
                Constructions++;
            }
        }

        [GameFrameXLazyModule]
        private sealed class LazyProbeModule : IGameFrameworkModule, ILazyProbeManager
        {
            public static int Constructions;

            public LazyProbeModule()
            {
                Constructions++;
            }
        }
    }
}
