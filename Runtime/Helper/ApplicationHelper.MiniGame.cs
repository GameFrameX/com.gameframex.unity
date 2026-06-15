namespace GameFrameX.Runtime
{
    public static partial class ApplicationHelper
    {
        /// <summary>
        /// 获取当前是否在WebGL小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on a WebGL Mini Game platform.
        /// </remarks>
        /// <value>如果在WebGL小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on a WebGL Mini Game platform; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_WEBGL_MINI_GAME
                return true;
#else
                return false;
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
        /// 获取当前是否在WebGL支付宝小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Alipay Mini Game platform.
        /// </remarks>
        /// <value>如果在支付宝小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Alipay Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLAlipayMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_ALIPAY_MINI_GAME
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
        /// 获取当前是否在WebGL快手小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL KuaiShou Mini Game platform.
        /// </remarks>
        /// <value>如果在快手小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on KuaiShou Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLKuaiShouMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_KUAISHOU_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL百度小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Baidu Mini Game platform.
        /// </remarks>
        /// <value>如果在百度小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Baidu Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLBaiduMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_BAIDU_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL京东小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL JingDong Mini Game platform.
        /// </remarks>
        /// <value>如果在京东小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on JingDong Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLJingDongMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_JINGDONG_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL淘宝小程序平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Taobao Mini Program platform.
        /// </remarks>
        /// <value>如果在淘宝小程序平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Taobao Mini Program; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLTaobaoMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_TAOBAO_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL美团小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Meituan Mini Game platform.
        /// </remarks>
        /// <value>如果在美团小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Meituan Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLMeituanMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_MEITUAN_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL Bilibili小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Bilibili Mini Game platform.
        /// </remarks>
        /// <value>如果在Bilibili小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Bilibili Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLBilibiliMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_BILIBILI_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL Discord小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Discord Mini Game platform.
        /// </remarks>
        /// <value>如果在Discord小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Discord Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLDiscordMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_DISCORD_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL YouTube小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL YouTube Mini Game platform.
        /// </remarks>
        /// <value>如果在YouTube小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on YouTube Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLYouTubeMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_YOUTUBE_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL Facebook小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Facebook Mini Game platform.
        /// </remarks>
        /// <value>如果在Facebook小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Facebook Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLFacebookMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_FACEBOOK_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL Google Play小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Google Play Mini Game platform.
        /// </remarks>
        /// <value>如果在Google Play小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Google Play Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLGooglePlayMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_GOOGLEPLAY_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL TikTok小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL TikTok Mini Game platform.
        /// </remarks>
        /// <value>如果在TikTok小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on TikTok Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLTikTokMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_TIKTOK_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL CrazyGames小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL CrazyGames Mini Game platform.
        /// </remarks>
        /// <value>如果在CrazyGames小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on CrazyGames Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLCrazyGamesMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_CRAZYGAMES_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL Poki小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Poki Mini Game platform.
        /// </remarks>
        /// <value>如果在Poki小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Poki Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLPokiMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_POKI_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL华为小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Huawei Mini Game platform.
        /// </remarks>
        /// <value>如果在华为小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Huawei Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLHuaweiMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_HUAWEI_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL OPPO小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL OPPO Mini Game platform.
        /// </remarks>
        /// <value>如果在OPPO小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on OPPO Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLOPPOMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_OPPO_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL vivo小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL vivo Mini Game platform.
        /// </remarks>
        /// <value>如果在vivo小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on vivo Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLVivoMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_VIVO_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL小米小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL Xiaomi Mini Game platform.
        /// </remarks>
        /// <value>如果在小米小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on Xiaomi Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLXiaomiMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_XIAOMI_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 获取当前是否在WebGL TapTap小游戏平台运行。
        /// </summary>
        /// <remarks>
        /// Gets whether the application is running on the WebGL TapTap Mini Game platform.
        /// </remarks>
        /// <value>如果在TapTap小游戏平台则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if on TapTap Mini Game; otherwise <c>false</c></value>
        [UnityEngine.Scripting.Preserve]
        public static bool IsWebGLTapTapMiniGame
        {
            get
            {
#if UNITY_WEBGL && ENABLE_TAPTAP_MINI_GAME
                return true;
#else
                return false;
#endif
            }
        }

        private static string WebGLPlatformName
        {
            get
            {
#if ENABLE_WECHAT_MINI_GAME
                return "WeChatMiniGame";
#elif ENABLE_ALIPAY_MINI_GAME
                return "AlipayMiniGame";
#elif ENABLE_DOUYIN_MINI_GAME
                return "DouYinMiniGame";
#elif ENABLE_KUAISHOU_MINI_GAME
                return "KuaiShouMiniGame";
#elif ENABLE_BAIDU_MINI_GAME
                return "BaiduMiniGame";
#elif ENABLE_JINGDONG_MINI_GAME
                return "JingDongMiniGame";
#elif ENABLE_TAOBAO_MINI_GAME
                return "TaobaoMiniGame";
#elif ENABLE_MEITUAN_MINI_GAME
                return "MeituanMiniGame";
#elif ENABLE_BILIBILI_MINI_GAME
                return "BilibiliMiniGame";
#elif ENABLE_DISCORD_MINI_GAME
                return "DiscordMiniGame";
#elif ENABLE_YOUTUBE_MINI_GAME
                return "YouTubeMiniGame";
#elif ENABLE_FACEBOOK_MINI_GAME
                return "FacebookMiniGame";
#elif ENABLE_GOOGLEPLAY_MINI_GAME
                return "GooglePlayMiniGame";
#elif ENABLE_TIKTOK_MINI_GAME
                return "TikTokMiniGame";
#elif ENABLE_CRAZYGAMES_MINI_GAME
                return "CrazyGamesMiniGame";
#elif ENABLE_POKI_MINI_GAME
                return "PokiMiniGame";
#elif ENABLE_HUAWEI_MINI_GAME
                return "HuaweiMiniGame";
#elif ENABLE_OPPO_MINI_GAME
                return "OPPOMiniGame";
#elif ENABLE_VIVO_MINI_GAME
                return "VivoMiniGame";
#elif ENABLE_XIAOMI_MINI_GAME
                return "XiaomiMiniGame";
#elif ENABLE_TAPTAP_MINI_GAME
                return "TapTapMiniGame";
#else
                return "WebGL";
#endif
            }
        }
    }
}
