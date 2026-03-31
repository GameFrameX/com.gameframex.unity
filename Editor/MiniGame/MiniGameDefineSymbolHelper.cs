#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    /// <summary>
    /// 小游戏宏定义帮助类
    /// </summary>
    public static class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// 微信小游戏的适配宏定义
        /// </summary>
        public readonly static string[] EnableWeChatMiniGameScriptingDefineSymbol = new string[] { "ENABLE_WECHAT_MINI_GAME", "WEIXINMINIGAME", };

        /// <summary>
        /// 抖音小游戏的适配宏定义
        /// </summary>
        public readonly static string[] EnableDouYinMiniGameScriptingDefineSymbol = new string[] { "ENABLE_DOUYIN_MINI_GAME", "DOUYINMINIGAME", "TTSDK_MIX_ENGINE", };

        /// <summary>
        /// 开启微信小游戏的适配
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable WeChat Mini Game(开启[微信小游戏]适配)", false, 100)]
#endif
        public static void EnableWeChatMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableWeChatMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"微信小游戏宏定义 [{define}] 已经打开");
            }
#endif
        }

        /// <summary>
        /// 关闭微信小游戏的适配
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable WeChat Mini Game(关闭[微信小游戏]适配)", false, 101)]
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
#endif
        }

        /// <summary>
        /// 开启抖音小游戏的适配
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable DouYin Mini Game(开启[抖音小游戏]适适配)", false, 200)]
#endif
        public static void EnableDouYinMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableDouYinMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"抖音小游戏宏定义 [{define}] 已经打开");
            }
#endif
        }

        /// <summary>
        /// 关闭抖音小游戏的适配
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable DouYin Mini Game(关闭[抖音小游戏]适配)", false, 201)]
#endif
        public static void DisableDouYinMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnableDouYinMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"抖音小游戏宏定义 [{define}] 已经关闭");
            }
#endif
        }
    }
}