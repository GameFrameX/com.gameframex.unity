#if UNITY_WEBGL
using UnityEditor;
#endif

namespace GameFrameX.Editor
{
    /// <summary>
    /// 小游戏宏定义帮助类。
    /// Mini game scripting define symbol helper.
    /// </summary>
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// WebGL 小游戏统一宏定义。
        /// Unified scripting define symbol for WebGL mini games.
        /// </summary>
        private const string EnableWebGLMiniGameScriptingDefineSymbol = "ENABLE_WEBGL_MINI_GAME";

        /// <summary>
        /// 获取所有小游戏平台宏定义集合。
        /// Gets all mini game platform define symbol arrays.
        /// </summary>
        /// <returns>小游戏平台宏定义数组集合 / Collection of mini game define symbol arrays.</returns>
        private static string[][] GetAllMiniGameScriptingDefineSymbols()
        {
            return new[]
            {
                EnableWeChatMiniGameScriptingDefineSymbol,
                EnableDouYinMiniGameScriptingDefineSymbol,
                EnableKuaiShouMiniGameScriptingDefineSymbol,
                EnableBaiduMiniGameScriptingDefineSymbol,
                EnableAlipayMiniGameScriptingDefineSymbol,
                EnableTapTapMiniGameScriptingDefineSymbol,
                EnableMeituanMiniGameScriptingDefineSymbol,
                EnableBilibiliMiniGameScriptingDefineSymbol,
                EnableJingDongMiniGameScriptingDefineSymbol,
                EnableTaobaoMiniGameScriptingDefineSymbol,
                EnableDiscordMiniGameScriptingDefineSymbol,
                EnableYouTubeMiniGameScriptingDefineSymbol,
                EnableFacebookMiniGameScriptingDefineSymbol,
                EnableGooglePlayMiniGameScriptingDefineSymbol,
                EnableVivoMiniGameScriptingDefineSymbol,
                EnableOPPOMiniGameScriptingDefineSymbol,
                EnableXiaomiMiniGameScriptingDefineSymbol,
                EnableHuaweiMiniGameScriptingDefineSymbol,
                EnableTikTokMiniGameScriptingDefineSymbol,
                EnableCrazyGamesMiniGameScriptingDefineSymbol,
                EnablePokiMiniGameScriptingDefineSymbol,
            };
        }

        /// <summary>
        /// 关闭除当前平台以外的其它小游戏平台宏定义。
        /// Disables define symbols of other mini game platforms except the current one.
        /// </summary>
        /// <param name="currentMiniGameScriptingDefineSymbols">当前平台宏定义集合 / Define symbols of the current platform.</param>
        private static void DisableOtherMiniGameScriptingDefineSymbols(string[] currentMiniGameScriptingDefineSymbols)
        {
#if UNITY_WEBGL
            var closedCount = 0;
            foreach (var defineSymbols in GetAllMiniGameScriptingDefineSymbols())
            {
                if (defineSymbols == null)
                {
                    continue;
                }

                if (object.ReferenceEquals(defineSymbols, currentMiniGameScriptingDefineSymbols))
                {
                    continue;
                }

                foreach (var define in defineSymbols)
                {
                    if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                    {
                        continue;
                    }

                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                    closedCount++;
                    UnityEngine.Debug.Log($"小游戏宏定义 [{define}] 已经关闭");
                }
            }

            UnityEngine.Debug.Log($"小游戏宏定义互斥清理完成，共关闭 {closedCount} 个宏定义");
#endif
        }

        /// <summary>
        /// 打开 WebGL 小游戏统一宏定义。
        /// Enables the unified WebGL mini game define symbol.
        /// </summary>
        private static void EnableUnifiedMiniGameScriptingDefineSymbol()
        {
#if UNITY_WEBGL
            if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, EnableWebGLMiniGameScriptingDefineSymbol))
            {
                return;
            }

            ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableWebGLMiniGameScriptingDefineSymbol);
            UnityEngine.Debug.Log($"小游戏统一宏定义 [{EnableWebGLMiniGameScriptingDefineSymbol}] 已经打开");
#endif
        }

        /// <summary>
        /// 根据当前各平台宏定义状态刷新 WebGL 小游戏统一宏定义。
        /// Refreshes the unified WebGL mini game define symbol according to platform define states.
        /// </summary>
        private static void RefreshUnifiedMiniGameScriptingDefineSymbol()
        {
#if UNITY_WEBGL
            var hasAnyMiniGameDefine = false;
            foreach (var defineSymbols in GetAllMiniGameScriptingDefineSymbols())
            {
                if (defineSymbols == null)
                {
                    continue;
                }

                foreach (var define in defineSymbols)
                {
                    if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                    {
                        continue;
                    }

                    hasAnyMiniGameDefine = true;
                    break;
                }

                if (hasAnyMiniGameDefine)
                {
                    break;
                }
            }

            var hasUnifiedMiniGameDefine = ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, EnableWebGLMiniGameScriptingDefineSymbol);
            if (hasAnyMiniGameDefine)
            {
                if (!hasUnifiedMiniGameDefine)
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableWebGLMiniGameScriptingDefineSymbol);
                    UnityEngine.Debug.Log($"小游戏统一宏定义 [{EnableWebGLMiniGameScriptingDefineSymbol}] 已经打开");
                }

                return;
            }

            if (hasUnifiedMiniGameDefine)
            {
                ScriptingDefineSymbols.RemoveScriptingDefineSymbol(EnableWebGLMiniGameScriptingDefineSymbol);
                UnityEngine.Debug.Log($"小游戏统一宏定义 [{EnableWebGLMiniGameScriptingDefineSymbol}] 已经关闭");
            }
#endif
        }
    }
}
