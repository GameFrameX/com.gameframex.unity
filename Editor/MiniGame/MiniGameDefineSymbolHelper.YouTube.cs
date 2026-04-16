#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// YouTube 平台适配宏定义集合。
        /// Define symbols for YouTube platform adaptation.
        /// </summary>
        public static readonly string[] EnableYouTubeMiniGameScriptingDefineSymbol = new[] { "ENABLE_YOUTUBE_MINI_GAME", "YOUTUBEMINIGAME" };

        /// <summary>
        /// 开启 YouTube 平台适配宏定义。
        /// Enables define symbols for YouTube platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable YouTube Mini Game(开启[YouTube]适配)", false, 2800)]
#endif
        public static void EnableYouTubeMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableYouTubeMiniGameScriptingDefineSymbol);

            foreach (var define in EnableYouTubeMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"YouTube 平台宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭 YouTube 平台适配宏定义。
        /// Disables define symbols for YouTube platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable YouTube Mini Game(关闭[YouTube]适配)", false, 2801)]
#endif
        public static void DisableYouTubeMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableYouTubeMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"YouTube 平台宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
