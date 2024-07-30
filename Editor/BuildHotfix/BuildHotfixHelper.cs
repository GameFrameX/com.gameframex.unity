using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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
        //Unity代码生成dll位置
        private const           string HotFixAssembliesDir = "Library/ScriptAssemblies";
        private static readonly string ScriptAssembliesDir = $"HybridCLRData/HotUpdateDlls/{EditorUserBuildSettings.activeBuildTarget}";

        private static readonly string[] HotfixDlls = new string[] { "Unity.Hotfix.dll", "Unity.Hotfix.pdb" };

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
        [MenuItem("Tools/Build/Copy Hotfix Code")]
        public static void CopyHotfixCode()
        {
            if (!Directory.Exists(CodeDir))
            {
                Directory.CreateDirectory(CodeDir);
            }

            foreach (var hotfix in HotfixDlls)
            {
                var srcPath = Path.Combine(HotFixAssembliesDir, hotfix);

                File.Copy(srcPath, Path.Combine(CodeDir, hotfix + Utility.Const.FileNameSuffix.Binary), true);
            }

            Debug.Log($"复制Hotfix DLL, Hotfix pdb到{CodeDir}完成");
            AssetDatabase.Refresh();
        }

        public const string AOTCodeDir = "Assets/Bundles/AOTCode/";

        /// <summary>
        /// 复制AOT代码
        /// </summary>
        [MenuItem("Tools/Build/Copy AOT Code")]
        public static void CopyAOTCode()
        {
            if (!Directory.Exists(AOTCodeDir))
            {
                Directory.CreateDirectory(AOTCodeDir);
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
            string        path          = Path.Combine(directoryInfo.Parent.FullName, "HybridCLRData", "AssembliesPostIl2CppStrip", EditorUserBuildSettings.activeBuildTarget.ToString());

            DirectoryInfo aotCodeDir    = new DirectoryInfo(path);
            var           files         = aotCodeDir.GetFiles("*.dll");
            var           stringBuilder = new StringBuilder();
            foreach (var fileInfo in files)
            {
                stringBuilder.AppendLine(fileInfo.Name);
                fileInfo.CopyTo(AOTCodeDir + "/" + fileInfo.Name + Utility.Const.FileNameSuffix.Binary, true);
            }

            Debug.Log(stringBuilder);
            Debug.Log($"复制AOT DLL, Hotfix pdb到{CodeDir}完成");
            AssetDatabase.Refresh();
        }
    }
}