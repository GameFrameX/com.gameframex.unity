#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// 淘宝小程序适配宏定义集合。
        /// Define symbols for Taobao mini game adaptation.
        /// </summary>
        public static readonly string[] EnableTaobaoMiniGameScriptingDefineSymbol = new[] { "ENABLE_TAOBAO_MINI_GAME", "TAOBAOMINIGAME" };

        /// <summary>
        /// 开启淘宝小程序适配宏定义。
        /// Enables define symbols for Taobao mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Taobao Mini Game(开启[淘宝小程序]适配)", false, 2600)]
#endif
        public static void EnableTaobaoMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableTaobaoMiniGameScriptingDefineSymbol);

            foreach (var define in EnableTaobaoMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"淘宝小程序宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭淘宝小程序适配宏定义。
        /// Disables define symbols for Taobao mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable Taobao Mini Game(关闭[淘宝小程序]适配)", false, 2601)]
#endif
        public static void DisableTaobaoMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableTaobaoMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"淘宝小程序宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
