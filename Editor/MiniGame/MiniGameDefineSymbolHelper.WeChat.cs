#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        public static readonly string[] EnableWeChatMiniGameScriptingDefineSymbol = new[] { "ENABLE_WECHAT_MINI_GAME", "WEIXINMINIGAME" };

#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable WeChat Mini Game(开启[微信小游戏]适配)", false, 2000)]
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

#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable WeChat Mini Game(关闭[微信小游戏]适配)", false, 2001)]
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
