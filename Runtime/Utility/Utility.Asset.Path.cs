namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// Asset Bundle 实用函数集，主要是路径拼接。
        /// </summary>
        /// <remarks>
        /// Asset Bundle utility functions, mainly for path concatenation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static class Asset
        {
            /// <summary>
            /// 路径常量和方法。
            /// </summary>
            /// <remarks>
            /// Path constants and methods.
            /// </remarks>
            public static class Path
            {
                /// <summary>
                /// 打包资源根路径
                /// </summary>
                /// <remarks>
                /// Root path of the bundled assets.
                /// </remarks>
                public const string BundlesPath = "Assets/Bundles";

                /// <summary>
                /// 打包资源文件夹名称
                /// </summary>
                /// <remarks>
                /// Folder name for the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryName = "Bundles";

                /// <summary>
                /// 打包资源文件夹Scene名称
                /// </summary>
                /// <remarks>
                /// Name of the Scene folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectorySceneName = "Scene";

                /// <summary>
                /// 打包资源文件夹Localization名称
                /// </summary>
                /// <remarks>
                /// Name of the Localization folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryLocalizationName = "Localization";

                /// <summary>
                /// 打包资源文件夹Config名称
                /// </summary>
                /// <remarks>
                /// Name of the Config folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryConfigName = "Config";

                /// <summary>
                /// 打包资源文件夹AOTCode名称
                /// </summary>
                /// <remarks>
                /// Name of the AOTCode folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryAOTCodeName = "AOTCode";

                /// <summary>
                /// 打包资源文件夹Code名称
                /// </summary>
                /// <remarks>
                /// Name of the Code folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryCodeName = "Code";

                /// <summary>
                /// 打包资源文件夹Sound名称
                /// </summary>
                /// <remarks>
                /// Name of the Sound folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectorySoundName = "Sound";

                /// <summary>
                /// 打包资源文件夹Prefab名称
                /// </summary>
                /// <remarks>
                /// Name of the Prefabs folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryPrefabName = "Prefabs";

                /// <summary>
                /// 打包资源文件夹Video名称
                /// </summary>
                /// <remarks>
                /// Name of the Video folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryVideoName = "Video";

                /// <summary>
                /// 打包资源文件夹Image名称
                /// </summary>
                /// <remarks>
                /// Name of the Image folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryImageName = "Image";

                /// <summary>
                /// 打包资源文件夹Fonts名称
                /// </summary>
                /// <remarks>
                /// Name of the Fonts folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryFontsName = "Fonts";

                /// <summary>
                /// 打包资源文件夹UI名称
                /// </summary>
                /// <remarks>
                /// Name of the UI folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryUIName = "UI";

                /// <summary>
                /// 打包资源文件夹Sprite名称
                /// </summary>
                /// <remarks>
                /// Name of the Sprite folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectorySpriteName = "Sprite";

                /// <summary>
                /// 打包资源文件夹Spine名称
                /// </summary>
                /// <remarks>
                /// Name of the Spine folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectorySpineName = "Spine";

                /// <summary>
                /// 打包资源文件夹Shader名称
                /// </summary>
                /// <remarks>
                /// Name of the Shader folder under the bundled assets.
                /// </remarks>
                public const string BundlesDirectoryShaderName = "Shader";

                /// <summary>
                /// 获取文件路径
                /// </summary>
                /// <remarks>
                /// Gets the file path.
                /// </remarks>
                /// <param name="filePath">相对于Bundles的路径，不要以/开头 / Path relative to Bundles; must not start with "/"</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetFilePath(string filePath)
                {
                    return $"{BundlesPath}/{filePath}";
                }

                /// <summary>
                /// 获取Spine文件路径
                /// </summary>
                /// <remarks>
                /// Gets the Spine file path.
                /// </remarks>
                /// <param name="filePath">相对于Bundles/Spine的路径，不要以/开头,需要携带扩展名 / Path relative to Bundles/Spine; must not start with "/" and must include the extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetSpinePath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectorySpineName, filePath);
                }

                /// <summary>
                /// 获取着色器文件路径
                /// </summary>
                /// <remarks>
                /// Gets the shader file path.
                /// </remarks>
                /// <param name="filePath">相对于Bundles/Shader的路径，不要以/开头,需要携带扩展名 / Path relative to Bundles/Shader; must not start with "/" and must include the extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetShaderPath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectoryShaderName, filePath);
                }

                /// <summary>
                /// 获取字体文件路径
                /// </summary>
                /// <remarks>
                /// Gets the font file path.
                /// </remarks>
                /// <param name="filePath">相对于Bundles/Fonts的路径，不要以/开头,需要携带扩展名 / Path relative to Bundles/Fonts; must not start with "/" and must include the extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetFontPath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectoryFontsName, filePath);
                }

                /// <summary>
                /// 获取图片文件路径
                /// </summary>
                /// <remarks>
                /// Gets the image file path.
                /// </remarks>
                /// <param name="filePath">相对于Bundles/Image的路径，不要以/开头,需要携带扩展名 / Path relative to Bundles/Image; must not start with "/" and must include the extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetImagePath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectoryImageName, filePath);
                }

                /// <summary>
                /// 获取视频文件路径
                /// </summary>
                /// <remarks>
                /// Gets the video file path.
                /// </remarks>
                /// <param name="filePath">相对于Bundles/Video的路径，不要以/开头,需要携带扩展名 / Path relative to Bundles/Video; must not start with "/" and must include the extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetVideoPath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectoryVideoName, filePath);
                }

                /// <summary>
                /// 获取Sprite文件路径
                /// </summary>
                /// <remarks>
                /// Gets the sprite file path.
                /// </remarks>
                /// <param name="filePath">相对于Bundles/Sprite的路径，不要以/开头,需要携带扩展名 / Path relative to Bundles/Sprite; must not start with "/" and must include the extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetSpritePath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectorySpriteName, filePath);
                }

                /// <summary>
                /// 获取Prefab文件路径
                /// </summary>
                /// <remarks>
                /// Gets the prefab file path.
                /// </remarks>
                /// <param name="filePath">相对于Bundles/Prefabs的路径，不要以/开头,需要携带扩展名 / Path relative to Bundles/Prefabs; must not start with "/" and must include the extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetPrefabPath(string filePath)
                {
                    return GetCategoryFilePath(BundlesDirectoryPrefabName, filePath);
                }

                /// <summary>
                /// 获取根据类别文件夹名称和文件路径获得完整文件路径
                /// </summary>
                /// <remarks>
                /// Gets the full file path from a category folder name and a file path.
                /// </remarks>
                /// <param name="category">相对于Bundles的类别名称 / Category name under Bundles</param>
                /// <param name="filePath">相对于Bundles的路径，不要以/开头 / Path relative to Bundles; must not start with "/"</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetCategoryFilePath(string category, string filePath)
                {
                    return $"{BundlesPath}/{category}/{filePath}";
                }

                /// <summary>
                /// 获取配置文件路径
                /// </summary>
                /// <remarks>
                /// Gets the configuration file path.
                /// </remarks>
                /// <param name="fileName">相对于Bundles/Config的路径，不要以/开头,需要携带扩展名 / Path relative to Bundles/Config; must not start with "/" and must include the extension</param>
                /// <param name="extension">文件扩展名称 / The file extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetConfigPath(string fileName, string extension = ".bytes")
                {
                    return GetCategoryFilePath(BundlesDirectoryConfigName, $"{fileName}{extension}");
                }

                /// <summary>
                /// 获取AOT元数据代码文件路径
                /// </summary>
                /// <remarks>
                /// Gets the AOT metadata code file path.
                /// </remarks>
                /// <param name="fileName">相对于Bundles/AOTCode的路径，不要以/开头,需要携带扩展名 / Path relative to Bundles/AOTCode; must not start with "/" and must include the extension</param>
                /// <param name="extension">文件扩展名称 / The file extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetAOTCodePath(string fileName, string extension = ".bytes")
                {
                    return GetCategoryFilePath(BundlesDirectoryAOTCodeName, $"{fileName}{extension}");
                }

                /// <summary>
                /// 获取代码文件路径
                /// </summary>
                /// <remarks>
                /// Gets the code file path.
                /// </remarks>
                /// <param name="fileName">相对于Bundles/Code的路径，不要以/开头,需要携带扩展名 / Path relative to Bundles/Code; must not start with "/" and must include the extension</param>
                /// <param name="extension">文件扩展名称 / The file extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetCodePath(string fileName, string extension = ".bytes")
                {
                    return GetCategoryFilePath(BundlesDirectoryCodeName, $"{fileName}{extension}");
                }

                /// <summary>
                /// 获取UI文件路径
                /// </summary>
                /// <remarks>
                /// Gets the UI package path.
                /// </remarks>
                /// <param name="uiPackageName">UI包名 / The UI package name</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetUIPackagePath(string uiPackageName)
                {
                    return GetCategoryFilePath(BundlesDirectoryUIName, $"{uiPackageName}/{uiPackageName}");
                }

                /// <summary>
                /// 获取UI文件路径
                /// </summary>
                /// <remarks>
                /// Gets the UI file path.
                /// </remarks>
                /// <param name="uiPath">UI路径 / The UI path</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
                [UnityEngine.Scripting.Preserve]
                public static string GetUIPath(string uiPath)
                {
                    return GetCategoryFilePath(BundlesDirectoryUIName, uiPath);
                }

                /// <summary>
                /// 获取声音文件路径
                /// </summary>
                /// <remarks>
                /// Gets the sound file path.
                /// </remarks>
                /// <param name="pathName">路径包含名称 / The path including the name</param>
                /// <param name="extension">扩展名称,默认为.mp3 / The extension; defaults to ".mp3"</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
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
                /// <remarks>
                /// Gets the scene file path.
                /// </remarks>
                /// <param name="pathName">路径包含名称 / The path including the name</param>
                /// <param name="extension">扩展名,默认为.unity / The extension; defaults to ".unity"</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
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
                /// <remarks>
                /// Gets the localization file path.
                /// </remarks>
                /// <param name="pathName">路径包含名称 / The path including the name</param>
                /// <param name="extension">文件扩展名 / The file extension</param>
                /// <returns>返回拼接好的路径 / The concatenated path</returns>
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