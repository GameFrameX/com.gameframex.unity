// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
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
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  CNB  仓库：https://cnb.cool/GameFrameX
//  CNB Repository:  https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;

using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 基础组件。
    /// </summary>
    /// <remarks>
    /// Base component that provides core game framework functionality including frame rate control,
    /// game speed management, background running settings, and helper initializations.
    /// </remarks>
    [DisallowMultipleComponent]
    [AddComponentMenu("GameFrameX/Base")]
    [UnityEngine.Scripting.Preserve]
    [GameFrameXAutoComponent(-10000)]
    [DefaultExecutionOrder(-500)]
    public sealed class BaseComponent : GameFrameworkComponent
    {
        private const int DefaultDpi = 96; // default windows dpi

        private float m_GameSpeedBeforePause = 1f;

        // [SerializeField] private bool m_EditorResourceMode = true;

        [SerializeField] private string m_TextHelperTypeName = "GameFrameX.Runtime.DefaultTextHelper";

        [SerializeField] private string m_VersionHelperTypeName = "GameFrameX.Runtime.DefaultVersionHelper";

        [SerializeField] private string m_LogHelperTypeName = "GameFrameX.Runtime.DefaultLogHelper";

        [SerializeField] private string m_CompressionHelperTypeName = "GameFrameX.Runtime.DefaultCompressionHelper";

        [SerializeField] private string m_JsonHelperTypeName = "GameFrameX.Runtime.LitJsonHelper";

        [SerializeField] private int m_FrameRate = 30;

        [SerializeField] private float m_GameSpeed = 1f;

        [SerializeField] private bool m_RunInBackground = true;

        [SerializeField] private bool m_NeverSleep = true;

        /// <summary>
        /// 获取或设置游戏帧率。
        /// </summary>
        /// <remarks>
        /// Gets or sets the game frame rate.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int FrameRate
        {
            get { return m_FrameRate; }
            set { Application.targetFrameRate = m_FrameRate = value; }
        }

        /// <summary>
        /// 获取或设置游戏速度。
        /// </summary>
        /// <remarks>
        /// Gets or sets the game speed.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public float GameSpeed
        {
            get { return m_GameSpeed; }
            set { Time.timeScale = m_GameSpeed = value >= 0f ? value : 0f; }
        }

        /// <summary>
        /// 获取游戏是否暂停。
        /// </summary>
        /// <remarks>
        /// Gets whether the game is paused.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsGamePaused
        {
            get { return m_GameSpeed <= 0f; }
        }

        /// <summary>
        /// 获取是否正常游戏速度。
        /// </summary>
        /// <remarks>
        /// Gets whether the game is running at normal speed.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsNormalGameSpeed
        {
            get { return m_GameSpeed == 1f; }
        }

        /// <summary>
        /// 获取或设置是否允许后台运行。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether the game can run in background.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool RunInBackground
        {
            get { return m_RunInBackground; }
            set { Application.runInBackground = m_RunInBackground = value; }
        }

        /// <summary>
        /// 获取或设置是否禁止休眠。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether sleep is disabled.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool NeverSleep
        {
            get { return m_NeverSleep; }
            set
            {
                m_NeverSleep = value;
                Screen.sleepTimeout = value ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
            }
        }

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        /// <remarks>
        /// Initializes the game framework component.
        /// </remarks>
        protected override void Awake()
        {
            IsAutoRegister = false;
            base.Awake();

            // 残留抑制已在 base.Awake 中生效（自动注入接管时本组件退场）：抑制后不得再初始化 helpers、订阅事件或驱动框架。
            // Suppression already happened in base.Awake: a suppressed component must not initialize helpers, subscribe, or drive the framework.
            if (m_SuppressedByAutoHost)
            {
                return;
            }

            DontDestroyOnLoad(gameObject);

            // helpers 初始化是框架唯一的 helper 接线路径，顺序保持 Text → Version → Log → Compression → Json。
            InitHelpers(
                m_TextHelperTypeName,
                m_VersionHelperTypeName,
                m_LogHelperTypeName,
                m_CompressionHelperTypeName,
                m_JsonHelperTypeName);
            Log.Info("Game Version: {0}, Unity Version: {1}", GameVersion.AppVersion, Application.unityVersion);
#if UNITY_5_3_OR_NEWER || UNITY_5_3
            Utility.Converter.ScreenDpi = Screen.dpi;
            if (Utility.Converter.ScreenDpi <= 0)
            {
                Utility.Converter.ScreenDpi = DefaultDpi;
            }

            // m_EditorResourceMode &= Application.isEditor;
            // if (m_EditorResourceMode)
            // {
            //     Log.Info(
            //         "During this run, Game Framework will use editor resource files, which you should validate first.");
            // }

            Application.targetFrameRate = m_FrameRate;
            Time.timeScale = m_GameSpeed;
            Application.runInBackground = m_RunInBackground;
            Screen.sleepTimeout = m_NeverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
#else
            Log.Error("Game Framework only applies with Unity 5.3 and above, but current Unity version is {0}.", Application.unityVersion);
            GameEntry.Shutdown(ShutdownType.Quit);
#endif
#if UNITY_5_6_OR_NEWER
            Application.lowMemory += OnLowMemory;
#endif
        }

        private void Start()
        {
        }

        private void Update()
        {
            GameFrameworkEntry.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void OnApplicationQuit()
        {
#if UNITY_5_6_OR_NEWER
            Application.lowMemory -= OnLowMemory;
#endif
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            if (m_SuppressedByAutoHost)
            {
                return;
            }

            GameFrameworkEntry.Shutdown();
        }

        /// <summary>
        /// 暂停游戏。
        /// </summary>
        /// <remarks>
        /// Pauses the game.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public void PauseGame()
        {
            if (IsGamePaused)
            {
                return;
            }

            m_GameSpeedBeforePause = GameSpeed;
            GameSpeed = 0f;
        }

        /// <summary>
        /// 恢复游戏。
        /// </summary>
        /// <remarks>
        /// Resumes the game.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public void ResumeGame()
        {
            if (!IsGamePaused)
            {
                return;
            }

            GameSpeed = m_GameSpeedBeforePause;
        }

        /// <summary>
        /// 重置为正常游戏速度。
        /// </summary>
        /// <remarks>
        /// Resets to normal game speed.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public void ResetNormalGameSpeed()
        {
            if (IsNormalGameSpeed)
            {
                return;
            }

            GameSpeed = 1f;
        }

        internal void Shutdown()
        {
            Destroy(gameObject);
        }

        private void OnLowMemory()
        {
            Log.Info("Low memory reported...");

            // 模块清理（如对象池释放）由 ILowMemoryAwareModule 分发。
            GameFrameworkEntry.NotifyLowMemory();
        }

        /// <summary>
        /// 初始化框架 helpers（Text / Version / Log / Compression / Json），框架唯一的 helper 接线路径。
        /// </summary>
        /// <remarks>
        /// 顺序保持 Text → Version → Log → Compression → Json。
        /// English: Initializes the framework helpers, the single helper-wiring path of the framework, in the order
        /// Text → Version → Log → Compression → Json.
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
