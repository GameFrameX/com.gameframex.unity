#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// vivo 小游戏适配宏定义集合。
        /// Define symbols for vivo mini game adaptation.
        /// </summary>
        public static readonly string[] EnableVivoMiniGameScriptingDefineSymbol = new[] { "ENABLE_VIVO_MINI_GAME", "VIVOMINIGAME" };

        /// <summary>
        /// 开启 vivo 小游戏适配宏定义。
        /// Enables define symbols for vivo mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Vivo Mini Game(开启[vivo小游戏]适配)", false, 3100)]
#endif
        public static void EnableVivoMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableVivoMiniGameScriptingDefineSymbol);

            foreach (var define in EnableVivoMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"vivo 小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭 vivo 小游戏适配宏定义。
        /// Disables define symbols for vivo mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable Vivo Mini Game(关闭[vivo小游戏]适配)", false, 3101)]
#endif
        public static void DisableVivoMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableVivoMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"vivo 小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
