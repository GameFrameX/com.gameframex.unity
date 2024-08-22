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
        public const string EnableWeChatMiniGameScriptingDefineSymbol = "ENABLE_WECHAT_MINI_GAME";
        public const string EnableDouYinMiniGameScriptingDefineSymbol = "ENABLE_DOUYIN_MINI_GAME";

        /// <summary>
        /// 开启微信小游戏的适配
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/MiniGame/Open WeChat MiniGame")]
#endif
        public static void OpenWeChatMiniGame()
        {
#if UNITY_WEBGL
            if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, EnableWeChatMiniGameScriptingDefineSymbol))
            {
                ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableWeChatMiniGameScriptingDefineSymbol);
            }

            Debug.Log($"微信小游戏宏定义 [{EnableDouYinMiniGameScriptingDefineSymbol}] 已经打开");
#endif
        }

        /// <summary>
        /// 关闭微信小游戏的适配
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/MiniGame/Close WeChat MiniGame")]
#endif
        public static void CloseWeChatMiniGame()
        {
#if UNITY_WEBGL
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol(EnableWeChatMiniGameScriptingDefineSymbol);
#endif
        }

        /// <summary>
        /// 开启抖音小游戏的适配
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/MiniGame/Open DouYin MiniGame")]
#endif
        public static void OpenDouYinMiniGame()
        {
#if UNITY_WEBGL
            if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, EnableDouYinMiniGameScriptingDefineSymbol))
            {
                ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableDouYinMiniGameScriptingDefineSymbol);
            }

            Debug.Log($"抖音小游戏宏定义 [{EnableDouYinMiniGameScriptingDefineSymbol}] 已经打开");
#endif
        }

        /// <summary>
        /// 关闭抖音小游戏的适配
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/MiniGame/Close DouYin MiniGame")]
#endif
        public static void CloseDouYinMiniGame()
        {
#if UNITY_WEBGL
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol(EnableDouYinMiniGameScriptingDefineSymbol);
#endif
        }
    }
}