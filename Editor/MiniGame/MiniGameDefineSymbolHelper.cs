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
    }
}
