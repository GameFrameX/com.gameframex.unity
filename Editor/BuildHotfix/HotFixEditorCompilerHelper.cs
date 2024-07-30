using UnityEditor;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 热更新程序集的编辑器编译指令帮助类
    /// </summary>
    public static class HotFixEditorCompilerHelper
    {
        /// <summary>
        /// 移除热更新程序集的编辑器编译指令
        /// </summary>
        [MenuItem("Tools/Build/HotFix Editor Compiler Remove", false, 100)]
        public static void RemoveEditor()
        {
            var path = "Assets/Hotfix/Unity.HotFix.asmdef";
            HotFixAssemblyDefinitionHelper.RemoveEditor(path);
        }

        /// <summary>
        /// 增加热更新程序集的编辑器编译指令
        /// </summary>
        [MenuItem("Tools/Build/HotFix Editor Compiler Add", false, 100)]
        public static void AddEditor()
        {
            var path = "Assets/Hotfix/Unity.HotFix.asmdef";
            HotFixAssemblyDefinitionHelper.AddEditor(path);
        }
    }
}