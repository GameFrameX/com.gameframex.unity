#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// Discord 平台适配宏定义集合。
        /// Define symbols for Discord platform adaptation.
        /// </summary>
        public static readonly string[] EnableDiscordMiniGameScriptingDefineSymbol = new[] { "ENABLE_DISCORD_MINI_GAME", "DISCORDMINIGAME" };

        /// <summary>
        /// 开启 Discord 平台适配宏定义。
        /// Enables define symbols for Discord platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/International Mini Games(海外小游戏)/Enable Discord Mini Game(开启[Discord]适配)", false, 2900)]
#endif
        public static void EnableDiscordMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableDiscordMiniGameScriptingDefineSymbol);

            foreach (var define in EnableDiscordMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"Discord 平台宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭 Discord 平台适配宏定义。
        /// Disables define symbols for Discord platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/International Mini Games(海外小游戏)/Disable Discord Mini Game(关闭[Discord]适配)", false, 2901)]
#endif
        public static void DisableDiscordMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableDiscordMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"Discord 平台宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
