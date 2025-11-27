using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using GameFrameX.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 热更新编辑器
    /// </summary>
    [InitializeOnLoad]
    public static class BuildHotfixHelper
    {
#if ENABLE_GAME_FRAME_X_HYBRID_CLR
        private static readonly string ScriptAssembliesDir = $"HybridCLRData/HotUpdateDlls/{EditorUserBuildSettings.activeBuildTarget}";
#endif
#if ENABLE_GAME_FRAME_X_HYBRID_CLR && !DISABLE_HYBRIDCLR
        //Unity代码生成dll位置
        private const string HotFixAssembliesDir = "Library/ScriptAssemblies";
        private static readonly string[] HotfixDlls = new string[] { "Unity.HotFix.dll" };

        //热更代码存放位置
        private const string CodeDir = "Assets/Bundles/Code/";

        static BuildHotfixHelper()
        {
            async Task WaitExecute()
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                //拷贝热更代码
                CopyHotfixCode();
            }

            _ = WaitExecute();
        }

        /// <summary>
        /// 复制热更新代码
        /// </summary>
        [MenuItem("GameFrameX/Build/Copy HotFix Code(复制热更新代码到Assets>Bundles>Code)", false, 10)]
        public static void CopyHotfixCode()
        {
            if (!Directory.Exists(CodeDir))
            {
                Directory.CreateDirectory(CodeDir);
            }

            foreach (var hotfix in HotfixDlls)
            {
                var srcPath = Path.Combine(HotFixAssembliesDir, hotfix);

                if (!File.Exists(srcPath))
                {
                    continue;
                }

                File.Copy(srcPath, Path.Combine(CodeDir, hotfix + Utility.Const.FileNameSuffix.Binary), true);
            }

            Debug.Log($"复制Hotfix DLL到{CodeDir}完成");
            AssetDatabase.Refresh();
        }
#endif
#if ENABLE_GAME_FRAME_X_HYBRID_CLR
        public const string AOTCodeDir = "Assets/Bundles/AOTCode/";

        /// <summary>
        /// 复制AOT代码
        /// </summary>
        [MenuItem("GameFrameX/Build/Copy AOT Code(复制AOT代码到Assets>Bundles>AOTCode)", false, 11)]
        public static void CopyAOTCode()
        {
            if (!Directory.Exists(AOTCodeDir))
            {
                Directory.CreateDirectory(AOTCodeDir);
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
            string path = Path.Combine(directoryInfo.Parent.FullName, "HybridCLRData", "AssembliesPostIl2CppStrip", EditorUserBuildSettings.activeBuildTarget.ToString());

            DirectoryInfo aotCodeDir = new DirectoryInfo(path);
            var files = aotCodeDir.GetFiles("*.dll");
            var stringBuilder = new StringBuilder();
            foreach (var fileInfo in files)
            {
                stringBuilder.AppendLine(fileInfo.Name);
                fileInfo.CopyTo(AOTCodeDir + "/" + fileInfo.Name + Utility.Const.FileNameSuffix.Binary, true);
            }

            Debug.Log(stringBuilder);
            Debug.Log($"复制AOT DLL到{AOTCodeDir}完成");
            AssetDatabase.Refresh();
        }
#endif

        public const string HybridCLRScriptingDefineSymbol = "DISABLE_HYBRIDCLR";

        /// <summary>
        /// 开启 HybridCLR 的支持。
        /// </summary>
        [MenuItem("GameFrameX/Scripting Define Symbols/Disable HybridCLR(关闭HybridCLR适配)", false, 5000)]
        public static void DisableHybridCLR()
        {
            ScriptingDefineSymbols.AddScriptingDefineSymbol(HybridCLRScriptingDefineSymbol);
        }

        /// <summary>
        /// 关闭 HybridCLR 的支持。
        /// </summary>
        [MenuItem("GameFrameX/Scripting Define Symbols/Enable HybridCLR(开启HybridCLR适配)", false, 5001)]
        public static void EnableHybridCLR()
        {
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol(HybridCLRScriptingDefineSymbol);
        }
    }
}