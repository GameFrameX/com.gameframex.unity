using System.Reflection;
using System.Text.RegularExpressions;
using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace GameFrameX.Tests
{
    [TestFixture]
    public sealed class GameFrameworkComponentRuntimeManagerTests
    {
        private GameObject m_GameObject;

        [TearDown]
        public void TearDown()
        {
            if (m_GameObject != null)
            {
                Object.DestroyImmediate(m_GameObject);
                m_GameObject = null;
            }

            // EditMode 下 EnsureStarted 建立的 [GameFrameX] Host 树不会随 Play 结束销毁，必须显式清理，否则泄漏进打开的场景。
            var hostField = typeof(GameFrameXRuntimeHost).GetField("s_Host", BindingFlags.Static | BindingFlags.NonPublic);
            var host = hostField.GetValue(null) as GameObject;
            if (host != null)
            {
                Object.DestroyImmediate(host);
            }

            GameEntry.Shutdown(ShutdownType.None);
            GameFrameworkEntry.Shutdown();
            GameFrameXRuntimeHost.Reset();
            GameFrameXRuntimeManagerResolver.Reset();
        }

        [Test]
        public void Awake_AutoResolvedManager_WritesComponentTypeForInspector()
        {
            GameFrameXRuntimeManagerResolver.RegisterDescriptors(new[]
            {
                new GameFrameXRuntimeManagerDescriptor(typeof(ITestRuntimeManager), typeof(TestRuntimeManager), 0),
            });
            Assert.AreEqual(
                typeof(TestRuntimeManager),
                GameFrameXRuntimeManagerResolver.Resolve(typeof(ITestRuntimeManager), null));

            m_GameObject = new GameObject("Runtime Manager Component Test");
            var component = m_GameObject.AddComponent<TestRuntimeManagerComponent>();

            // EditMode 下 AddComponent 不触发 Awake，反射驱动还原运行时行为
            var awakeMethod = component.GetType().GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(awakeMethod, "组件应具有 Awake 生命周期方法");
            awakeMethod.Invoke(component, null);

            var componentTypeField = typeof(GameFrameworkComponent).GetField(
                "componentType",
                BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(componentTypeField);
            Assert.AreEqual(typeof(TestRuntimeManager).FullName, componentTypeField.GetValue(component));
        }

        [Test]
        public void CreatePlan_UsesComponentAttributeOrder()
        {
            var scanResult = new GameFrameXRuntimeScanResult();
            scanResult.ComponentTypes.Add(typeof(DefaultRuntimeOrderComponent));
            scanResult.ComponentTypes.Add(typeof(LateRuntimeOrderComponent));
            scanResult.ComponentTypes.Add(typeof(EarlyRuntimeOrderComponent));

            var plan = GameFrameXRuntimePlanner.CreatePlan(scanResult, null);

            Assert.AreEqual(typeof(EarlyRuntimeOrderComponent), plan.Components[0].ComponentType);
            Assert.AreEqual(typeof(DefaultRuntimeOrderComponent), plan.Components[1].ComponentType);
            Assert.AreEqual(typeof(LateRuntimeOrderComponent), plan.Components[2].ComponentType);
            Assert.AreEqual(-10, plan.Components[0].Order);
            Assert.AreEqual(0, plan.Components[1].Order);
            Assert.AreEqual(10, plan.Components[2].Order);
        }

        [Test]
        public void CreatePlan_RuntimeOverrideOrderWinsOverAttributeOrder()
        {
            var scanResult = new GameFrameXRuntimeScanResult();
            scanResult.ComponentTypes.Add(typeof(LateRuntimeOrderComponent));
            scanResult.ComponentTypes.Add(typeof(EarlyRuntimeOrderComponent));
            var overrideContext = new GameFrameXRuntimeOverrideContext();
            overrideContext.SetComponentOrder(typeof(EarlyRuntimeOrderComponent), 20);

            var plan = GameFrameXRuntimePlanner.CreatePlan(scanResult, overrideContext);

            Assert.AreEqual(typeof(LateRuntimeOrderComponent), plan.Components[0].ComponentType);
            Assert.AreEqual(typeof(EarlyRuntimeOrderComponent), plan.Components[1].ComponentType);
            Assert.AreEqual(20, plan.Components[1].Order);
        }

        [Test]
        public void CreatePlan_UsesCoreComponentAttributeOrder()
        {
            var scanResult = new GameFrameXRuntimeScanResult();
            scanResult.ComponentTypes.Add(typeof(ReferencePoolComponent));
            scanResult.ComponentTypes.Add(typeof(BaseComponent));

            var plan = GameFrameXRuntimePlanner.CreatePlan(scanResult, null);

            Assert.AreEqual(typeof(BaseComponent), plan.Components[0].ComponentType);
            Assert.AreEqual(typeof(ReferencePoolComponent), plan.Components[1].ComponentType);
            Assert.AreEqual(-10000, plan.Components[0].Order);
            Assert.AreEqual(-9000, plan.Components[1].Order);
        }

        [Test]
        public void Scan_IgnoresUnitTestAssemblyTypes()
        {
            var scanResult = GameFrameXRuntimeScanner.Scan();

            Assert.IsFalse(scanResult.ComponentTypes.Contains(typeof(TestRuntimeManagerComponent)));
            Assert.IsFalse(scanResult.ComponentTypes.Contains(typeof(DefaultRuntimeOrderComponent)));

            foreach (var descriptor in scanResult.ManagerDescriptors)
            {
                Assert.AreNotEqual(typeof(TestRuntimeManager), descriptor.ImplementationType);
            }
        }

        [Test]
        public void Awake_WhenAutoInjectionClaimed_SuppressesSceneEntry()
        {
            GameFrameXRuntimeHost.Reset();
            m_GameObject = new GameObject("Manual GFX");
            var component = m_GameObject.AddComponent<TestRuntimeManagerComponent>();

            // Bootstrap 的 BeforeSceneLoad 宣告接管后，场景内手工摆放的 GFX 组件必须退场。
            GameFrameXRuntimeHost.ClaimAutoInjection();
            Assert.IsTrue(GameFrameXRuntimeHost.IsAutoInjectionClaimed);

            var awakeMethod = component.GetType().GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic);
            LogAssert.Expect(LogType.Warning, new Regex(@"\[GameFrameX\].*was suppressed"));
            awakeMethod.Invoke(component, null);

            Assert.IsFalse(component.enabled, "接管生效时场景残留组件应被禁用");
            Assert.IsNull(GameEntry.GetComponent<TestRuntimeManagerComponent>(), "被抑制的组件不得注册到 GameEntry");
        }

        [Test]
        public void EnsureStarted_ClearsClaimBeforeMountingComponents()
        {
            GameFrameXRuntimeHost.Reset();
            GameFrameXRuntimeHost.ClaimAutoInjection();
            Assert.IsTrue(GameFrameXRuntimeHost.IsAutoInjectionClaimed);

            GameFrameXRuntimeHost.EnsureStarted();

            Assert.IsFalse(GameFrameXRuntimeHost.IsAutoInjectionClaimed,
                "Host 挂载自有组件前必须清除接管标记，否则自己挂的组件会被抑制");
            Assert.IsNotNull(GameFrameXRuntimeHost.LastPlan);
        }

        [Test]
        public void Reset_ClearsAutoInjectionState()
        {
            GameFrameXRuntimeHost.ClaimAutoInjection();
            GameFrameXRuntimeHost.EnsureStarted();

            GameFrameXRuntimeHost.Reset();

            Assert.IsFalse(GameFrameXRuntimeHost.IsAutoInjectionClaimed);
            Assert.IsNull(GameFrameXRuntimeHost.LastPlan);
        }

        private interface ITestRuntimeManager
        {
        }

        private sealed class TestRuntimeManager : IGameFrameworkModule, ITestRuntimeManager
        {

        }

        private sealed class TestRuntimeManagerComponent : GameFrameworkComponent
        {
            protected override void Awake()
            {
                InterfaceComponentType = typeof(ITestRuntimeManager);
                base.Awake();
            }
        }

        private sealed class DefaultRuntimeOrderComponent : GameFrameworkComponent
        {
        }

        [GameFrameXAutoComponent(-10)]
        private sealed class EarlyRuntimeOrderComponent : GameFrameworkComponent
        {
        }

        [GameFrameXAutoComponent(10)]
        private sealed class LateRuntimeOrderComponent : GameFrameworkComponent
        {
        }
    }
}
