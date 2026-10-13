#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// Google Play 游戏平台适配宏定义集合。
        /// </summary>
        /// <remarks>
        /// Define symbols for Google Play Games platform adaptation.
        /// </remarks>
        public static readonly string[] EnableGooglePlayMiniGameScriptingDefineSymbol = new[] { "ENABLE_GOOGLEPLAY_MINI_GAME", "GOOGLEPLAYMINIGAME" };

        /// <summary>
        /// 开启 Google Play 游戏平台适配宏定义。
        /// </summary>
        /// <remarks>
        /// Enables define symbols for Google Play Games platform adaptation.
        /// </remarks>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/International Mini Games(海外小游戏)/Enable Google Play Mini Game(开启[Google Play]适配)", false, 3200)]
#endif
        public static void EnableGooglePlayMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableGooglePlayMiniGameScriptingDefineSymbol);

            foreach (var define in EnableGooglePlayMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"Google Play 游戏平台宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭 Google Play 游戏平台适配宏定义。
        /// </summary>
        /// <remarks>
        /// Disables define symbols for Google Play Games platform adaptation.
        /// </remarks>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/International Mini Games(海外小游戏)/Disable Google Play Mini Game(关闭[Google Play]适配)", false, 3201)]
#endif
        public static void DisableGooglePlayMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableGooglePlayMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"Google Play 游戏平台宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
