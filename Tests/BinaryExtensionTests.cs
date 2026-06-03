using System;
using System.IO;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class BinaryExtensionTests
    {
        #region 7-Bit Encoded Int32

        [Test]
        public void WriteAndRead7BitEncodedInt32_Zero_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedInt32(0);
                ms.Position = 0;
                int result = reader.Read7BitEncodedInt32();

                Assert.AreEqual(0, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedInt32_SmallValue_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedInt32(127);
                ms.Position = 0;
                int result = reader.Read7BitEncodedInt32();

                Assert.AreEqual(127, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedInt32_LargeValue_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedInt32(12345678);
                ms.Position = 0;
                int result = reader.Read7BitEncodedInt32();

                Assert.AreEqual(12345678, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedInt32_NegativeValue_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedInt32(-1);
                ms.Position = 0;
                int result = reader.Read7BitEncodedInt32();

                Assert.AreEqual(-1, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedInt32_MaxValue_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedInt32(int.MaxValue);
                ms.Position = 0;
                int result = reader.Read7BitEncodedInt32();

                Assert.AreEqual(int.MaxValue, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedInt32_MinValue_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedInt32(int.MinValue);
                ms.Position = 0;
                int result = reader.Read7BitEncodedInt32();

                Assert.AreEqual(int.MinValue, result);
            }
        }

        #endregion

        #region 7-Bit Encoded UInt32

        [Test]
        public void WriteAndRead7BitEncodedUInt32_Zero_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedUInt32(0u);
                ms.Position = 0;
                uint result = reader.Read7BitEncodedUInt32();

                Assert.AreEqual(0u, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedUInt32_MaxValue_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedUInt32(uint.MaxValue);
                ms.Position = 0;
                uint result = reader.Read7BitEncodedUInt32();

                Assert.AreEqual(uint.MaxValue, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedUInt32_LargeValue_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedUInt32(3000000000u);
                ms.Position = 0;
                uint result = reader.Read7BitEncodedUInt32();

                Assert.AreEqual(3000000000u, result);
            }
        }

        #endregion

        #region 7-Bit Encoded Int64

        [Test]
        public void WriteAndRead7BitEncodedInt64_Zero_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedInt64(0L);
                ms.Position = 0;
                long result = reader.Read7BitEncodedInt64();

                Assert.AreEqual(0L, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedInt64_MaxValue_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedInt64(long.MaxValue);
                ms.Position = 0;
                long result = reader.Read7BitEncodedInt64();

                Assert.AreEqual(long.MaxValue, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedInt64_MinValue_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedInt64(long.MinValue);
                ms.Position = 0;
                long result = reader.Read7BitEncodedInt64();

                Assert.AreEqual(long.MinValue, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedInt64_LargePositive_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                long value = 12345678901234567L;
                writer.Write7BitEncodedInt64(value);
                ms.Position = 0;
                long result = reader.Read7BitEncodedInt64();

                Assert.AreEqual(value, result);
            }
        }

        #endregion

        #region 7-Bit Encoded UInt64

        [Test]
        public void WriteAndRead7BitEncodedUInt64_Zero_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedUInt64(0UL);
                ms.Position = 0;
                ulong result = reader.Read7BitEncodedUInt64();

                Assert.AreEqual(0UL, result);
            }
        }

        [Test]
        public void WriteAndRead7BitEncodedUInt64_MaxValue_RoundTrip()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                writer.Write7BitEncodedUInt64(ulong.MaxValue);
                ms.Position = 0;
                ulong result = reader.Read7BitEncodedUInt64();

                Assert.AreEqual(ulong.MaxValue, result);
            }
        }

        #endregion

        #region 7-Bit Encoding Size Verification

        [Test]
        public void Write7BitEncodedInt32_SmallValue_UsesSingleByte()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            {
                writer.Write7BitEncodedInt32(64);
                Assert.AreEqual(1, ms.Length);
            }
        }

        [Test]
        public void Write7BitEncodedInt32_LargeValue_UsesMultipleBytes()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            {
                writer.Write7BitEncodedInt32(16384);
                Assert.AreEqual(3, ms.Length);
            }
        }

        #endregion
    }
}
