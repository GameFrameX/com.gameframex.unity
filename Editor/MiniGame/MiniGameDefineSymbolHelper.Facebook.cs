#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// Facebook 平台适配宏定义集合。
        /// Define symbols for Facebook platform adaptation.
        /// </summary>
        public static readonly string[] EnableFacebookMiniGameScriptingDefineSymbol = new[] { "ENABLE_FACEBOOK_MINI_GAME", "FACEBOOKMINIGAME" };

        /// <summary>
        /// 开启 Facebook 平台适配宏定义。
        /// Enables define symbols for Facebook platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Facebook Mini Game(开启[Facebook]适配)", false, 2900)]
#endif
        public static void EnableFacebookMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableFacebookMiniGameScriptingDefineSymbol);

            foreach (var define in EnableFacebookMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"Facebook 平台宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭 Facebook 平台适配宏定义。
        /// Disables define symbols for Facebook platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable Facebook Mini Game(关闭[Facebook]适配)", false, 2901)]
#endif
        public static void DisableFacebookMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableFacebookMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"Facebook 平台宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
