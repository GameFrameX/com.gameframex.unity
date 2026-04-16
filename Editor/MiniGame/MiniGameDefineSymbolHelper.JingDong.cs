#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// 京东小游戏适配宏定义集合。
        /// Define symbols for JingDong (JD.com) mini game adaptation.
        /// </summary>
        public static readonly string[] EnableJingDongMiniGameScriptingDefineSymbol = new[] { "ENABLE_JINGDONG_MINI_GAME", "JINGDONGMINIGAME" };

        /// <summary>
        /// 开启京东小游戏适配宏定义。
        /// Enables define symbols for JingDong mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable JingDong Mini Game(开启[京东小游戏]适配)", false, 2500)]
#endif
        public static void EnableJingDongMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableJingDongMiniGameScriptingDefineSymbol);

            foreach (var define in EnableJingDongMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"京东小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭京东小游戏适配宏定义。
        /// Disables define symbols for JingDong mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable JingDong Mini Game(关闭[京东小游戏]适配)", false, 2501)]
#endif
        public static void DisableJingDongMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableJingDongMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"京东小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
