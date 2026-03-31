using System;
using System.IO;
using System.Security.Cryptography;
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
        [UnityEngine.Scripting.Preserve]
        public static partial class Hash
        {
            /// <summary>
            /// HMACSHA256 哈希算法的实现。
            /// </summary>
            /// <remarks>
            /// HMACSHA256 hash algorithm implementation.
            /// </remarks>
            [UnityEngine.Scripting.Preserve]
            public static class HMACSha256
            {
                /// <summary>
                /// 使用提供的密钥对指定消息进行 HMACSHA256 哈希计算。
                /// </summary>
                /// <remarks>
                /// Computes the HMACSHA256 hash of the specified message using the provided key.
                /// </remarks>
                /// <param name="message">要进行哈希计算的消息 / The message to hash</param>
                /// <param name="key">用于哈希计算的密钥 / The key for hash calculation</param>
                /// <returns>Base64 编码的哈希值 / The Base64 encoded hash value</returns>
                [UnityEngine.Scripting.Preserve]
                public static string Hash(string message, string key)
                {
                    byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);

                    using (var hmac = new HMACSHA256(keyBytes))
                    {
                        byte[] hashBytes = hmac.ComputeHash(messageBytes);
                        return Convert.ToBase64String(hashBytes);
                    }
                }
            }
        }
    }
}