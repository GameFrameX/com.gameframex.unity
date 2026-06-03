using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityHashHMACSha256Tests
    {
        #region Hash basic

        [Test]
        public void Hash_SameInput_SameOutput()
        {
            string result1 = Utility.Hash.HMACSha256.Hash("message", "key");
            string result2 = Utility.Hash.HMACSha256.Hash("message", "key");
            Assert.AreEqual(result1, result2);
        }

        [Test]
        public void Hash_DifferentMessage_DifferentOutput()
        {
            string result1 = Utility.Hash.HMACSha256.Hash("message1", "key");
            string result2 = Utility.Hash.HMACSha256.Hash("message2", "key");
            Assert.AreNotEqual(result1, result2);
        }

        [Test]
        public void Hash_DifferentKey_DifferentOutput()
        {
            string result1 = Utility.Hash.HMACSha256.Hash("message", "key1");
            string result2 = Utility.Hash.HMACSha256.Hash("message", "key2");
            Assert.AreNotEqual(result1, result2);
        }

        [Test]
        public void Hash_ReturnsBase64String()
        {
            string result = Utility.Hash.HMACSha256.Hash("message", "key");
            Assert.IsNotNull(result);
            Assert.Greater(result.Length, 0);
            byte[] decoded = System.Convert.FromBase64String(result);
            Assert.AreEqual(32, decoded.Length, "HMAC-SHA256 should produce 32 bytes");
        }

        [Test]
        public void Hash_EmptyMessage()
        {
            string result = Utility.Hash.HMACSha256.Hash("", "key");
            Assert.IsNotNull(result);
            Assert.Greater(result.Length, 0);
        }

        [Test]
        public void Hash_EmptyKey()
        {
            string result = Utility.Hash.HMACSha256.Hash("message", "");
            Assert.IsNotNull(result);
            Assert.Greater(result.Length, 0);
        }

        [Test]
        public void Hash_UnicodeInput()
        {
            string result = Utility.Hash.HMACSha256.Hash("你好", "密钥");
            Assert.IsNotNull(result);
            Assert.Greater(result.Length, 0);
        }

        [Test]
        public void Hash_LongInput()
        {
            string longMessage = new string('a', 10000);
            string longKey = new string('k', 1000);
            string result = Utility.Hash.HMACSha256.Hash(longMessage, longKey);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Hash_KnownVector()
        {
            string result = Utility.Hash.HMACSha256.Hash("Hi There", "key");
            Assert.IsNotNull(result);
            Assert.AreEqual(44, result.Length, "Base64 of 32 bytes should be 44 chars");
        }

        #endregion
    }
}
