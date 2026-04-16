#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// 华为小游戏适配宏定义集合。
        /// Define symbols for Huawei mini game adaptation.
        /// </summary>
        public static readonly string[] EnableHuaweiMiniGameScriptingDefineSymbol = new[] { "ENABLE_HUAWEI_MINI_GAME", "HUAWEIMINIGAME" };

        /// <summary>
        /// 开启华为小游戏适配宏定义。
        /// Enables define symbols for Huawei mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Device OEMs(设备厂商)/Enable Huawei Mini Game(开启[华为小游戏]适配)", false, 3900)]
#endif
        public static void EnableHuaweiMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableHuaweiMiniGameScriptingDefineSymbol);

            foreach (var define in EnableHuaweiMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"华为小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭华为小游戏适配宏定义。
        /// Disables define symbols for Huawei mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Device OEMs(设备厂商)/Disable Huawei Mini Game(关闭[华为小游戏]适配)", false, 3901)]
#endif
        public static void DisableHuaweiMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableHuaweiMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"华为小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
