// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
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
    [DisallowMultipleComponent]
    [AddComponentMenu("GameFrameX/Base")]
    [UnityEngine.Scripting.Preserve]
    [DefaultExecutionOrder(-500)]
    public sealed class BaseComponent : GameFrameworkComponent
    {
        private const int DefaultDpi = 96; // default windows dpi

        private float m_GameSpeedBeforePause = 1f;

        // [SerializeField] private bool m_EditorResourceMode = true;

        [SerializeField] private string m_TextHelperTypeName = "UnityGameFramework.Runtime.DefaultTextHelper";

        [SerializeField] private string m_VersionHelperTypeName = "UnityGameFramework.Runtime.DefaultVersionHelper";

        [SerializeField] private string m_LogHelperTypeName = "UnityGameFramework.Runtime.DefaultLogHelper";

        [SerializeField] private string m_CompressionHelperTypeName = "UnityGameFramework.Runtime.DefaultCompressionHelper";

        [SerializeField] private string m_JsonHelperTypeName = "UnityGameFramework.Runtime.DefaultJsonHelper";

        [SerializeField] private int m_FrameRate = 30;

        [SerializeField] private float m_GameSpeed = 1f;

        [SerializeField] private bool m_RunInBackground = true;

        [SerializeField] private bool m_NeverSleep = true;

        /// <summary>
        /// 获取或设置是否使用编辑器资源模式（仅编辑器内有效）。
        /// </summary>
        // public bool EditorResourceMode
        // {
        //     get { return m_EditorResourceMode; }
        //     set { m_EditorResourceMode = value; }
        // }

        /*/// <summary>
        /// 获取或设置编辑器资源辅助器。
        /// </summary>
        public IResourceManager EditorResourceHelper { get; set; }*/

        /// <summary>
        /// 获取或设置游戏帧率。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int FrameRate
        {
            get { return m_FrameRate; }
            set { Application.targetFrameRate = m_FrameRate = value; }
        }

        /// <summary>
        /// 获取或设置游戏速度。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public float GameSpeed
        {
            get { return m_GameSpeed; }
            set { Time.timeScale = m_GameSpeed = value >= 0f ? value : 0f; }
        }

        /// <summary>
        /// 获取游戏是否暂停。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool IsGamePaused
        {
            get { return m_GameSpeed <= 0f; }
        }

        /// <summary>
        /// 获取是否正常游戏速度。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool IsNormalGameSpeed
        {
            get { return m_GameSpeed == 1f; }
        }

        /// <summary>
        /// 获取或设置是否允许后台运行。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool RunInBackground
        {
            get { return m_RunInBackground; }
            set { Application.runInBackground = m_RunInBackground = value; }
        }

        /// <summary>
        /// 获取或设置是否禁止休眠。
        /// </summary>
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
        protected override void Awake()
        {
            IsAutoRegister = false;
            base.Awake();

            DontDestroyOnLoad(this);
            InitTextHelper();
            InitVersionHelper();
            InitLogHelper();
            // Log.Info("Game Framework Version: {0}", GameFramework.Version.GameFrameworkVersion);
            Log.Info("Game Version: {0}, Unity Version: {1}", Version.GameVersion, Application.unityVersion);
#if UNITY_5_3_OR_NEWER || UNITY_5_3
            InitCompressionHelper();
            InitJsonHelper();

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
            GameFrameworkEntry.Shutdown();
        }

        /// <summary>
        /// 暂停游戏。
        /// </summary>
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

        private void InitTextHelper()
        {
            if (string.IsNullOrEmpty(m_TextHelperTypeName))
            {
                return;
            }

            Type textHelperType = Utility.Assembly.GetType(m_TextHelperTypeName);
            if (textHelperType == null)
            {
                Log.Error("Can not find text helper type '{0}'.", m_TextHelperTypeName);
                return;
            }

            Utility.Text.ITextHelper textHelper = (Utility.Text.ITextHelper)Activator.CreateInstance(textHelperType);
            if (textHelper == null)
            {
                Log.Error("Can not create text helper instance '{0}'.", m_TextHelperTypeName);
                return;
            }

            Utility.Text.SetTextHelper(textHelper);
        }

        private void InitVersionHelper()
        {
            if (string.IsNullOrEmpty(m_VersionHelperTypeName))
            {
                return;
            }

            Type versionHelperType = Utility.Assembly.GetType(m_VersionHelperTypeName);
            if (versionHelperType == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not find version helper type '{0}'.",
                                                                     m_VersionHelperTypeName));
            }

            Version.IVersionHelper versionHelper =
                (Version.IVersionHelper)Activator.CreateInstance(versionHelperType);
            if (versionHelper == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not create version helper instance '{0}'.",
                                                                     m_VersionHelperTypeName));
            }

            Version.SetVersionHelper(versionHelper);
        }

        private void InitLogHelper()
        {
            if (string.IsNullOrEmpty(m_LogHelperTypeName))
            {
                return;
            }

            Type logHelperType = Utility.Assembly.GetType(m_LogHelperTypeName);
            if (logHelperType == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not find log helper type '{0}'.",
                                                                     m_LogHelperTypeName));
            }

            GameFrameworkLog.ILogHelper logHelper =
                (GameFrameworkLog.ILogHelper)Activator.CreateInstance(logHelperType);
            if (logHelper == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not create log helper instance '{0}'.",
                                                                     m_LogHelperTypeName));
            }

            GameFrameworkLog.SetLogHelper(logHelper);
        }

        private void InitCompressionHelper()
        {
            if (string.IsNullOrEmpty(m_CompressionHelperTypeName))
            {
                return;
            }

            Type compressionHelperType = Utility.Assembly.GetType(m_CompressionHelperTypeName);
            if (compressionHelperType == null)
            {
                Log.Error("Can not find compression helper type '{0}'.", m_CompressionHelperTypeName);
                return;
            }

            Utility.Compression.ICompressionHelper compressionHelper =
                (Utility.Compression.ICompressionHelper)Activator.CreateInstance(compressionHelperType);
            if (compressionHelper == null)
            {
                Log.Error("Can not create compression helper instance '{0}'.", m_CompressionHelperTypeName);
                return;
            }

            Utility.Compression.SetCompressionHelper(compressionHelper);
        }

        private void InitJsonHelper()
        {
            if (string.IsNullOrEmpty(m_JsonHelperTypeName))
            {
                return;
            }

            Type jsonHelperType = Utility.Assembly.GetType(m_JsonHelperTypeName);
            if (jsonHelperType == null)
            {
                Log.Error("Can not find JSON helper type '{0}'.", m_JsonHelperTypeName);
                return;
            }

            Utility.Json.IJsonHelper jsonHelper = (Utility.Json.IJsonHelper)Activator.CreateInstance(jsonHelperType);
            if (jsonHelper == null)
            {
                Log.Error("Can not create JSON helper instance '{0}'.", m_JsonHelperTypeName);
                return;
            }

            Utility.Json.SetJsonHelper(jsonHelper);
        }

        private void OnLowMemory()
        {
            Log.Info("Low memory reported...");

            ObjectPoolComponent objectPoolComponent = GameEntry.GetComponent<ObjectPoolComponent>();
            if (objectPoolComponent != null)
            {
                objectPoolComponent.ReleaseAllUnused();
            }

            /*AssetComponent resourceComponent = GameEntry.GetComponent<AssetComponent>();
            if (resourceComponent != null)
            {
                // resourceComponent.ForceUnloadUnusedAssets(true);
            }*/
        }
    }
}