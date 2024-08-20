using UnityEditor;
using UnityEngine;

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
        [MenuItem("GameFrameX/MiniGame/Open WeChat MiniGame")]
        public static void OpenWeChatMiniGame()
        {
            if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, EnableWeChatMiniGameScriptingDefineSymbol))
            {
                ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableWeChatMiniGameScriptingDefineSymbol);
            }

            Debug.Log($"微信小游戏宏定义 [{EnableDouYinMiniGameScriptingDefineSymbol}] 已经打开");
        }

        /// <summary>
        /// 关闭微信小游戏的适配
        /// </summary>
        [MenuItem("GameFrameX/MiniGame/Close WeChat MiniGame")]
        public static void CloseWeChatMiniGame()
        {
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol(EnableWeChatMiniGameScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启抖音小游戏的适配
        /// </summary>
        [MenuItem("GameFrameX/MiniGame/Open DouYin MiniGame")]
        public static void OpenDouYinMiniGame()
        {
            if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, EnableDouYinMiniGameScriptingDefineSymbol))
            {
                ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableDouYinMiniGameScriptingDefineSymbol);
            }

            Debug.Log($"抖音小游戏宏定义 [{EnableDouYinMiniGameScriptingDefineSymbol}] 已经打开");
        }

        /// <summary>
        /// 关闭抖音小游戏的适配
        /// </summary>
        [MenuItem("GameFrameX/MiniGame/Close DouYin MiniGame")]
        public static void CloseDouYinMiniGame()
        {
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol(EnableDouYinMiniGameScriptingDefineSymbol);
        }
    }
}