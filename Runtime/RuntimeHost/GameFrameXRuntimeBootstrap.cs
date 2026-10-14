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
            GameFrameXRuntimeContributorHost.Reset();
            GameFrameXOptions.Reset();
            // C29：AssemblyLoad 钩子先退订再重订——关闭 Domain Reload 的重复 Play 不会双订阅、不会产生重复描述符。
            // C29: the AssemblyLoad hook unsubscribes first, then re-subscribes — repeated Play with Domain Reload disabled never double-subscribes nor duplicates descriptors.
            // 不对称生命周期：GameFrameworkEntry.Shutdown 之后【不】退订。Shutdown 会清空模块实例与映射（C8），
            // Asymmetric lifecycle: the hook is deliberately NOT unsubscribed after GameFrameworkEntry.Shutdown. Shutdown clears module instances and type maps (C8),
            // 但新加载程序集仍应能注册描述符（服务 Restart 语义：重启会话内迟加载程序集照常可用）。
            // yet newly loaded assemblies must still be able to register descriptors (Restart semantics: late-loaded assemblies remain usable inside a restarted session).
            GameFrameXRuntimeAssemblyLoadHook.Unsubscribe();
            GameFrameXRuntimeAssemblyLoadHook.EnsureSubscribed();
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
            // 双入口之一（C28）：在场景组件 Awake 与 Auto Host 之前应用 Options 贡献者；与 Headless() 头部入口互为幂等兜底。
            GameFrameXRuntimeContributorHost.EnsureApplied();
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
