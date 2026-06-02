using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 加密解密相关的实用函数。
        /// </summary>
        /// <remarks>
        /// Encryption and decryption related utility functions.
        /// </remarks>
        public static partial class Encryption
        {
            /// <summary>
            /// AES 加密算法的实现。
            /// </summary>
            /// <remarks>
            /// AES encryption algorithm implementation.
            /// </remarks>
            [UnityEngine.Scripting.Preserve]
            public static class Aes
            {
                #region 加密

                #region 加密字符串

                /// <summary>
                /// AES 加密（高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法）。
                /// </summary>
                /// <remarks>
                /// AES encryption (Advanced Encryption Standard, the next generation encryption algorithm standard, fast and highly secure, one implementation of the AES standard is the Rijndael algorithm).
                /// </remarks>
                /// <param name="EncryptString">待加密密文 / The plaintext to encrypt</param>
                /// <param name="EncryptKey">加密密钥 / The encryption key</param>
                /// <returns>加密后的 Base64 字符串 / The Base64 encrypted string</returns>
                [Obsolete("AESEncrypt uses hardcoded IV/Salt. Use AESEncryptSecure instead.")]
                [UnityEngine.Scripting.Preserve]
                public static string AESEncrypt(string EncryptString, string EncryptKey)
                {
                    return AESEncryptSecure(EncryptString, EncryptKey);
                }

                #endregion

                #region 加密字节数组

                /// <summary>
                /// AES 加密（高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法）。
                /// </summary>
                /// <remarks>
                /// AES encryption (Advanced Encryption Standard, the next generation encryption algorithm standard, fast and highly secure, one implementation of the AES standard is the Rijndael algorithm).
                /// </remarks>
                /// <param name="EncryptByte">待加密的字节数组 / The byte array to encrypt</param>
                /// <param name="EncryptKey">加密密钥 / The encryption key</param>
                /// <returns>加密后的字节数组 / The encrypted byte array</returns>
                [Obsolete("AESEncrypt uses hardcoded IV/Salt. Use AESEncryptSecure instead.")]
                [UnityEngine.Scripting.Preserve]
                public static byte[] AESEncrypt(byte[] EncryptByte, string EncryptKey)
                {
                    return AESEncryptSecure(EncryptByte, EncryptKey);
                }

                #endregion

                #endregion

                #region 解密

                #region 解密字符串

                /// <summary>
                /// AES 解密（高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法）。
                /// </summary>
                /// <remarks>
                /// AES decryption (Advanced Encryption Standard, the next generation encryption algorithm standard, fast and highly secure, one implementation of the AES standard is the Rijndael algorithm).
                /// </remarks>
                /// <param name="DecryptString">待解密密文 / The ciphertext to decrypt</param>
                /// <param name="DecryptKey">解密密钥 / The decryption key</param>
                /// <returns>解密后的字符串 / The decrypted string</returns>
                [Obsolete("AESDecrypt uses hardcoded IV/Salt. Use AESDecryptSecure instead.")]
                [UnityEngine.Scripting.Preserve]
                public static string AESDecrypt(string DecryptString, string DecryptKey)
                {
                    return AESDecryptSecure(DecryptString, DecryptKey);
                }

                #endregion

                #region 解密字节数组

                /// <summary>
                /// AES 解密（高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法）。
                /// </summary>
                /// <remarks>
                /// AES decryption (Advanced Encryption Standard, the next generation encryption algorithm standard, fast and highly secure, one implementation of the AES standard is the Rijndael algorithm).
                /// </remarks>
                /// <param name="DecryptByte">待解密的字节数组 / The byte array to decrypt</param>
                /// <param name="DecryptKey">解密密钥 / The decryption key</param>
                /// <returns>解密后的字节数组 / The decrypted byte array</returns>
                [Obsolete("AESDecrypt uses hardcoded IV/Salt. Use AESDecryptSecure instead.")]
                [UnityEngine.Scripting.Preserve]
                public static byte[] AESDecrypt(byte[] DecryptByte, string DecryptKey)
                {
                    return AESDecryptSecure(DecryptByte, DecryptKey);
                }

                #endregion

                #endregion

                #region 安全加密

                #region 安全加密字符串

                /// <summary>
                /// AES 安全加密（使用随机 IV 和 Salt，相同明文产生不同密文）。
                /// </summary>
                /// <remarks>
                /// AES secure encryption (uses random IV and Salt, same plaintext produces different ciphertext).
                /// </remarks>
                /// <param name="EncryptString">待加密明文 / The plaintext to encrypt</param>
                /// <param name="EncryptKey">加密密钥 / The encryption key</param>
                /// <returns>加密后的 Base64 字符串 / The Base64 encrypted string</returns>
                [UnityEngine.Scripting.Preserve]
                public static string AESEncryptSecure(string EncryptString, string EncryptKey)
                {
                    return Convert.ToBase64String(AESEncryptSecure(Encoding.UTF8.GetBytes(EncryptString), EncryptKey));
                }

                #endregion

                #region 安全加密字节数组

                /// <summary>
                /// AES 安全加密（使用随机 IV 和 Salt，相同明文产生不同密文）。
                /// 密文格式：[IV(16字节)][Salt(16字节)][加密数据]。
                /// </summary>
                /// <remarks>
                /// AES secure encryption (uses random IV and Salt, same plaintext produces different ciphertext).
                /// Ciphertext format: [IV(16 bytes)][Salt(16 bytes)][Encrypted data].
                /// </remarks>
                /// <param name="EncryptByte">待加密的字节数组 / The byte array to encrypt</param>
                /// <param name="EncryptKey">加密密钥 / The encryption key</param>
                /// <returns>加密后的字节数组 / The encrypted byte array</returns>
                [UnityEngine.Scripting.Preserve]
                public static byte[] AESEncryptSecure(byte[] EncryptByte, string EncryptKey)
                {
                    if (EncryptByte == null)
                    {
                        throw new ArgumentNullException("EncryptByte");
                    }

                    if (EncryptByte.Length == 0)
                    {
                        throw (new Exception("明文不得为空"));
                    }

                    if (string.IsNullOrEmpty(EncryptKey))
                    {
                        throw (new Exception("密钥不得为空"));
                    }

                    byte[] m_strEncrypt;
                    byte[] m_btIV = new byte[16];
                    byte[] m_salt = new byte[16];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(m_btIV);
                        rng.GetBytes(m_salt);
                    }

                    Rijndael m_AESProvider = Rijndael.Create();
                    try
                    {
                        MemoryStream m_stream = new MemoryStream();
                        m_stream.Write(m_btIV, 0, m_btIV.Length);
                        m_stream.Write(m_salt, 0, m_salt.Length);
                        PasswordDeriveBytes pdb = new PasswordDeriveBytes(EncryptKey, m_salt);
                        ICryptoTransform transform = m_AESProvider.CreateEncryptor(pdb.GetBytes(32), m_btIV);
                        CryptoStream m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                        m_csstream.Write(EncryptByte, 0, EncryptByte.Length);
                        m_csstream.FlushFinalBlock();
                        m_strEncrypt = m_stream.ToArray();
                        m_stream.Close();
                        m_stream.Dispose();
                        m_csstream.Close();
                        m_csstream.Dispose();
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    catch (CryptographicException ex)
                    {
                        throw ex;
                    }
                    catch (ArgumentException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        m_AESProvider.Clear();
                    }

                    return m_strEncrypt;
                }

                #endregion

                #endregion

                #region 安全解密

                #region 安全解密字符串

                /// <summary>
                /// AES 安全解密（配合 AESEncryptSecure 使用，从密文头部提取随机 IV 和 Salt）。
                /// </summary>
                /// <remarks>
                /// AES secure decryption (used with AESEncryptSecure, extracts random IV and Salt from ciphertext header).
                /// </remarks>
                /// <param name="DecryptString">待解密密文 / The ciphertext to decrypt</param>
                /// <param name="DecryptKey">解密密钥 / The decryption key</param>
                /// <returns>解密后的字符串 / The decrypted string</returns>
                [UnityEngine.Scripting.Preserve]
                public static string AESDecryptSecure(string DecryptString, string DecryptKey)
                {
                    return Encoding.UTF8.GetString(AESDecryptSecure(Convert.FromBase64String(DecryptString), DecryptKey));
                }

                #endregion

                #region 安全解密字节数组

                /// <summary>
                /// AES 安全解密（配合 AESEncryptSecure 使用，从密文头部提取随机 IV 和 Salt）。
                /// 密文格式：[IV(16字节)][Salt(16字节)][加密数据]。
                /// </summary>
                /// <remarks>
                /// AES secure decryption (used with AESEncryptSecure, extracts random IV and Salt from ciphertext header).
                /// Ciphertext format: [IV(16 bytes)][Salt(16 bytes)][Encrypted data].
                /// </remarks>
                /// <param name="DecryptByte">待解密的字节数组 / The byte array to decrypt</param>
                /// <param name="DecryptKey">解密密钥 / The decryption key</param>
                /// <returns>解密后的字节数组 / The decrypted byte array</returns>
                [UnityEngine.Scripting.Preserve]
                public static byte[] AESDecryptSecure(byte[] DecryptByte, string DecryptKey)
                {
                    if (DecryptByte == null)
                    {
                        throw new ArgumentNullException("DecryptByte");
                    }

                    if (DecryptByte.Length <= 32)
                    {
                        throw (new Exception("密文不得为空"));
                    }

                    if (string.IsNullOrEmpty(DecryptKey))
                    {
                        throw (new Exception("密钥不得为空"));
                    }

                    byte[] m_strDecrypt;
                    byte[] m_btIV = new byte[16];
                    byte[] m_salt = new byte[16];
                    byte[] m_EncryptData = new byte[DecryptByte.Length - 32];
                    Buffer.BlockCopy(DecryptByte, 0, m_btIV, 0, 16);
                    Buffer.BlockCopy(DecryptByte, 16, m_salt, 0, 16);
                    Buffer.BlockCopy(DecryptByte, 32, m_EncryptData, 0, m_EncryptData.Length);
                    Rijndael m_AESProvider = Rijndael.Create();
                    try
                    {
                        MemoryStream m_stream = new MemoryStream();
                        PasswordDeriveBytes pdb = new PasswordDeriveBytes(DecryptKey, m_salt);
                        ICryptoTransform transform = m_AESProvider.CreateDecryptor(pdb.GetBytes(32), m_btIV);
                        CryptoStream m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                        m_csstream.Write(m_EncryptData, 0, m_EncryptData.Length);
                        m_csstream.FlushFinalBlock();
                        m_strDecrypt = m_stream.ToArray();
                        m_stream.Close();
                        m_stream.Dispose();
                        m_csstream.Close();
                        m_csstream.Dispose();
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    catch (CryptographicException ex)
                    {
                        throw ex;
                    }
                    catch (ArgumentException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        m_AESProvider.Clear();
                    }

                    return m_strDecrypt;
                }

                #endregion

                #endregion
            }
        }
    }
}