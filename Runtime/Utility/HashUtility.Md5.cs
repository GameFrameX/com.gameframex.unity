using System;
using System.IO;
using System.Text;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    public static partial class HashUtility
    {
        /// <summary>
        /// MD5 哈希算法的实现。
        /// </summary>
        /// <remarks>
        /// MD5 hash algorithm implementation.
        /// </remarks>
        [Preserve]
        public static class Md5
        {
            /// <summary>
            /// 获取字符串的 MD5 哈希值。
            /// </summary>
            /// <remarks>
            /// Gets the MD5 hash value of the string.
            /// </remarks>
            /// <param name="input">要计算哈希的字符串 / The string to hash</param>
            /// <returns>MD5 哈希值 / The MD5 hash value</returns>
            [Preserve]
            public static string Hash(string input)
            {
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    var data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                    return ToHash(data);
                }
            }

            /// <summary>
            /// 获取流的 MD5 哈希值。
            /// </summary>
            /// <remarks>
            /// Gets the MD5 hash value of the stream.
            /// </remarks>
            /// <param name="input">要计算哈希的流 / The stream to hash</param>
            /// <returns>MD5 哈希值 / The MD5 hash value</returns>
            [Preserve]
            public static string Hash(Stream input)
            {
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    var data = md5.ComputeHash(input);
                    return ToHash(data);
                }
            }

            /// <summary>
            /// 验证 MD5 哈希值是否一致。
            /// </summary>
            /// <remarks>
            /// Verifies if the MD5 hash values match.
            /// </remarks>
            /// <param name="input">要验证的哈希值 / The hash value to verify</param>
            /// <param name="hash">目标哈希值 / The target hash value</param>
            /// <returns>如果一致则返回 true，否则返回 false / Returns true if they match, otherwise false</returns>
            [Preserve]
            public static bool IsVerify(string input, string hash)
            {
                var comparer = StringComparer.OrdinalIgnoreCase;
                return 0 == comparer.Compare(input, hash);
            }

            static string ToHash(byte[] data)
            {
                var sb = new StringBuilder();
                foreach (var t in data)
                {
                    sb.Append(t.ToString("x2"));
                }

                return sb.ToString();
            }

            /// <summary>
            /// 获取指定文件路径的 MD5 哈希值。
            /// </summary>
            /// <remarks>
            /// Gets the MD5 hash value of the file at the specified path.
            /// </remarks>
            /// <param name="filePath">文件路径 / The file path</param>
            /// <returns>MD5 哈希值 / The MD5 hash value</returns>
            [Preserve]
            public static string FileHash(string filePath)
            {
                using (var file = new FileStream(filePath, FileMode.Open))
                {
                    return Hash(file);
                }
            }
        }
    }
}