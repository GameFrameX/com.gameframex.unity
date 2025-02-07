using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using Debug = UnityEngine.Debug;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace GameFrameX.Editor
{
    public partial class PackageManagerWindow
    {
        /// <summary>
        /// 包信息
        /// </summary>
        public sealed class PackageManagerInfo
        {
            /// <summary>
            /// 包名称
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// 包 git 地址
            /// </summary>
            public string GitUrl { get; }

            /// <summary>
            /// 包信息
            /// </summary>
            public PackageInfo Package { get; }

            /// <summary>
            /// 版本列表，Tags
            /// </summary>
            public List<string> Versions { get; }

            public PackageManagerInfo(string name, PackageInfo package, string gitUrl)
            {
                Name = name;
                Package = package;
                GitUrl = gitUrl;
                Versions = new List<string>();
            }
        }

        private List<PackageManagerInfo> _packages;
        private ListRequest _listRequest;

        private PackagesManifest PackagesManifest { get; set; }

        private void OnPackageListUpdate()
        {
            if (_listRequest.IsCompleted)
            {
                if (_listRequest.Status == StatusCode.Success)
                {
                    _packages.Clear();
                    foreach (var package in _listRequest.Result)
                    {
                        if (package.name.StartsWith("com.unity."))
                        {
                            // 过滤 Unity 内置包
                            continue;
                        }

                        if (package.source == PackageSource.Git)
                        {
                            if (PackagesManifest.Dependencies.TryGetValue(package.name, out var gitUrl))
                            {
                                var item = new PackageManagerInfo(package.name, package, gitUrl);
                                _packages.Add(item);
                            }
                        }
                    }

                    _packages.Sort((x, y) => x.Name.CompareTo(y.Name));
                }
                else
                {
                    Debug.LogError($"Error retrieving packages: {_listRequest.Error.message}");
                }

                isLoadingPackageList = false;
                EditorApplication.update -= OnPackageListUpdate;
            }
        }

        /// <summary>
        /// 是否正在加载包标签列表
        /// </summary>
        public bool isLoadingPackageTagList = false;

        /// <summary>
        /// 获取 git 标签
        /// </summary>
        /// <param name="gitUrl">从 git 获取的地址</param>
        /// <param name="package">包信息对象</param>
        private void FetchGitTags(string gitUrl, PackageManagerInfo package)
        {
            if (package.Versions.Count > 0)
            {
                return;
            }

            isLoadingPackageTagList = true;
            Task.Run(() =>
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = $"ls-remote --tags {gitUrl}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (Process process = Process.Start(startInfo))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        string[] tags = result.Split('\n');

                        foreach (var tag in tags)
                        {
                            if (!string.IsNullOrEmpty(tag) && !tag.Contains("^{"))
                            {
                                // 提取标签名
                                var tagName = tag.Split('\t')[1].Replace("refs/tags/", "").Trim();
                                package.Versions.Add(tagName);
                            }
                        }

                        package.Versions.Sort((x, y) => -x.CompareTo(y));
                    }
                }

                isLoadingPackageTagList = false;
            });
        }

        /// <summary>
        /// 加载已安装的包
        /// </summary>
        void StartLoadPackages()
        {
            _packages = new List<PackageManagerInfo>();
            var manifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");
            string jsonContent = File.ReadAllText(manifestPath);
            PackagesManifest = JsonConvert.DeserializeObject<PackagesManifest>(jsonContent);
        }

        /// <summary>
        /// 是否正在加载包列表
        /// </summary>
        private bool isLoadingPackageList = false;

        /// <summary>
        /// 加载所有包信息
        /// </summary>
        void LoadPackages()
        {
            isLoadingPackageList = true;
            _listRequest = Client.List();
            EditorApplication.update += OnPackageListUpdate;
        }

        /// <summary>
        /// 保存包信息
        /// </summary>
        /// <param name="manifest"></param>
        void SavePackages(PackagesManifest manifest)
        {
            var manifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");
            File.WriteAllText(manifestPath, JsonConvert.SerializeObject(manifest, Formatting.Indented));
        }
    }
}