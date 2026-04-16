#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// Bilibili小游戏适配宏定义集合。
        /// Define symbols for Bilibili mini game adaptation.
        /// </summary>
        public static readonly string[] EnableBilibiliMiniGameScriptingDefineSymbol = new[] { "ENABLE_BILIBILI_MINI_GAME", "BILIBILIMINIGAME" };

        /// <summary>
        /// 开启Bilibili小游戏适配宏定义。
        /// Enables define symbols for Bilibili mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Domestic Mini Games(国内小游戏)/Enable Bilibili Mini Game(开启[Bilibili小游戏]适配)", false, 2700)]
#endif
        public static void EnableBilibiliMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableBilibiliMiniGameScriptingDefineSymbol);

            foreach (var define in EnableBilibiliMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"Bilibili小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭Bilibili小游戏适配宏定义。
        /// Disables define symbols for Bilibili mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Domestic Mini Games(国内小游戏)/Disable Bilibili Mini Game(关闭[Bilibili小游戏]适配)", false, 2701)]
#endif
        public static void DisableBilibiliMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableBilibiliMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"Bilibili小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
