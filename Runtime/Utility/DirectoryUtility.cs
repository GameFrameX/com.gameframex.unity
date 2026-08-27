using System;
using UnityEngine;

namespace GameFrameX.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public static partial class DirectoryUtility
    {
        /// <summary>
        /// 移除空文件夹。
        /// </summary>
        /// <remarks>
        /// Removes empty directories.
        /// </remarks>
        /// <param name="directoryName">要处理的文件夹名称 / The directory name to process</param>
        /// <returns>是否移除空文件夹成功 / Whether the empty directory was removed successfully</returns>
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

        [UnityEngine.Scripting.Preserve]
        public static void CreateIfNotExists(string path)
        {
            if (!System.IO.Directory.Exists(path) && !string.IsNullOrEmpty(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }

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
                e.Message.Print(Color.red);
            }
        }

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
    }
}