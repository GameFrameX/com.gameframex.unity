using System;
using System.Text;
using UnityEngine;

namespace GameFrameX.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public static class PathHelper
    {
        /// <summary>
        ///应用程序外部资源路径存放路径(热更新资源路径)
        /// </summary>
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
            return path.Replace('\\', '/').Replace("\\", "/");
        }

        static readonly StringBuilder CombineStringBuilder = new StringBuilder();

        /// <summary>
        /// 拼接路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static string Combine(params string[] paths)
        {
            CombineStringBuilder.Clear();
            const string separatorA = "/";
            const string separatorB = "\\";
            for (var index = 0; index < paths.Length - 1; index++)
            {
                var path = paths[index];
                CombineStringBuilder.Append(path);
                if (path.EndsWithFast(separatorA) || path.EndsWithFast(separatorB))
                {
                    continue;
                }

                if (path.StartsWithFast(separatorA) || path.StartsWithFast(separatorB))
                {
                    continue;
                }

                CombineStringBuilder.Append(separatorA);
            }

            CombineStringBuilder.Append(paths[paths.Length - 1]);
            return CombineStringBuilder.ToString();
        }
    }
}