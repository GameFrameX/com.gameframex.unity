#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        public static readonly string[] EnableAlipayMiniGameScriptingDefineSymbol = new[] { "ENABLE_ALIPAY_MINI_GAME", "ALIPAYMINIGAME" };

#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Alipay Mini Game(开启[支付宝小游戏]适配)", false, 2400)]
#endif
        public static void EnableAlipayMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableAlipayMiniGameScriptingDefineSymbol);

            foreach (var define in EnableAlipayMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"支付宝小游戏宏定义 [{define}] 已经打开");
            }
#endif
        }

#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable Alipay Mini Game(关闭[支付宝小游戏]适配)", false, 2401)]
#endif
        public static void DisableAlipayMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableAlipayMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"支付宝小游戏宏定义 [{define}] 已经关闭");
            }
#endif
        }
    }
}
