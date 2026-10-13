using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 自动运行时启动入口。
    /// </summary>
    /// <remarks>
    /// Entry point of the GameFrameX auto runtime.
    /// </remarks>
    [Preserve]
    public static class GameFrameXRuntimeBootstrap
    {
        /// <summary>
        /// 重置自动运行时静态状态，兼容关闭 Domain Reload 的 PlayMode。
        /// </summary>
        /// <remarks>
        /// Resets the static state of the auto runtime for PlayMode with Domain Reload disabled.
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reset()
        {
            GameFrameXRuntimeModeResolver.Reset();
            GameFrameXRuntimeHost.Reset();
            GameFrameXOptions.Reset();
        }

        /// <summary>
        /// 在场景加载前触发自动运行时类加载。
        /// </summary>
        /// <remarks>
        /// Triggers auto runtime class loading before scene load.
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
        }

        /// <summary>
        /// 在首个场景加载后启动 GameFrameX 自动运行时，保留旧场景节点的 Inspector 配置优先级。
        /// </summary>
        /// <remarks>
        /// Mutual exclusion across three modes: HeadlessMode (host contract first, no host created) &gt; ManualSceneMode (manual scene entry) &gt; AutoPackageMode (auto host).
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Start()
        {
            var modeResult = GameFrameXRuntimeModeResolver.Resolve();
            if (modeResult.Mode == GameFrameXRuntimeMode.HeadlessMode)
            {
                GameFrameXRuntimeHost.RecordHeadlessMode(modeResult);
                GameFrameworkEntry.InstantiateAllModules(); // Headless() 已实例化过则为幂等
                return;
            }

            if (modeResult.Mode == GameFrameXRuntimeMode.ManualSceneMode)
            {
                GameFrameXRuntimeHost.RecordManualSceneMode(modeResult);
                GameFrameworkEntry.InstantiateAllModules(); // Manual 模式下无组件承载的模块同样立即创建
                return;
            }

            GameFrameXRuntimeHost.EnsureStarted(modeResult);
            GameFrameworkEntry.InstantiateAllModules(); // 安装即启用：兜底全量创建（组件 Awake 已按需创建的为幂等跳过）
        }
    }
}
