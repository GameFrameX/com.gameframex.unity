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
        /// Define symbols for Google Play Games platform adaptation.
        /// </summary>
        public static readonly string[] EnableGooglePlayMiniGameScriptingDefineSymbol = new[] { "ENABLE_GOOGLEPLAY_MINI_GAME", "GOOGLEPLAYMINIGAME" };

        /// <summary>
        /// 开启 Google Play 游戏平台适配宏定义。
        /// Enables define symbols for Google Play Games platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Google Play Mini Game(开启[Google Play]适配)", false, 3000)]
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
        /// Disables define symbols for Google Play Games platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable Google Play Mini Game(关闭[Google Play]适配)", false, 3001)]
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
