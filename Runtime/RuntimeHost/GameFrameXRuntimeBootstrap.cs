using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 自动运行时启动入口（唯一启动路径）。
    /// </summary>
    /// <remarks>
    /// 安装即生效：本包被引用后，任意场景都无需任何改动即自动注入 GameFrameX 运行时。
    /// 没有互斥模式、没有开关、没有场景入口——启动即创建 <c>[GameFrameX]</c> 常驻 Host 并挂载全部组件。
    /// <para>
    /// English: The single startup entry point of the GameFrameX runtime. As soon as this package is referenced the
    /// runtime auto-injects into any scene with zero scene changes: no mutually exclusive modes, no switches, no scene
    /// entries — startup creates the persistent <c>[GameFrameX]</c> host and mounts every component.
    /// </para>
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
            GameFrameXRuntimeHost.Reset();
            GameFrameXRuntimeContributorHost.Reset();
            GameFrameXOptions.Reset();
            // AssemblyLoad 钩子先退订再重订——关闭 Domain Reload 的重复 Play 不会双订阅、不会产生重复描述符。
            // The AssemblyLoad hook unsubscribes first, then re-subscribes — repeated Play with Domain Reload disabled never double-subscribes nor duplicates descriptors.
            // 不对称生命周期：GameFrameworkEntry.Shutdown 之后【不】退订。Shutdown 会清空模块实例与映射，
            // Asymmetric lifecycle: the hook is deliberately NOT unsubscribed after GameFrameworkEntry.Shutdown. Shutdown clears module instances and type maps,
            // 但新加载程序集仍应能注册描述符（服务 Restart 语义：重启会话内迟加载程序集照常可用）。
            // yet newly loaded assemblies must still be able to register descriptors (Restart semantics: late-loaded assemblies remain usable inside a restarted session).
            GameFrameXRuntimeAssemblyLoadHook.Unsubscribe();
            GameFrameXRuntimeAssemblyLoadHook.EnsureSubscribed();
        }

        /// <summary>
        /// 在场景加载前宣告自动注入已接管，并应用 Options 贡献者。
        /// </summary>
        /// <remarks>
        /// 宣告必须在场景内组件的 <c>Awake</c> 之前完成：场景里手工摆放的 GFX 组件据此自我抑制并告警，
        /// 使框架对组件所有权保持唯一（详见 <see cref="GameFrameXRuntimeHost.ClaimAutoInjection"/>）。
        /// 同时应用 Options 贡献者——必须早于任何模块实例化（场景组件 Awake 发生在场景加载阶段，晚于本入口）。
        /// <para>
        /// English: Declares auto-injection ownership before any scene component Awakes, so GFX components placed by hand
        /// in a scene suppress themselves with a warning and the framework keeps sole ownership of components
        /// (see <see cref="GameFrameXRuntimeHost.ClaimAutoInjection"/>). Options contributors are applied here as well,
        /// because this must happen before any module is instantiated.
        /// </para>
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
            GameFrameXRuntimeHost.ClaimAutoInjection();
            GameFrameXRuntimeContributorHost.EnsureApplied();
        }

        /// <summary>
        /// 在首个场景加载后启动自动运行时：清除接管标记、创建 Host 并挂载全部组件。
        /// </summary>
        /// <remarks>
        /// 清标记必须发生在创建 Host 之前——Host 自己挂载的组件才能正常注册（场景内残留组件已在此前抑制完毕）。
        /// English: The claim is cleared before the host is created so the components the host mounts itself register normally
        /// (leftover scene components have already been suppressed by this point).
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Start()
        {
            GameFrameXRuntimeHost.EnsureStarted();
            GameFrameworkEntry.InstantiateAllModules(); // 安装即启用：兜底全量创建（组件 Awake 已按需创建的为幂等跳过）
        }
    }
}
