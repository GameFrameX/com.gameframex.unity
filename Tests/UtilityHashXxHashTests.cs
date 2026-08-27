using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityHashXxHashTests
    {
        #region Hash32 (byte[])

        [Test]
        public void Hash32_EmptyArray()
        {
            uint result = HashUtility.XXHash.Hash32(new byte[0]);
            Assert.AreNotEqual(0u, result);
        }

        [Test]
        public void Hash32_SameInput_SameOutput()
        {
            byte[] data = new byte[] { 1, 2, 3, 4, 5 };
            uint hash1 = HashUtility.XXHash.Hash32(data);
            uint hash2 = HashUtility.XXHash.Hash32(data);
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash32_DifferentInput_DifferentOutput()
        {
            byte[] data1 = new byte[] { 1, 2, 3 };
            byte[] data2 = new byte[] { 4, 5, 6 };
            uint hash1 = HashUtility.XXHash.Hash32(data1);
            uint hash2 = HashUtility.XXHash.Hash32(data2);
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void Hash32_LargeInput()
        {
            byte[] data = new byte[10000];
            new Random().NextBytes(data);
            uint result = HashUtility.XXHash.Hash32(data);
            Assert.AreNotEqual(0u, result);
        }

        #endregion

        #region Hash32 (string)

        [Test]
        public void Hash32_EmptyString()
        {
            uint result = HashUtility.XXHash.Hash32("");
            Assert.AreNotEqual(0u, result);
        }

        [Test]
        public void Hash32_SameString_SameOutput()
        {
            uint hash1 = HashUtility.XXHash.Hash32("test");
            uint hash2 = HashUtility.XXHash.Hash32("test");
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash32_DifferentString_DifferentOutput()
        {
            uint hash1 = HashUtility.XXHash.Hash32("foo");
            uint hash2 = HashUtility.XXHash.Hash32("bar");
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void Hash32_UnicodeString()
        {
            uint result = HashUtility.XXHash.Hash32("中文测试");
            Assert.AreNotEqual(0u, result);
        }

        #endregion

        #region Hash32 (Type)

        [Test]
        public void Hash32_Type_SameType_SameOutput()
        {
            uint hash1 = HashUtility.XXHash.Hash32(typeof(string));
            uint hash2 = HashUtility.XXHash.Hash32(typeof(string));
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash32_Type_DifferentType_DifferentOutput()
        {
            uint hash1 = HashUtility.XXHash.Hash32(typeof(string));
            uint hash2 = HashUtility.XXHash.Hash32(typeof(int));
            Assert.AreNotEqual(hash1, hash2);
        }

        #endregion

        #region Hash32<T>

        [Test]
        public void Hash32_Generic_SameType_SameOutput()
        {
            uint hash1 = HashUtility.XXHash.Hash32<string>();
            uint hash2 = HashUtility.XXHash.Hash32<string>();
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash32_Generic_MatchesTypeOverload()
        {
            uint hash1 = HashUtility.XXHash.Hash32(typeof(int));
            uint hash2 = HashUtility.XXHash.Hash32<int>();
            Assert.AreEqual(hash1, hash2);
        }

        #endregion

        #region Hash64 (byte[])

        [Test]
        public void Hash64_EmptyArray()
        {
            ulong result = HashUtility.XXHash.Hash64(new byte[0]);
            Assert.AreNotEqual(0ul, result);
        }

        [Test]
        public void Hash64_SameInput_SameOutput()
        {
            byte[] data = new byte[] { 10, 20, 30 };
            ulong hash1 = HashUtility.XXHash.Hash64(data);
            ulong hash2 = HashUtility.XXHash.Hash64(data);
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Hash64_DifferentInput_DifferentOutput()
        {
            byte[] data1 = new byte[] { 1, 2, 3 };
            byte[] data2 = new byte[] { 3, 2, 1 };
            ulong hash1 = HashUtility.XXHash.Hash64(data1);
            ulong hash2 = HashUtility.XXHash.Hash64(data2);
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void Hash64_LargeInput()
        {
            byte[] data = new byte[10000];
            new Random().NextBytes(data);
            ulong result = HashUtility.XXHash.Hash64(data);
            Assert.AreNotEqual(0ul, result);
        }

        #endregion

        #region Hash64 (string)

        [Test]
        public void Hash64_EmptyString()
        {
            ulong result = HashUtility.XXHash.Hash64("");
            Assert.AreNotEqual(0ul, result);
        }

        [Test]
        public void Hash64_SameString_SameOutput()
        {
            ulong hash1 = HashUtility.XXHash.Hash64("test");
            ulong hash2 = HashUtility.XXHash.Hash64("test");
            Assert.AreEqual(hash1, hash2);
        }

        #endregion

        #region Hash64 (Type)

        [Test]
        public void Hash64_Type_SameType_SameOutput()
        {
            ulong hash1 = HashUtility.XXHash.Hash64(typeof(string));
            ulong hash2 = HashUtility.XXHash.Hash64(typeof(string));
            Assert.AreEqual(hash1, hash2);
        }

        #endregion

        #region Hash64<T>

        [Test]
        public void Hash64_Generic_MatchesTypeOverload()
        {
            ulong hash1 = HashUtility.XXHash.Hash64(typeof(string));
            ulong hash2 = HashUtility.XXHash.Hash64<string>();
            Assert.AreEqual(hash1, hash2);
        }

        #endregion

        #region Cross-32-64 consistency

        [Test]
        public void Hash32_And_Hash64_BothNonZero()
        {
            string input = "cross-check";
            uint h32 = HashUtility.XXHash.Hash32(input);
            ulong h64 = HashUtility.XXHash.Hash64(input);
            Assert.AreNotEqual(0u, h32);
            Assert.AreNotEqual(0ul, h64);
        }

        #endregion
    }
}