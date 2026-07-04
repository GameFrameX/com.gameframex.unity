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
            GameFrameXRuntimeModeResolver.Reset();
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
        public void Resolve_NoBaseComponent_UsesAutoPackageMode()
        {
            GameFrameXRuntimeModeResolver.Reset();

            var result = GameFrameXRuntimeModeResolver.Resolve();

            Assert.AreEqual(GameFrameXRuntimeMode.AutoPackageMode, result.Mode);
            Assert.AreEqual(result, GameFrameXRuntimeModeResolver.LastResult);
            Assert.IsTrue(result.Reason.Contains("No registered or scene BaseComponent"));
        }

        [Test]
        public void Resolve_SceneBaseComponent_UsesManualSceneMode()
        {
            GameFrameXRuntimeModeResolver.Reset();
            m_GameObject = new GameObject("Manual GFX");
            m_GameObject.AddComponent<BaseComponent>();

            var result = GameFrameXRuntimeModeResolver.Resolve();

            Assert.AreEqual(GameFrameXRuntimeMode.ManualSceneMode, result.Mode);
            Assert.AreEqual("Manual GFX", result.ManualEntryPath);
        }

        [Test]
        public void RecordManualSceneMode_DoesNotCreateAutoHostPlan()
        {
            GameFrameXRuntimeHost.Reset();
            var result = new GameFrameXRuntimeModeResult(
                GameFrameXRuntimeMode.ManualSceneMode,
                "Unit test manual entry.",
                "Manual GFX");

            GameFrameXRuntimeHost.RecordManualSceneMode(result);

            Assert.AreEqual(result, GameFrameXRuntimeHost.LastModeResult);
            Assert.IsNotNull(GameFrameXRuntimeHost.LastPlan);
            Assert.AreEqual(0, GameFrameXRuntimeHost.LastPlan.Components.Count);
            Assert.IsTrue(GameFrameXRuntimeHost.LastPlan.Diagnostics[0].Contains("ManualSceneMode"));
            Assert.IsNull(GameObject.Find("[GameFrameX]"));
        }

        [Test]
        public void Reset_ClearsRuntimeModeState()
        {
            GameFrameXRuntimeModeResolver.Resolve();
            GameFrameXRuntimeHost.RecordManualSceneMode(new GameFrameXRuntimeModeResult(
                GameFrameXRuntimeMode.ManualSceneMode,
                "Unit test manual entry.",
                "Manual GFX"));

            GameFrameXRuntimeModeResolver.Reset();
            GameFrameXRuntimeHost.Reset();

            Assert.IsNull(GameFrameXRuntimeModeResolver.LastResult);
            Assert.IsNull(GameFrameXRuntimeHost.LastModeResult);
            Assert.IsNull(GameFrameXRuntimeHost.LastPlan);
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
