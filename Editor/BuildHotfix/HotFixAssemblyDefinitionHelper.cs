using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 热更新程序集定义帮助类
    /// </summary>
    /// <remarks>
    /// Helper class for hot update assembly definitions.
    /// </remarks>
    public static class HotFixAssemblyDefinitionHelper
    {
        sealed class AssemblyDefinitionInfoVersionDefines
        {
            /// <summary>
            /// 版本定义名称
            /// </summary>
            /// <remarks>
            /// The name of the version define.
            /// </remarks>
            public string name { get; set; }
            /// <summary>
            /// 版本定义表达式
            /// </summary>
            /// <remarks>
            /// The expression of the version define.
            /// </remarks>
            public string expression { get; set; }
            /// <summary>
            /// 版本定义宏
            /// </summary>
            /// <remarks>
            /// The define symbol of the version define.
            /// </remarks>
            public string define { get; set; }
        }

        sealed class AssemblyDefinitionInfo
        {
            /// <summary>
            /// 程序集名称
            /// </summary>
            /// <remarks>
            /// The name of the assembly.
            /// </remarks>
            public string name { get; set; }

            /// <summary>
            /// 根命名空间
            /// </summary>
            /// <remarks>
            /// The root namespace of the assembly.
            /// </remarks>
            public string rootNamespace { get; set; }

            /// <summary>
            /// 引用的程序集列表
            /// </summary>
            /// <remarks>
            /// The list of referenced assemblies.
            /// </remarks>
            public List<string> references { get; set; }

            /// <summary>
            /// 包含的平台列表
            /// </summary>
            /// <remarks>
            /// The list of included platforms.
            /// </remarks>
            public List<string> includePlatforms { get; set; }

            /// <summary>
            /// 排除的平台列表
            /// </summary>
            /// <remarks>
            /// The list of excluded platforms.
            /// </remarks>
            public List<string> excludePlatforms { get; set; }

            /// <summary>
            /// 是否允许不安全代码
            /// </summary>
            /// <remarks>
            /// Whether unsafe code is allowed.
            /// </remarks>
            public bool allowUnsafeCode { get; set; }

            /// <summary>
            /// 是否重写引用
            /// </summary>
            /// <remarks>
            /// Whether references are overridden.
            /// </remarks>
            public bool overrideReferences { get; set; }

            /// <summary>
            /// 预编译引用列表
            /// </summary>
            /// <remarks>
            /// The list of precompiled references.
            /// </remarks>
            public List<string> precompiledReferences { get; set; }

            /// <summary>
            /// 是否自动引用
            /// </summary>
            /// <remarks>
            /// Whether the assembly is auto-referenced.
            /// </remarks>
            public bool autoReferenced { get; set; }

            /// <summary>
            /// 宏定义约束列表
            /// </summary>
            /// <remarks>
            /// The list of define constraints.
            /// </remarks>
            public List<string> defineConstraints { get; set; }

            /// <summary>
            /// 版本定义列表
            /// </summary>
            /// <remarks>
            /// The list of version defines.
            /// </remarks>
            public List<AssemblyDefinitionInfoVersionDefines> versionDefines { get; set; }

            /// <summary>
            /// 是否不引用引擎程序集
            /// </summary>
            /// <remarks>
            /// Whether engine references are disabled.
            /// </remarks>
            public bool noEngineReferences { get; set; }
        }


        internal static void AddEditor(string path)
        {
            AssemblyDefinitionAsset assemblyDefinitionAsset = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(path);
            AssemblyDefinitionInfo info = LitJSON.Runtime.JsonMapper.ToObject<AssemblyDefinitionInfo>(assemblyDefinitionAsset.text);
            bool isEditor = info.excludePlatforms.Any(m => m == "Editor");
            if (!isEditor)
            {
                info.excludePlatforms.Add("Editor");
                System.IO.File.WriteAllText(path, LitJSON.Runtime.JsonMapper.ToJson(info, true));
                AssetDatabase.ImportAsset(path);
            }
        }


        internal static void RemoveEditor(string path)
        {
            AssemblyDefinitionAsset assemblyDefinitionAsset = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(path);
            AssemblyDefinitionInfo info = LitJSON.Runtime.JsonMapper.ToObject<AssemblyDefinitionInfo>(assemblyDefinitionAsset.text);
            bool isEditor = info.excludePlatforms.Any(m => m == "Editor");
            if (isEditor)
            {
                info.excludePlatforms.Remove("Editor");
            }

            System.IO.File.WriteAllText(path, LitJSON.Runtime.JsonMapper.ToJson(info, true));
            AssetDatabase.ImportAsset(path);
        }
    }
}