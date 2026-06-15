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
    public static partial class ApplicationHelper
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
        /// 获取当前是否在鸿蒙平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the HarmonyOS platform.
        /// </remarks>
        /// <value>如果在鸿蒙平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on HarmonyOS; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsHarmonyOS
        {
            get
            {
#if UNITY_HARMONYOS || UNITY_OPENHARMONY || HARMONYOS || OPENHARMONY
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
        /// <item><description>"HarmonyOS": 鸿蒙平台 / HarmonyOS platform</description></item>
        /// <item><description>"WebGL": WebGL平台（未启用小游戏平台宏时） / WebGL platform when no mini game platform define is enabled</description></item>
        /// <item><description>"WeChatMiniGame": 微信小游戏 / WeChat Mini Game</description></item>
        /// <item><description>"AlipayMiniGame": 支付宝小游戏 / Alipay Mini Game</description></item>
        /// <item><description>"DouYinMiniGame": 抖音小游戏 / DouYin Mini Game</description></item>
        /// <item><description>"KuaiShouMiniGame": 快手小游戏 / KuaiShou Mini Game</description></item>
        /// <item><description>"BaiduMiniGame": 百度小游戏 / Baidu Mini Game</description></item>
        /// <item><description>"JingDongMiniGame": 京东小游戏 / JingDong Mini Game</description></item>
        /// <item><description>"TaobaoMiniGame": 淘宝小程序 / Taobao Mini Program</description></item>
        /// <item><description>"MeituanMiniGame": 美团小游戏 / Meituan Mini Game</description></item>
        /// <item><description>"BilibiliMiniGame": Bilibili小游戏 / Bilibili Mini Game</description></item>
        /// <item><description>"DiscordMiniGame": Discord小游戏 / Discord Mini Game</description></item>
        /// <item><description>"YouTubeMiniGame": YouTube小游戏 / YouTube Mini Game</description></item>
        /// <item><description>"FacebookMiniGame": Facebook小游戏 / Facebook Mini Game</description></item>
        /// <item><description>"GooglePlayMiniGame": Google Play小游戏 / Google Play Mini Game</description></item>
        /// <item><description>"TikTokMiniGame": TikTok小游戏 / TikTok Mini Game</description></item>
        /// <item><description>"CrazyGamesMiniGame": CrazyGames小游戏 / CrazyGames Mini Game</description></item>
        /// <item><description>"PokiMiniGame": Poki小游戏 / Poki Mini Game</description></item>
        /// <item><description>"HuaweiMiniGame": 华为小游戏 / Huawei Mini Game</description></item>
        /// <item><description>"OPPOMiniGame": OPPO小游戏 / OPPO Mini Game</description></item>
        /// <item><description>"VivoMiniGame": vivo小游戏 / vivo Mini Game</description></item>
        /// <item><description>"XiaomiMiniGame": 小米小游戏 / Xiaomi Mini Game</description></item>
        /// <item><description>"TapTapMiniGame": TapTap小游戏 / TapTap Mini Game</description></item>
        /// <item><description>"Windows": Windows平台 / Windows platform</description></item>
        /// <item><description>空字符串: 其他未定义的平台 / Empty string: other undefined platforms</description></item>
        /// </list>
        /// 在WebGL构建中会优先根据已启用的小游戏平台宏返回具体平台名。
        /// In WebGL builds, the enabled mini game platform define takes precedence over the generic WebGL name.
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
#elif UNITY_HARMONYOS || UNITY_OPENHARMONY || HARMONYOS || OPENHARMONY
                return "HarmonyOS";
#elif UNITY_WEBGL
                return WebGLPlatformName;
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
