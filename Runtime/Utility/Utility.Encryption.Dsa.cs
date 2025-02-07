using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 加密解密相关的实用函数。
        /// </summary>
        public static partial class Encryption
        {
            [UnityEngine.Scripting.Preserve]
            public sealed class Dsa
            {
                private readonly DSACryptoServiceProvider _dsa = null;

                /// <summary>
                /// Dsa 构造函数，使用提供的 DSACryptoServiceProvider 实例初始化。
                /// </summary>
                /// <param name="dsa">DSACryptoServiceProvider 实例</param>
                [UnityEngine.Scripting.Preserve]
                public Dsa(DSACryptoServiceProvider dsa)
                {
                    _dsa = dsa;
                }

                /// <summary>
                /// Dsa 构造函数，使用提供的密钥字符串初始化。
                /// </summary>
                /// <param name="key">密钥字符串</param>
                [UnityEngine.Scripting.Preserve]
                public Dsa(string key)
                {
                    DSACryptoServiceProvider rsa = new DSACryptoServiceProvider();
                    rsa.FromXmlString(key);
                    this._dsa = rsa;
                }

                /// <summary>
                /// 创建 DSA 密钥对。
                /// </summary>
                /// <returns>包含私钥和公钥的字典</returns>
                [UnityEngine.Scripting.Preserve]
                public static Dictionary<string, string> Make()
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
                    dic["privatekey"] = dsa.ToXmlString(true);
                    dic["publickey"] = dsa.ToXmlString(false);
                    return dic;
                }

                /// <summary>
                /// 使用私钥对数据进行签名。
                /// </summary>
                /// <param name="dataToSign">要签名的数据</param>
                /// <param name="privateKey">私钥字符串</param>
                /// <returns>签名后的字节数组</returns>
                [UnityEngine.Scripting.Preserve]
                public static byte[] DSASignData(byte[] dataToSign, string privateKey)
                {
                    try
                    {
                        DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
                        dsa.FromXmlString(privateKey);
                        return dsa.SignData(dataToSign);
                    }
                    catch
                    {
                        return null;
                    }
                }

                /// <summary>
                /// 使用私钥对字符串数据进行签名。
                /// </summary>
                /// <param name="dataToSign">要签名的字符串数据</param>
                /// <param name="privateKey">私钥字符串</param>
                /// <returns>签名后的 Base64 字符串</returns>
                [UnityEngine.Scripting.Preserve]
                public static string DSASignData(string dataToSign, string privateKey)
                {
                    byte[] res = DSASignData(Encoding.UTF8.GetBytes(dataToSign), privateKey);
                    return Convert.ToBase64String(res);
                }

                /// <summary>
                /// 使用 DSA 对数据进行签名。
                /// </summary>
                /// <param name="dataToSign">要签名的数据</param>
                /// <returns>签名后的字节数组</returns>
                [UnityEngine.Scripting.Preserve]
                public byte[] SignData(byte[] dataToSign)
                {
                    try
                    {
                        return _dsa.SignData(dataToSign);
                    }
                    catch
                    {
                        return null;
                    }
                }

                /// <summary>
                /// 使用 DSA 对字符串数据进行签名。
                /// </summary>
                /// <param name="dataToSign">要签名的字符串数据</param>
                /// <returns>签名后的 Base64 字符串</returns>
                [UnityEngine.Scripting.Preserve]
                public string SignData(string dataToSign)
                {
                    byte[] res = SignData(Encoding.UTF8.GetBytes(dataToSign));
                    return Convert.ToBase64String(res);
                }

                /// <summary>
                /// 验证签名。
                /// </summary>
                /// <param name="dataToVerify">要验证的数据</param>
                /// <param name="signedData">签名数据</param>
                /// <param name="privateKey">私钥字符串</param>
                /// <returns>验证结果</returns>
                [UnityEngine.Scripting.Preserve]
                public static bool DSAVerifyData(byte[] dataToVerify, byte[] signedData, string privateKey)
                {
                    try
                    {
                        DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
                        dsa.FromXmlString(privateKey);
                        return dsa.VerifyData(dataToVerify, signedData);
                    }
                    catch
                    {
                        return false;
                    }
                }

                /// <summary>
                /// 验证签名。
                /// </summary>
                /// <param name="dataToVerify">要验证的字符串数据</param>
                /// <param name="signedData">签名的 Base64 字符串</param>
                /// <param name="privateKey">私钥字符串</param>
                /// <returns>验证结果</returns>
                [UnityEngine.Scripting.Preserve]
                public static bool DSAVerifyData(string dataToVerify, string signedData, string privateKey)
                {
                    return DSAVerifyData(Encoding.UTF8.GetBytes(dataToVerify), Convert.FromBase64String(signedData),
                        privateKey);
                }

                /// <summary>
                /// 使用 DSA 验证签名。
                /// </summary>
                /// <param name="dataToVerify">要验证的数据</param>
                /// <param name="signedData">签名数据</param>
                /// <returns>验证结果</returns>
                [UnityEngine.Scripting.Preserve]
                public bool VerifyData(byte[] dataToVerify, byte[] signedData)
                {
                    try
                    {
                        return _dsa.VerifyData(dataToVerify, signedData);
                    }
                    catch
                    {
                        return false;
                    }
                }

                /// <summary>
                /// 使用 DSA 验证签名。
                /// </summary>
                /// <param name="dataToVerify">要验证的字符串数据</param>
                /// <param name="signedData">签名的 Base64 字符串</param>
                /// <returns>验证结果</returns>
                [UnityEngine.Scripting.Preserve]
                public bool VerifyData(string dataToVerify, string signedData)
                {
                    try
                    {
                        return VerifyData(Encoding.UTF8.GetBytes(dataToVerify), Convert.FromBase64String(signedData));
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
    }
}