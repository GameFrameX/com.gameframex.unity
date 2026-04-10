#if UNITY_WEBGL
using UnityEditor;
#endif

namespace GameFrameX.Editor
{
    /// <summary>
    /// 小游戏宏定义帮助类
    /// </summary>
    public static partial class MiniGameDefineSymbolHelper
    {
        private const string EnableWebGLMiniGameScriptingDefineSymbol = "ENABLE_WEBGL_MINI_GAME";

        private static string[][] GetAllMiniGameScriptingDefineSymbols()
        {
            return new[]
            {
                EnableWeChatMiniGameScriptingDefineSymbol,
                EnableDouYinMiniGameScriptingDefineSymbol,
                EnableKuaiShouMiniGameScriptingDefineSymbol,
                EnableBaiduMiniGameScriptingDefineSymbol,
                EnableAlipayMiniGameScriptingDefineSymbol,
                EnableTapTapMiniGameScriptingDefineSymbol,
            };
        }

        private static void DisableOtherMiniGameScriptingDefineSymbols(string[] currentMiniGameScriptingDefineSymbols)
        {
#if UNITY_WEBGL
            var closedCount = 0;
            foreach (var defineSymbols in GetAllMiniGameScriptingDefineSymbols())
            {
                if (defineSymbols == null)
                {
                    continue;
                }

                if (object.ReferenceEquals(defineSymbols, currentMiniGameScriptingDefineSymbols))
                {
                    continue;
                }

                foreach (var define in defineSymbols)
                {
                    if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                    {
                        continue;
                    }

                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                    closedCount++;
                    UnityEngine.Debug.Log($"小游戏宏定义 [{define}] 已经关闭");
                }
            }

            UnityEngine.Debug.Log($"小游戏宏定义互斥清理完成，共关闭 {closedCount} 个宏定义");
#endif
        }

        private static void EnableUnifiedMiniGameScriptingDefineSymbol()
        {
#if UNITY_WEBGL
            if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, EnableWebGLMiniGameScriptingDefineSymbol))
            {
                return;
            }

            ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableWebGLMiniGameScriptingDefineSymbol);
            UnityEngine.Debug.Log($"小游戏统一宏定义 [{EnableWebGLMiniGameScriptingDefineSymbol}] 已经打开");
#endif
        }

        private static void RefreshUnifiedMiniGameScriptingDefineSymbol()
        {
#if UNITY_WEBGL
            var hasAnyMiniGameDefine = false;
            foreach (var defineSymbols in GetAllMiniGameScriptingDefineSymbols())
            {
                if (defineSymbols == null)
                {
                    continue;
                }

                foreach (var define in defineSymbols)
                {
                    if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                    {
                        continue;
                    }

                    hasAnyMiniGameDefine = true;
                    break;
                }

                if (hasAnyMiniGameDefine)
                {
                    break;
                }
            }

            var hasUnifiedMiniGameDefine = ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, EnableWebGLMiniGameScriptingDefineSymbol);
            if (hasAnyMiniGameDefine)
            {
                if (!hasUnifiedMiniGameDefine)
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableWebGLMiniGameScriptingDefineSymbol);
                    UnityEngine.Debug.Log($"小游戏统一宏定义 [{EnableWebGLMiniGameScriptingDefineSymbol}] 已经打开");
                }

                return;
            }

            if (hasUnifiedMiniGameDefine)
            {
                ScriptingDefineSymbols.RemoveScriptingDefineSymbol(EnableWebGLMiniGameScriptingDefineSymbol);
                UnityEngine.Debug.Log($"小游戏统一宏定义 [{EnableWebGLMiniGameScriptingDefineSymbol}] 已经关闭");
            }
#endif
        }
    }
}
