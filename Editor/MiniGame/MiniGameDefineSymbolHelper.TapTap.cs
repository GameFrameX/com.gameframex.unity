#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        public static readonly string[] EnableTapTapMiniGameScriptingDefineSymbol = new[] { "ENABLE_TAPTAP_MINI_GAME", "TAPTAPMINIGAME" };

#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable TapTap Mini Game(开启[TapTap小游戏]适配)", false, 2500)]
#endif
        public static void EnableTapTapMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableTapTapMiniGameScriptingDefineSymbol);

            foreach (var define in EnableTapTapMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"TapTap小游戏宏定义 [{define}] 已经打开");
            }
#endif
        }

#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable TapTap Mini Game(关闭[TapTap小游戏]适配)", false, 2501)]
#endif
        public static void DisableTapTapMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableTapTapMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"TapTap小游戏宏定义 [{define}] 已经关闭");
            }
#endif
        }
    }
}
