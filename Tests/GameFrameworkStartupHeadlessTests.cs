using System.Reflection;
using GameFrameX.ObjectPool;
using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace GameFrameX.Tests
{
    /// <summary>
    /// Headless 启动路径宿主模拟器测试：
    /// 纯 C#、零 MonoBehaviour、零 UnityEngine.Object 创建，
    /// 覆盖三行契约（Headless → 驱动 → Shutdown 必调）与可重入约束。
    /// </summary>
    [TestFixture]
    public sealed class GameFrameworkStartupHeadlessTests
    {
        [SetUp]
        public void SetUp()
        {
            GameFrameworkEntry.Shutdown();
            GameFrameXRuntimeHost.Reset();
            GameFrameworkLog.SetLogHelper(null);
            Utility.Text.SetTextHelper(null);
        }

        [TearDown]
        public void TearDown()
        {
            GameFrameworkEntry.Shutdown();
            GameFrameXRuntimeHost.Reset();
            GameFrameworkLog.SetLogHelper(null);
        }

        [Test]
        public void Headless_InitializesDefaultHelpers_AvailableImmediately()
        {
            GameFrameworkStartup.Headless();

            Assert.IsNotNull(GetStaticField(typeof(Utility.Text), "s_TextHelper"), "Text helper 应在 Headless 后立即可用");
            Assert.IsNotNull(GetStaticField(typeof(GameFrameworkLog), "_logHelper"), "Log helper 应在 Headless 后立即可用");
            Assert.IsNotNull(GetStaticField(typeof(GameVersion), "_versionHelper"), "Version helper 应在 Headless 后立即可用");
            Assert.AreEqual("v1", Utility.Text.Format("v{0}", 1));
        }

        [Test]
        public void Headless_RegistersManagerModules_FromAssemblyScan()
        {
            GameFrameworkStartup.Headless();

            var objectPoolManager = GameFrameworkEntry.GetModule<IObjectPoolManager>();
            Assert.IsNotNull(objectPoolManager, "Headless 应通过扫描器注册 ObjectPoolManager 模块映射");
            Assert.IsInstanceOf<ObjectPoolManager>(objectPoolManager);
            Assert.IsInstanceOf<ILowMemoryAwareModule>(objectPoolManager, "ObjectPoolManager 应实现低内存能力接口");
        }

        [Test]
        public void HostSimulation_HeadlessTickLowMemoryShutdown_IsReentrant()
        {
            // 第一段会话：Headless → 注册 → tick → 低内存 → Shutdown
            GameFrameworkStartup.Headless();
            GameFrameworkEntry.RegisterModule(typeof(IHeadlessProbe), typeof(HeadlessProbeModule));
            var probe = (HeadlessProbeModule)GameFrameworkEntry.GetModule(typeof(HeadlessProbeModule));
            Assert.IsNotNull(probe, "注册后的探针模块应可解析");

            GameFrameworkEntry.Update(0.1f, 0.1f);
            GameFrameworkEntry.Update(0.1f, 0.1f);
            GameFrameworkEntry.Update(0.1f, 0.1f);
            Assert.AreEqual(3, probe.Ticks, "模块应被宿主驱动 tick 三次");

            GameFrameworkEntry.NotifyLowMemory();
            Assert.IsTrue(probe.LowMemoryNotified, "NotifyLowMemory 应分发到低内存感知模块");

            GameFrameworkEntry.Shutdown();
            Assert.IsTrue(probe.ShutdownCalled, "Shutdown 应清理模块");
            Assert.IsNull(GetStaticField(typeof(GameFrameworkLog), "_logHelper"), "Shutdown 后 Log helper 应被清理");
            var moduleTypeMap = GetStaticField(typeof(GameFrameworkEntry), "s_ModuleTypeMap") as System.Collections.IDictionary;
            Assert.AreEqual(0, moduleTypeMap.Count, "Shutdown 应清空模块类型映射，避免跨会话旧实现泄漏");

            // 第二段会话：再次 Headless，断言可重入
            Assert.DoesNotThrow(() => GameFrameworkStartup.Headless(), "Shutdown 后再次 Headless 应可重入");
            var probeAgain = (HeadlessProbeModule)GameFrameworkEntry.GetModule(typeof(HeadlessProbeModule));
            Assert.IsNotNull(probeAgain);
            Assert.AreNotSame(probe, probeAgain, "重入后应是全新模块实例");
            Assert.AreEqual(0, probeAgain.Ticks);
        }

        [Test]
        public void Headless_WhileSessionRunning_ThrowsSingleInstanceGuard()
        {
            GameFrameworkStartup.Headless();
            GameFrameworkEntry.RegisterModule(typeof(IHeadlessProbe), typeof(HeadlessProbeModule));
            GameFrameworkEntry.GetModule(typeof(HeadlessProbeModule));

            Assert.Throws<GameFrameworkException>(() => GameFrameworkStartup.Headless(),
                "会话运行中重复 Headless 应被单实例守卫拒绝");
        }

        [Test]
        public void NotifyLowMemory_DispatchesOnlyToLowMemoryAwareModules()
        {
            GameFrameworkStartup.Headless();
            GameFrameworkEntry.RegisterModule(typeof(IHeadlessProbe), typeof(HeadlessProbeModule));
            GameFrameworkEntry.RegisterModule(typeof(IHeadlessPlainProbe), typeof(HeadlessPlainModule));
            var probe = (HeadlessProbeModule)GameFrameworkEntry.GetModule(typeof(HeadlessProbeModule));
            var plain = (HeadlessPlainModule)GameFrameworkEntry.GetModule(typeof(HeadlessPlainModule));

            GameFrameworkEntry.NotifyLowMemory();

            Assert.IsTrue(probe.LowMemoryNotified);
            Assert.IsFalse(plain.LowMemoryNotified, "未声明低内存能力的模块不应被分发");
        }

        [Test]
        public void Headless_LogRouting_HostBridgeHelperReceivesFrameworkLogs()
        {
            GameFrameworkStartup.Headless();
            Assert.IsNotNull(GetStaticField(typeof(GameFrameworkLog), "_logHelper"), "Headless 应已设置默认日志辅助器");
            var bridgeHelper = new HostLogBridgeHelper();
            GameFrameworkLog.SetLogHelper(bridgeHelper);

            GameFrameworkLog.Info("host bridge {0}", 42);

            Assert.AreEqual(1, bridgeHelper.Received.Count);
            StringAssert.Contains("host bridge 42", bridgeHelper.Received[0]);
        }

        [Test]
        public void UnityGameFrameworkDriver_DrivesEntryUpdateAndShutsDownOnDestroy()
        {
            GameFrameworkStartup.Headless();
            GameFrameworkEntry.RegisterModule(typeof(IHeadlessProbe), typeof(HeadlessProbeModule));
            var probe = (HeadlessProbeModule)GameFrameworkEntry.GetModule(typeof(HeadlessProbeModule));

            var driverGameObject = new GameObject("headless-driver-test");
            var driver = driverGameObject.AddComponent<UnityGameFrameworkDriver>();

            InvokePrivate(driver, "Awake");
            Assert.IsTrue(driver.enabled, "无 BaseComponent 时 Driver 应保持激活");
            InvokePrivate(driver, "Update");
            InvokePrivate(driver, "Update");
            Assert.AreEqual(2, probe.Ticks, "Driver.Update 应驱动 GameFrameworkEntry.Update");

            InvokePrivate(driver, "OnDestroy");
            Assert.IsTrue(probe.ShutdownCalled, "Driver.OnDestroy 应触发 GameFrameworkEntry.Shutdown");

            Object.DestroyImmediate(driverGameObject);
        }


        [Test]
        public void BaseComponent_WhenHeadlessActive_IsSuppressedAndNeverShutsDownHostSession()
        {
            GameFrameworkStartup.Headless();
            GameFrameworkEntry.RegisterModule(typeof(IHeadlessProbe), typeof(HeadlessProbeModule));
            var probe = (HeadlessProbeModule)GameFrameworkEntry.GetModule(typeof(HeadlessProbeModule));

            var baseComponentObject = new GameObject("Suppressed Base Component");
            var baseComponent = baseComponentObject.AddComponent<BaseComponent>();

            InvokePrivate(baseComponent, "Awake");
            Assert.IsFalse(baseComponent.enabled, "Headless 激活时场景残留 BaseComponent 应自我抑制");
            Assert.IsNull(GameEntry.GetComponent<BaseComponent>(), "被抑制的 BaseComponent 不得注册进 GameEntry");

            InvokePrivate(baseComponent, "OnDestroy");
            Assert.IsFalse(probe.ShutdownCalled, "被抑制的 BaseComponent 销毁时不得关停宿主 Headless 会话");

            Object.DestroyImmediate(baseComponentObject);
        }

        [Test]
        public void UnityGameFrameworkDriver_DeactivatesWhenBaseComponentDrivesComponentMode()
        {
            GameFrameworkEntry.RegisterModule(typeof(IHeadlessProbe), typeof(HeadlessProbeModule));
            var probe = (HeadlessProbeModule)GameFrameworkEntry.GetModule(typeof(HeadlessProbeModule));

            var baseComponentObject = new GameObject("Component Mode Entry");
            baseComponentObject.AddComponent<BaseComponent>();

            var driverGameObject = new GameObject("Misplaced Driver");
            var driver = driverGameObject.AddComponent<UnityGameFrameworkDriver>();

            InvokePrivate(driver, "Awake");
            Assert.IsFalse(driver.enabled, "组件模式（BaseComponent 已驱动）下误挂的 Driver 应自动禁用");

            InvokePrivate(driver, "OnDestroy");
            Assert.IsFalse(probe.ShutdownCalled, "退场 Driver 销毁时不得触发 GameFrameworkEntry.Shutdown");

            Object.DestroyImmediate(driverGameObject);
            Object.DestroyImmediate(baseComponentObject);
        }

        private static object GetStaticField(System.Type type, string fieldName)
        {
            var field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(field, "缺少静态字段 " + type.FullName + "." + fieldName);
            return field.GetValue(null);
        }

        private static void InvokePrivate(object target, string methodName)
        {
            var method = target.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method, "缺少私有方法 " + methodName);
            method.Invoke(target, null);
        }

        private interface IHeadlessProbe
        {
        }

        private interface IHeadlessPlainProbe
        {
        }

        private sealed class HeadlessProbeModule : IGameFrameworkModule, IHeadlessProbe, IUpdatableModule, IShutdownableModule, ILowMemoryAwareModule
        {
            public int Ticks;
            public bool LowMemoryNotified;
            public bool ShutdownCalled;

            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                Ticks++;
            }

            public void OnLowMemory()
            {
                LowMemoryNotified = true;
            }

            public void Shutdown()
            {
                ShutdownCalled = true;
            }
        }

        private sealed class HeadlessPlainModule : IGameFrameworkModule, IHeadlessPlainProbe
        {
            public bool LowMemoryNotified;

            public void OnLowMemory()
            {
                LowMemoryNotified = true;
            }
        }

        private sealed class HostLogBridgeHelper : GameFrameworkLog.ILogHelper
        {
            public readonly System.Collections.Generic.List<string> Received = new System.Collections.Generic.List<string>();

            public void Log(GameFrameworkLogLevel level, object message)
            {
                Received.Add(message == null ? string.Empty : message.ToString());
            }
        }
    }
}
