using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 文件帮助类。
    /// </summary>
    /// <remarks>
    /// File helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class FileHelper
    {
        /// <summary>
        /// 获取目录下的所有文件。
        /// </summary>
        /// <remarks>
        /// Recursively gets all files in the specified directory.
        /// </remarks>
        /// <param name="files">用于存储文件路径的列表 / List to store file paths</param>
        /// <param name="dir">目标目录 / Target directory</param>
        [UnityEngine.Scripting.Preserve]
        public static void GetAllFiles(List<string> files, string dir)
        {
            if (!Directory.Exists(dir))
            {
                return;
            }

            string[] strings = Directory.GetFiles(dir);
            foreach (string item in strings)
            {
                files.Add(item);
            }

            string[] subDirs = Directory.GetDirectories(dir);
            foreach (string subDir in subDirs)
            {
                GetAllFiles(files, subDir);
            }
        }

        /// <summary>
        /// 清理目录。
        /// </summary>
        /// <remarks>
        /// Cleans the specified directory by deleting all files and subdirectories.
        /// </remarks>
        /// <param name="dir">目标目录路径 / Target directory path</param>
        [UnityEngine.Scripting.Preserve]
        public static void CleanDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                return;
            }

            foreach (string subDir in Directory.GetDirectories(dir))
            {
                Directory.Delete(subDir, true);
            }

            foreach (string subFile in Directory.GetFiles(dir))
            {
                File.Delete(subFile);
            }
        }

        /// <summary>
        /// 目录复制。
        /// </summary>
        /// <remarks>
        /// Copies a directory and all its contents to the target location.
        /// </remarks>
        /// <param name="srcDir">源目录路径 / Source directory path</param>
        /// <param name="targetDir">目标目录路径 / Target directory path</param>
        /// <exception cref="Exception">当父目录拷贝到子目录时抛出 / Thrown when trying to copy parent directory to child directory</exception>
        [UnityEngine.Scripting.Preserve]
        public static void CopyDirectory(string srcDir, string targetDir)
        {
            DirectoryInfo source = new DirectoryInfo(srcDir);
            DirectoryInfo target = new DirectoryInfo(targetDir);

            if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("父目录不能拷贝到子目录！");
            }

            if (!source.Exists)
            {
                return;
            }

            if (!target.Exists)
            {
                target.Create();
            }

            FileInfo[] files = source.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                File.Copy(files[i].FullName, Path.Combine(target.FullName, files[i].Name), true);
            }

            DirectoryInfo[] dirs = source.GetDirectories();

            for (int j = 0; j < dirs.Length; j++)
            {
                CopyDirectory(dirs[j].FullName, Path.Combine(target.FullName, dirs[j].Name));
            }
        }

        /// <summary>
        /// 复制文件到目标目录。
        /// </summary>
        /// <remarks>
        /// Copies a file to the target location.
        /// </remarks>
        /// <param name="sourceFileName">源文件路径 / Source file path</param>
        /// <param name="destFileName">目标文件路径 / Destination file path</param>
        /// <param name="overwrite">是否覆盖已存在的文件 / Whether to overwrite existing file</param>
        [UnityEngine.Scripting.Preserve]
        public static void Copy(string sourceFileName, string destFileName, bool overwrite = false)
        {
            if (!File.Exists(sourceFileName))
            {
                return;
            }

            File.Copy(sourceFileName, destFileName, overwrite);
        }

        /// <summary>
        /// 删除文件。
        /// </summary>
        /// <remarks>
        /// Deletes the specified file.
        /// </remarks>
        /// <param name="path">文件路径 / File path to delete</param>
        [UnityEngine.Scripting.Preserve]
        public static void Delete(string path)
        {
            File.Delete(path);
        }

        /// <summary>
        /// 判断文件是否存在。
        /// </summary>
        /// <remarks>
        /// Determines whether the specified file exists.
        /// </remarks>
        /// <param name="path">文件路径 / File path to check</param>
        /// <returns>如果文件存在则为 <c>true</c>；否则为 <c>false</c> / <c>true</c> if file exists; otherwise <c>false</c></returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsExists(string path)
        {
#if ENABLE_GAME_FRAME_X_READ_ASSETS
            if (IsAndroidReadOnlyPath(path, out var readPath))
            {
                return BlankReadAssets.BlankReadAssets.IsFileExists(readPath);
            }
#endif
            return File.Exists(path);
        }

        [UnityEngine.Scripting.Preserve]
        private static bool IsAndroidReadOnlyPath(string path, out string readPath)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (PathHelper.NormalizePath(path).Contains(PathHelper.AppResPath))
                {
                    readPath = path.Substring(PathHelper.AppResPath.Length);
                    return true;
                }
            }

            readPath = null;
            return false;
        }

        /// <summary>
        /// 移动文件到目标目录。
        /// </summary>
        /// <remarks>
        /// Moves a file from source to destination.
        /// </remarks>
        /// <param name="sourceFileName">文件源路径 / Source file path</param>
        /// <param name="destFileName">目标文件路径 / Destination file path</param>
        [UnityEngine.Scripting.Preserve]
        public static void Move(string sourceFileName, string destFileName)
        {
            if (!File.Exists(sourceFileName))
            {
                return;
            }

            Copy(sourceFileName, destFileName, true);
            Delete(sourceFileName);
        }

        /// <summary>
        /// 读取指定路径的文件内容（字节数组）。
        /// </summary>
        /// <remarks>
        /// Reads all bytes from the specified file.
        /// </remarks>
        /// <param name="path">文件路径 / File path</param>
        /// <returns>文件内容字节数组 / File content as byte array</returns>
        [UnityEngine.Scripting.Preserve]
        public static byte[] ReadAllBytes(string path)
        {
#if ENABLE_GAME_FRAME_X_READ_ASSETS
            if (IsAndroidReadOnlyPath((path), out var readPath))
            {
                return BlankReadAssets.BlankReadAssets.Read(readPath);
            }
#endif

            return File.ReadAllBytes(path);
        }

        /// <summary>
        /// 读取指定路径的文件内容（指定编码）。
        /// </summary>
        /// <remarks>
        /// Reads all text from the specified file with the given encoding.
        /// </remarks>
        /// <param name="path">文件路径 / File path</param>
        /// <param name="encoding">文本编码 / Text encoding</param>
        /// <returns>文件内容字符串 / File content as string</returns>
        [UnityEngine.Scripting.Preserve]
        public static string ReadAllText(string path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }

        /// <summary>
        /// 读取指定路径的文件内容（UTF-8编码）。
        /// </summary>
        /// <remarks>
        /// Reads all text from the specified file using UTF-8 encoding.
        /// </remarks>
        /// <param name="path">文件路径 / File path</param>
        /// <returns>文件内容字符串 / File content as string</returns>
        [UnityEngine.Scripting.Preserve]
        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path, Encoding.UTF8);
        }

        /// <summary>
        /// 读取指定路径的文件所有行（指定编码）。
        /// </summary>
        /// <remarks>
        /// Reads all lines from the specified file with the given encoding.
        /// </remarks>
        /// <param name="path">文件路径 / File path</param>
        /// <param name="encoding">文本编码 / Text encoding</param>
        /// <returns>文件所有行的数组 / Array of all lines in the file</returns>
        [UnityEngine.Scripting.Preserve]
        public static string[] ReadAllLines(string path, Encoding encoding)
        {
            return File.ReadAllLines(path, encoding);
        }

        /// <summary>
        /// 读取指定路径的文件所有行（UTF-8编码）。
        /// </summary>
        /// <remarks>
        /// Reads all lines from the specified file using UTF-8 encoding.
        /// </remarks>
        /// <param name="path">文件路径 / File path</param>
        /// <returns>文件所有行的数组 / Array of all lines in the file</returns>
        [UnityEngine.Scripting.Preserve]
        public static string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path, Encoding.UTF8);
        }

        /// <summary>
        /// 写入所有行到指定路径的文件（指定编码）。
        /// </summary>
        /// <remarks>
        /// Writes all lines to the specified file with the given encoding.
        /// </remarks>
        /// <param name="path">文件路径 / File path</param>
        /// <param name="lines">要写入的所有行 / Lines to write</param>
        /// <param name="encoding">文本编码 / Text encoding</param>
        [UnityEngine.Scripting.Preserve]
        public static void WriteAllLines(string path, string[] lines, Encoding encoding)
        {
            File.WriteAllLines(path, lines, encoding);
        }

        /// <summary>
        /// 写入所有行到指定路径的文件（UTF-8编码）。
        /// </summary>
        /// <remarks>
        /// Writes all lines to the specified file using UTF-8 encoding.
        /// </remarks>
        /// <param name="path">文件路径 / File path</param>
        /// <param name="lines">要写入的所有行 / Lines to write</param>
        [UnityEngine.Scripting.Preserve]
        public static void WriteAllLines(string path, string[] lines)
        {
            File.WriteAllLines(path, lines, Encoding.UTF8);
        }

        /// <summary>
        /// 写入文本内容到指定路径的文件（指定编码）。
        /// </summary>
        /// <remarks>
        /// Writes text content to the specified file with the given encoding.
        /// </remarks>
        /// <param name="path">文件路径 / File path</param>
        /// <param name="content">要写入的文本内容 / Text content to write</param>
        /// <param name="encoding">文本编码 / Text encoding</param>
        [UnityEngine.Scripting.Preserve]
        public static void WriteAllText(string path, string content, Encoding encoding)
        {
            File.WriteAllText(path, content, encoding);
        }

        /// <summary>
        /// 写入文本内容到指定路径的文件（UTF-8编码）。
        /// </summary>
        /// <remarks>
        /// Writes text content to the specified file using UTF-8 encoding.
        /// </remarks>
        /// <param name="path">文件路径 / File path</param>
        /// <param name="content">要写入的文本内容 / Text content to write</param>
        [UnityEngine.Scripting.Preserve]
        public static void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content, Encoding.UTF8);
        }

        /// <summary>
        /// 写入字节数组到指定路径的文件。
        /// </summary>
        /// <remarks>
        /// Writes byte array to the specified file.
        /// </remarks>
        /// <param name="path">文件路径 / File path</param>
        /// <param name="buffer">要写入的字节数组 / Byte array to write</param>
        [UnityEngine.Scripting.Preserve]
        public static void WriteAllBytes(string path, byte[] buffer)
        {
            File.WriteAllBytes(path, buffer);
        }
    }
}