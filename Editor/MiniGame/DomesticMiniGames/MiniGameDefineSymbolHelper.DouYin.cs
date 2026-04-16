#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// 抖音小游戏适配宏定义集合。
        /// Define symbols for DouYin mini game adaptation.
        /// </summary>
        public static readonly string[] EnableDouYinMiniGameScriptingDefineSymbol = new[] { "ENABLE_DOUYIN_MINI_GAME", "DOUYINMINIGAME", "TTSDK_MIX_ENGINE" };

        /// <summary>
        /// 开启抖音小游戏适配宏定义。
        /// Enables define symbols for DouYin mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Domestic Mini Games(国内小游戏)/Enable DouYin Mini Game(开启[抖音小游戏]适配)", false, 2100)]
#endif
        public static void EnableDouYinMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableDouYinMiniGameScriptingDefineSymbol);

            foreach (var define in EnableDouYinMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"抖音小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭抖音小游戏适配宏定义。
        /// Disables define symbols for DouYin mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Domestic Mini Games(国内小游戏)/Disable DouYin Mini Game(关闭[抖音小游戏]适配)", false, 2101)]
#endif
        public static void DisableDouYinMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableDouYinMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"抖音小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
