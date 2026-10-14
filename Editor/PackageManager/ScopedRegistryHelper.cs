// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and applicable international regulations.
// 
//  使用本项目须严格遵守相应法律法规与开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 Apache License 2.0 许可证分发，
//  This project is licensed under the Apache License 2.0,
//  完整许可证文本请参见源代码根目录的 LICENSE 文件。
//  please refer to the LICENSE file at the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legal rights and interests of others, as prohibited by laws and regulations.
// 
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes or liabilities arising from secondary development based on this project
//  本组织与贡献者概不承担。
//  shall be borne by the developer; the organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository: https://gitee.com/GameFrameX
//  CNB  仓库：https://cnb.cool/GameFrameX
//  CNB Repository: https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System.IO;
using GameFrameX.LitJSON.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    /// <summary>
    /// GameFrameX Scoped Registry 配置工具，并在 manifest.json 缺失配置时静默自动补全。
    /// </summary>
    /// <remarks>
    /// GameFrameX scoped registry helper that silently auto-configures manifest.json when the registry is missing.
    /// </remarks>
    public static class ScopedRegistryHelper
    {
        private const string RegistryName = "GameFrameX";
        private const string RegistryUrl = "https://gameframex.upm.alianblank.uk";
        private const string RegistryScope = "com.gameframex.unity";
        private const string AutoConfigSessionKey = "GameFrameX.ScopedRegistry.AutoConfigAttempted";
        private const string OpenUpmRegistryName = "package.openupm.com";
        private const string OpenUpmRegistryUrl = "https://package.openupm.com";

        private static readonly string ManifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");
        private static readonly string ManifestBackupPath = ManifestPath + ".gameframex.bak";

        /// <summary>
        /// 编辑器加载后自动检查并补全 Scoped Registry 配置。
        /// </summary>
        /// <remarks>
        /// Automatically checks and completes the scoped registry configuration after editor load.
        /// </remarks>
        [InitializeOnLoadMethod]
        private static void AutoConfigureOnLoad()
        {
            // 批处理模式（CI）不静默改动工程文件
            // Do not silently modify project files in batch mode (CI)
            if (Application.isBatchMode)
            {
                return;
            }

            EditorApplication.delayCall += EnsureGameFrameXScopedRegistry;
        }

        /// <summary>
        /// 确保 manifest.json 已配置能解析 GameFrameX 包的 Scoped Registry；缺失时静默写入（写前备份）。
        /// </summary>
        /// <remarks>
        /// Ensures manifest.json has a scoped registry able to resolve GameFrameX packages; silently writes a backup first when missing.
        /// </remarks>
        public static void EnsureGameFrameXScopedRegistry()
        {
            if (SessionState.GetBool(AutoConfigSessionKey, false))
            {
                return;
            }

            SessionState.SetBool(AutoConfigSessionKey, true);

            JsonData manifest = ReadManifest();
            if (manifest == null)
            {
                return;
            }

            bool changed = false;

            if (!IsScopeCovered(manifest))
            {
                changed = TryMergeScopedRegistry(manifest, RegistryName, RegistryUrl, new[] { RegistryScope });
            }

            // OpenUPM registry 默认注册：scopes 留空，不替用户决定解析范围，后续由 openupm CLI / 手动安装追加
            // Registers the OpenUPM registry by default: scopes stay empty, resolution scope is decided later by openupm CLI or manual installs
            changed |= TryMergeScopedRegistry(manifest, OpenUpmRegistryName, OpenUpmRegistryUrl, new string[0]);

            if (changed)
            {
                WriteManifest(manifest);
                Debug.Log($"[GameFrameX] manifest.json 已自动补全 Scoped Registry 配置（原文件备份：{ManifestBackupPath}）。");
            }
        }

        /// <summary>
        /// 判断 manifest.json 中是否已有任意 registry 的 scopes 覆盖 GameFrameX 包 scope（含 OpenUPM 等第三方 registry）。
        /// </summary>
        /// <remarks>
        /// Returns whether any registry scopes in manifest.json already cover the GameFrameX package scope (including third-party registries such as OpenUPM).
        /// </remarks>
        public static bool HasGameFrameXScopedRegistry()
        {
            JsonData manifest = ReadManifest();

            return manifest != null && IsScopeCovered(manifest);
        }

        /// <summary>
        /// 按 name 或 url 匹配并合并 scoped registry 配置，返回是否发生写入。
        /// </summary>
        /// <remarks>
        /// Merges the scoped registry entry matched by name or url; returns whether the manifest was written.
        /// </remarks>
        public static bool SetScopedRegistry(string name, string url, string[] scopes)
        {
            JsonData manifest = ReadManifest();
            if (manifest == null)
            {
                return false;
            }

            if (!TryMergeScopedRegistry(manifest, name, url, scopes))
            {
                return false;
            }

            WriteManifest(manifest);

            return true;
        }

        /// <summary>
        /// 将 scoped registry 条目按 name 或 url 匹配合并进 manifest；返回是否有变更（不写盘）。
        /// </summary>
        /// <remarks>
        /// Merges the scoped registry entry matched by name or url into the manifest; returns whether anything changed (no disk write).
        /// </remarks>
        private static bool TryMergeScopedRegistry(JsonData manifest, string name, string url, string[] scopes)
        {
            if (!manifest.ContainsKey("scopedRegistries") || !manifest["scopedRegistries"].IsArray)
            {
                var registries = new JsonData();
                registries.SetJsonType(JsonType.Array);
                manifest["scopedRegistries"] = registries;
            }

            JsonData scopedRegistries = manifest["scopedRegistries"];
            JsonData existing = FindRegistry(scopedRegistries, name, url);
            bool changed;

            if (existing == null)
            {
                var entry = new JsonData();
                entry["name"] = name;
                entry["url"] = url;

                var scopesArray = new JsonData();
                scopesArray.SetJsonType(JsonType.Array);
                foreach (string scope in scopes)
                {
                    scopesArray.Add(scope);
                }

                entry["scopes"] = scopesArray;
                scopedRegistries.Add(entry);
                changed = true;
            }
            else
            {
                if (!existing.ContainsKey("scopes") || !existing["scopes"].IsArray)
                {
                    var scopesArray = new JsonData();
                    scopesArray.SetJsonType(JsonType.Array);
                    existing["scopes"] = scopesArray;
                }

                JsonData existingScopes = existing["scopes"];
                changed = false;
                foreach (string scope in scopes)
                {
                    if (!ContainsScope(existingScopes, scope))
                    {
                        existingScopes.Add(scope);
                        changed = true;
                    }
                }
            }

            return changed;
        }

        /// <summary>
        /// 备份并写回 manifest.json，随后刷新资源数据库触发包重解析。
        /// </summary>
        /// <remarks>
        /// Backs up and writes manifest.json, then refreshes the asset database to trigger package resolution.
        /// </remarks>
        private static void WriteManifest(JsonData manifest)
        {
            File.Copy(ManifestPath, ManifestBackupPath, true);
            File.WriteAllText(ManifestPath, JsonMapper.ToJson(manifest, true));
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 读取并解析 manifest.json；文件不存在或解析失败时返回 null。
        /// </summary>
        /// <remarks>
        /// Reads and parses manifest.json; returns null when the file is missing or malformed.
        /// </remarks>
        private static JsonData ReadManifest()
        {
            if (!File.Exists(ManifestPath))
            {
                return null;
            }

            try
            {
                return JsonMapper.ToObject(File.ReadAllText(ManifestPath));
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[GameFrameX] 解析 manifest.json 失败，跳过 Scoped Registry 自动配置：{e.Message}");

                return null;
            }
        }

        /// <summary>
        /// 判断任意 registry 的 scopes 是否按前缀语义覆盖 GameFrameX 包 scope。
        /// </summary>
        /// <remarks>
        /// Returns whether any registry scopes cover the GameFrameX package scope by prefix semantics.
        /// </remarks>
        private static bool IsScopeCovered(JsonData manifest)
        {
            if (!manifest.ContainsKey("scopedRegistries") || !manifest["scopedRegistries"].IsArray)
            {
                return false;
            }

            foreach (JsonData registry in manifest["scopedRegistries"])
            {
                if (!registry.IsObject || !registry.ContainsKey("scopes") || !registry["scopes"].IsArray)
                {
                    continue;
                }

                foreach (JsonData scope in registry["scopes"])
                {
                    string value = scope == null ? null : scope.ToString();
                    if (value == RegistryScope || (value != null && RegistryScope.StartsWith(value + ".")))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static JsonData FindRegistry(JsonData registries, string name, string url)
        {
            foreach (JsonData registry in registries)
            {
                if (!registry.IsObject)
                {
                    continue;
                }

                if ((registry.ContainsKey("name") && registry["name"].ToString() == name) ||
                    (registry.ContainsKey("url") && registry["url"].ToString() == url))
                {
                    return registry;
                }
            }

            return null;
        }

        private static bool ContainsScope(JsonData scopes, string scope)
        {
            foreach (JsonData existing in scopes)
            {
                if (existing != null && existing.ToString() == scope)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
