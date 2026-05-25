using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    public partial class PackageManagerWindow
    {
        private void OnGUIByBanner()
        {
            bool hasRegistry = ScopedRegistryHelper.HasGameFrameXScopedRegistry();

            if (!hasRegistry)
            {
                EditorGUILayout.HelpBox(
                    "强烈建议配置 GameFrameX Scoped Registry！配置后可通过 Unity 内置 Package Manager 管理所有 GameFrameX 包，获得更好的版本管理和依赖解析体验。",
                    MessageType.Warning);
                if (GUILayout.Button("Set GameFrameX Scoped Registry"))
                {
                    ScopedRegistryHelper.SetGameFrameXScopedRegistry();
                }
            }
            else
            {
                EditorGUILayout.HelpBox(
                    "检测到部分 GameFrameX 包仍通过 Git 安装，强烈建议迁移到 Scoped Registry 以获得更好的版本管理和依赖解析体验。",
                    MessageType.Warning);
                if (GUILayout.Button("Migrate Git Packages to Registry(迁移 Git 包到 Registry)"))
                {
                    ScopedRegistryHelper.MigrateToRegistry();
                }
            }
        }
    }
}
