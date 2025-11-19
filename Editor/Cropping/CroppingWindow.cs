// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFrameX.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 防裁剪代码生成窗口
    /// </summary>
    public sealed class CroppingWindow : EditorWindow
    {
        [MenuItem("GameFrameX/Cropping(防止裁剪代码生成)", false, 2001)]
        public static void ShowWindow()
        {
            var window = GetWindow<CroppingWindow>("Cropping");
            window.minSize = new UnityEngine.Vector2(800, 600);
            window.maxSize = window.minSize;
            window.maximized = false;
            window.Show();
        }

        private string[] _dropdownOptions = new string[] { "Empty" };
        private readonly string[] _ignoredTypes = new string[] { "UnityEngine".ToLower(), "UnityEditor".ToLower(), "Mono".ToLower(), "System".ToLower(), "dnlib".ToLower(), "Unity.Hotfix".ToLower(), "Unity.Baselib".ToLower(), ".Editor".ToLower(), "JetBrains".ToLower(), "NUnit".ToLower() };
        private int _selectedDropdownIndex = 0;
        private string _searchText = string.Empty;

        private void OnGUI()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                GUILayout.Label("查询类型:", EditorStyles.label, GUILayout.Width(100)); // 设置宽度以确保一致性
                _searchText = EditorGUILayout.TextField(_searchText, EditorStyles.toolbarTextField, GUILayout.Width(600));
                GUILayout.FlexibleSpace(); // 使中间部分可以自动伸缩
                if (GUILayout.Button("Search(查询)", EditorStyles.toolbarButton))
                {
                    if (string.IsNullOrWhiteSpace(_searchText))
                    {
                        ShowNotification(new GUIContent() { text = "搜索内容不能为空" });
                    }
                    else
                    {
                        string searchText = _searchText.ToLower();
                        var types = Utility.Assembly.GetTypes();
                        var result = new List<string>();
                        foreach (var type in types)
                        {
                            var fullName = type.FullName.ToLower();
                            var isFind = false;
                            foreach (var ignoredType in _ignoredTypes)
                            {
                                if (fullName.Contains(ignoredType))
                                {
                                    isFind = true;
                                    break;
                                }
                            }

                            if (isFind)
                            {
                                continue;
                            }

                            if (fullName.Contains(searchText))
                            {
                                result.Add(type.FullName);
                            }
                        }

                        _dropdownOptions = result.ToArray();
                    }
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(5); // 使中间部分可以自动伸缩
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("类型选择:", EditorStyles.label, GUILayout.Width(100));
                int newDropdownIndex = EditorGUILayout.Popup(_selectedDropdownIndex, _dropdownOptions, EditorStyles.toolbarDropDown, GUILayout.Width(600));
                if (newDropdownIndex != _selectedDropdownIndex)
                {
                    _selectedDropdownIndex = newDropdownIndex;
                }

                GUILayout.FlexibleSpace(); // 使中间部分可以自动伸缩
                if (GUILayout.Button("Generate(生成)", EditorStyles.toolbarButton))
                {
                    Generate(_dropdownOptions[_selectedDropdownIndex]);
                }
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.TextArea(_generatedText, GUILayout.ExpandHeight(true));
        }

        private string _generatedText = string.Empty;

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="targetTypeName"></param>
        private void Generate(string targetTypeName)
        {
            _generatedText = string.Empty;
            var targetType = Utility.Assembly.GetType(targetTypeName);
            if (targetType != null)
            {
                var types = targetType.Assembly.GetTypes();
                types = types.OrderBy(m => m.FullName).ToArray();
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var type in types)
                {
                    if (type.IsNestedPrivate)
                    {
                        continue;
                    }

                    if (type.FullName.Contains("PrivateImplementationDetails"))
                    {
                        continue;
                    }

                    stringBuilder.AppendLine(" _ = typeof(" + type.FullName.Replace("+", ".").Replace("`1", "<>").Replace("`2", "<,>") + ");");
                }

                _generatedText = stringBuilder.ToString();
                ShowNotification(new GUIContent() { text = "请将代码复制到CroppingHelper.cs 中" });
            }
        }
    }
}