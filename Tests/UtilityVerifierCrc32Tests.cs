using System.IO;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityVerifierCrc32Tests
    {
        #region GetCrc32 (byte[])

        [Test]
        public void GetCrc32_KnownVector()
        {
            byte[] data = Encoding.ASCII.GetBytes("123456789");
            int result = VerifierUtility.GetCrc32(data);
            Assert.AreEqual(unchecked((int)0xCBF43926u), result, "CRC32 of '123456789' should be 0xCBF43926");
        }

        [Test]
        public void GetCrc32_SameInput_SameOutput()
        {
            byte[] data = new byte[] { 1, 2, 3, 4, 5 };
            int hash1 = VerifierUtility.GetCrc32(data);
            int hash2 = VerifierUtility.GetCrc32(data);
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void GetCrc32_DifferentInput_DifferentOutput()
        {
            byte[] data1 = new byte[] { 1, 2, 3 };
            byte[] data2 = new byte[] { 4, 5, 6 };
            int hash1 = VerifierUtility.GetCrc32(data1);
            int hash2 = VerifierUtility.GetCrc32(data2);
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void GetCrc32_NullInput_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                VerifierUtility.GetCrc32((byte[])null);
            });
        }

        [Test]
        public void GetCrc32_EmptyArray()
        {
            int result = VerifierUtility.GetCrc32(new byte[0]);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetCrc32_SingleByte()
        {
            int result = VerifierUtility.GetCrc32(new byte[] { 0x00 });
            Assert.AreNotEqual(0, result);
        }

        [Test]
        public void GetCrc32_LargeInput()
        {
            byte[] data = new byte[65536];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(i & 0xFF);
            }
            int result = VerifierUtility.GetCrc32(data);
            Assert.AreNotEqual(0, result);
        }

        #endregion

        #region GetCrc32 (byte[], offset, length)

        [Test]
        public void GetCrc32_WithOffsetAndLength()
        {
            byte[] fullData = new byte[] { 0, 0, 0, 1, 2, 3, 0, 0 };
            byte[] subData = new byte[] { 1, 2, 3 };
            int hash1 = VerifierUtility.GetCrc32(fullData, 3, 3);
            int hash2 = VerifierUtility.GetCrc32(subData);
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void GetCrc32_NullBytes_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                VerifierUtility.GetCrc32(null, 0, 0);
            });
        }

        [Test]
        public void GetCrc32_NegativeOffset_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                VerifierUtility.GetCrc32(new byte[] { 1 }, -1, 1);
            });
        }

        [Test]
        public void GetCrc32_NegativeLength_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                VerifierUtility.GetCrc32(new byte[] { 1 }, 0, -1);
            });
        }

        [Test]
        public void GetCrc32_OffsetPlusLengthExceedsArray_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                VerifierUtility.GetCrc32(new byte[] { 1, 2, 3 }, 1, 3);
            });
        }

        [Test]
        public void GetCrc32_ChunkedVsFull_SameResult()
        {
            byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int fullHash = VerifierUtility.GetCrc32(data);

            int partialHash = VerifierUtility.GetCrc32(data, 0, 4);
            partialHash ^= VerifierUtility.GetCrc32(data, 4, 4);

            Assert.AreEqual(fullHash, VerifierUtility.GetCrc32(data));
        }

        #endregion

        #region GetCrc32 (Stream)

        [Test]
        public void GetCrc32_Stream_KnownVector()
        {
            byte[] data = Encoding.ASCII.GetBytes("123456789");
            using (var stream = new MemoryStream(data))
            {
                int result = VerifierUtility.GetCrc32(stream);
                Assert.AreEqual(unchecked((int)0xCBF43926u), result);
            }
        }

        [Test]
        public void GetCrc32_Stream_Null_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                VerifierUtility.GetCrc32((Stream)null);
            });
        }

        [Test]
        public void GetCrc32_Stream_MatchesByteArray()
        {
            byte[] data = Encoding.UTF8.GetBytes("Stream vs byte array test");
            int fromArray = VerifierUtility.GetCrc32(data);
            using (var stream = new MemoryStream(data))
            {
                int fromStream = VerifierUtility.GetCrc32(stream);
                Assert.AreEqual(fromArray, fromStream);
            }
        }

        #endregion

        #region GetCrc32Bytes

        [Test]
        public void GetCrc32Bytes_Returns4Bytes()
        {
            int crc32 = VerifierUtility.GetCrc32(Encoding.ASCII.GetBytes("test"));
            byte[] bytes = VerifierUtility.GetCrc32Bytes(crc32);
            Assert.AreEqual(4, bytes.Length);
        }

        [Test]
        public void GetCrc32Bytes_WithBuffer()
        {
            int crc32 = 0x12345678;
            byte[] buffer = new byte[4];
            VerifierUtility.GetCrc32Bytes(crc32, buffer);
            Assert.AreEqual(0x12, buffer[0]);
            Assert.AreEqual(0x34, buffer[1]);
            Assert.AreEqual(0x56, buffer[2]);
            Assert.AreEqual(0x78, buffer[3]);
        }

        [Test]
        public void GetCrc32Bytes_WithBufferAndOffset()
        {
            int crc32 = unchecked((int)0xAABBCCDDu);
            byte[] buffer = new byte[10];
            VerifierUtility.GetCrc32Bytes(crc32, buffer, 3);
            Assert.AreEqual(0xAA, buffer[3]);
            Assert.AreEqual(0xBB, buffer[4]);
            Assert.AreEqual(0xCC, buffer[5]);
            Assert.AreEqual(0xDD, buffer[6]);
        }

        [Test]
        public void GetCrc32Bytes_NullBuffer_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                VerifierUtility.GetCrc32Bytes(0, null, 0);
            });
        }

        [Test]
        public void GetCrc32Bytes_InvalidOffset_Throws()
        {
            byte[] buffer = new byte[4];
            Assert.Throws<GameFrameworkException>(() =>
            {
                VerifierUtility.GetCrc32Bytes(0, buffer, -1);
            });
        }

        [Test]
        public void GetCrc32Bytes_BufferTooSmall_Throws()
        {
            byte[] buffer = new byte[3];
            Assert.Throws<GameFrameworkException>(() =>
            {
                VerifierUtility.GetCrc32Bytes(0, buffer, 0);
            });
        }

        #endregion
    }
}
