#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        public static readonly string[] EnableBaiduMiniGameScriptingDefineSymbol = new[] { "ENABLE_BAIDU_MINI_GAME", "BAIDUMINIGAME" };

#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Baidu Mini Game(开启[百度小游戏]适配)", false, 2300)]
#endif
        public static void EnableBaiduMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnableBaiduMiniGameScriptingDefineSymbol);

            foreach (var define in EnableBaiduMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"百度小游戏宏定义 [{define}] 已经打开");
            }
#endif
        }

#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable Baidu Mini Game(关闭[百度小游戏]适配)", false, 2301)]
#endif
        public static void DisableBaiduMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableBaiduMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"百度小游戏宏定义 [{define}] 已经关闭");
            }
#endif
        }
    }
}
