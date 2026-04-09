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
        private static readonly string[][] AllMiniGameScriptingDefineSymbols =
        {
            EnableWeChatMiniGameScriptingDefineSymbol,
            EnableDouYinMiniGameScriptingDefineSymbol,
            EnableKuaiShouMiniGameScriptingDefineSymbol,
            EnableBaiduMiniGameScriptingDefineSymbol,
            EnableAlipayMiniGameScriptingDefineSymbol,
            EnableTapTapMiniGameScriptingDefineSymbol,
        };

        private static void DisableOtherMiniGameScriptingDefineSymbols(string[] currentMiniGameScriptingDefineSymbols)
        {
#if UNITY_WEBGL
            foreach (var defineSymbols in AllMiniGameScriptingDefineSymbols)
            {
                if (object.ReferenceEquals(defineSymbols, currentMiniGameScriptingDefineSymbols))
                {
                    continue;
                }

                foreach (var define in defineSymbols)
                {
                    if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                    {
                        ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                        UnityEngine.Debug.Log($"小游戏宏定义 [{define}] 已经关闭");
                    }
                }
            }
#endif
        }
    }
}
