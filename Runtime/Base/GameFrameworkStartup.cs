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
//  Any legal disputes and liabilities arising from secondary development based on this project
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
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// GameFrameX 启动入口。
    /// </summary>
    /// <remarks>
    /// 提供与 Unity 场景生命周期解耦的 Headless 启动路径：零 MonoBehaviour、零场景污染，供宿主框架以三行契约接入。
    /// 三行契约（宿主侧全部代码）：
    /// <code>
    /// GameFrameworkStartup.Headless();                                  // 1. 启动：初始化默认 helpers 并注册模块（须在首个场景加载前调用）
    /// GameFrameworkEntry.Update(elapseSeconds, realElapseSeconds);      // 2. 驱动：宿主主循环转发（Unity 宿主可直接挂 UnityGameFrameworkDriver）
    /// GameFrameworkEntry.Shutdown();                                    // 3. 关闭：宿主退出时必调，否则静态模块泄漏
    /// </code>
    /// 单实例约束：每次运行只允许一个 GameFrameX 实例；会话运行中重复调用 <see cref="Headless"/> 会抛出异常。
    /// Headless 模式下禁止再挂任何 GameFrameX 组件（BaseComponent 等），场景中的 GFX 入口会被三模式互斥裁决拒绝并输出诊断。
    /// <para>
    /// English: Provides a Headless startup path decoupled from the Unity scene lifecycle (no MonoBehaviour, no scene pollution),
    /// letting a host framework integrate with a three-line contract: Headless() to start, GameFrameworkEntry.Update() to drive,
    /// and GameFrameworkEntry.Shutdown() to close. Only one GameFrameX instance is allowed per run; calling <see cref="Headless"/>
    /// during a running session throws, and no GameFrameX components may be mounted in Headless mode.
    /// </para>
    /// </remarks>
    [Preserve]
    public static class GameFrameworkStartup
    {
        private const string DefaultTextHelperTypeName = "GameFrameX.Runtime.DefaultTextHelper";
        private const string DefaultVersionHelperTypeName = "GameFrameX.Runtime.DefaultVersionHelper";
        private const string DefaultLogHelperTypeName = "GameFrameX.Runtime.DefaultLogHelper";
        private const string DefaultCompressionHelperTypeName = "GameFrameX.Runtime.DefaultCompressionHelper";
        private const string DefaultJsonHelperTypeName = "GameFrameX.Runtime.LitJsonHelper";

        /// <summary>
        /// 必须在首个场景加载前调用，确保三模式互斥裁决能在自动 Host 启动前看到 Headless 契约。
        /// </summary>
        /// <remarks>
        /// 单实例语义：上一段会话已有模块实例化且尚未 <see cref="GameFrameworkEntry.Shutdown"/> 时重复调用会抛出 <see cref="GameFrameworkException"/>；
        /// 空会话（已 Shutdown 或从未实例化模块）重复调用为幂等操作——helpers 按同配置重新初始化，模块映射重新扫描注册。
        /// 模块仅注册接口到实现的映射（Shutdown 时随会话清空），实例化仍按需发生在首次 <c>GetModule</c>。
        /// <para>
        /// English: Must be called before the first scene load so the three-mode mutual-exclusion verdict can observe the Headless
        /// contract before the automatic host starts. Calling it again while a previous session is still running (before
        /// <see cref="GameFrameworkEntry.Shutdown"/>) throws <see cref="GameFrameworkException"/>; calling it again on an empty session
        /// is idempotent. Since install-means-enable (C9), registered modules are instantiated eagerly.
        /// </para>
        /// </remarks>
        /// <exception cref="GameFrameworkException">上一段 Headless 会话仍在运行（尚未调用 <see cref="GameFrameworkEntry.Shutdown"/>）。 / The previous Headless session is still running (GameFrameworkEntry.Shutdown has not been called yet).</exception>
        [Preserve]
        public static void Headless()
        {
            if (GameFrameXRuntimeHost.IsHeadlessRequested && GameFrameworkEntry.HasInstantiatedModules)
            {
                throw new GameFrameworkException(
                    "GameFrameX headless startup is already running. Call GameFrameworkEntry.Shutdown() before starting a new headless session.");
            }

            GameFrameXRuntimeHost.MarkHeadlessRequested();

            // 双入口之一（C28）：宿主可能在 BeforeSceneLoad 阶段前直接调用 Headless()，此处兜底应用 Options 贡献者
            //（必须早于下方模块实例化；与 Bootstrap.BeforeSceneLoad 入口互为幂等）。
            GameFrameXRuntimeContributorHost.EnsureApplied();

            InitHelpers(
                DefaultTextHelperTypeName,
                DefaultVersionHelperTypeName,
                DefaultLogHelperTypeName,
                DefaultCompressionHelperTypeName,
                DefaultJsonHelperTypeName);

            var scanResult = GameFrameXRuntimeScanner.Scan();
            GameFrameXRuntimeManagerResolver.RegisterDescriptors(scanResult.ManagerDescriptors);
            foreach (var descriptor in scanResult.ManagerDescriptors)
            {
                GameFrameworkEntry.RegisterModule(descriptor.InterfaceType, descriptor.ImplementationType);
            }

            // 安装即启用（C9）：注册后立即全量实例化——Headless 语义从"仅注册映射"升级为"注册并全量实例化"。
            GameFrameworkEntry.InstantiateAllModules();
            GameFrameworkLog.Info("GameFrameX headless runtime started. Manager modules registered: {0}.", scanResult.ManagerDescriptors.Count);
        }

        /// <summary>
        /// 初始化框架 helpers（Text / Version / Log / Compression / Json），BaseComponent 序列化配置与 Headless 默认值共享同一路径。
        /// </summary>
        /// <remarks>
        /// Initializes the framework helpers (Text / Version / Log / Compression / Json); BaseComponent serialized configuration
        /// and the Headless defaults share this same path.
        /// </remarks>
        /// <param name="textHelperTypeName">文本辅助器类型名。 / The type name of the text helper.</param>
        /// <param name="versionHelperTypeName">版本辅助器类型名。 / The type name of the version helper.</param>
        /// <param name="logHelperTypeName">日志辅助器类型名。 / The type name of the log helper.</param>
        /// <param name="compressionHelperTypeName">压缩辅助器类型名。 / The type name of the compression helper.</param>
        /// <param name="jsonHelperTypeName">JSON 辅助器类型名。 / The type name of the JSON helper.</param>
        internal static void InitHelpers(string textHelperTypeName, string versionHelperTypeName, string logHelperTypeName, string compressionHelperTypeName, string jsonHelperTypeName)
        {
            InitTextHelper(textHelperTypeName);
            InitVersionHelper(versionHelperTypeName);
            InitLogHelper(logHelperTypeName);
            InitCompressionHelper(compressionHelperTypeName);
            InitJsonHelper(jsonHelperTypeName);
        }

        private static void InitTextHelper(string textHelperTypeName)
        {
            if (string.IsNullOrEmpty(textHelperTypeName))
            {
                return;
            }

            var textHelperType = Utility.Assembly.GetType(textHelperTypeName);
            if (textHelperType == null)
            {
                Log.Error("Can not find text helper type '{0}'.", textHelperTypeName);
                return;
            }

            var textHelper = (Utility.Text.ITextHelper)Activator.CreateInstance(textHelperType);
            if (textHelper == null)
            {
                Log.Error("Can not create text helper instance '{0}'.", textHelperTypeName);
                return;
            }

            Utility.Text.SetTextHelper(textHelper);
        }

        private static void InitVersionHelper(string versionHelperTypeName)
        {
            if (string.IsNullOrEmpty(versionHelperTypeName))
            {
                return;
            }

            var versionHelperType = Utility.Assembly.GetType(versionHelperTypeName);
            if (versionHelperType == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not find version helper type '{0}'.", versionHelperTypeName));
            }

            var versionHelper = (GameVersion.IVersionHelper)Activator.CreateInstance(versionHelperType);
            if (versionHelper == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not create version helper instance '{0}'.", versionHelperTypeName));
            }

            GameVersion.SetVersionHelper(versionHelper);
        }

        private static void InitLogHelper(string logHelperTypeName)
        {
            if (string.IsNullOrEmpty(logHelperTypeName))
            {
                return;
            }

            var logHelperType = Utility.Assembly.GetType(logHelperTypeName);
            if (logHelperType == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not find log helper type '{0}'.", logHelperTypeName));
            }

            var logHelper = (GameFrameworkLog.ILogHelper)Activator.CreateInstance(logHelperType);
            if (logHelper == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not create log helper instance '{0}'.", logHelperTypeName));
            }

            GameFrameworkLog.SetLogHelper(logHelper);
        }

        private static void InitCompressionHelper(string compressionHelperTypeName)
        {
            if (string.IsNullOrEmpty(compressionHelperTypeName))
            {
                return;
            }

            var compressionHelperType = Utility.Assembly.GetType(compressionHelperTypeName);
            if (compressionHelperType == null)
            {
                Log.Error("Can not find compression helper type '{0}'.", compressionHelperTypeName);
                return;
            }

            var compressionHelper = (Utility.Compression.ICompressionHelper)Activator.CreateInstance(compressionHelperType);
            if (compressionHelper == null)
            {
                Log.Error("Can not create compression helper instance '{0}'.", compressionHelperTypeName);
                return;
            }

            Utility.Compression.SetCompressionHelper(compressionHelper);
        }

        private static void InitJsonHelper(string jsonHelperTypeName)
        {
            if (string.IsNullOrEmpty(jsonHelperTypeName))
            {
                return;
            }

            var jsonHelperType = Utility.Assembly.GetType(jsonHelperTypeName);
            if (jsonHelperType == null)
            {
                Log.Error("Can not find JSON helper type '{0}'.", jsonHelperTypeName);
                return;
            }

            var jsonHelper = (Utility.Json.IJsonHelper)Activator.CreateInstance(jsonHelperType);
            if (jsonHelper == null)
            {
                Log.Error("Can not create JSON helper instance '{0}'.", jsonHelperTypeName);
                return;
            }

            Utility.Json.SetJsonHelper(jsonHelper);
        }
    }
}
