using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 热更新 AOT 元数据保留生成功能的 Unity 菜单入口。
    /// </summary>
    /// <remarks>
    /// Unity menu entry for the hot-update AOT metadata preservation generation feature.
    /// </remarks>
    internal static class AotPreserveMenu
    {
        /// <summary>
        /// 通过 Unity 菜单触发生成 link.xml 与引用代码文件，并输出日志。
        /// </summary>
        /// <remarks>
        /// Triggers generation of link.xml and reference code files via the Unity menu, and logs the result.
        /// </remarks>
        [MenuItem(AotPreserveConstants.MenuPath)]
        public static void Generate()
        {
            AotPreserveGenerator.GenerateAndWrite();
            Debug.Log("Hotfix AOT preserve files generated under " + AotPreserveConstants.GeneratedDirectory);
        }
    }
}
