using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;

namespace GameFrameX.Editor
{
    public static class HotFixAssemblyDefinitionHelper
    {
        sealed class AssemblyDefinitionInfoVersionDefines
        {
            public string name { get; set; }
            public string expression { get; set; }
            public string define { get; set; }
        }

        sealed class AssemblyDefinitionInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string rootNamespace { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> references { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> includePlatforms { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> excludePlatforms { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool allowUnsafeCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool overrideReferences { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> precompiledReferences { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool autoReferenced { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> defineConstraints { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<AssemblyDefinitionInfoVersionDefines> versionDefines { get; set; }

            /// <summary>
            /// 
            /// </summary>
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