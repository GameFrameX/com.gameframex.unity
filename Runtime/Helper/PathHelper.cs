using System;
using System.Text;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 路径帮助类。
    /// </summary>
    /// <remarks>
    /// Path helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class PathHelper
    {
        /// <summary>
        /// 应用程序外部资源路径存放路径（热更新资源路径）。
        /// </summary>
        /// <remarks>
        /// Application external resource path (hot update resource path).
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static string AppHotfixResPath
        {
            get
            {
                string game = Application.productName;
                string path = $"{Application.persistentDataPath}/{game}/";
                return path;
            }
        }

        /// <summary>
        /// 应用程序内部资源路径存放路径
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static string AppResPath
        {
            get { return NormalizePath(Application.streamingAssetsPath); }
        }

        /// <summary>
        /// 应用程序内部资源路径存放路径(www/webrequest专用)
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static string AppResPath4Web
        {
            get
            {
#if UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_EDITOR
                return $"file://{Application.streamingAssetsPath}";
#else
                return NormalizePath(Application.streamingAssetsPath);
#endif
            }
        }

        /// <summary>
        /// 获取平台名称
        /// </summary>
        /// <remarks>
        /// 此属性已废弃，请使用 ApplicationHelper.PlatformName 替代
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [Obsolete("此方法已废弃，请使用 ApplicationHelper.PlatformName 替代")]
        public static string GetPlatformName
        {
            get
            {
#if UNITY_ANDROID
                return $"Android";
#elif UNITY_STANDALONE_OSX
                return $"MacOs";
#elif UNITY_IOS || UNITY_IPHONE
                return $"iOS";
#elif UNITY_WEBGL
                return $"WebGL";
#elif UNITY_STANDALONE_WIN
                return $"Windows";
#else
                return string.Empty;
#endif
            }
        }

        /// <summary>
        /// 规范化路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static string NormalizePath(string path)
        {
            if (path == null)
            {
                return null;
            }

            return path.Replace('\\', '/');
        }

        /// <summary>
        /// 获取远程格式的路径（带有 file:// 或 http:// 前缀）。
        /// 原属旧路径门面，无状态通用工具统一收敛到 Helper 层（C27）。
        /// </summary>
        /// <remarks>
        /// Gets the remote format path (with file:// or http:// prefix).
        /// </remarks>
        /// <param name="path">原始路径 / The original path</param>
        /// <returns>远程格式路径 / The remote format path</returns>
        [UnityEngine.Scripting.Preserve]
        public static string GetRemotePath(string path)
        {
            string regularPath = NormalizePath(path);
            if (regularPath == null)
            {
                return null;
            }

            return regularPath.Contains("://") ? regularPath : ("file:///" + regularPath).Replace("file:////", "file:///");
        }

        /// <summary>
        /// 移除空文件夹。
        /// 原两处重复实现去重保留一份（C27）。
        /// 不使用 SearchOption.AllDirectories，逐层递归自底向上删除，以便在可能产生异常的环境下删除尽可能多的目录；任何异常吞掉并返回 false。
        /// </summary>
        /// <remarks>
        /// Removes empty directories recursively (bottom-up).
        /// </remarks>
        /// <param name="directoryName">要处理的文件夹名称 / The directory name to process</param>
        /// <returns>是否移除空文件夹成功 / Whether the empty directory was removed successfully</returns>
        /// <exception cref="GameFrameworkException">当 <paramref name="directoryName"/> 为 null 或空字符串时抛出 / Thrown when <paramref name="directoryName"/> is null or empty</exception>
        [UnityEngine.Scripting.Preserve]
        public static bool RemoveEmptyDirectory(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName))
            {
                throw new GameFrameworkException("Directory name is invalid.");
            }

            try
            {
                if (!System.IO.Directory.Exists(directoryName))
                {
                    return false;
                }

                // 不使用 SearchOption.AllDirectories，以便于在可能产生异常的环境下删除尽可能多的目录
                string[] subDirectoryNames = System.IO.Directory.GetDirectories(directoryName, "*");
                int subDirectoryCount = subDirectoryNames.Length;
                foreach (string subDirectoryName in subDirectoryNames)
                {
                    if (RemoveEmptyDirectory(subDirectoryName))
                    {
                        subDirectoryCount--;
                    }
                }

                if (subDirectoryCount > 0)
                {
                    return false;
                }

                if (System.IO.Directory.GetFiles(directoryName, "*").Length > 0)
                {
                    return false;
                }

                System.IO.Directory.Delete(directoryName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 若指定目录不存在则创建它（C27 自旧目录工具迁入）。
        /// </summary>
        /// <remarks>
        /// Creates the specified directory if it does not exist.
        /// </remarks>
        /// <param name="path">目录路径；为 null 或空字符串时不执行任何操作 / The directory path; does nothing when it is null or empty</param>
        [UnityEngine.Scripting.Preserve]
        public static void CreateIfNotExists(string path)
        {
            if (!System.IO.Directory.Exists(path) && !string.IsNullOrEmpty(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 若指定目录存在则删除它；删除前先清空目录内全部文件，异常吞掉并记录日志（C27 自旧目录工具迁入）。
        /// </summary>
        /// <remarks>
        /// Deletes the specified directory if it exists. All files inside are cleared before deletion, and any exception is caught and logged.
        /// </remarks>
        /// <param name="path">目录路径 / The directory path</param>
        /// <param name="recursive">是否递归删除子目录 / Whether to delete subdirectories recursively</param>
        [UnityEngine.Scripting.Preserve]
        public static void DeleteIfExists(string path, bool recursive = true)
        {
            if (!System.IO.Directory.Exists(path))
            {
                return;
            }

            try
            {
                ClearFiles(path);
                System.IO.Directory.Delete(path, recursive);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        /// <summary>
        /// 递归删除指定目录下的所有文件，但保留目录结构；目录不存在时不执行任何操作（C27 自旧目录工具迁入）。
        /// </summary>
        /// <remarks>
        /// Recursively deletes all files under the specified directory while keeping the directory structure.
        /// </remarks>
        /// <param name="path">目录路径 / The directory path</param>
        [UnityEngine.Scripting.Preserve]
        public static void ClearFiles(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                return;
            }

            var dirInfo = new System.IO.DirectoryInfo(path);

            var files = dirInfo.GetFiles();
            foreach (var file in files)
            {
                System.IO.File.Delete(file.FullName);
            }

            var subDirs = dirInfo.GetDirectories();
            foreach (var subDir in subDirs)
            {
                ClearFiles(subDir.FullName);
            }
        }


        /// <summary>
        /// 拼接路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static string Combine(params string[] paths)
        {
            var sb = new StringBuilder();
            const string separatorA = "/";
            const string separatorB = "\\";
            for (var index = 0; index < paths.Length - 1; index++)
            {
                var path = paths[index];
                sb.Append(path);
                if (path.EndsWithFast(separatorA) || path.EndsWithFast(separatorB))
                {
                    continue;
                }

                sb.Append(separatorA);
            }

            sb.Append(paths[paths.Length - 1]);
            return sb.ToString();
        }
    }
}