#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// 小米小游戏适配宏定义集合。
        /// Define symbols for Xiaomi mini game adaptation.
        /// </summary>
        public static readonly string[] EnableXiaomiMiniGameScriptingDefineSymbol = new[] { "ENABLE_XIAOMI_MINI_GAME", "XIAOMIMINIGAME" };

        /// <summary>
        /// 开启小米小游戏适配宏定义。
        /// Enables define symbols for Xiaomi mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Device OEMs(设备厂商)/Enable Xiaomi Mini Game(开启[小米小游戏]适配)", false, 3800)]
#endif
        public static void EnableXiaomiMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableXiaomiMiniGameScriptingDefineSymbol);

            foreach (var define in EnableXiaomiMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"小米小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭小米小游戏适配宏定义。
        /// Disables define symbols for Xiaomi mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Device OEMs(设备厂商)/Disable Xiaomi Mini Game(关闭[小米小游戏]适配)", false, 3801)]
#endif
        public static void DisableXiaomiMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableXiaomiMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"小米小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
