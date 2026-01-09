using System;
using System.Collections.Generic;
using GameFrameX.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    public partial class PackageManagerWindow : EditorWindow
    {
        [MenuItem("GameFrameX/Package Manager(GameFrameX的包管理器)", false, 2000)]
        public static void ShowWindow()
        {
            var window = GetWindow<PackageManagerWindow>("GameFrameX");
            window.minSize = new UnityEngine.Vector2(800, 600);
            window.Show();
        }


        private Vector2 _leftScrollPosition;

        private Vector2 _rightVersionScrollPosition;

        // private List<PackageInfo> items = new List<PackageInfo>();
        private PackageManagerInfo selectedItem;


        private void OnEnable()
        {
            StartLoadPackages();
            LoadPackages();
            // 初始化示例数据
            // string json = FileHelper.ReadAllText("packagelist.json");
            // items = JsonConvert.DeserializeObject<List<PackageInfo>>(json);
        }

        private void OnGUI()
        {
            // 工具条
            OnGUIByToolBar();

            // 垂直布局
            GUILayout.BeginHorizontal();

            if (isLoadingPackageList)
            {
                GUILayout.BeginVertical(GUILayout.Height(position.height));
                {
                    GUILayout.FlexibleSpace(); // 添加灵活间距，居中内容
                    var loadingStyle = new GUIStyle(GUI.skin.label)
                    {
                        fontSize = 32, // 设置字体大小
                        alignment = TextAnchor.MiddleCenter, // 设置文本居中
                    };
                    GUILayout.Label("Loading...", loadingStyle);
                    // 结束居中的布局
                    GUILayout.FlexibleSpace(); // 再次添加灵活间距
                }
                GUILayout.EndVertical();
            }
            else
            {
                // 左侧滚动列表
                GUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));
                {
                    _leftScrollPosition = GUILayout.BeginScrollView(_leftScrollPosition, false, true);
                    {
                        foreach (var packageInfo in _packages)
                        {
                            if (GUILayout.Button($"{packageInfo.Package.displayName} {packageInfo.Package.version}"))
                            {
                                // 点击项目后显示版本和描述
                                if (selectedItem != packageInfo)
                                {
                                    selectedItem = packageInfo;
                                    FetchGitTags(selectedItem.GitUrl, selectedItem);
                                }
                            }
                        }
                    }
                    GUILayout.EndScrollView();
                }
                GUILayout.EndVertical();
            }

            // 右侧描述信息
            GUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));
            if (selectedItem != null)
            {
                GUILayout.Label("Name: ", EditorStyles.largeLabel);
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label(selectedItem.Package.displayName, EditorStyles.largeLabel);
                    if (selectedItem.Package.author.url.IsNotNullOrWhiteSpace())
                    {
                        if (GUILayout.Button("Open Package Page", EditorStyles.miniButton, GUILayout.Width(150)))
                        {
                            Application.OpenURL(selectedItem.Package.author.url);
                        }
                    }
                }

                GUILayout.EndHorizontal();
                // 分割线
                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Author: " + selectedItem.Package.author.name);
                    if (selectedItem.Package.author.url.IsNotNullOrWhiteSpace())
                    {
                        if (GUILayout.Button("Open Author Page", EditorStyles.miniButton, GUILayout.Width(150)))
                        {
                            Application.OpenURL(selectedItem.Package.author.url);
                        }
                    }
                }

                GUILayout.EndHorizontal();

                GUILayout.Label("Version: " + selectedItem.Package.version, EditorStyles.boldLabel);
                GUILayout.Label(selectedItem.Name, EditorStyles.largeLabel);

                #region Open URL

                GUILayout.BeginHorizontal();
                {
                    // if (selectedItem.DocumentationUrl.IsNotNullOrWhiteSpace())
                    // {
                    //     if (GUILayout.Button("View Documentation"))
                    //     {
                    //         Application.OpenURL(selectedItem.DocumentationUrl);
                    //     }
                    // }
                    //
                    // if (selectedItem.ChangelogUrl.IsNotNullOrWhiteSpace())
                    // {
                    //     if (GUILayout.Button("View Changelog"))
                    //     {
                    //         Application.OpenURL(selectedItem.ChangelogUrl);
                    //     }
                    // }
                    //
                    // if (selectedItem.LicensesUrl.IsNotNullOrWhiteSpace())
                    // {
                    //     if (GUILayout.Button("View Licenses"))
                    //     {
                    //         Application.OpenURL(selectedItem.LicensesUrl);
                    //     }
                    // }
                }

                GUILayout.EndHorizontal();

                #endregion

                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));

                GUILayout.Label(selectedItem.Package.description, EditorStyles.wordWrappedLabel);

                GUILayout.Label("Install From:", EditorStyles.wordWrappedLabel);
                GUILayout.Label(selectedItem.GitUrl, EditorStyles.wordWrappedLabel);

                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
                GUILayout.Label("Other Versions:", EditorStyles.boldLabel);

                if (isLoadingPackageTagList)
                {
                    GUILayout.BeginVertical(GUILayout.Height(position.height - 300));
                    {
                        GUILayout.FlexibleSpace(); // 添加灵活间距，居中内容
                        var loadingStyle = new GUIStyle(GUI.skin.label)
                        {
                            fontSize = 24, // 设置字体大小
                            alignment = TextAnchor.MiddleCenter, // 设置文本居中
                        };
                        GUILayout.Label("Loading...", loadingStyle);
                        // 结束居中的布局
                        GUILayout.FlexibleSpace(); // 再次添加灵活间距
                    }
                    GUILayout.EndVertical();
                }
                else
                {
                    if (selectedItem.Versions.Count > 0)
                    {
                        if (GUILayout.Button("该库缺失Tag或者版本标记? 我要反馈"))
                        {
                            Application.OpenURL("https://github.com/GameFrameX/GameFrameX.Unity/issues/new");
                        }

                        _rightVersionScrollPosition = GUILayout.BeginScrollView(_rightVersionScrollPosition, false, true);
                        {
                            foreach (var version in selectedItem.Versions)
                            {
                                GUILayout.BeginHorizontal();
                                GUILayout.Label(version);
                                if (version != selectedItem.Package.version)
                                {
                                    if (GUILayout.Button("Update/Downgrade", EditorStyles.miniButton, GUILayout.Width(200)))
                                    {
                                        // 点击项目后显示版本和描述
                                        UpdateToVersion(selectedItem.Name, version);
                                    }
                                }
                                else
                                {
                                    GUILayout.Label("Current Version", LabelGreenStyle, GUILayout.Width(200));
                                }

                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndScrollView();
                    }
                    else
                    {
                        GUILayout.Label("该库缺失Tag或者版本标记。请提交Issue反馈");
                        if (GUILayout.Button("打开反馈页面https://github.com/gameframex/gameframex.unity", GUILayout.Height(100)))
                        {
                            Application.OpenURL("https://github.com/GameFrameX/GameFrameX.Unity/issues/new");
                        }
                    }
                }
            }

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        void UpdateToVersion(string packageName, string version)
        {
            StartLoadPackages();

            PackagesManifest saveManifest = new PackagesManifest
            {
                Dependencies = new Dictionary<string, string>(PackagesManifest.Dependencies),
            };
            foreach (var dependency in PackagesManifest.Dependencies)
            {
                if (dependency.Key.Equals(packageName, StringComparison.OrdinalIgnoreCase))
                {
                    string packageUrl = dependency.Value;
                    if (packageUrl.Contains(".git"))
                    {
                        int indexTag = packageUrl.LastIndexOf("#", StringComparison.InvariantCulture);
                        string url;
                        if (indexTag >= 0)
                        {
                            // 已有版本号
                            url = packageUrl.Substring(0, indexTag);
                            url = $"{url}#{version}";
                        }
                        else
                        {
                            url = packageUrl.Split(new[] { ".git", }, StringSplitOptions.RemoveEmptyEntries)[0];
                            url = $"{url}.git#{version}";
                        }

                        saveManifest.Dependencies[dependency.Key] = url;
                        UpdateAllPackageHelper.UpdatePackages(packageName, url);
                        continue;
                    }

                    saveManifest.Dependencies[dependency.Key] = packageUrl;
                }
            }

            SavePackages(saveManifest);
            UpdateAllPackageHelper.StartUpdate();
            AssetDatabase.Refresh();
        }
    }
}