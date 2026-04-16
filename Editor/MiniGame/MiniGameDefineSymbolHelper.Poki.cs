#if UNITY_WEBGL
using UnityEditor;
using UnityEngine;
#endif

namespace GameFrameX.Editor
{
    public static partial class MiniGameDefineSymbolHelper
    {
        /// <summary>
        /// Poki 平台适配宏定义集合。
        /// Define symbols for Poki platform adaptation.
        /// </summary>
        public static readonly string[] EnablePokiMiniGameScriptingDefineSymbol = new[] { "ENABLE_POKI_MINI_GAME", "POKIMINIGAME" };

        /// <summary>
        /// 开启 Poki 平台适配宏定义。
        /// Enables define symbols for Poki platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable Poki Mini Game(开启[Poki]适配)", false, 3700)]
#endif
        public static void EnablePokiMiniGame()
        {
#if UNITY_WEBGL
            DisableOtherMiniGameScriptingDefineSymbols(EnablePokiMiniGameScriptingDefineSymbol);

            foreach (var define in EnablePokiMiniGameScriptingDefineSymbol)
            {
                if (!ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.AddScriptingDefineSymbol(define);
                }

                Debug.Log($"Poki 平台宏定义 [{define}] 已经打开");
            }

            EnableUnifiedMiniGameScriptingDefineSymbol();
#endif
        }

        /// <summary>
        /// 关闭 Poki 平台适配宏定义。
        /// Disables define symbols for Poki platform adaptation.
        /// </summary>
#if UNITY_WEBGL
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable Poki Mini Game(关闭[Poki]适配)", false, 3701)]
#endif
        public static void DisablePokiMiniGame()
        {
#if UNITY_WEBGL
            foreach (var define in EnablePokiMiniGameScriptingDefineSymbol)
            {
                if (ScriptingDefineSymbols.HasScriptingDefineSymbol(BuildTargetGroup.WebGL, define))
                {
                    ScriptingDefineSymbols.RemoveScriptingDefineSymbol(define);
                }

                Debug.Log($"Poki 平台宏定义 [{define}] 已经关闭");
            }

            RefreshUnifiedMiniGameScriptingDefineSymbol();
#endif
        }
    }
}
