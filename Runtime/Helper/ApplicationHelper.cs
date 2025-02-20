using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 应用帮助类
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class ApplicationHelper
    {
        /// <summary>
        /// 是否是编辑器
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static bool IsEditor
        {
            get
            {
#if UNITY_EDITOR
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 是否是安卓
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static bool IsAndroid
        {
            get
            {
#if UNITY_ANDROID
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 是否是WebGL平台
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGL
        {
            get
            {
#if UNITY_WEBGL
                return true;
#else
                return Application.platform == RuntimePlatform.WebGLPlayer;
#endif
            }
        }

        /// <summary>
        /// 是否是Windows平台
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWindows
        {
            get
            {
#if UNITY_STANDALONE_WIN
                return true;
#endif
                return Application.platform == RuntimePlatform.WindowsPlayer;
            }
        }

        /// <summary>
        /// 是否是Linux平台
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static bool IsLinux
        {
            get { return Application.platform == RuntimePlatform.LinuxPlayer; }
        }

        /// <summary>
        /// 是否是Mac平台
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static bool IsMacOsx
        {
            get
            {
#if UNITY_STANDALONE_OSX
                return true;
#endif
                return Application.platform == RuntimePlatform.OSXPlayer;
            }
        }

        /// <summary>
        /// 是否是iOS 移动平台
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static bool IsIOS
        {
            get
            {
#if UNITY_IOS
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            return;
#endif
            Application.Quit();
        }
#if UNITY_IOS
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void open_url(string url);
#endif
        /// <summary>
        /// 打开URL
        /// </summary>
        /// <param name="url">url地址</param>
        [UnityEngine.Scripting.Preserve]
        public static void OpenURL(string url)
        {
#if UNITY_EDITOR
            Application.OpenURL(url);
            return;
#endif
#if UNITY_IOS
            open_url(url);
#else
            Application.OpenURL(url);
#endif
        }
    }
}