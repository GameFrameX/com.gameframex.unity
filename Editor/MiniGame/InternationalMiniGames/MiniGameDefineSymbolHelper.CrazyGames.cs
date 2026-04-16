#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// CrazyGames 平台适配宏定义集合。
        /// Define symbols for CrazyGames platform adaptation.
        /// </summary>
        public static readonly string[] EnableCrazyGamesMiniGameScriptingDefineSymbol = new[] { "ENABLE_CRAZYGAMES_MINI_GAME", "CRAZYGAMESMINIGAME" };

        /// <summary>
        /// 开启 CrazyGames 平台适配宏定义。
        /// Enables define symbols for CrazyGames platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/International Mini Games(海外小游戏)/Enable CrazyGames Mini Game(开启[CrazyGames]适配)", false, 3400)]
#endif
        public static void EnableCrazyGamesMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableCrazyGamesMiniGameScriptingDefineSymbol);

            foreach (var define in EnableCrazyGamesMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"CrazyGames 平台宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭 CrazyGames 平台适配宏定义。
        /// Disables define symbols for CrazyGames platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/International Mini Games(海外小游戏)/Disable CrazyGames Mini Game(关闭[CrazyGames]适配)", false, 3401)]
#endif
        public static void DisableCrazyGamesMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableCrazyGamesMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"CrazyGames 平台宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
