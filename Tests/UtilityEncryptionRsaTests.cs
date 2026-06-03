using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityEncryptionRsaTests
    {
        private Dictionary<string, string> _keys;
        private string _publicKey;
        private string _privateKey;

        [SetUp]
        public void SetUp()
        {
            _keys = Utility.Encryption.Rsa.Make();
            _publicKey = _keys["publicKey"];
            _privateKey = _keys["privateKey"];
        }

        #region Make (key generation)

        [Test]
        public void Make_ReturnsPrivateKeyAndPublicKey()
        {
            Assert.IsTrue(_keys.ContainsKey("privateKey"));
            Assert.IsTrue(_keys.ContainsKey("publicKey"));
            Assert.IsNotNull(_keys["privateKey"]);
            Assert.IsNotNull(_keys["publicKey"]);
            Assert.IsNotEmpty(_keys["privateKey"]);
            Assert.IsNotEmpty(_keys["publicKey"]);
        }

        [Test]
        public void Make_PrivateKeyContainsPublicData()
        {
            Assert.IsTrue(_keys["privateKey"].Contains("<Modulus>"));
            Assert.IsTrue(_keys["publicKey"].Contains("<Modulus>"));
        }

        #endregion

        #region Static encrypt/decrypt round-trip

        [Test]
        public void RSAEncrypt_RSADecrypt_String_RoundTrip()
        {
            string plaintext = "Hello RSA!";
            string encrypted = Utility.Encryption.Rsa.RSAEncrypt(_publicKey, plaintext);
            string decrypted = Utility.Encryption.Rsa.RSADecrypt(_privateKey, encrypted);
            Assert.AreEqual(plaintext, decrypted);
        }

        [Test]
        public void RSAEncrypt_RSADecrypt_Bytes_RoundTrip()
        {
            byte[] data = Encoding.UTF8.GetBytes("Byte data test");
            byte[] encrypted = Utility.Encryption.Rsa.RSAEncrypt(_publicKey, data);
            byte[] decrypted = Utility.Encryption.Rsa.RSADecrypt(_privateKey, encrypted);
            CollectionAssert.AreEqual(data, decrypted);
        }

        #endregion

        #region Instance encrypt/decrypt round-trip

        [Test]
        public void Instance_Encrypt_Decrypt_String_RoundTrip()
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_privateKey);
                var rsaInstance = new Utility.Encryption.Rsa(rsa);

                string plaintext = "Instance test";
                string encrypted = rsaInstance.Encrypt(plaintext);
                string decrypted = rsaInstance.Decrypt(encrypted);
                Assert.AreEqual(plaintext, decrypted);
            }
        }

        [Test]
        public void Instance_Encrypt_Decrypt_Bytes_RoundTrip()
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_privateKey);
                var rsaInstance = new Utility.Encryption.Rsa(rsa);

                byte[] data = Encoding.UTF8.GetBytes("Instance bytes");
                byte[] encrypted = rsaInstance.Encrypt(data);
                byte[] decrypted = rsaInstance.Decrypt(encrypted);
                CollectionAssert.AreEqual(data, decrypted);
            }
        }

        #endregion

        #region Instance with key string

        [Test]
        public void Instance_CreateWithKeyString_RoundTrip()
        {
            var rsaInstance = new Utility.Encryption.Rsa(_privateKey);
            string plaintext = "Key string test";
            string encrypted = rsaInstance.Encrypt(plaintext);
            string decrypted = rsaInstance.Decrypt(encrypted);
            Assert.AreEqual(plaintext, decrypted);
        }

        #endregion

        #region Sign/Verify

        [Test]
        public void RSASignData_RSAVerifyData_Bytes_RoundTrip()
        {
            byte[] data = Encoding.UTF8.GetBytes("Sign this data");
            byte[] signature = Utility.Encryption.Rsa.RSASignData(data, _privateKey);
            Assert.IsNotNull(signature);
            Assert.Greater(signature.Length, 0);

            bool verified = Utility.Encryption.Rsa.RSAVerifyData(data, signature, _publicKey);
            Assert.IsTrue(verified);
        }

        [Test]
        public void RSASignData_RSAVerifyData_String_RoundTrip()
        {
            string data = "String to sign";
            string signature = Utility.Encryption.Rsa.RSASignData(data, _privateKey);
            Assert.IsNotNull(signature);

            bool verified = Utility.Encryption.Rsa.RSAVerifyData(data, signature, _publicKey);
            Assert.IsTrue(verified);
        }

        [Test]
        public void Instance_SignData_VerifyData_RoundTrip()
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_privateKey);
                var rsaInstance = new Utility.Encryption.Rsa(rsa);

                byte[] data = Encoding.UTF8.GetBytes("Instance sign");
                byte[] signature = rsaInstance.SignData(data);
                bool verified = rsaInstance.VerifyData(data, signature);
                Assert.IsTrue(verified);
            }
        }

        [Test]
        public void Instance_SignData_VerifyData_String_RoundTrip()
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_privateKey);
                var rsaInstance = new Utility.Encryption.Rsa(rsa);

                string data = "Instance string sign";
                string signature = rsaInstance.SignData(data);
                bool verified = rsaInstance.VerifyData(data, signature);
                Assert.IsTrue(verified);
            }
        }

        [Test]
        public void RSAVerifyData_WrongData_ReturnsFalse()
        {
            byte[] data = Encoding.UTF8.GetBytes("Original data");
            byte[] signature = Utility.Encryption.Rsa.RSASignData(data, _privateKey);
            byte[] wrongData = Encoding.UTF8.GetBytes("Wrong data");
            bool verified = Utility.Encryption.Rsa.RSAVerifyData(wrongData, signature, _publicKey);
            Assert.IsFalse(verified);
        }

        [Test]
        public void RSAVerifyData_WrongSignature_ReturnsFalse()
        {
            byte[] data = Encoding.UTF8.GetBytes("Original data");
            byte[] signature = Utility.Encryption.Rsa.RSASignData(data, _privateKey);
            signature[0] = (byte)(signature[0] ^ 0xFF);
            bool verified = Utility.Encryption.Rsa.RSAVerifyData(data, signature, _publicKey);
            Assert.IsFalse(verified);
        }

        #endregion

        #region Error cases

        [Test]
        public void RSASignData_InvalidKey_Throws()
        {
            byte[] data = Encoding.UTF8.GetBytes("test");
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Encryption.Rsa.RSASignData(data, "invalid key");
            });
        }

        [Test]
        public void RSAVerifyData_InvalidKey_Throws()
        {
            byte[] data = Encoding.UTF8.GetBytes("test");
            byte[] sig = new byte[128];
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Encryption.Rsa.RSAVerifyData(data, sig, "invalid key");
            });
        }

        #endregion

        #region Unicode

        [Test]
        public void RSAEncrypt_RSADecrypt_Unicode_RoundTrip()
        {
            string plaintext = "你好世界 🌍";
            string encrypted = Utility.Encryption.Rsa.RSAEncrypt(_publicKey, plaintext);
            string decrypted = Utility.Encryption.Rsa.RSADecrypt(_privateKey, encrypted);
            Assert.AreEqual(plaintext, decrypted);
        }

        #endregion
    }
}
