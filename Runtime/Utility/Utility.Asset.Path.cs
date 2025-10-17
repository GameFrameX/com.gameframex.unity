namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// AB实用函数集，主要是路径拼接
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static class Asset
        {
            /// <summary>
            /// 路径
            /// </summary>
            public static class Path
            {
                /// <summary>
                /// 打包资源根路径
                /// </summary>
                public const string BundlesPath = "Assets/Bundles";

                /// <summary>
                /// 打包资源文件夹名称
                /// </summary>
                public const string BundlesDirectoryName = "Bundles";

                /// <summary>
                /// 打包资源文件夹Scene名称
                /// </summary>
                public const string BundlesDirectorySceneName = "Scene";

                /// <summary>
                /// 打包资源文件夹Localization名称
                /// </summary>
                public const string BundlesDirectoryLocalizationName = "Localization";

                /// <summary>
                /// 打包资源文件夹Config名称
                /// </summary>
                public const string BundlesDirectoryConfigName = "Config";

                /// <summary>
                /// 打包资源文件夹AOTCode名称
                /// </summary>
                public const string BundlesDirectoryAOTCodeName = "AOTCode";

                /// <summary>
                /// 打包资源文件夹Code名称
                /// </summary>
                public const string BundlesDirectoryCodeName = "Code";

                /// <summary>
                /// 打包资源文件夹Sound名称
                /// </summary>
                public const string BundlesDirectorySoundName = "Sound";

                /// <summary>
                /// 打包资源文件夹Prefab名称
                /// </summary>
                public const string BundlesDirectoryPrefabName = "Prefabs";

                /// <summary>
                /// 打包资源文件夹Video名称
                /// </summary>
                public const string BundlesDirectoryVideoName = "Video";

                /// <summary>
                /// 打包资源文件夹Image名称
                /// </summary>
                public const string BundlesDirectoryImageName = "Image";

                /// <summary>
                /// 打包资源文件夹Fonts名称
                /// </summary>
                public const string BundlesDirectoryFontsName = "Fonts";

                /// <summary>
                /// 打包资源文件夹UI名称
                /// </summary>
                public const string BundlesDirectoryUIName = "UI";

                /// <summary>
                /// 打包资源文件夹Sprite名称
                /// </summary>
                public const string BundlesDirectorySpriteName = "Sprite";

                /// <summary>
                /// 打包资源文件夹Spine名称
                /// </summary>
                public const string BundlesDirectorySpineName = "Spine";

                /// <summary>
                /// 打包资源文件夹Shader名称
                /// </summary>
                public const string BundlesDirectoryShaderName = "Shader";

                /// <summary>
                /// 获取文件路径
                /// </summary>
                /// <param name="filePath">相对于Bundles的路径，不要以/开头</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetFilePath(string filePath)
                {
                    return $"{BundlesPath}/{filePath}";
                }

                /// <summary>
                /// 获取Spine文件路径
                /// </summary>
                /// <param name="filePath">相对于Bundles/Spine的路径，不要以/开头,需要携带扩展名</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetSpinePath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectorySpineName, filePath);
                }

                /// <summary>
                /// 获取着色器文件路径
                /// </summary>
                /// <param name="filePath">相对于Bundles/Shader的路径，不要以/开头,需要携带扩展名</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetShaderPath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectoryShaderName, filePath);
                }

                /// <summary>
                /// 获取字体文件路径
                /// </summary>
                /// <param name="filePath">相对于Bundles/Fonts的路径，不要以/开头,需要携带扩展名</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetFontPath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectoryFontsName, filePath);
                }

                /// <summary>
                /// 获取图片文件路径
                /// </summary>
                /// <param name="filePath">相对于Bundles/Image的路径，不要以/开头,需要携带扩展名</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetImagePath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectoryImageName, filePath);
                }

                /// <summary>
                /// 获取视频文件路径
                /// </summary>
                /// <param name="filePath">相对于Bundles/Video的路径，不要以/开头,需要携带扩展名</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetVideoPath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectoryVideoName, filePath);
                }

                /// <summary>
                /// 获取Sprite文件路径
                /// </summary>
                /// <param name="filePath">相对于Bundles/Sprite的路径，不要以/开头,需要携带扩展名</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetSpritePath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectorySpriteName, filePath);
                }

                /// <summary>
                /// 获取Prefab文件路径
                /// </summary>
                /// <param name="filePath">相对于Bundles/Prefabs的路径，不要以/开头,需要携带扩展名</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetPrefabPath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectoryPrefabName, filePath);
                }

                /// <summary>
                /// 获取根据类别文件夹名称和文件路径获得完整文件路径
                /// </summary>
                /// <param name="category">相对于Bundles的类别名称</param>
                /// <param name="filePath">相对于Bundles的路径，不要以/开头</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetCategoryFilePath(string category, string filePath)
                {
                    return $"{BundlesPath}/{category}/{filePath}";
                }

                /// <summary>
                /// 获取配置文件路径
                /// </summary>
                /// <param name="fileName">相对于Bundles/Config的路径，不要以/开头,需要携带扩展名</param>
                /// <param name="extension">文件扩展名称</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetConfigPath(string fileName, string extension = ".bytes")
                {
                    return GetCategoryFilePath(BundlesDirectoryConfigName, $"{fileName}{extension}");
                }

                /// <summary>
                /// 获取AOT元数据代码文件路径
                /// </summary>
                /// <param name="fileName">相对于Bundles/AOTCode的路径，不要以/开头,需要携带扩展名</param>
                /// <param name="extension">文件扩展名称</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetAOTCodePath(string fileName, string extension = ".bytes")
                {
                    return GetCategoryFilePath(BundlesDirectoryAOTCodeName, $"{fileName}{extension}");
                }

                /// <summary>
                /// 获取代码文件路径
                /// </summary>
                /// <param name="fileName">相对于Bundles/Code的路径，不要以/开头,需要携带扩展名</param>
                /// <param name="extension">文件扩展名称</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetCodePath(string fileName, string extension = ".bytes")
                {
                    return GetCategoryFilePath(BundlesDirectoryCodeName, $"{fileName}{extension}");
                }

                /// <summary>
                /// 获取UI文件路径
                /// </summary>
                /// <param name="uiPackageName">UI包名</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetUIPackagePath(string uiPackageName)
                {
                    return GetCategoryFilePath(BundlesDirectoryUIName, $"{uiPackageName}/{uiPackageName}");
                }

                /// <summary>
                /// 获取UI文件路径
                /// </summary>
                /// <param name="uiPath">UI路径</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetUIPath(string uiPath)
                {
                    return GetCategoryFilePath(BundlesDirectoryUIName, uiPath);
                }

                /// <summary>
                /// 获取声音文件路径
                /// </summary>
                /// <param name="pathName">路径包含名称</param>
                /// <param name="extension">扩展名称,默认为.mp3</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetSoundPath(string pathName, string extension = ".mp3")
                {
                    if (pathName.IndexOf('.') >= 0)
                    {
                        return GetCategoryFilePath(BundlesDirectorySoundName, pathName);
                    }

                    return GetCategoryFilePath(BundlesDirectorySoundName, $"{pathName}{extension}");
                }

                /// <summary>
                /// 获取场景文件路径
                /// </summary>
                /// <param name="pathName">路径包含名称</param>
                /// <param name="extension">扩展名,默认为.unity</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetScenePath(string pathName, string extension = ".unity")
                {
                    if (pathName.IndexOf('.') >= 0)
                    {
                        return GetCategoryFilePath(BundlesDirectorySceneName, pathName);
                    }

                    return GetCategoryFilePath(BundlesDirectorySceneName, $"{pathName}{extension}");
                }

                /// <summary>
                /// 获取本地化文件路径
                /// </summary>
                /// <param name="pathName">路径包含名称</param>
                /// <param name="extension">文件扩展名</param>
                /// <returns>返回拼接好的路径</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetLocalizationPath(string pathName, string extension = ".xml")
                {
                    if (pathName.IndexOf('.') >= 0)
                    {
                        return GetCategoryFilePath(BundlesDirectoryLocalizationName, pathName);
                    }

                    return GetCategoryFilePath(BundlesDirectoryLocalizationName, $"{pathName}{extension}");
                }
            }
        }
    }
}