using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace GameFrameX.Tests
{
    /// <summary>
    /// 三运行模式互斥裁决测试：HeadlessMode 与 ManualSceneMode / AutoPackageMode 互斥，
    /// Headless 宿主契约激活时拒绝任何自动 Host 创建与混合接管。
    /// </summary>
    [TestFixture]
    public sealed class GameFrameXRuntimeModeHeadlessTests
    {
        private GameObject m_SceneBaseComponentObject;

        [SetUp]
        public void SetUp()
        {
            GameFrameworkEntry.Shutdown();
            GameFrameXRuntimeHost.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            if (m_SceneBaseComponentObject != null)
            {
                Object.DestroyImmediate(m_SceneBaseComponentObject);
                m_SceneBaseComponentObject = null;
            }

            GameFrameworkEntry.Shutdown();
            GameFrameXRuntimeHost.Reset();
        }

        [Test]
        public void Resolve_HeadlessRequestedWithoutSceneEntry_ReturnsHeadlessMode()
        {
            GameFrameworkStartup.Headless();

            var modeResult = GameFrameXRuntimeModeResolver.Resolve();

            Assert.AreEqual(GameFrameXRuntimeMode.HeadlessMode, modeResult.Mode);
            StringAssert.Contains("Headless", modeResult.Reason, "裁决原因应明确指向 Headless 宿主契约");
        }

        [Test]
        public void Resolve_HeadlessRequestedWithSceneBaseComponent_RejectsManualSceneMixing()
        {
            GameFrameworkStartup.Headless();
            m_SceneBaseComponentObject = new GameObject("Manual Scene Entry");
            m_SceneBaseComponentObject.AddComponent<BaseComponent>();

            var modeResult = GameFrameXRuntimeModeResolver.Resolve();

            Assert.AreEqual(GameFrameXRuntimeMode.HeadlessMode, modeResult.Mode,
                "Headless 激活时场景 BaseComponent 不得把裁决拉回 ManualSceneMode");
            StringAssert.Contains("rejected", modeResult.Reason, "混合接管必须被拒绝且诊断明确");
            Assert.IsFalse(string.IsNullOrEmpty(modeResult.ManualEntryPath), "诊断应包含被拒绝的手动入口路径");
        }

        [Test]
        public void EnsureStarted_HeadlessModeResult_DoesNotCreateRuntimeHost()
        {
            GameFrameXRuntimeHost.EnsureStarted(new GameFrameXRuntimeModeResult(
                GameFrameXRuntimeMode.HeadlessMode,
                "Headless startup was requested by the host.",
                string.Empty));

            Assert.IsNull(GameObject.Find("[GameFrameX]"), "Headless 模式下不得创建 [GameFrameX] 自动 Host");
            Assert.IsNotNull(GameFrameXRuntimeHost.LastModeResult);
            Assert.AreEqual(GameFrameXRuntimeMode.HeadlessMode, GameFrameXRuntimeHost.LastModeResult.Mode);
        }

        [Test]
        public void Resolve_AfterHostReset_FallsBackToAutoPackageMode()
        {
            GameFrameworkStartup.Headless();
            Assert.AreEqual(GameFrameXRuntimeMode.HeadlessMode, GameFrameXRuntimeModeResolver.Resolve().Mode);

            GameFrameXRuntimeHost.Reset();

            Assert.AreEqual(GameFrameXRuntimeMode.AutoPackageMode, GameFrameXRuntimeModeResolver.Resolve().Mode,
                "Host.Reset（SubsystemRegistration 重放）后应重新裁决，不得残留 Headless 状态");
        }
    }
}
