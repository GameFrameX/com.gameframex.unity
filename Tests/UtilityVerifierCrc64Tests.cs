using System;
using System.IO;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityVerifierCrc64Tests
    {
        #region GetCrc64 (byte[] via Utility.Verifier)

        [Test]
        public void GetCrc64_SameInput_SameOutput()
        {
            byte[] data = Encoding.ASCII.GetBytes("123456789");
            ulong hash1 = Utility.Verifier.GetCrc64(data);
            ulong hash2 = Utility.Verifier.GetCrc64(data);
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void GetCrc64_DifferentInput_DifferentOutput()
        {
            byte[] data1 = Encoding.ASCII.GetBytes("foo");
            byte[] data2 = Encoding.ASCII.GetBytes("bar");
            ulong hash1 = Utility.Verifier.GetCrc64(data1);
            ulong hash2 = Utility.Verifier.GetCrc64(data2);
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void GetCrc64_EmptyArray()
        {
            ulong result = Utility.Verifier.GetCrc64(new byte[0]);
            Assert.AreEqual(0ul, result);
        }

        [Test]
        public void GetCrc64_SingleByte()
        {
            ulong result = Utility.Verifier.GetCrc64(new byte[] { 42 });
            Assert.AreNotEqual(0ul, result);
        }

        [Test]
        public void GetCrc64_LargeInput()
        {
            byte[] data = new byte[65536];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(i & 0xFF);
            }
            ulong result = Utility.Verifier.GetCrc64(data);
            Assert.AreNotEqual(0ul, result);
        }

        #endregion

        #region GetCrc64 (Stream)

        [Test]
        public void GetCrc64_Stream_MatchesByteArray()
        {
            byte[] data = Encoding.UTF8.GetBytes("Stream vs byte array test");
            ulong fromArray = Utility.Verifier.GetCrc64(data);
            using (var stream = new MemoryStream(data))
            {
                ulong fromStream = Utility.Verifier.GetCrc64(stream);
                Assert.AreEqual(fromArray, fromStream);
            }
        }

        [Test]
        public void GetCrc64_Stream_Null_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Utility.Verifier.GetCrc64((Stream)null);
            });
        }

        [Test]
        public void GetCrc64_Stream_EmptyStream()
        {
            using (var stream = new MemoryStream())
            {
                ulong result = Utility.Verifier.GetCrc64(stream);
                Assert.AreEqual(0ul, result);
            }
        }

        #endregion

        #region Crc64 instance API

        [Test]
        public void Crc64_Hash_NullInput_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Utility.Verifier.Crc64.Hash(null);
            });
        }

        [Test]
        public void Crc64_Hash_EmptyArray()
        {
            byte[] result = Utility.Verifier.Crc64.Hash(new byte[0]);
            Assert.AreEqual(8, result.Length);
            foreach (byte b in result)
            {
                Assert.AreEqual(0, b);
            }
        }

        [Test]
        public void Crc64_Hash_SameInput_SameOutput()
        {
            byte[] data = new byte[] { 1, 2, 3, 4, 5 };
            byte[] hash1 = Utility.Verifier.Crc64.Hash(data);
            byte[] hash2 = Utility.Verifier.Crc64.Hash(data);
            CollectionAssert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Crc64_Hash_Returns8Bytes()
        {
            byte[] data = new byte[] { 42 };
            byte[] result = Utility.Verifier.Crc64.Hash(data);
            Assert.AreEqual(8, result.Length);
        }

        [Test]
        public void Crc64_HashToUInt64_EmptyInput_IsZero()
        {
            ulong result = Utility.Verifier.Crc64.HashToUInt64(new byte[0]);
            Assert.AreEqual(0ul, result);
        }

        [Test]
        public void Crc64_HashToUInt64_SameInput_SameOutput()
        {
            byte[] data = new byte[] { 10, 20, 30 };
            ulong hash1 = Utility.Verifier.Crc64.HashToUInt64(data);
            ulong hash2 = Utility.Verifier.Crc64.HashToUInt64(data);
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Crc64_AppendAndGetHash()
        {
            var crc = new Utility.Verifier.Crc64();
            crc.Append(Encoding.ASCII.GetBytes("test data"));
            byte[] hash = crc.GetCurrentHash();
            Assert.AreEqual(8, hash.Length);
        }

        [Test]
        public void Crc64_ResetClearsState()
        {
            var crc = new Utility.Verifier.Crc64();
            crc.Append(new byte[] { 1, 2, 3 });
            crc.Reset();
            Assert.AreEqual(0ul, crc.GetCurrentHashAsUInt64());
        }

        [Test]
        public void Crc64_AppendInChunks_SameAsFull()
        {
            byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            var crc1 = new Utility.Verifier.Crc64();
            crc1.Append(data);

            var crc2 = new Utility.Verifier.Crc64();
            crc2.Append(new byte[] { 1, 2, 3, 4 });
            crc2.Append(new byte[] { 5, 6, 7, 8 });

            Assert.AreEqual(crc1.GetCurrentHashAsUInt64(), crc2.GetCurrentHashAsUInt64());
        }

        [Test]
        public void Crc64_GetHashAndReset_ClearsState()
        {
            var crc = new Utility.Verifier.Crc64();
            crc.Append(new byte[] { 1, 2, 3 });
            byte[] hash = crc.GetHashAndReset();
            Assert.AreEqual(8, hash.Length);
            Assert.AreEqual(0ul, crc.GetCurrentHashAsUInt64());
        }

        [Test]
        public void Crc64_HashLengthInBytes_Is8()
        {
            var crc = new Utility.Verifier.Crc64();
            Assert.AreEqual(8, crc.HashLengthInBytes);
        }

        [Test]
        public void Crc64_GetCurrentHashTwice_SameResult()
        {
            var crc = new Utility.Verifier.Crc64();
            crc.Append(new byte[] { 42 });
            byte[] hash1 = crc.GetCurrentHash();
            byte[] hash2 = crc.GetCurrentHash();
            CollectionAssert.AreEqual(hash1, hash2);
        }

        [Test]
        public void Crc64_AppendByteArray_Null_Throws()
        {
            var crc = new Utility.Verifier.Crc64();
            Assert.Throws<ArgumentNullException>(() =>
            {
                crc.Append((byte[])null);
            });
        }

        [Test]
        public void Crc64_AppendStream_Null_Throws()
        {
            var crc = new Utility.Verifier.Crc64();
            Assert.Throws<ArgumentNullException>(() =>
            {
                crc.Append((Stream)null);
            });
        }

        [Test]
        public void Crc64_TryHash_DestinationTooSmall_ReturnsFalse()
        {
            Span<byte> dest = stackalloc byte[4];
            bool result = Utility.Verifier.Crc64.TryHash(new byte[] { 1 }, dest, out int written);
            Assert.IsFalse(result);
            Assert.AreEqual(0, written);
        }

        [Test]
        public void Crc64_TryHash_DestinationLargeEnough_ReturnsTrue()
        {
            Span<byte> dest = stackalloc byte[8];
            ReadOnlySpan<byte> source = new ReadOnlySpan<byte>(new byte[] { 1, 2, 3 });
            bool result = Utility.Verifier.Crc64.TryHash(source, dest, out int written);
            Assert.IsTrue(result);
            Assert.AreEqual(8, written);
        }

        [Test]
        public void Crc64_AppendStream()
        {
            byte[] data = Encoding.ASCII.GetBytes("stream test");
            using (var stream = new MemoryStream(data))
            {
                var crc = new Utility.Verifier.Crc64();
                crc.Append(stream);
                byte[] hash = crc.GetCurrentHash();
                Assert.AreEqual(8, hash.Length);
            }
        }

        #endregion
    }
}
