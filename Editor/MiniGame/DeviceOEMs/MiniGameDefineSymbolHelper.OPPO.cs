#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// OPPO 小游戏适配宏定义集合。
        /// Define symbols for OPPO mini game adaptation.
        /// </summary>
        public static readonly string[] EnableOPPOMiniGameScriptingDefineSymbol = new[] { "ENABLE_OPPO_MINI_GAME", "OPPOSMINIGAME" };

        /// <summary>
        /// 开启 OPPO 小游戏适配宏定义。
        /// Enables define symbols for OPPO mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Device OEMs(设备厂商)/Enable OPPO Mini Game(开启[OPPO小游戏]适配)", false, 3700)]
#endif
        public static void EnableOPPOMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableOPPOMiniGameScriptingDefineSymbol);

            foreach (var define in EnableOPPOMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"OPPO 小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭 OPPO 小游戏适配宏定义。
        /// Disables define symbols for OPPO mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Device OEMs(设备厂商)/Disable OPPO Mini Game(关闭[OPPO小游戏]适配)", false, 3701)]
#endif
        public static void DisableOPPOMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableOPPOMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"OPPO 小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
