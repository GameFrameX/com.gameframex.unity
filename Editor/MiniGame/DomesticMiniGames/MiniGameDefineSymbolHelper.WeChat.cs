#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// 微信小游戏适配宏定义集合。
        /// Define symbols for WeChat mini game adaptation.
        /// </summary>
        public static readonly string[] EnableWeChatMiniGameScriptingDefineSymbol = new[] { "ENABLE_WECHAT_MINI_GAME", "WEIXINMINIGAME" };

        /// <summary>
        /// 开启微信小游戏适配宏定义。
        /// Enables define symbols for WeChat mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Domestic Mini Games(国内小游戏)/Enable WeChat Mini Game(开启[微信小游戏]适配)", false, 2000)]
#endif
        public static void EnableWeChatMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableWeChatMiniGameScriptingDefineSymbol);

            foreach (var define in EnableWeChatMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"微信小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭微信小游戏适配宏定义。
        /// Disables define symbols for WeChat mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Domestic Mini Games(国内小游戏)/Disable WeChat Mini Game(关闭[微信小游戏]适配)", false, 2001)]
#endif
        public static void DisableWeChatMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableWeChatMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"微信小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
