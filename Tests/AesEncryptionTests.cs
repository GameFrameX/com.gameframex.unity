/*
using System;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    public class AesEncryptionTests
    {
        private const string TestKey = "TestKey1234567890";
        private const string TestPlaintext = "Hello GameFrameX! 你好世界 🎮";
        private static readonly byte[] TestPlaintextBytes = Encoding.UTF8.GetBytes(TestPlaintext);

        #region AESEncrypt/AESDecrypt (委托到 Secure 版本)

        [Test]
        public void AESEncrypt_Decrypt_String_RoundTrip()
        {
            #pragma warning disable CS0618
            string encrypted = Utility.Encryption.Aes.AESEncrypt(TestPlaintext, TestKey);
            string decrypted = Utility.Encryption.Aes.AESDecrypt(encrypted, TestKey);
            #pragma warning restore CS0618

            Assert.AreEqual(TestPlaintext, decrypted);
        }

        [Test]
        public void AESEncrypt_Decrypt_Bytes_RoundTrip()
        {
            #pragma warning disable CS0618
            byte[] encrypted = Utility.Encryption.Aes.AESEncrypt(TestPlaintextBytes, TestKey);
            byte[] decrypted = Utility.Encryption.Aes.AESDecrypt(encrypted, TestKey);
            #pragma warning restore CS0618

            CollectionAssert.AreEqual(TestPlaintextBytes, decrypted);
        }

        [Test]
        public void AESEncrypt_SamePlaintext_ProducesDifferentCiphertext()
        {
            #pragma warning disable CS0618
            string encrypted1 = Utility.Encryption.Aes.AESEncrypt(TestPlaintext, TestKey);
            string encrypted2 = Utility.Encryption.Aes.AESEncrypt(TestPlaintext, TestKey);
            #pragma warning restore CS0618

            Assert.AreNotEqual(encrypted1, encrypted2, "相同明文应产生不同密文（随机 IV/Salt）");
        }

        [Test]
        public void AESEncrypt_Bytes_SamePlaintext_ProducesDifferentCiphertext()
        {
            #pragma warning disable CS0618
            byte[] encrypted1 = Utility.Encryption.Aes.AESEncrypt(TestPlaintextBytes, TestKey);
            byte[] encrypted2 = Utility.Encryption.Aes.AESEncrypt(TestPlaintextBytes, TestKey);
            #pragma warning restore CS0618

            // 两个密文长度相同但内容不同（因为随机 IV/Salt）
            Assert.AreEqual(encrypted1.Length, encrypted2.Length);
            bool allSame = true;
            for (int i = 0; i < encrypted1.Length; i++)
            {
                if (encrypted1[i] != encrypted2[i])
                {
                    allSame = false;
                    break;
                }
            }

            Assert.IsFalse(allSame, "相同明文字节数组应产生不同密文（随机 IV/Salt）");
        }

        [Test]
        public void AESEncrypt_NullBytes_Throws()
        {
            #pragma warning disable CS0618
            Assert.Throws<ArgumentNullException>(() =>
            {
                Utility.Encryption.Aes.AESEncrypt(null, TestKey);
            });
            #pragma warning restore CS0618
        }

        [Test]
        public void AESEncrypt_EmptyKey_Throws()
        {
            #pragma warning disable CS0618
            Assert.Throws<Exception>(() =>
            {
                Utility.Encryption.Aes.AESEncrypt(TestPlaintext, "");
            });
            #pragma warning restore CS0618
        }

        [Test]
        public void AESDecrypt_ShortCiphertext_Throws()
        {
            byte[] shortData = new byte[16];
            #pragma warning disable CS0618
            Assert.Throws<Exception>(() =>
            {
                Utility.Encryption.Aes.AESDecrypt(shortData, TestKey);
            });
            #pragma warning restore CS0618
        }

        #endregion

        #region AESEncryptSecure/AESDecryptSecure (安全版本)

        [Test]
        public void AESEncryptSecure_DecryptSecure_String_RoundTrip()
        {
            string encrypted = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintext, TestKey);
            string decrypted = Utility.Encryption.Aes.AESDecryptSecure(encrypted, TestKey);

            Assert.AreEqual(TestPlaintext, decrypted);
        }

        [Test]
        public void AESEncryptSecure_DecryptSecure_Bytes_RoundTrip()
        {
            byte[] encrypted = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, TestKey);
            byte[] decrypted = Utility.Encryption.Aes.AESDecryptSecure(encrypted, TestKey);

            CollectionAssert.AreEqual(TestPlaintextBytes, decrypted);
        }

        [Test]
        public void AESEncryptSecure_SamePlaintext_ProducesDifferentCiphertext()
        {
            string encrypted1 = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintext, TestKey);
            string encrypted2 = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintext, TestKey);

            Assert.AreNotEqual(encrypted1, encrypted2);
        }

        [Test]
        public void AESEncryptSecure_NullBytes_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Utility.Encryption.Aes.AESEncryptSecure(null, TestKey);
            });
        }

        [Test]
        public void AESDecryptSecure_NullBytes_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Utility.Encryption.Aes.AESDecryptSecure(null, TestKey);
            });
        }

        [Test]
        public void AESDecryptSecure_TooShort_Throws()
        {
            byte[] shortData = new byte[32];
            Assert.Throws<Exception>(() =>
            {
                Utility.Encryption.Aes.AESDecryptSecure(shortData, TestKey);
            });
        }

        [Test]
        public void AESEncryptSecure_CiphertextFormat_ContainsIVAndSalt()
        {
            byte[] encrypted = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, TestKey);

            // 密文格式: [IV(16)][Salt(16)][加密数据]
            Assert.Greater(encrypted.Length, 32 + TestPlaintextBytes.Length,
                "密文应至少包含 32 字节头部（IV+Salt）+ 明文加密数据");
        }

        #endregion

        #region 跨方法兼容（委托验证）

        [Test]
        public void AESEncrypt_ProducesDataDecryptableBy_AESDecryptSecure()
        {
            #pragma warning disable CS0618
            byte[] encrypted = Utility.Encryption.Aes.AESEncrypt(TestPlaintextBytes, TestKey);
            byte[] decrypted = Utility.Encryption.Aes.AESDecryptSecure(encrypted, TestKey);
            #pragma warning restore CS0618

            CollectionAssert.AreEqual(TestPlaintextBytes, decrypted,
                "AESEncrypt 委托到 Secure 版本，密文应能被 AESDecryptSecure 解密");
        }

        [Test]
        public void AESEncryptSecure_ProducesDataDecryptableBy_AESDecrypt()
        {
            byte[] encrypted = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, TestKey);
            #pragma warning disable CS0618
            byte[] decrypted = Utility.Encryption.Aes.AESDecrypt(encrypted, TestKey);
            #pragma warning restore CS0618

            CollectionAssert.AreEqual(TestPlaintextBytes, decrypted,
                "AESEncryptSecure 的密文应能被 AESDecrypt 解密（委托关系）");
        }

        #endregion
    }
}
*/
