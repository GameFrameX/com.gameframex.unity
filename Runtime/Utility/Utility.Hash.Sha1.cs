using System;
using System.IO;
using System.Text;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 哈希计算相关的实用函数。
        /// </summary>
        /// <remarks>
        /// Hash calculation related utility functions.
        /// </remarks>
        public static partial class Hash
        {
            /// <summary>
            /// SHA1 哈希算法的实现。
            /// </summary>
            /// <remarks>
            /// SHA1 hash algorithm implementation.
            /// </remarks>
            [UnityEngine.Scripting.Preserve]
            public static class Sha1
            {

                /// <summary>
                /// 计算给定内容的 SHA1 哈希值，使用 UTF-8 编码。
                /// </summary>
                /// <remarks>
                /// Computes the SHA1 hash value of the given content using UTF-8 encoding.
                /// </remarks>
                /// <param name="content">要计算哈希值的内容 / The content to hash</param>
                /// <returns>返回计算得到的 SHA1 哈希值 / The computed SHA1 hash value</returns>
                [UnityEngine.Scripting.Preserve]
                public static string Hash(string content)
                {
                    return Hash(content, Encoding.UTF8);
                }

                /// <summary>
                /// 使用指定编码计算 SHA1 哈希值。
                /// </summary>
                /// <remarks>
                /// Computes the SHA1 hash value using the specified encoding.
                /// </remarks>
                /// <param name="content">要计算哈希值的内容 / The content to hash</param>
                /// <param name="encode">用于编码的 Encoding 对象 / The encoding to use</param>
                /// <returns>返回计算得到的 SHA1 哈希值 / The computed SHA1 hash value</returns>
                [UnityEngine.Scripting.Preserve]
                public static string Hash(string content, Encoding encode)
                {
                    //创建SHA1对象
                    using (var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider())
                    {
                        //将待加密字符串转为byte类型
                        var bytesIn = encode.GetBytes(content);
                        var bytesOut = sha1.ComputeHash(bytesIn);
                        var result = BitConverter.ToString(bytesOut); //将运算结果转为string类型
                        result = result.Replace("-", string.Empty).ToLower();
                        return result;
                    }
                }
            }
        }
    }
}