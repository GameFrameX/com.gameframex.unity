// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

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

                            if (fullName.Contains(_searchText))
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