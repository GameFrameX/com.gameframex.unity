using System;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// Options 贡献者宿主：扫描发现 <see cref="IGameFrameXOptionsContributor"/> 实现并在最早实例化点之前一次性应用。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 单入口幂等：<see cref="GameFrameXRuntimeBootstrap"/> 的 BeforeSceneLoad 调用 <see cref="EnsureApplied"/>，
    /// 早于场景组件的 Awake（发生在场景加载阶段）与自动 Host 的创建，因此必然早于任何模块实例化。
    /// 每个 domain 只应用一次，<c>SubsystemRegistration</c> 时随 Bootstrap 重置（兼容关闭 Domain Reload 的重复 Play）。
    /// </para>
    /// <para>
    /// 响亮失败：贡献者实例化失败（缺公共无参构造）或 <see cref="IGameFrameXOptionsContributor.ConfigureOptions"/> 抛异常时，
    /// 输出 <c>[GameFrameX]</c> 前缀 Warning 后跳过该贡献者（不中断启动、不静默吞掉）——诊断走 <c>UnityEngine.Debug</c>，
    /// 因为应用可能早于 Log helper 接线。
    /// </para>
    /// <para>
    /// English: Host that scans for <see cref="IGameFrameXOptionsContributor"/> implementations and applies them once, before the
    /// earliest module instantiation. The single idempotent entry point (Bootstrap BeforeSceneLoad) runs before scene component Awakes
    /// and before the auto host is created. Instantiation or ConfigureOptions failures are reported as
    /// <c>[GameFrameX]</c> warnings via <c>UnityEngine.Debug</c> and skipped — loud, but a single bad contributor cannot break startup.
    /// </para>
    /// </remarks>
    [Preserve]
    public static class GameFrameXRuntimeContributorHost
    {
        private static bool s_Applied;

        /// <summary>
        /// 确保贡献者已应用（每 domain 一次，幂等；双入口之一）。
        /// </summary>
        /// <remarks>
        /// Ensures contributors have been applied (once per domain, idempotent; one of the two entry points).
        /// </remarks>
        public static void EnsureApplied()
        {
            if (s_Applied)
            {
                return;
            }

            // 先置位再应用：防止贡献者内部间接触发 EnsureApplied 造成重入。
            s_Applied = true;
            Apply(GameFrameXRuntimeScanner.Scan());
        }

        /// <summary>
        /// 重置贡献者应用状态，兼容关闭 Domain Reload 的 PlayMode（由 Bootstrap 的 SubsystemRegistration 重置调用）。
        /// </summary>
        /// <remarks>
        /// Resets the applied flag for PlayMode with Domain Reload disabled; called by the Bootstrap SubsystemRegistration reset.
        /// </remarks>
        public static void Reset()
        {
            s_Applied = false;
        }

        /// <summary>
        /// 将扫描结果中的贡献者逐个实例化并应用（测试注入扫描结果的入口；不做幂等判断）。
        /// </summary>
        /// <param name="scanResult">运行时扫描结果。 / The runtime scan result.</param>
        /// <remarks>
        /// English: Instantiates and applies each contributor found in the scan result (seam for feeding a scan result in tests;
        /// no idempotency guard here — <see cref="EnsureApplied"/> owns the once-per-domain flag).
        /// </remarks>
        internal static void Apply(GameFrameXRuntimeScanResult scanResult)
        {
            if (scanResult == null)
            {
                return;
            }

            foreach (var contributorType in scanResult.OptionsContributorTypes)
            {
                object instance;
                try
                {
                    instance = Activator.CreateInstance(contributorType);
                }
                catch (Exception exception)
                {
                    UnityEngine.Debug.LogWarning("[GameFrameX] Options contributor '" + contributorType.FullName + "' 缺少公共无参构造或实例化失败，已跳过：" + exception.Message);
                    continue;
                }

                if (!(instance is IGameFrameXOptionsContributor contributor))
                {
                    continue;
                }

                var context = new GameFrameXOptionsContributorContext();
                try
                {
                    contributor.ConfigureOptions(context);
                }
                catch (Exception exception)
                {
                    UnityEngine.Debug.LogWarning("[GameFrameX] Options contributor '" + contributorType.FullName + "' 执行 ConfigureOptions 抛出异常，已跳过该贡献者：" + exception.Message);
                }
            }
        }
    }
}
