using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 迟加载程序集增量注册钩子（C29 起权威）：订阅 <see cref="AppDomain.AssemblyLoad"/>，
    /// 新程序集加载时增量注册其 Manager 描述符（Editor / Tests 程序集与全量扫描同门跳过）。
    /// </summary>
    /// <remarks>
    /// Late-loaded assembly incremental registration hook (authoritative since C29): subscribes to
    /// <see cref="AppDomain.AssemblyLoad"/> and incrementally registers manager descriptors from newly
    /// loaded assemblies (Editor / Tests assemblies are skipped by the same gate as the full scan).
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class GameFrameXRuntimeAssemblyLoadHook
    {
        private static readonly AssemblyLoadEventHandler Handler = OnAssemblyLoad;
        private static bool s_Subscribed;

        /// <summary>
        /// 当前是否已订阅 AssemblyLoad 事件。
        /// </summary>
        /// <remarks>
        /// Whether the AssemblyLoad event is currently subscribed.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static bool IsSubscribed
        {
            [UnityEngine.Scripting.Preserve]
            get { return s_Subscribed; }
        }

        /// <summary>
        /// 确保 AssemblyLoad 订阅已建立（幂等）：重复调用不产生双订阅。
        /// 由 Bootstrap 的 SubsystemRegistration 重置在退订后调用（兼容关闭 Domain Reload 的重复 Play）。
        /// </summary>
        /// <remarks>
        /// Ensures the AssemblyLoad subscription is established (idempotent): repeated calls never double-subscribe.
        /// Invoked by the Bootstrap SubsystemRegistration reset after unsubscribing (compatible with repeated Play with Domain Reload disabled).
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void EnsureSubscribed()
        {
            if (s_Subscribed)
            {
                return;
            }

            AppDomain.CurrentDomain.AssemblyLoad += Handler;
            s_Subscribed = true;
        }

        /// <summary>
        /// 退订 AssemblyLoad 事件（幂等）。测试与 SubsystemRegistration 重置路径使用。
        /// </summary>
        /// <remarks>
        /// Unsubscribes from the AssemblyLoad event (idempotent). Used by tests and the SubsystemRegistration reset path.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void Unsubscribe()
        {
            if (!s_Subscribed)
            {
                return;
            }

            AppDomain.CurrentDomain.AssemblyLoad -= Handler;
            s_Subscribed = false;
        }

        private static void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            GameFrameXRuntimeManagerResolver.RegisterAssembly(args.LoadedAssembly);
        }
    }
}
