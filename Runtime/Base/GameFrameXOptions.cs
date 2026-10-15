// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents and other related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and other international regulations.
//
//  使用本项目须严格遵守相应法律法规与开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
//
//  本项目采用 Apache License 2.0 许可证分发，
//  This project is licensed under the Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
//
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes or liabilities arising from secondary development based on this project
//  本组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
//
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  CNB 仓库：https://cnb.cool/GameFrameX
//  CNB Repository: https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using System.Collections.Generic;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 模块 Options 启动配置注册表（单一事实源的默认值入口）。
    /// </summary>
    /// <remarks>
    /// 配置终态（C9）：模块状态是唯一事实源——本注册表只承载各模块 Options 的<b>默认值</b>，
    /// 在模块首次实例化时一次性消费；运行期修改参数一律走模块公开 API。
    ///
    /// 时机（唯一入口）：游戏在 <c>[RuntimeInitializeOnLoadMethod(BeforeSceneLoad)]</c> 中 Configure——
    /// 必须早于场景组件 Awake 与自动 Host 的 AfterSceneLoad（该阶段跨程序集顺序不定，不可挂载注入）。
    ///
    /// 响亮失败：某模块 Options 已被消费（即该模块已实例化）后再次 Configure 会抛出 <see cref="GameFrameworkException"/>——
    /// 启动期全量实例化完成后，任何 Configure 都必然违例，配置时机错误不会静默。
    /// 未消费诊断：<see cref="Reset"/> / <see cref="GameFrameworkEntry.Shutdown"/> 时对"已注册但从未被任何模块消费"的
    /// Options 输出 Warning（成因仅剩：包未安装 / 组件被结构性禁用）。
    ///
    /// 分档约定：Options 字段默认为实时型（运行期可经模块 API 修改）；启动期型字段（如代理数量、Helper 类型名）
    /// 用 <see cref="GameFrameXStartupOnlyAttribute"/> 标注，模块侧对运行期修改 Warning 并拒绝。
    ///
    /// 示例：
    /// <code>
    /// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    /// static void ConfigureFramework()
    /// {
    ///     GameFrameXOptions.Configure(new DownloadOptions { Timeout = 60f }); // 只写差异，其余用包默认值
    /// }
    /// </code>
    /// <para>
    /// English: Registry of module Options startup configuration and the single source of truth for default values.
    /// It only carries each module's default Options, consumed once at first instantiation; runtime changes go through module APIs.
    /// Configure must run before scene load; late Configure throws <see cref="GameFrameworkException"/>.
    /// </para>
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class GameFrameXOptions
    {
        private sealed class OptionsEntry
        {
            public object Value;
            public bool Demanded;
        }

        private static readonly Dictionary<Type, OptionsEntry> s_Entries = new Dictionary<Type, OptionsEntry>(8);

        /// <summary>
        /// 注册某模块的 Options 默认值；未被消费前允许覆盖（后写胜出）。
        /// </summary>
        /// <remarks>
        /// Registers the default Options of a module; overwriting is allowed before consumption (last write wins).
        /// </remarks>
        /// <typeparam name="TOptions">Options 类型。 / The Options type.</typeparam>
        /// <param name="options">Options 实例。 / The Options instance.</param>
        /// <exception cref="GameFrameworkException">options 为 null，或该 Options 已被模块消费（时机违例，见类备注）。 / options is null, or the Options has already been consumed by its module (timing violation, see class remarks).</exception>
        [UnityEngine.Scripting.Preserve]
        public static void Configure<TOptions>(TOptions options) where TOptions : class
        {
            if (options == null)
            {
                throw new GameFrameworkException("GameFrameX options instance is invalid (null).");
            }

            var key = typeof(TOptions);
            if (s_Entries.TryGetValue(key, out var entry))
            {
                if (entry.Demanded)
                {
                    throw new GameFrameworkException(Utility.Text.Format(
                        "Options '{0}' has already been consumed by its module (module instantiated). Call Configure before module creation — before scene load (BeforeSceneLoad).",
                        key.FullName));
                }

                entry.Value = options;
                return;
            }

            s_Entries[key] = new OptionsEntry { Value = options };
        }

        /// <summary>
        /// 模块在首次实例化时读取自己的 Options；无论命中与否都会标记"已被该模块消费"（此后 Configure 即时机违例）。
        /// </summary>
        /// <typeparam name="TOptions">Options 类型。 / The Options type.</typeparam>
        /// <param name="options">读到的 Options 实例；未注册时为 null。 / The Options instance read; null when not registered.</param>
        /// <returns>是否成功读到 Options 实例。 / Whether an Options instance was successfully read.</returns>
        /// <remarks>
        /// Called by a module on first instantiation to read its own Options; hit or miss, the entry is marked as consumed
        /// (any later Configure is a timing violation).
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static bool TryGet<TOptions>(out TOptions options) where TOptions : class
        {
            var key = typeof(TOptions);
            if (s_Entries.TryGetValue(key, out var entry))
            {
                entry.Demanded = true;
                if (entry.Value != null)
                {
                    options = (TOptions)entry.Value;
                    return true;
                }

                // 此前 TryGet 未命中留下的"已消费、无值"条目：返回未注册，但仍保持已消费标记。
                options = null;
                return false;
            }

            // 未注册也标记已消费：模块已经跑过初始化查找，事后 Configure 永远不会生效，必须响亮拒绝。
            s_Entries[key] = new OptionsEntry { Value = null, Demanded = true };
            options = null;
            return false;
        }

        /// <summary>
        /// 判断某 Options 类型是否已被手动 Configure（供贡献者上下文执行手动优先的类型粒度跳过）。
        /// </summary>
        /// <param name="optionsType">Options 类型。 / The Options type.</param>
        /// <returns>已 Configure 返回 true。 / true when already configured.</returns>
        /// <remarks>
        /// English: Indicates whether an Options type has been configured manually, used by the contributor context to
        /// implement the per-type manual-first skip. “已消费但无值” 的条目视为未手动配置（无手动值需要保护）。
        /// </remarks>
        internal static bool HasConfigured(Type optionsType)
        {
            return s_Entries.TryGetValue(optionsType, out var entry) && entry.Value != null;
        }

        /// <summary>
        /// 清空全部 Options 状态；对"已注册但从未被任何模块消费"的条目输出 Warning 诊断。
        /// </summary>
        /// <remarks>由 <see cref="GameFrameworkEntry.Shutdown"/> 与 Bootstrap 的 SubsystemRegistration 重置调用；兼容关闭 Domain Reload 的重复 Play。 English: Called by <see cref="GameFrameworkEntry.Shutdown"/> and the Bootstrap SubsystemRegistration reset; compatible with repeated Play when Domain Reload is disabled.</remarks>
        [UnityEngine.Scripting.Preserve]
        public static void Reset()
        {
            foreach (var pair in s_Entries)
            {
                if (pair.Value.Value != null && !pair.Value.Demanded)
                {
                    // 诊断必须绕开框架日志门面：Options 重置可能发生在日志 helper 接线之前（如 EditMode / 早期启动）。
                    UnityEngine.Debug.LogWarning("[GameFrameX] Options '" + pair.Key.FullName + "' 已配置但从未被消费：对应模块从未创建（包未安装或组件被结构性禁用）。");
                }
            }

            s_Entries.Clear();
        }
    }
}
