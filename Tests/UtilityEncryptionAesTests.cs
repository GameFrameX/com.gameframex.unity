using System;
using System.Security.Cryptography;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityEncryptionAesTests
    {
        private const string TestKey = "TestKey1234567890";
        private const string TestPlaintext = "Hello GameFrameX!";
        private static readonly byte[] TestPlaintextBytes = Encoding.UTF8.GetBytes(TestPlaintext);

        #region AESEncryptSecure / AESDecryptSecure string round-trip

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

        #endregion

        #region AESEncrypt / AESDecrypt (obsolete delegates to Secure)

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

        #endregion

        #region Random IV/Salt produces different ciphertext

        [Test]
        public void AESEncryptSecure_SamePlaintext_ProducesDifferentCiphertext()
        {
            string encrypted1 = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintext, TestKey);
            string encrypted2 = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintext, TestKey);
            Assert.AreNotEqual(encrypted1, encrypted2);
        }

        [Test]
        public void AESEncryptSecure_Bytes_SamePlaintext_ProducesDifferentCiphertext()
        {
            byte[] encrypted1 = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, TestKey);
            byte[] encrypted2 = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, TestKey);
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
            Assert.IsFalse(allSame);
        }

        #endregion

        #region Ciphertext format

        [Test]
        public void AESEncryptSecure_CiphertextFormat_ContainsIVAndSalt()
        {
            byte[] encrypted = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, TestKey);
            Assert.Greater(encrypted.Length, 32 + TestPlaintextBytes.Length);
        }

        #endregion

        #region Error cases

        [Test]
        public void AESEncryptSecure_NullBytes_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Utility.Encryption.Aes.AESEncryptSecure((byte[])null, TestKey);
            });
        }

        [Test]
        public void AESEncryptSecure_EmptyBytes_Throws()
        {
            Assert.Throws<Exception>(() =>
            {
                Utility.Encryption.Aes.AESEncryptSecure(new byte[0], TestKey);
            });
        }

        [Test]
        public void AESEncryptSecure_NullKey_Throws()
        {
            Assert.Throws<Exception>(() =>
            {
                Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, null);
            });
        }

        [Test]
        public void AESEncryptSecure_EmptyKey_Throws()
        {
            Assert.Throws<Exception>(() =>
            {
                Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, "");
            });
        }

        [Test]
        public void AESDecryptSecure_NullBytes_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Utility.Encryption.Aes.AESDecryptSecure((byte[])null, TestKey);
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
        public void AESDecryptSecure_EmptyKey_Throws()
        {
            byte[] encrypted = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, TestKey);
            Assert.Throws<Exception>(() =>
            {
                Utility.Encryption.Aes.AESDecryptSecure(encrypted, "");
            });
        }

        [Test]
        public void AESDecryptSecure_WrongKey_Throws()
        {
            byte[] encrypted = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, TestKey);
            Assert.Throws<CryptographicException>(() =>
            {
                Utility.Encryption.Aes.AESDecryptSecure(encrypted, "WrongKey1234567890");
            });
        }

        #endregion

        #region Cross-method compatibility

        [Test]
        public void AESEncrypt_ProducesDataDecryptableBy_AESDecryptSecure()
        {
            #pragma warning disable CS0618
            byte[] encrypted = Utility.Encryption.Aes.AESEncrypt(TestPlaintextBytes, TestKey);
            #pragma warning restore CS0618
            byte[] decrypted = Utility.Encryption.Aes.AESDecryptSecure(encrypted, TestKey);
            CollectionAssert.AreEqual(TestPlaintextBytes, decrypted);
        }

        [Test]
        public void AESEncryptSecure_ProducesDataDecryptableBy_AESDecrypt()
        {
            byte[] encrypted = Utility.Encryption.Aes.AESEncryptSecure(TestPlaintextBytes, TestKey);
            #pragma warning disable CS0618
            byte[] decrypted = Utility.Encryption.Aes.AESDecrypt(encrypted, TestKey);
            #pragma warning restore CS0618
            CollectionAssert.AreEqual(TestPlaintextBytes, decrypted);
        }

        #endregion

        #region Unicode plaintext

        [Test]
        public void AESEncryptSecure_DecryptSecure_Unicode_RoundTrip()
        {
            string unicode = "你好世界 🎮";
            string encrypted = Utility.Encryption.Aes.AESEncryptSecure(unicode, TestKey);
            string decrypted = Utility.Encryption.Aes.AESDecryptSecure(encrypted, TestKey);
            Assert.AreEqual(unicode, decrypted);
        }

        #endregion

        #region Multiple round-trips

        [Test]
        public void AESEncryptSecure_MultipleRoundTrips()
        {
            for (int i = 0; i < 10; i++)
            {
                string plaintext = "Round trip test " + i;
                string encrypted = Utility.Encryption.Aes.AESEncryptSecure(plaintext, TestKey);
                string decrypted = Utility.Encryption.Aes.AESDecryptSecure(encrypted, TestKey);
                Assert.AreEqual(plaintext, decrypted, $"Failed at iteration {i}");
            }
        }

        #endregion
    }
}
