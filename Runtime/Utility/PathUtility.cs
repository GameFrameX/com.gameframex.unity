// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 Apache License 2.0 许可证分发，
//  This project is licensed under the Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  CNB  仓库：https://cnb.cool/GameFrameX
//  CNB Repository:  https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using System.Text;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 路径相关的实用函数。
    /// </summary>
    /// <remarks>
    /// Path related utility functions.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class PathUtility
    {
        /// <summary>
        /// 获取规范的路径。
        /// </summary>
        /// <remarks>
        /// Gets the regularized path.
        /// </remarks>
        /// <param name="path">要规范的路径 / The path to regularize</param>
        /// <returns>规范的路径 / The regularized path</returns>
        [UnityEngine.Scripting.Preserve]
        public static string GetRegularPath(string path)
        {
            if (path == null)
            {
                return null;
            }

            return path.Replace('\\', '/');
        }

        /// <summary>
        /// 获取远程格式的路径（带有file:// 或 http:// 前缀）。
        /// </summary>
        /// <remarks>
        /// Gets the remote format path (with file:// or http:// prefix).
        /// </remarks>
        /// <param name="path">原始路径 / The original path</param>
        /// <returns>远程格式路径 / The remote format path</returns>
        [UnityEngine.Scripting.Preserve]
        public static string GetRemotePath(string path)
        {
            string regularPath = GetRegularPath(path);
            if (regularPath == null)
            {
                return null;
            }

            return regularPath.Contains("://") ? regularPath : ("file:///" + regularPath).Replace("file:////", "file:///");
        }

        [UnityEngine.Scripting.Preserve]
        public static void CheckPath(string path, bool isFilePath = true, bool forceMode = false)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            string finalDir;
            {
                if (isFilePath)
                {
                    finalDir = System.IO.Path.GetDirectoryName(path);
                    if (forceMode)
                    {
                        FileUtility.DeleteIfExists(path);
                    }
                }
                else
                {
                    finalDir = path;
                    if (forceMode)
                    {
                        DirectoryUtility.DeleteIfExists(path);
                    }
                }
            }

            DirectoryUtility.CreateIfNotExists(finalDir);
        }

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
            get { return PathUtility.GetRegularPath(Application.streamingAssetsPath); }
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
                return PathUtility.GetRegularPath(Application.streamingAssetsPath);
#endif
            }
        }

        /// <summary>
        /// 获取平台名称
        /// </summary>
        /// <remarks>
        /// 此属性已废弃，请使用 ApplicationUtility.PlatformName 替代
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [Obsolete("此方法已废弃，请使用 ApplicationUtility.PlatformName 替代")]
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

                if (path.StartsWithFast(separatorA) || path.StartsWithFast(separatorB))
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