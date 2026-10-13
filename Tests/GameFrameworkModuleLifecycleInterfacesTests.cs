using System.Collections.Generic;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public sealed class GameFrameworkModuleLifecycleInterfacesTests
    {
        private static readonly List<string> s_Log = new List<string>();
        private static int s_HighInstanceCount;

        [SetUp]
        public void SetUp()
        {
            GameFrameworkEntry.Shutdown();
            s_Log.Clear();
            s_HighInstanceCount = 0;
        }

        [TearDown]
        public void TearDown()
        {
            GameFrameworkEntry.Shutdown();
            s_Log.Clear();
        }

        [Test]
        public void Update_DispatchesOnlyToUpdatableModules_InPriorityOrder()
        {
            RegisterAllProbes();

            GameFrameworkEntry.GetModule<ILifecycleProbePlain>();
            GameFrameworkEntry.GetModule<ILifecycleProbeTickOnly>();
            GameFrameworkEntry.GetModule<ILifecycleProbeLow>();
            GameFrameworkEntry.GetModule<ILifecycleProbeHigh>();

            GameFrameworkEntry.Update(1f, 1f);
            GameFrameworkEntry.Update(1f, 1f);

            // 列表按 Priority 降维有序：high(7) -> low(1) -> 同优先级按创建顺序 tickonly(0)；plain 未声明能力不被调用。
            Assert.AreEqual(
                new[]
                {
                    "high:update", "low:update", "tickonly:update",
                    "high:update", "low:update", "tickonly:update",
                },
                s_Log.ToArray());
        }

        [Test]
        public void Update_PlainModuleWithoutCapabilities_NeverDispatched()
        {
            RegisterAllProbes();

            GameFrameworkEntry.GetModule<ILifecycleProbePlain>();
            GameFrameworkEntry.GetModule<ILifecycleProbeHigh>();
            s_Log.Clear();

            GameFrameworkEntry.Update(1f, 1f);
            GameFrameworkEntry.Shutdown();

            CollectionAssert.AreEquivalent(new[] { "high:update", "high:shutdown" }, s_Log);
        }

        [Test]
        public void Shutdown_RunsReversePriorityOrder_AndSkipsNonShutdownable()
        {
            RegisterAllProbes();

            GameFrameworkEntry.GetModule<ILifecycleProbeTickOnly>();
            GameFrameworkEntry.GetModule<ILifecycleProbePlain>();
            GameFrameworkEntry.GetModule<ILifecycleProbeLow>();
            GameFrameworkEntry.GetModule<ILifecycleProbeHigh>();
            s_Log.Clear();

            GameFrameworkEntry.Shutdown();

            // 列表顺序 high -> low -> tickonly -> plain，反序关闭：low 先于 high；tickonly/plain 未声明 IShutdownableModule 被跳过。
            Assert.AreEqual(new[] { "low:shutdown", "high:shutdown" }, s_Log.ToArray());
        }

        [Test]
        public void Restart_AfterShutdown_CreatesFreshModulesAndTicksAgain()
        {
            RegisterAllProbes();

            var first = GameFrameworkEntry.GetModule<ILifecycleProbeHigh>();
            GameFrameworkEntry.Update(1f, 1f);
            GameFrameworkEntry.Shutdown();
            RegisterAllProbes(); // Shutdown 清空模块类型映射（C8 起），重启会话须重新注册
            s_Log.Clear();

            var second = GameFrameworkEntry.GetModule<ILifecycleProbeHigh>();
            Assert.AreNotSame(first, second);
            Assert.AreEqual(2, s_HighInstanceCount);

            GameFrameworkEntry.Update(1f, 1f);
            GameFrameworkEntry.Shutdown();

            Assert.AreEqual(new[] { "high:update", "high:shutdown" }, s_Log.ToArray());
        }

        private static void RegisterAllProbes()
        {
            GameFrameworkEntry.RegisterModule(typeof(ILifecycleProbePlain), typeof(LifecycleProbePlain));
            GameFrameworkEntry.RegisterModule(typeof(ILifecycleProbeTickOnly), typeof(LifecycleProbeTickOnly));
            GameFrameworkEntry.RegisterModule(typeof(ILifecycleProbeLow), typeof(LifecycleProbeLow));
            GameFrameworkEntry.RegisterModule(typeof(ILifecycleProbeHigh), typeof(LifecycleProbeHigh));
        }

        private interface ILifecycleProbePlain
        {
        }

        private interface ILifecycleProbeTickOnly
        {
        }

        private interface ILifecycleProbeLow
        {
        }

        private interface ILifecycleProbeHigh
        {
        }

        private sealed class LifecycleProbePlain : IGameFrameworkModule, ILifecycleProbePlain
        {
        }

        private sealed class LifecycleProbeTickOnly : IGameFrameworkModule, ILifecycleProbeTickOnly, IUpdatableModule
        {
            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                s_Log.Add("tickonly:update");
            }
        }

        private sealed class LifecycleProbeLow : IGameFrameworkModule, ILifecycleProbeLow, IUpdatableModule, IShutdownableModule, IPrioritizedModule
        {
            public int Priority
            {
                get { return 1; }
            }

            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                s_Log.Add("low:update");
            }

            public void Shutdown()
            {
                s_Log.Add("low:shutdown");
            }
        }

        private sealed class LifecycleProbeHigh : IGameFrameworkModule, ILifecycleProbeHigh, IUpdatableModule, IShutdownableModule, IPrioritizedModule
        {
            public LifecycleProbeHigh()
            {
                s_HighInstanceCount++;
            }

            public int Priority
            {
                get { return 7; }
            }

            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                s_Log.Add("high:update");
            }

            public void Shutdown()
            {
                s_Log.Add("high:shutdown");
            }
        }
    }
}
