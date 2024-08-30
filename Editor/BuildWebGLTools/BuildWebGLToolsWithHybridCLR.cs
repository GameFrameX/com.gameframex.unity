#if UNITY_WEBGL && UNITY_EDITOR && !UNITY_2021_3_OR_NEWER

using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 当使用HybridCLR发布到WEBGL的时候执行
    /// </summary>
    public static class BuildWebGLToolsWithHybridCLR
    {
        /// <summary>
        /// 获取工程路径
        /// </summary>
        /// <returns></returns>
        private static string GetProjectPath()
        {
            return Application.dataPath.Replace("Assets", string.Empty);
        }

        [MenuItem("GameFrameX/Print WEBGL Environment Command Line")]
        private static void Print()
        {
#if UNITY_EDITOR_OSX
            string commandLine1 = $"cd {EditorApplication.applicationPath}/Contents/il2cpp";
            string commandLine2 = "mv libil2cpp libil2cpp-origin";
            string commandLine3 = $"ln -s \"{GetProjectPath()}/HybridCLRData/LocalIl2CppData-OSXEditor/il2cpp/libil2cpp\" libil2cpp";
#else
            string commandLine1 = $"cd /d {EditorApplication.applicationPath.Replace(".exe", string.Empty)}/Editor/Data/il2cpp";
            string commandLine2 = "ren libil2cpp libil2cpp-origin";
            string commandLine3 = $"mklink /D libil2cpp \"{GetProjectPath()}/HybridCLRData/LocalIl2CppData-WindowsEditor/il2cpp/libil2cpp\"";
#endif
            Debug.Log("打开命令行终端。依次执行以下3条命令");
            Debug.Log(commandLine1);
            Debug.Log(commandLine2);
            Debug.Log(commandLine3);
        }
    }
}
#endif