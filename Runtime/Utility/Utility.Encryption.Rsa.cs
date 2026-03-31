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
            /// <summary>
            /// RSA 加密算法的实现。
            /// </summary>
            /// <remarks>
            /// RSA encryption algorithm implementation.
            /// </remarks>
            [UnityEngine.Scripting.Preserve]
            public sealed class Rsa
            {
                private readonly RSACryptoServiceProvider _rsa = null;

                /// <summary>
                /// 使用提供的 RSACryptoServiceProvider 实例初始化 Rsa 类。
                /// </summary>
                /// <remarks>
                /// Initializes the Rsa class with the provided RSACryptoServiceProvider instance.
                /// </remarks>
                /// <param name="rsa">RSACryptoServiceProvider 实例 / The RSACryptoServiceProvider instance</param>
                [UnityEngine.Scripting.Preserve]
                public Rsa(RSACryptoServiceProvider rsa)
                {
                    _rsa = rsa;
                }

                /// <summary>
                /// 使用提供的密钥字符串初始化 Rsa 类。
                /// </summary>
                /// <remarks>
                /// Initializes the Rsa class with the provided key string.
                /// </remarks>
                /// <param name="key">密钥字符串 / The key string</param>
                [UnityEngine.Scripting.Preserve]
                public Rsa(string key)
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.FromXmlString(key);
                    this._rsa = rsa;
                }

                /// <summary>
                /// 创建 RSA 密钥对。
                /// </summary>
                /// <remarks>
                /// Creates an RSA key pair.
                /// </remarks>
                /// <returns>包含私钥和公钥的字典 / A dictionary containing the private key and public key</returns>
                [UnityEngine.Scripting.Preserve]
                public static Dictionary<string, string> Make()
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    RSACryptoServiceProvider dsa = new RSACryptoServiceProvider();
                    dic["privateKey"] = dsa.ToXmlString(true);
                    dic["publicKey"] = dsa.ToXmlString(false);
                    return dic;
                }

                /// <summary>
                /// 使用公钥加密内容。
                /// </summary>
                /// <remarks>
                /// Encrypts content using the public key.
                /// </remarks>
                /// <param name="publicKey">公钥 / The public key</param>
                /// <param name="content">要加密的内容 / The content to encrypt</param>
                /// <returns>加密后的 Base64 字符串 / The Base64 encrypted string</returns>
                [UnityEngine.Scripting.Preserve]
                public static string RSAEncrypt(string publicKey, string content)
                {
                    byte[] res = RSAEncrypt(publicKey, Encoding.UTF8.GetBytes(content));
                    return Convert.ToBase64String(res);
                }

                [UnityEngine.Scripting.Preserve]
                public string Encrypt(string content)
                {
                    byte[] res = Encrypt(Encoding.UTF8.GetBytes(content));
                    return Convert.ToBase64String(res);
                }

                [UnityEngine.Scripting.Preserve]
                public static byte[] RSAEncrypt(string publicKey, byte[] content)
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.FromXmlString(publicKey);
                    byte[] cipherBytes = rsa.Encrypt(content, false);
                    return cipherBytes;
                }

                [UnityEngine.Scripting.Preserve]
                public byte[] Encrypt(byte[] content)
                {
                    byte[] cipherBytes = _rsa.Encrypt(content, false);
                    return cipherBytes;
                }

                /// <summary>
                /// 使用私钥解密内容。
                /// </summary>
                /// <remarks>
                /// Decrypts content using the private key.
                /// </remarks>
                /// <param name="privateKey">私钥 / The private key</param>
                /// <param name="content">加密后的内容 / The encrypted content</param>
                /// <returns>解密后的内容 / The decrypted content</returns>
                [UnityEngine.Scripting.Preserve]
                public static string RSADecrypt(string privateKey, string content)
                {
                    byte[] res = RSADecrypt(privateKey, Convert.FromBase64String(content));
                    return Encoding.UTF8.GetString(res);
                }

                [UnityEngine.Scripting.Preserve]
                public static byte[] RSADecrypt(string privateKey, byte[] content)
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.FromXmlString(privateKey);
                    byte[] cipherBytes = rsa.Decrypt(content, false);
                    return cipherBytes;
                }

                [UnityEngine.Scripting.Preserve]
                public string Decrypt(string content)
                {
                    byte[] res = Decrypt(Convert.FromBase64String(content));
                    return Encoding.UTF8.GetString(res);
                }

                [UnityEngine.Scripting.Preserve]
                public byte[] Decrypt(byte[] content)
                {
                    byte[] bytes = _rsa.Decrypt(content, false);
                    return bytes;
                }

                /// <summary>
                /// 使用私钥对数据进行签名。
                /// </summary>
                /// <remarks>
                /// Signs data using the private key.
                /// </remarks>
                /// <param name="dataToSign">要签名的数据 / The data to sign</param>
                /// <param name="privateKey">私钥字符串 / The private key string</param>
                /// <returns>签名后的字节数组 / The signed byte array</returns>
                [UnityEngine.Scripting.Preserve]
                public static byte[] RSASignData(byte[] dataToSign, string privateKey)
                {
                    try
                    {
                        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                        rsa.FromXmlString(privateKey);
                        return rsa.SignData(dataToSign, new SHA1CryptoServiceProvider());
                    }
                    catch
                    {
                        return null;
                    }
                }

                [UnityEngine.Scripting.Preserve]
                public static string RSASignData(string dataToSign, string privateKey)
                {
                    byte[] res = RSASignData(Encoding.UTF8.GetBytes(dataToSign), privateKey);
                    return Convert.ToBase64String(res);
                }

                [UnityEngine.Scripting.Preserve]
                public byte[] SignData(byte[] dataToSign)
                {
                    try
                    {
                        return _rsa.SignData(dataToSign, new SHA1CryptoServiceProvider());
                    }
                    catch
                    {
                        return null;
                    }
                }

                [UnityEngine.Scripting.Preserve]
                public string SignData(string dataToSign)
                {
                    byte[] res = SignData(Encoding.UTF8.GetBytes(dataToSign));
                    return Convert.ToBase64String(res);
                }

                /// <summary>
                /// 验证签名。
                /// </summary>
                /// <remarks>
                /// Verifies the signature.
                /// </remarks>
                /// <param name="dataToVerify">要验证的数据 / The data to verify</param>
                /// <param name="signedData">签名数据 / The signed data</param>
                /// <param name="publicKey">公钥字符串 / The public key string</param>
                /// <returns>验证结果 / The verification result</returns>
                [UnityEngine.Scripting.Preserve]
                public static bool RSAVerifyData(byte[] dataToVerify, byte[] signedData, string publicKey)
                {
                    try
                    {
                        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                        rsa.FromXmlString(publicKey);
                        return rsa.VerifyData(dataToVerify, new SHA1CryptoServiceProvider(), signedData);
                    }
                    catch
                    {
                        return false;
                    }
                }

                [UnityEngine.Scripting.Preserve]
                public static bool RSAVerifyData(string dataToVerify, string signedData, string publicKey)
                {
                    return RSAVerifyData(Encoding.UTF8.GetBytes(dataToVerify), Convert.FromBase64String(signedData),
                        publicKey);
                }

                [UnityEngine.Scripting.Preserve]
                public bool VerifyData(byte[] dataToVerify, byte[] signedData)
                {
                    try
                    {
                        return _rsa.VerifyData(dataToVerify, new SHA1CryptoServiceProvider(), signedData);
                    }
                    catch
                    {
                        return false;
                    }
                }

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