using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 应用帮助类。
    /// </summary>
    /// <remarks>
    /// Application helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class ApplicationHelper
    {
        /// <summary>
        /// 获取当前是否在Unity编辑器中运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running in the Unity editor.
        /// </remarks>
        /// <value>如果在编辑器中则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if in editor; otherwise <c>false</c></value>
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
        /// 获取当前是否在Android平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the Android platform.
        /// </remarks>
        /// <value>如果在Android平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Android; otherwise <c>false</c></value>
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
        /// 获取当前是否在WebGL平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL platform.
        /// </remarks>
        /// <value>如果在WebGL平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on WebGL; otherwise <c>false</c></value>
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
        /// 获取当前是否在WebGL微信小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL WeChat Mini Game platform.
        /// </remarks>
        /// <value>如果在微信小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on WeChat Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLWeChatMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_WECHAT_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL抖音小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Douyin Mini Game platform.
        /// </remarks>
        /// <value>如果在抖音小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Douyin Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLDouYinMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_DOUYIN_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在Windows平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the Windows platform.
        /// </remarks>
        /// <value>如果在Windows平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Windows; otherwise <c>false</c></value>
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
        /// 获取当前是否在Linux平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the Linux platform.
        /// </remarks>
        /// <value>如果在Linux平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Linux; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsLinux
        {
            get { return Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.LinuxEditor; }
        }

        /// <summary>
        /// 获取当前是否在macOS平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the macOS platform.
        /// </remarks>
        /// <value>如果在macOS平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on macOS; otherwise <c>false</c></value>
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
        /// 获取当前运行平台的名称。
        /// </summary>
        /// <remarks>
        /// Gets the name of the current running platform.
        /// </remarks>
        /// <value>
        /// 平台名称字符串 / Platform name string：
        /// <list type="bullet">
        /// <item><description>"Android": Android平台 / Android platform</description></item>
        /// <item><description>"MacOs": macOS平台 / macOS platform</description></item>
        /// <item><description>"iOS": iOS平台 / iOS platform</description></item>
        /// <item><description>"WebGL": WebGL平台 / WebGL platform</description></item>
        /// <item><description>"Windows": Windows平台 / Windows platform</description></item>
        /// <item><description>空字符串: 其他未定义的平台 / Empty string: other undefined platforms</description></item>
        /// </list>
        /// </value>
        [UnityEngine.Scripting.Preserve]
        public static string PlatformName
        {
            get
            {
#if UNITY_ANDROID
                return "Android";
#elif UNITY_STANDALONE_OSX
                return "MacOs";
#elif UNITY_IOS || UNITY_IPHONE
                return "iOS";
#elif UNITY_WEBGL
                return "WebGL";
#elif UNITY_STANDALONE_WIN
                return "Windows";
#else
                return string.Empty;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在iOS移动平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the iOS mobile platform.
        /// </remarks>
        /// <value>如果在iOS平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on iOS; otherwise <c>false</c></value>
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
        /// 退出应用程序。
        /// </summary>
        /// <remarks>
        /// Quits the application.
        /// </remarks>
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
        /// 打开指定的URL。
        /// </summary>
        /// <remarks>
        /// Opens the specified URL.
        /// </remarks>
        /// <param name="url">要打开的URL地址 / URL to open</param>
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

#if UNITY_IOS
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void open_setting();
#endif
        /// <summary>
        /// 打开系统设置界面。
        /// </summary>
        /// <remarks>
        /// Opens the system settings interface.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void OpenSetting()
        {
#if UNITY_EDITOR
            return;
#endif
#if UNITY_IOS
            open_setting();
#endif
        }


#if UNITY_IOS
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void open_request_tracking_authorization();
#endif
        /// <summary>
        /// 打开请求跟踪授权（iOS）。
        /// </summary>
        /// <remarks>
        /// Opens the request tracking authorization (iOS).
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void OpenRequestTrackingAuthorization()
        {
#if UNITY_EDITOR
            return;
#endif
#if UNITY_IOS
            open_request_tracking_authorization();
#endif
        }
    }
}