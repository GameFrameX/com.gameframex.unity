using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    public partial class PackageManagerWindow
    {
        // 下拉列表的选项
        private readonly string[] _dropdownOptions = new string[] { "gitee.com", "github.com" };
        private int _selectedDropdownIndex = 0;

        private void OnGUIByToolBar()
        {
            // 创建一个工具条
            // 工具条
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Label("镜像列表:", EditorStyles.label, GUILayout.Width(100)); // 设置宽度以确保一致性
            // 下拉列表
            int newDropdownIndex = EditorGUILayout.Popup(_selectedDropdownIndex, _dropdownOptions, EditorStyles.toolbarDropDown);
            if (newDropdownIndex != _selectedDropdownIndex)
            {
                // 弹出提示框，请求确认
                var confirmed = EditorUtility.DisplayDialog("切换提示", $"您将要将所有的包的下载地址切换为镜像地址:\n{_dropdownOptions[newDropdownIndex]}\n可能会遇到部分库没有的情况。请提交Issue反馈", "确认", "取消");

                if (confirmed)
                {
                    // 如果用户选择"Yes"，则更新所选索引
                    _selectedDropdownIndex = newDropdownIndex;
                    Dictionary<string, string> dependencies = new Dictionary<string, string>(PackagesManifest.Dependencies);
                    foreach (var manifestDependency in PackagesManifest.Dependencies)
                    {
                        if (manifestDependency.Value.StartsWith("https://"))
                        {
                            var path = manifestDependency.Value.Replace("https://", string.Empty).Replace("\\", "/");
                            var index = path.IndexOf("/", System.StringComparison.OrdinalIgnoreCase);
                            if (index >= 0)
                            {
                                path = path.Substring(index);
                            }

                            dependencies[manifestDependency.Key] = $"https://{_dropdownOptions[_selectedDropdownIndex]}{path}";
                        }
                    }

                    PackagesManifest.Dependencies = dependencies;
                    SavePackages(PackagesManifest);
                }
            }

            // 自动伸缩的中间部分
            GUILayout.FlexibleSpace(); // 使中间部分可以自动伸缩

            if (GUILayout.Button("Refresh(重新加载)", EditorStyles.toolbarButton))
            {
                StartLoadPackages();
                LoadPackages();
            }

            GUILayout.FlexibleSpace(); // 使中间部分可以自动伸缩

            // 更新按钮
            if (GUILayout.Button("Update All Packages(更新所有包到最新)", EditorStyles.toolbarButton))
            {
                UpdateAllPackageHelper.UpdatePackages();
            }

            GUILayout.EndHorizontal();
        }
    }
}