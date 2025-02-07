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
        public static partial class Hash
        {
            /// <summary>
            /// Sha1
            /// </summary>
            /// <summary>
            /// Sha1 哈希算法的实现。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public static class Sha1
            {

                /// <summary>
                /// 计算给定内容的Sha1哈希值，使用UTF-8编码。
                /// </summary>
                /// <param name="content">要计算哈希值的内容。</param>
                /// <returns>返回计算得到的Sha1哈希值。</returns>
                [UnityEngine.Scripting.Preserve]
                public static string Hash(string content)
                {
                    return Hash(content, Encoding.UTF8);
                }

                /// <summary>
                /// 使用指定编码计算Sha1哈希值。
                /// </summary>
                /// <param name="content">要计算哈希值的内容。</param>
                /// <param name="encode">用于编码的Encoding对象。</param>
                /// <returns>返回计算得到的Sha1哈希值。</returns>
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