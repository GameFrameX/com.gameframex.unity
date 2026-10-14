using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// Options 贡献者写入上下文：承载手动优先的跳过语义与跳过诊断。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 手动优先（C28）：游戏侧 <c>GameFrameXOptions.Configure</c> 是显式意图，必须永远胜出。贡献者应用晚于 BeforeSceneLoad
    /// 阶段开始，而该阶段内游戏初始化器与 Bootstrap 执行顺序不定，因此跳过判断按<b>类型粒度</b>进行：
    /// 已 Configure 的类型直接跳过并输出诊断——两种执行顺序下手动值均必胜（先执行 → 贡献者跳过；后执行 → 后写覆盖贡献者值）。
    /// 诊断走 <c>UnityEngine.Debug</c> + <c>[GameFrameX]</c> 前缀（早于 Log helper 接线，<see cref="GameFrameXOptions.Reset"/> 同款约定）。
    /// </para>
    /// <para>
    /// English: Write context handed to Options contributors. Implements manual-first semantics: Options types already configured
    /// explicitly by the game are skipped per type with a <c>[GameFrameX]</c> diagnostic, so manual values always win regardless of the
    /// execution order inside the BeforeSceneLoad phase. Diagnostics go through <c>UnityEngine.Debug</c> because contributor application
    /// may run before the Log helper is wired.
    /// </para>
    /// </remarks>
    [Preserve]
    public sealed class GameFrameXOptionsContributorContext
    {
        private readonly List<Type> m_SkippedTypes = new List<Type>();

        /// <summary>
        /// 获取本次写入中被手动优先语义跳过的 Options 类型（诊断用）。
        /// </summary>
        /// <remarks>
        /// Gets the Options types skipped by the manual-first semantics during this write (for diagnostics).
        /// </remarks>
        public IReadOnlyList<Type> SkippedTypes
        {
            get { return m_SkippedTypes; }
        }

        /// <summary>
        /// 写入一个模块 Options 默认值；该类型已被游戏侧手动 Configure 时跳过（手动优先）并输出诊断。
        /// </summary>
        /// <typeparam name="TOptions">Options 类型。 / The Options type.</typeparam>
        /// <param name="options">Options 实例。 / The Options instance.</param>
        /// <remarks>
        /// English: Writes a module Options default value; types already configured manually are skipped (manual-first) with a
        /// diagnostic, and a null instance is rejected loudly instead of throwing so a single bad contributor cannot break startup.
        /// </remarks>
        public void Configure<TOptions>(TOptions options) where TOptions : class
        {
            if (options == null)
            {
                // 响亮失败但不中断启动：单个贡献者写坏不应拖垮整条静默链路。
                UnityEngine.Debug.LogWarning("[GameFrameX] Options contributor 提供了 null 的 Options 实例，已跳过：" + typeof(TOptions).FullName);
                return;
            }

            var optionsType = typeof(TOptions);
            if (GameFrameXOptions.HasConfigured(optionsType))
            {
                UnityEngine.Debug.LogWarning("[GameFrameX] Options contributor 跳过 '" + optionsType.FullName + "'：该类型已被手动 Configure，手动值优先（贡献者值不生效）。");
                m_SkippedTypes.Add(optionsType);
                return;
            }

            GameFrameXOptions.Configure(options);
        }
    }
}
