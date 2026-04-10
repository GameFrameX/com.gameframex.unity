#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// 快手小游戏适配宏定义集合。
        /// Define symbols for KuaiShou mini game adaptation.
        /// </summary>
        public static readonly string[] EnableKuaiShouMiniGameScriptingDefineSymbol = new[] { "ENABLE_KUAISHOU_MINI_GAME", "KUAISHOUMINIGAME" };

        /// <summary>
        /// 开启快手小游戏适配宏定义。
        /// Enables define symbols for KuaiShou mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable KuaiShou Mini Game(开启[快手小游戏]适配)", false, 2200)]
#endif
        public static void EnableKuaiShouMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableKuaiShouMiniGameScriptingDefineSymbol);

            foreach (var define in EnableKuaiShouMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"快手小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭快手小游戏适配宏定义。
        /// Disables define symbols for KuaiShou mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable KuaiShou Mini Game(关闭[快手小游戏]适配)", false, 2201)]
#endif
        public static void DisableKuaiShouMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableKuaiShouMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"快手小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
