using System;
using System.Reflection;
using GameFrameX.Runtime;
using GameFrameX.TestSupport;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    /// <summary>
    /// 迟加载程序集增量注册用例（C29）：
    /// 通过 Reflection.Emit 生成真实程序集并 Assembly.Load 字节流，验证 AssemblyLoad 钩子增量注册、
    /// Editor/Tests 门过滤、幂等去重、退订阻断与命名约定迟加载解析。
    /// </summary>
    [TestFixture]
    public class GameFrameXRuntimeLateAssemblyTests
    {
        private static int s_Counter;

        [SetUp]
        public void SetUp()
        {
            GameFrameXRuntimeManagerResolver.Reset();
            GameFrameXRuntimeAssemblyLoadHook.Unsubscribe();
        }

        [TearDown]
        public void TearDown()
        {
            GameFrameXRuntimeAssemblyLoadHook.Unsubscribe();
            GameFrameXRuntimeManagerResolver.Reset();
        }

        [Test]
        public void LateLoadedManager_IsRegisteredIncrementally_WithoutManualCall()
        {
            GameFrameXRuntimeAssemblyLoadHook.EnsureSubscribed();
            string token = NextToken();
            Assembly loaded = BuildAndLoadAssembly("GameFrameX.Late.Asm" + token, "Late" + token, true, "LateImpl" + token);

            Type interfaceType = loaded.GetType("GameFrameX.Late.ILate" + token + "Manager");
            Assert.NotNull(interfaceType, "emitted interface should exist in late assembly");

            Type resolved = GameFrameXRuntimeManagerResolver.Resolve(interfaceType, null);
            Assert.NotNull(resolved, "late-loaded manager implementation should be resolvable via descriptor");
            Assert.AreSame(loaded.GetType("GameFrameX.Late.LateImpl" + token), resolved);
        }

        [Test]
        public void TestsAssembly_IsFilteredByIncrementalGate()
        {
            GameFrameXRuntimeAssemblyLoadHook.EnsureSubscribed();
            string token = NextToken();
            // 程序集名以 .Tests 结尾 → 走 Editor/Tests 门；实现类名刻意偏离命名约定，排除约定兜底干扰。
            Assembly loaded = BuildAndLoadAssembly("GameFrameX.Late.Asm" + token + ".Tests", "Skipped" + token, true, "OffConvention" + token);

            Type interfaceType = loaded.GetType("GameFrameX.Late.ISkipped" + token + "Manager");
            Assert.NotNull(interfaceType);

            Type resolved = GameFrameXRuntimeManagerResolver.Resolve(interfaceType, null);
            Assert.Null(resolved, "assemblies matching the Tests gate must not register manager descriptors");
        }

        [Test]
        public void RegisterAssembly_IsIdempotent_ByAssemblyGranularity()
        {
            string token = NextToken();
            Assembly loaded = BuildAndLoadAssembly("GameFrameX.Late.Asm" + token, "Dup" + token, true, "DupImpl" + token);
            // AssemblyLoad 钩子未订阅时由手动注册驱动，重复注册必须幂等。
            GameFrameXRuntimeManagerResolver.RegisterAssembly(loaded);
            GameFrameXRuntimeManagerResolver.RegisterAssembly(loaded);

            Type interfaceType = loaded.GetType("GameFrameX.Late.IDup" + token + "Manager");
            var descriptorsField = typeof(GameFrameXRuntimeManagerResolver)
                .GetField("ManagerDescriptors", BindingFlags.NonPublic | BindingFlags.Static);
            Assert.NotNull(descriptorsField);
            var descriptors = (System.Collections.IDictionary)descriptorsField.GetValue(null);
            Assert.IsTrue(descriptors.Contains(interfaceType), "descriptor should be registered after manual registration");
            var list = (System.Collections.ICollection)descriptors[interfaceType];
            Assert.AreEqual(1, list.Count, "duplicate registration must not produce duplicate descriptors");
        }

        [Test]
        public void UnsubscribedHook_StopsIncrementalRegistration_AfterDescriptorsReady()
        {
            // 先经钩子注册 A：RegisterDescriptors 置位 s_DescriptorsReady，此后钩子是唯一的增量通道。
            // First register A via the hook: RegisterDescriptors sets s_DescriptorsReady, after which the hook is the only incremental channel.
            GameFrameXRuntimeAssemblyLoadHook.EnsureSubscribed();
            Assert.IsTrue(GameFrameXRuntimeAssemblyLoadHook.IsSubscribed);
            string tokenA = NextToken();
            Assembly loadedA = BuildAndLoadAssembly("GameFrameX.Late.Asm" + tokenA, "Sub" + tokenA, true, "SubImpl" + tokenA);
            Type interfaceA = loadedA.GetType("GameFrameX.Late.ISub" + tokenA + "Manager");
            Assert.NotNull(GameFrameXRuntimeManagerResolver.Resolve(interfaceA, null), "hook-registered assembly should resolve");

            // 退订后新加载程序集既不走钩子、也不再触发全量兜底（已 ready）→ 不再增量注册；
            // 这正是 Bootstrap 只在 SubsystemRegistration 退订重订、Shutdown 不退订的原因（Restart 语义见 Bootstrap 注释）。
            // After unsubscribing, newly loaded assemblies go neither through the hook nor the full fallback (already ready) → no incremental registration;
            // which is exactly why the Bootstrap only unsubscribes/resubscribes at SubsystemRegistration and never unsubscribes at Shutdown (Restart semantics, see Bootstrap comments).
            GameFrameXRuntimeAssemblyLoadHook.Unsubscribe();
            Assert.IsFalse(GameFrameXRuntimeAssemblyLoadHook.IsSubscribed);
            string tokenB = NextToken();
            Assembly loadedB = BuildAndLoadAssembly("GameFrameX.Late.Asm" + tokenB, "Sub" + tokenB, true, "SubImpl" + tokenB);
            Type interfaceB = loadedB.GetType("GameFrameX.Late.ISub" + tokenB + "Manager");
            Type resolvedB = GameFrameXRuntimeManagerResolver.Resolve(interfaceB, null);
            Assert.Null(resolvedB, "unsubscribed hook must stop incremental registration once descriptors are ready");
        }

        [Test]
        public void IncrementalRegistration_BeforeFirstResolve_DoesNotSuppressFullFallback()
        {
            // 首个 Resolve 前经钩子增量注册含描述符程序集，不得提前置位 s_DescriptorsReady：
            // 否则全量兜底被抑制，订阅之前已加载的启动程序集描述符永不注册（P1 修复回归钉）。
            // Incrementally registering a descriptor-bearing assembly via the hook before the first Resolve
            // must not set s_DescriptorsReady prematurely: the full fallback would be suppressed and
            // descriptors of startup assemblies loaded before subscription would never register (P1 fix pin).
            GameFrameXRuntimeAssemblyLoadHook.EnsureSubscribed();
            string token = NextToken();
            Assembly loaded = BuildAndLoadAssembly("GameFrameX.Late.Asm" + token, "Ready" + token, true, "ReadyImpl" + token);

            var readyField = typeof(GameFrameXRuntimeManagerResolver)
                .GetField("s_DescriptorsReady", BindingFlags.NonPublic | BindingFlags.Static);
            Assert.NotNull(readyField);
            Assert.IsFalse((bool)readyField.GetValue(null),
                "incremental registration before the first Resolve must not mark descriptors ready");

            // 首个 Resolve 触发全量兜底（去重集跳过已注册的迟加载程序集），描述符不重复。
            // The first Resolve triggers the full fallback (dedup skips the already-registered late assembly); no duplicate descriptors.
            Type interfaceType = loaded.GetType("GameFrameX.Late.IReady" + token + "Manager");
            Assert.NotNull(GameFrameXRuntimeManagerResolver.Resolve(interfaceType, null));
            Assert.IsTrue((bool)readyField.GetValue(null), "the full fallback must mark descriptors ready");

            var descriptorsField = typeof(GameFrameXRuntimeManagerResolver)
                .GetField("ManagerDescriptors", BindingFlags.NonPublic | BindingFlags.Static);
            var descriptors = (System.Collections.IDictionary)descriptorsField.GetValue(null);
            Assert.AreEqual(1, ((System.Collections.ICollection)descriptors[interfaceType]).Count,
                "full fallback must not duplicate hook-registered descriptors");
        }

        [Test]
        public void EnsureSubscribed_Twice_DoesNotDoubleSubscribe()
        {
            GameFrameXRuntimeAssemblyLoadHook.EnsureSubscribed();
            GameFrameXRuntimeAssemblyLoadHook.EnsureSubscribed();
            Assert.IsTrue(GameFrameXRuntimeAssemblyLoadHook.IsSubscribed);

            string token = NextToken();
            Assembly loaded = BuildAndLoadAssembly("GameFrameX.Late.Asm" + token, "Once" + token, true, "OnceImpl" + token);

            Type interfaceType = loaded.GetType("GameFrameX.Late.IOnce" + token + "Manager");
            var descriptorsField = typeof(GameFrameXRuntimeManagerResolver)
                .GetField("ManagerDescriptors", BindingFlags.NonPublic | BindingFlags.Static);
            var descriptors = (System.Collections.IDictionary)descriptorsField.GetValue(null);
            Assert.IsTrue(descriptors.Contains(interfaceType));
            Assert.AreEqual(1, ((System.Collections.ICollection)descriptors[interfaceType]).Count,
                "double EnsureSubscribed must not produce duplicate descriptors");
        }

        [Test]
        public void NamingConvention_ResolvesLateLoadedType_ViaLiveAssemblyFetch()
        {
            string token = NextToken();
            // 接口不继承 IGameFrameworkModule → 不产生描述符，仅剩命名约定兜底路径（实现类名须符合 I{X}Manager → {X}Manager 约定）。
            // The interface does not extend IGameFrameworkModule → no descriptors; only the naming convention fallback remains (implementation name must follow I{X}Manager → {X}Manager).
            Assembly loaded = BuildAndLoadAssembly("GameFrameX.Late.Conv" + token, "Conv" + token, false, "Conv" + token + "Manager");

            Type interfaceType = loaded.GetType("GameFrameX.Late.IConv" + token + "Manager");
            Assert.NotNull(interfaceType);

            Type byUtility = Utility.Assembly.GetType("GameFrameX.Late.Conv" + token + "Manager");
            Assert.NotNull(byUtility, "Utility.Assembly.GetType must resolve types from late-loaded assemblies (live fetch)");

            Type resolved = GameFrameXRuntimeManagerResolver.Resolve(interfaceType, null);
            Assert.NotNull(resolved, "naming convention fallback must see late-loaded assemblies");
            Assert.AreSame(byUtility, resolved);
        }

        private static string NextToken()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }

        /// <summary>
        /// 经预编译测试支持 DLL（Reflection.Emit，netstandard2.1 编译面不可用）生成并加载真实程序集：
        /// 可选定义 IGameFrameworkModule 派生接口 + 实现类（产生 Manager 描述符），
        /// 或仅定义普通接口 + 实现类（仅命名约定可见）。Assembly.LoadFrom 必然触发 AssemblyLoad 事件并进入 AppDomain 程序集列表。
        /// </summary>
        private static Assembly BuildAndLoadAssembly(string assemblyName, string typeName, bool managerPath, string implementationName)
        {
            return LateAssemblyEmitSupport.BuildAndLoad(assemblyName, typeName, implementationName, managerPath);
        }
    }
}
