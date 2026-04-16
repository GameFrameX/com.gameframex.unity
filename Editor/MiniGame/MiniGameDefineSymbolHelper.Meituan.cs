#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// 美团小游戏适配宏定义集合。
        /// Define symbols for Meituan mini game adaptation.
        /// </summary>
        public static readonly string[] EnableMeituanMiniGameScriptingDefineSymbol = new[] { "ENABLE_MEITUAN_MINI_GAME", "MEITUANMINIGAME" };

        /// <summary>
        /// 开启美团小游戏适配宏定义。
        /// Enables define symbols for Meituan mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Meituan Mini Game(开启[美团小游戏]适配)", false, 2600)]
#endif
        public static void EnableMeituanMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableMeituanMiniGameScriptingDefineSymbol);

            foreach (var define in EnableMeituanMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"美团小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭美团小游戏适配宏定义。
        /// Disables define symbols for Meituan mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable Meituan Mini Game(关闭[美团小游戏]适配)", false, 2601)]
#endif
        public static void DisableMeituanMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableMeituanMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"美团小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
