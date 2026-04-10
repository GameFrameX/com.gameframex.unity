#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// TapTap 小游戏适配宏定义集合。
        /// Define symbols for TapTap mini game adaptation.
        /// </summary>
        public static readonly string[] EnableTapTapMiniGameScriptingDefineSymbol = new[] { "ENABLE_TAPTAP_MINI_GAME", "TAPTAPMINIGAME" };

        /// <summary>
        /// 开启 TapTap 小游戏适配宏定义。
        /// Enables define symbols for TapTap mini game adaptation.
        /// </summary>
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

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭 TapTap 小游戏适配宏定义。
        /// Disables define symbols for TapTap mini game adaptation.
        /// </summary>
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

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
