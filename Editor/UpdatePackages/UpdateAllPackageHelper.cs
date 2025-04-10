using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using Newtonsoft.Json.Linq;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 更新包帮助类
    /// </summary>
    public static class UpdateAllPackageHelper
    {
        private static AddRequest _addRequest;
        private static readonly Queue<(string name, string url)> PackagesToUpdate = new Queue<(string, string)>();
        private static int _allPackagesCount = 0;
        private static int _updatingPackagesIndex = 0;

        /// <summary>
        /// 更新包列表
        /// </summary>
        [MenuItem("GameFrameX/Update All Packages(更新所有git包)", false, 2000)]
        public static void UpdatePackages()
        {
            var result = EditorUtility.DisplayDialog("更新包提示", "是否更新所有包?\n 更新完成之后需要[重启、重启、重启]Unity", "是", "否");
            if (result)
            {
                var manifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");
                UpdatePackagesFromManifest(manifestPath);
            }
        }

        /// <summary>
        /// 更新指定包
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <param name="packageUrl">包地址</param>
        public static void UpdatePackages(string packageName, string packageUrl)
        {
            PackagesToUpdate.Enqueue((packageName, packageUrl));
        }

        private static void UpdatePackagesFromManifest(string manifestPath)
        {
            string jsonContent = File.ReadAllText(manifestPath);
            JObject manifest = JObject.Parse(jsonContent);
            JObject dependencies = (JObject)manifest["dependencies"];

            if (dependencies != null)
            {
                foreach (var package in dependencies)
                {
                    string packageName = package.Key;
                    string packageUrl = package.Value.ToString();
                    if (packageUrl.EndsWith(".git"))
                    {
                        UpdatePackages(packageName, packageUrl);
                    }
                }
            }

            StartUpdate();
        }

        /// <summary>
        /// 开始更新
        /// </summary>
        public static void StartUpdate()
        {
            _allPackagesCount = PackagesToUpdate.Count;
            _updatingPackagesIndex = 0;
            if (PackagesToUpdate.Count > 0)
            {
                UpdateNextPackage();
            }
            else
            {
                UnityEngine.Debug.Log("No packages to update.");
            }
        }

        private static void UpdateNextPackage()
        {
            if (PackagesToUpdate.Count > 0)
            {
                _updatingPackagesIndex++;
                var (packageName, packageUrl) = PackagesToUpdate.Dequeue();
                _addRequest = Client.Add(packageUrl);
                EditorApplication.update += UpdatingProgressHandler;
                var isCancelableProgressBar = EditorUtility.DisplayCancelableProgressBar("正在更新包", $"{_updatingPackagesIndex}/{_allPackagesCount} ({packageName})", (float)_updatingPackagesIndex / _allPackagesCount);
                if (isCancelableProgressBar)
                {
                    EditorUtility.DisplayProgressBar("正在取消更新", "请等待...", 0.5f);
                    PackagesToUpdate.Clear();
                    EditorUtility.ClearProgressBar();
                    EditorApplication.update -= UpdatingProgressHandler;
                    AssetDatabase.Refresh();
                }
            }
            else
            {
                EditorUtility.ClearProgressBar();
                UnityEngine.Debug.Log("All packages updated.");
                AssetDatabase.Refresh();
            }
        }

        private static void UpdatingProgressHandler()
        {
            if (_addRequest.IsCompleted)
            {
                if (_addRequest.Status == StatusCode.Success)
                {
                    UnityEngine.Debug.Log($"Updated package: {_addRequest.Result.packageId}");
                }
                else if (_addRequest.Status >= StatusCode.Failure)
                {
                    UnityEngine.Debug.LogError($"Failed to update package: {_addRequest.Error.message}");
                }

                EditorApplication.update -= UpdatingProgressHandler;
                UpdateNextPackage();
            }
        }
    }
}