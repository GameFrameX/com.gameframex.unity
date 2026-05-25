using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace GameFrameX.Editor
{
    public static class ScopedRegistryHelper
    {
        private static readonly string ManifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");

        [MenuItem("GameFrameX/Package Manager/Set OpenUPM Scoped Registry", false, 2002)]
        public static void SetOpenUpmScopedRegistry()
        {
            SetScopedRegistry(
                "package.openupm.com",
                "https://package.openupm.com",
                new[] { "io.sentry" }
            );
        }

        [MenuItem("GameFrameX/Package Manager/Set GameFrameX Scoped Registry", false, 2003)]
        public static void SetGameFrameXScopedRegistry()
        {
            SetScopedRegistry(
                "GameFrameX",
                "https://gameframex.upm.alianblank.uk",
                new[] { "com.gameframex" }
            );
        }

        private static void SetScopedRegistry(string name, string url, string[] scopes)
        {
            string json = File.ReadAllText(ManifestPath);
            JObject manifest = JObject.Parse(json);

            JArray scopedRegistries = manifest["scopedRegistries"] as JArray;
            if (scopedRegistries == null)
            {
                scopedRegistries = new JArray();
                manifest["scopedRegistries"] = scopedRegistries;
            }

            JToken existing = scopedRegistries.FirstOrDefault(r =>
                r["name"]?.ToString() == name || r["url"]?.ToString() == url);

            if (existing != null)
            {
                JArray existingScopes = existing["scopes"] as JArray;
                if (existingScopes == null)
                {
                    existingScopes = new JArray();
                    existing["scopes"] = existingScopes;
                }

                bool changed = false;
                foreach (string scope in scopes)
                {
                    if (!existingScopes.Any(s => s.ToString() == scope))
                    {
                        existingScopes.Add(scope);
                        changed = true;
                    }
                }

                if (changed)
                {
                    File.WriteAllText(ManifestPath, manifest.ToString(Formatting.Indented));
                    Debug.Log($"[ScopedRegistry] Updated scopes for '{name}'.");
                    EditorUtility.DisplayDialog("Scoped Registry", $"Updated scopes for '{name}'.", "OK");
                }
                else
                {
                    Debug.Log($"[ScopedRegistry] '{name}' is already configured with all required scopes.");
                    EditorUtility.DisplayDialog("Scoped Registry", $"'{name}' is already configured.", "OK");
                }
            }
            else
            {
                JObject newRegistry = new JObject();
                newRegistry["name"] = name;
                newRegistry["url"] = url;
                newRegistry["scopes"] = new JArray(scopes);
                scopedRegistries.Add(newRegistry);

                File.WriteAllText(ManifestPath, manifest.ToString(Formatting.Indented));
                Debug.Log($"[ScopedRegistry] Added '{name}' scoped registry.");
                EditorUtility.DisplayDialog("Scoped Registry", $"Added '{name}' scoped registry.", "OK");
            }

            AssetDatabase.Refresh();
        }

        public static bool HasGameFrameXScopedRegistry()
        {
            if (!File.Exists(ManifestPath))
            {
                return false;
            }

            string json = File.ReadAllText(ManifestPath);
            JObject manifest = JObject.Parse(json);
            JArray scopedRegistries = manifest["scopedRegistries"] as JArray;
            if (scopedRegistries == null)
            {
                return false;
            }

            return scopedRegistries.Any(r =>
                r["url"]?.ToString() == "https://gameframex.upm.alianblank.uk");
        }

        public static void MigrateToRegistry()
        {
            bool confirmed = EditorUtility.DisplayDialog(
                "迁移提示",
                "将把所有 com.gameframex.* 的 git 依赖迁移为 Scoped Registry 版本。\n\n" +
                "迁移前请确保已提交当前更改。\n迁移完成后需要重启 Unity。\n\n是否继续？",
                "确认迁移",
                "取消");
            if (!confirmed)
            {
                return;
            }

            SetScopedRegistry("GameFrameX", "https://gameframex.upm.alianblank.uk", new[] { "com.gameframex" });

            string json = File.ReadAllText(ManifestPath);
            JObject manifest = JObject.Parse(json);
            JObject dependencies = manifest["dependencies"] as JObject;
            if (dependencies == null)
            {
                return;
            }

            _packagesToMigrate.Clear();
            foreach (var dep in dependencies)
            {
                if (dep.Key.StartsWith("com.gameframex.") && dep.Value.ToString().Contains(".git"))
                {
                    _packagesToMigrate.Enqueue(dep.Key);
                }
            }

            if (_packagesToMigrate.Count == 0)
            {
                EditorUtility.DisplayDialog("迁移", "没有发现需要迁移的 git 包。", "OK");
                return;
            }

            _migrateTotal = _packagesToMigrate.Count;
            _migrateIndex = 0;
            MigrateNextPackage();
        }

        private static readonly Queue<string> _packagesToMigrate = new Queue<string>();
        private static AddRequest _migrateAddRequest;
        private static int _migrateTotal;
        private static int _migrateIndex;

        private static void MigrateNextPackage()
        {
            if (_packagesToMigrate.Count > 0)
            {
                _migrateIndex++;
                string packageName = _packagesToMigrate.Dequeue();
                _migrateAddRequest = Client.Add(packageName);
                EditorApplication.update += MigrateProgressHandler;
                bool canceled = EditorUtility.DisplayCancelableProgressBar(
                    "正在迁移包",
                    string.Format("{0}/{1} ({2})", _migrateIndex, _migrateTotal, packageName),
                    (float)_migrateIndex / _migrateTotal);
                if (canceled)
                {
                    EditorUtility.DisplayProgressBar("正在取消迁移", "请等待...", 0.5f);
                    _packagesToMigrate.Clear();
                    EditorUtility.ClearProgressBar();
                    EditorApplication.update -= MigrateProgressHandler;
                    AssetDatabase.Refresh();
                }
            }
            else
            {
                EditorUtility.ClearProgressBar();
                Debug.Log("[ScopedRegistry] Migration completed.");
                EditorUtility.DisplayDialog("迁移完成", "所有 GameFrameX 包已迁移到 Registry。\n请重启 Unity 以完成迁移。", "OK");
                AssetDatabase.Refresh();
            }
        }

        private static void MigrateProgressHandler()
        {
            if (_migrateAddRequest.IsCompleted)
            {
                if (_migrateAddRequest.Status == StatusCode.Success)
                {
                    Debug.Log(string.Format("[ScopedRegistry] Migrated: {0}", _migrateAddRequest.Result.packageId));
                }
                else if (_migrateAddRequest.Status >= StatusCode.Failure)
                {
                    Debug.LogError(string.Format("[ScopedRegistry] Failed to migrate: {0}", _migrateAddRequest.Error.message));
                }

                EditorApplication.update -= MigrateProgressHandler;
                MigrateNextPackage();
            }
        }
    }
}
