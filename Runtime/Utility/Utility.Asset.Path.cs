namespace GameFrameX
{
    public static partial class Utility
    {
        /// <summary>
        /// AB实用函数集，主要是路径拼接
        /// </summary>
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
                /// 获取配置文件路径
                /// </summary>
                /// <param name="fileName"></param>
                /// <param name="extension"></param>
                /// <returns></returns>
                public static string GetConfigPath(string fileName, string extension = ".bytes")
                {
                    return $"{BundlesPath}/Config/{fileName}{extension}";
                }

                /// <summary>
                /// 获取AOT元数据代码文件路径
                /// </summary>
                /// <param name="fileName"></param>
                /// <param name="extension"></param>
                /// <returns></returns>
                public static string GetAOTCodePath(string fileName, string extension = ".bytes")
                {
                    return $"{BundlesPath}/AOTCode/{fileName}{extension}";
                }

                /// <summary>
                /// 获取代码文件路径
                /// </summary>
                /// <param name="fileName"></param>
                /// <param name="extension"></param>
                /// <returns></returns>
                public static string GetCodePath(string fileName, string extension = ".bytes")
                {
                    return $"{BundlesPath}/Code/{fileName}{extension}";
                }

                /// <summary>
                /// 获取UI文件路径
                /// </summary>
                /// <param name="uiPackageName">UI包名</param>
                /// <returns></returns>
                public static string GetUIPackagePath(string uiPackageName)
                {
                    return $"{BundlesPath}/UI/{uiPackageName}/{uiPackageName}";
                }

                /// <summary>
                /// 获取声音文件路径
                /// </summary>
                /// <param name="pathName">路径包含名称</param>
                /// <param name="extension">扩展名称,默认为.mp3</param>
                /// <returns></returns>
                public static string GetSoundPath(string pathName, string extension = ".mp3")
                {
                    if (pathName.IndexOf('.') >= 0)
                    {
                        return $"{BundlesPath}/Sound/{pathName}";
                    }

                    return $"{BundlesPath}/Sound/{pathName}{extension}";
                }

                /// <summary>
                /// 获取场景文件路径
                /// </summary>
                /// <param name="pathName">路径包含名称</param>
                /// <param name="extension">扩展名,默认为.unity</param>
                /// <returns></returns>
                public static string GetScenePath(string pathName, string extension = ".unity")
                {
                    if (pathName.IndexOf('.') >= 0)
                    {
                        return $"{BundlesPath}/Scene/{pathName}";
                    }

                    return $"{BundlesPath}/Scene/{pathName}{extension}";
                }

                /// <summary>
                /// 获取本地化文件路径
                /// </summary>
                /// <param name="pathName">路径包含名称</param>
                /// <param name="extension"></param>
                /// <returns></returns>
                public static string GetLocalizationPath(string pathName, string extension = ".xml")
                {
                    if (pathName.IndexOf('.') >= 0)
                    {
                        return $"{BundlesPath}/Localization/{pathName}";
                    }

                    return $"{BundlesPath}/Localization/{pathName}{extension}";
                }
            }
        }
    }
}