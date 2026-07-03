using System.Reflection;
using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;

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

            GameEntry.Shutdown(ShutdownType.None);
            GameFrameworkEntry.Shutdown();
            GameFrameXRuntimeManagerResolver.Reset();
        }

        [Test]
        public void Awake_AutoResolvedManager_WritesComponentTypeForInspector()
        {
            m_GameObject = new GameObject("Runtime Manager Component Test");
            var component = m_GameObject.AddComponent<TestRuntimeManagerComponent>();

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
            scanResult.ComponentTypes.Add(typeof(ObjectPoolComponent));
            scanResult.ComponentTypes.Add(typeof(ReferencePoolComponent));
            scanResult.ComponentTypes.Add(typeof(BaseComponent));

            var plan = GameFrameXRuntimePlanner.CreatePlan(scanResult, null);

            Assert.AreEqual(typeof(BaseComponent), plan.Components[0].ComponentType);
            Assert.AreEqual(typeof(ReferencePoolComponent), plan.Components[1].ComponentType);
            Assert.AreEqual(typeof(ObjectPoolComponent), plan.Components[2].ComponentType);
            Assert.AreEqual(-10000, plan.Components[0].Order);
            Assert.AreEqual(-9000, plan.Components[1].Order);
            Assert.AreEqual(-8000, plan.Components[2].Order);
        }

        private interface ITestRuntimeManager
        {
        }

        private sealed class TestRuntimeManager : GameFrameworkModule, ITestRuntimeManager
        {
            protected override void Update(float elapseSeconds, float realElapseSeconds)
            {
            }

            protected override void Shutdown()
            {
            }
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
