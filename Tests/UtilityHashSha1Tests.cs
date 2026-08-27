using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityHashSha1Tests
    {
        #region Hash (UTF-8)

        [Test]
        public void Hash_EmptyString_ReturnsExpectedSha1()
        {
            string result = HashUtility.Sha1.Hash("");
            Assert.AreEqual("da39a3ee5e6b4b0d3255bfef95601890afd80709", result);
        }

        [Test]
        public void Hash_KnownInput_ReturnsExpectedSha1()
        {
            string result = HashUtility.Sha1.Hash("Hello World");
            Assert.AreEqual("0a4d55a8d778e5022fab701977c5d840bbc486d0", result);
        }

        [Test]
        public void Hash_AnotherKnownInput()
        {
            string result = HashUtility.Sha1.Hash("abcdefghijklmnopqrstuvwxyz");
            Assert.AreEqual("32d10c7b8cf96570ca04ce37f2a19d84240d3a89", result);
        }

        [Test]
        public void Hash_SameInput_SameOutput()
        {
            string hash1 = HashUtility.Sha1.Hash("deterministic");
            string hash2 = HashUtility.Sha1.Hash("deterministic");
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash_DifferentInput_DifferentOutput()
        {
            string hash1 = HashUtility.Sha1.Hash("foo");
            string hash2 = HashUtility.Sha1.Hash("bar");
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void Hash_Returns40CharHexString()
        {
            string result = HashUtility.Sha1.Hash("test");
            Assert.AreEqual(40, result.Length);
            foreach (char c in result)
            {
                Assert.IsTrue(
                    (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f'),
                    $"Character '{c}' is not a lowercase hex digit");
            }
        }

        [Test]
        public void Hash_UnicodeInput()
        {
            string result = HashUtility.Sha1.Hash("中文");
            Assert.AreEqual(40, result.Length);
        }

        [Test]
        public void Hash_LongInput()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                sb.Append("x");
            }
            string result = HashUtility.Sha1.Hash(sb.ToString());
            Assert.AreEqual(40, result.Length);
        }

        #endregion

        #region Hash (custom encoding)

        [Test]
        public void Hash_WithUnicodeEncoding()
        {
            string utf8Hash = HashUtility.Sha1.Hash("test", Encoding.UTF8);
            string unicodeHash = HashUtility.Sha1.Hash("test", Encoding.Unicode);
            Assert.AreEqual(40, utf8Hash.Length);
            Assert.AreEqual(40, unicodeHash.Length);
            Assert.AreNotEqual(utf8Hash, unicodeHash);
        }

        [Test]
        public void Hash_WithASCII_SameAsUTF8ForASCII()
        {
            string input = "ASCII only";
            string utf8Result = HashUtility.Sha1.Hash(input, Encoding.UTF8);
            string asciiResult = HashUtility.Sha1.Hash(input, Encoding.ASCII);
            Assert.AreEqual(utf8Result, asciiResult);
        }

        #endregion
    }
}