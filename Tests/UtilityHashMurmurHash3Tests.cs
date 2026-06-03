using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityHashMurmurHash3Tests
    {
        #region Hash basic

        [Test]
        public void Hash_SameInput_SameOutput()
        {
            uint hash1 = Utility.Hash.MurmurHash3.Hash("test");
            uint hash2 = Utility.Hash.MurmurHash3.Hash("test");
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash_DifferentInput_DifferentOutput()
        {
            uint hash1 = Utility.Hash.MurmurHash3.Hash("input1");
            uint hash2 = Utility.Hash.MurmurHash3.Hash("input2");
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void Hash_EmptyString()
        {
            uint result = Utility.Hash.MurmurHash3.Hash("");
            Assert.AreNotEqual(0u, result);
        }

        [Test]
        public void Hash_SingleChar()
        {
            uint result = Utility.Hash.MurmurHash3.Hash("a");
            Assert.AreNotEqual(0u, result);
        }

        [Test]
        public void Hash_DefaultSeed_Seed27()
        {
            uint hash1 = Utility.Hash.MurmurHash3.Hash("test", 27);
            uint hash2 = Utility.Hash.MurmurHash3.Hash("test");
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash_DifferentSeed_DifferentResult()
        {
            uint hash1 = Utility.Hash.MurmurHash3.Hash("test", 0);
            uint hash2 = Utility.Hash.MurmurHash3.Hash("test", 1);
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void Hash_SameSeed_SameResult()
        {
            uint hash1 = Utility.Hash.MurmurHash3.Hash("test", 42);
            uint hash2 = Utility.Hash.MurmurHash3.Hash("test", 42);
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash_MultipleLengths()
        {
            for (int len = 0; len < 64; len++)
            {
                string input = new string('a', len);
                uint result = Utility.Hash.MurmurHash3.Hash(input);
                Assert.AreNotEqual(0u, result, $"Hash for length {len} should not be zero (but may be)");
            }
        }

        [Test]
        public void Hash_4ByteAlignedInput()
        {
            string input = "abcd";
            uint hash1 = Utility.Hash.MurmurHash3.Hash(input);
            uint hash2 = Utility.Hash.MurmurHash3.Hash(input);
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash_NonAlignedInput()
        {
            string input = "abcde";
            uint hash1 = Utility.Hash.MurmurHash3.Hash(input);
            uint hash2 = Utility.Hash.MurmurHash3.Hash(input);
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash_UnicodeInput()
        {
            uint result = Utility.Hash.MurmurHash3.Hash("你好世界");
            Assert.AreNotEqual(0u, result);
        }

        [Test]
        public void Hash_LongInput()
        {
            string input = new string('z', 10000);
            uint result = Utility.Hash.MurmurHash3.Hash(input);
            Assert.AreNotEqual(0u, result);
        }

        [Test]
        public void Hash_SeedZero()
        {
            uint result = Utility.Hash.MurmurHash3.Hash("test", 0);
            Assert.AreNotEqual(0u, result);
        }

        [Test]
        public void Hash_LargeSeed()
        {
            uint result = Utility.Hash.MurmurHash3.Hash("test", uint.MaxValue);
            Assert.AreNotEqual(0u, result);
        }

        #endregion
    }
}
