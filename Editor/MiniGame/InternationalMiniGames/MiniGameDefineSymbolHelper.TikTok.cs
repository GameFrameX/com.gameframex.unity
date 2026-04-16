#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// TikTok 小游戏适配宏定义集合。
        /// Define symbols for TikTok mini game adaptation.
        /// </summary>
        public static readonly string[] EnableTikTokMiniGameScriptingDefineSymbol = new[] { "ENABLE_TIKTOK_MINI_GAME", "TIKTOKMINIGAME" };

        /// <summary>
        /// 开启 TikTok 小游戏适配宏定义。
        /// Enables define symbols for TikTok mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/International Mini Games(海外小游戏)/Enable TikTok Mini Game(开启[TikTok小游戏]适配)", false, 3300)]
#endif
        public static void EnableTikTokMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableTikTokMiniGameScriptingDefineSymbol);

            foreach (var define in EnableTikTokMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"TikTok 小游戏宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭 TikTok 小游戏适配宏定义。
        /// Disables define symbols for TikTok mini game adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/International Mini Games(海外小游戏)/Disable TikTok Mini Game(关闭[TikTok小游戏]适配)", false, 3301)]
#endif
        public static void DisableTikTokMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableTikTokMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"TikTok 小游戏宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
