using UnityEditor;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 热更新程序集的编辑器编译指令帮助类
    /// </summary>
    /// <remarks>
    /// Helper class for editor compile directives of hot update assemblies.
    /// </remarks>
    public static class HotFixEditorCompilerHelper
    {
        /// <summary>
        /// 移除热更新程序集的编辑器编译指令
        /// </summary>
        /// <remarks>
        /// Removes the editor compile directive of the hot update assembly.
        /// </remarks>
        [MenuItem("GameFrameX/Build/HotFix Editor Compiler Remove(移除热更新程序集的编辑器编译指令)", false, 15)]
        public static void RemoveEditor()
        {
            var path = "Assets/Hotfix/Unity.HotFix.asmdef";
            HotFixAssemblyDefinitionHelper.RemoveEditor(path);
        }

        /// <summary>
        /// 增加热更新程序集的编辑器编译指令
        /// </summary>
        /// <remarks>
        /// Adds the editor compile directive of the hot update assembly.
        /// </remarks>
        [MenuItem("GameFrameX/Build/HotFix Editor Compiler Add(增加热更新程序集的编辑器编译指令)", false, 16)]
        public static void AddEditor()
        {
            var path = "Assets/Hotfix/Unity.HotFix.asmdef";
            HotFixAssemblyDefinitionHelper.AddEditor(path);
        }
    }
}