using System.IO;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityHashMd5Tests
    {
        #region Hash (string)

        [Test]
        public void Hash_KnownInput_ReturnsExpectedMd5()
        {
            string result = HashUtility.MD5.Hash("");
            Assert.AreEqual("d41d8cd98f00b204e9800998ecf8427e", result);
        }

        [Test]
        public void Hash_HelloWorld_ReturnsExpectedMd5()
        {
            string result = HashUtility.MD5.Hash("Hello World");
            Assert.AreEqual("b10a8db164e0754105b7a99be72e3fe5", result);
        }

        [Test]
        public void Hash_LowercaseAlphabet()
        {
            string result = HashUtility.MD5.Hash("abcdefghijklmnopqrstuvwxyz");
            Assert.AreEqual("c3fcd3d76192e4007dfb496cca67e13b", result);
        }

        [Test]
        public void Hash_NumericString()
        {
            string result = HashUtility.MD5.Hash("1234567890");
            Assert.AreEqual("e807f1fcf82d132f9bb018ca6738a19f", result);
        }

        [Test]
        public void Hash_SameInput_SameOutput()
        {
            string input = "consistency check";
            string hash1 = HashUtility.MD5.Hash(input);
            string hash2 = HashUtility.MD5.Hash(input);
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash_DifferentInput_DifferentOutput()
        {
            string hash1 = HashUtility.MD5.Hash("input1");
            string hash2 = HashUtility.MD5.Hash("input2");
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void Hash_Returns32CharHexString()
        {
            string result = HashUtility.MD5.Hash("test");
            Assert.AreEqual(32, result.Length);
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
            string result = HashUtility.MD5.Hash("你好");
            Assert.AreEqual(32, result.Length);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Hash_LongInput()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                sb.Append("a");
            }
            string result = HashUtility.MD5.Hash(sb.ToString());
            Assert.AreEqual(32, result.Length);
        }

        #endregion

        #region Hash (Stream)

        [Test]
        public void Hash_Stream_ReturnsExpectedMd5()
        {
            byte[] data = Encoding.UTF8.GetBytes("Hello World");
            using (MemoryStream stream = new MemoryStream(data))
            {
                string result = HashUtility.MD5.Hash(stream);
                Assert.AreEqual("b10a8db164e0754105b7a99be72e3fe5", result);
            }
        }

        [Test]
        public void Hash_EmptyStream()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                string result = HashUtility.MD5.Hash(stream);
                Assert.AreEqual("d41d8cd98f00b204e9800998ecf8427e", result);
            }
        }

        #endregion

        #region IsVerify

        [Test]
        public void IsVerify_SameHash_ReturnsTrue()
        {
            string hash = HashUtility.MD5.Hash("test");
            Assert.IsTrue(HashUtility.MD5.IsVerify(hash, hash));
        }

        [Test]
        public void IsVerify_DifferentHash_ReturnsFalse()
        {
            string hash1 = HashUtility.MD5.Hash("test1");
            string hash2 = HashUtility.MD5.Hash("test2");
            Assert.IsFalse(HashUtility.MD5.IsVerify(hash1, hash2));
        }

        [Test]
        public void IsVerify_CaseInsensitive()
        {
            string hash1 = HashUtility.MD5.Hash("test");
            string hash2 = hash1.ToUpper();
            Assert.IsTrue(HashUtility.MD5.IsVerify(hash1, hash2));
        }

        #endregion

        #region FileHash

        [Test]
        public void FileHash_KnownFile_ReturnsExpectedMd5()
        {
            string tempFile = Path.GetTempFileName();
            try
            {
                File.WriteAllText(tempFile, "Hello World");
                string result = HashUtility.MD5.FileHash(tempFile);
                Assert.AreEqual("b10a8db164e0754105b7a99be72e3fe5", result);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        #endregion
    }
}