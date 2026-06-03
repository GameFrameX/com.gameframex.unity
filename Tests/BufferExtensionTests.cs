using System;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class BufferExtensionTests
    {
        #region Constants

        [Test]
        public void Constants_HaveCorrectValues()
        {
            Assert.AreEqual(4, BufferExtension.IntSize);
            Assert.AreEqual(4, BufferExtension.UIntSize);
            Assert.AreEqual(2, BufferExtension.ShortSize);
            Assert.AreEqual(2, BufferExtension.UShortSize);
            Assert.AreEqual(8, BufferExtension.LongSize);
            Assert.AreEqual(4, BufferExtension.FloatSize);
            Assert.AreEqual(8, BufferExtension.DoubleSize);
            Assert.AreEqual(1, BufferExtension.ByteSize);
            Assert.AreEqual(1, BufferExtension.SbyteSize);
            Assert.AreEqual(1, BufferExtension.BoolSize);
        }

        #endregion

        #region Write/Read Int

        [Test]
        public void WriteAndReadInt_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteInt(12345, ref offset);

            Assert.AreEqual(4, offset);

            offset = 0;
            int result = buffer.ReadInt(ref offset);

            Assert.AreEqual(12345, result);
        }

        [Test]
        public void WriteAndReadInt_Negative_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteInt(-99999, ref offset);

            offset = 0;
            int result = buffer.ReadInt(ref offset);

            Assert.AreEqual(-99999, result);
        }

        [Test]
        public void WriteAndReadInt_MaxValue_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteInt(int.MaxValue, ref offset);

            offset = 0;
            int result = buffer.ReadInt(ref offset);

            Assert.AreEqual(int.MaxValue, result);
        }

        [Test]
        public void WriteInt_OutOfBounds_ThrowsArgumentOutOfRangeException()
        {
            var buffer = new byte[2];
            int offset = 0;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                buffer.WriteInt(1, ref offset);
            });
        }

        [Test]
        public void ReadInt_OutOfBounds_ThrowsArgumentOutOfRangeException()
        {
            var buffer = new byte[2];
            int offset = 0;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                buffer.ReadInt(ref offset);
            });
        }

        [Test]
        public void WriteInt_NegativeOffset_ThrowsArgumentOutOfRangeException()
        {
            var buffer = new byte[16];
            int offset = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                buffer.WriteInt(1, ref offset);
            });
        }

        #endregion

        #region Write/Read UInt

        [Test]
        public void WriteAndReadUInt_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteUInt(123456789u, ref offset);

            offset = 0;
            uint result = buffer.ReadUInt(ref offset);

            Assert.AreEqual(123456789u, result);
        }

        [Test]
        public void WriteAndReadUInt_MaxValue_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteUInt(uint.MaxValue, ref offset);

            offset = 0;
            uint result = buffer.ReadUInt(ref offset);

            Assert.AreEqual(uint.MaxValue, result);
        }

        #endregion

        #region Write/Read Short

        [Test]
        public void WriteAndReadShort_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteShort(1234, ref offset);

            offset = 0;
            short result = buffer.ReadShort(ref offset);

            Assert.AreEqual(1234, result);
        }

        [Test]
        public void WriteAndReadShort_Negative_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteShort(-1234, ref offset);

            offset = 0;
            short result = buffer.ReadShort(ref offset);

            Assert.AreEqual(-1234, result);
        }

        #endregion

        #region Write/Read UShort

        [Test]
        public void WriteAndReadUShort_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteUShort(65000, ref offset);

            offset = 0;
            ushort result = buffer.ReadUShort(ref offset);

            Assert.AreEqual(65000, result);
        }

        #endregion

        #region Write/Read Long

        [Test]
        public void WriteAndReadLong_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteLong(123456789012345L, ref offset);

            offset = 0;
            long result = buffer.ReadLong(ref offset);

            Assert.AreEqual(123456789012345L, result);
        }

        [Test]
        public void WriteAndReadLong_MaxValue_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteLong(long.MaxValue, ref offset);

            offset = 0;
            long result = buffer.ReadLong(ref offset);

            Assert.AreEqual(long.MaxValue, result);
        }

        #endregion

        #region Write/Read Float

        [Test]
        public void WriteAndReadFloat_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteFloat(3.14f, ref offset);

            offset = 0;
            float result = buffer.ReadFloat(ref offset);

            Assert.AreEqual(3.14f, result, 0.0001f);
        }

        [Test]
        public void WriteAndReadFloat_Zero_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteFloat(0.0f, ref offset);

            offset = 0;
            float result = buffer.ReadFloat(ref offset);

            Assert.AreEqual(0.0f, result);
        }

        [Test]
        public void WriteAndReadFloat_Negative_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteFloat(-99.99f, ref offset);

            offset = 0;
            float result = buffer.ReadFloat(ref offset);

            Assert.AreEqual(-99.99f, result, 0.01f);
        }

        #endregion

        #region Write/Read Double

        [Test]
        public void WriteAndReadDouble_RoundTrip()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteDouble(3.14159265358979, ref offset);

            offset = 0;
            double result = buffer.ReadDouble(ref offset);

            Assert.AreEqual(3.14159265358979, result, 0.0000001);
        }

        #endregion

        #region Write/Read Byte

        [Test]
        public void WriteAndReadByte_RoundTrip()
        {
            var buffer = new byte[4];
            int offset = 0;
            buffer.WriteByte(200, ref offset);

            Assert.AreEqual(1, offset);

            offset = 0;
            byte result = buffer.ReadByte(ref offset);

            Assert.AreEqual(200, result);
        }

        #endregion

        #region Write/Read SByte

        [Test]
        public void WriteAndReadSByte_RoundTrip()
        {
            var buffer = new byte[4];
            int offset = 0;
            buffer.WriteSByte(-42, ref offset);

            offset = 0;
            sbyte result = buffer.ReadSByte(ref offset);

            Assert.AreEqual(-42, result);
        }

        #endregion

        #region Write/Read Bool

        [Test]
        public void WriteAndReadBool_True_RoundTrip()
        {
            var buffer = new byte[4];
            int offset = 0;
            buffer.WriteBool(true, ref offset);

            offset = 0;
            bool result = buffer.ReadBool(ref offset);

            Assert.IsTrue(result);
        }

        [Test]
        public void WriteAndReadBool_False_RoundTrip()
        {
            var buffer = new byte[4];
            int offset = 0;
            buffer.WriteBool(false, ref offset);

            offset = 0;
            bool result = buffer.ReadBool(ref offset);

            Assert.IsFalse(result);
        }

        #endregion

        #region Write/Read Bytes (with length prefix)

        [Test]
        public void WriteAndReadBytes_RoundTrip()
        {
            var buffer = new byte[32];
            int offset = 0;
            buffer.WriteBytes(new byte[] { 10, 20, 30 }, ref offset);

            offset = 0;
            byte[] result = buffer.ReadBytes(ref offset);

            CollectionAssert.AreEqual(new byte[] { 10, 20, 30 }, result);
        }

        [Test]
        public void WriteBytes_Null_WritesZeroLength()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteBytes(null, ref offset);

            Assert.AreEqual(4, offset);

            offset = 0;
            byte[] result = buffer.ReadBytes(ref offset);

            Assert.IsEmpty(result);
        }

        #endregion

        #region Write/Read Bytes (with explicit length)

        [Test]
        public void ReadBytes_WithExplicitOffsetAndLen_ReturnsSlice()
        {
            var buffer = new byte[] { 0, 1, 2, 3, 4, 5 };
            int offset = 2;

            byte[] result = buffer.ReadBytes(offset, 3);

            CollectionAssert.AreEqual(new byte[] { 2, 3, 4 }, result);
        }

        [Test]
        public void ReadBytes_ZeroLen_ReturnsEmpty()
        {
            var buffer = new byte[] { 1, 2, 3 };

            byte[] result = buffer.ReadBytes(0, 0);

            Assert.IsEmpty(result);
        }

        [Test]
        public void ReadBytes_WithRefOffset_AdvancesOffset()
        {
            var buffer = new byte[] { 1, 2, 3, 4, 5 };
            int offset = 1;

            byte[] result = buffer.ReadBytes(ref offset, 3);

            CollectionAssert.AreEqual(new byte[] { 2, 3, 4 }, result);
            Assert.AreEqual(4, offset);
        }

        #endregion

        #region WriteBytesWithoutLength

        [Test]
        public void WriteBytesWithoutLength_WritesRawBytes()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteBytesWithoutLength(new byte[] { 0xAA, 0xBB }, ref offset);

            Assert.AreEqual(2, offset);
            Assert.AreEqual(0xAA, buffer[0]);
            Assert.AreEqual(0xBB, buffer[1]);
        }

        [Test]
        public void WriteBytesWithoutLength_Null_WritesZeroInt()
        {
            var buffer = new byte[16];
            int offset = 0;
            buffer.WriteBytesWithoutLength(null, ref offset);

            Assert.AreEqual(4, offset);
        }

        #endregion

        #region Write/Read String

        [Test]
        public void WriteAndReadString_RoundTrip()
        {
            var buffer = new byte[64];
            int offset = 0;
            buffer.WriteString("hello world", ref offset);

            offset = 0;
            string result = buffer.ReadString(ref offset);

            Assert.AreEqual("hello world", result);
        }

        [Test]
        public void WriteString_Null_WritesEmpty()
        {
            var buffer = new byte[64];
            int offset = 0;
            buffer.WriteString(null, ref offset);

            offset = 0;
            string result = buffer.ReadString(ref offset);

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void WriteAndReadString_Empty_RoundTrip()
        {
            var buffer = new byte[64];
            int offset = 0;
            buffer.WriteString(string.Empty, ref offset);

            offset = 0;
            string result = buffer.ReadString(ref offset);

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void WriteAndReadString_Unicode_RoundTrip()
        {
            var buffer = new byte[128];
            int offset = 0;
            buffer.WriteString("中文测试", ref offset);

            offset = 0;
            string result = buffer.ReadString(ref offset);

            Assert.AreEqual("中文测试", result);
        }

        #endregion

        #region Multiple Sequential Writes/Reads

        [Test]
        public void SequentialWriteAndRead_AllTypes_RoundTrip()
        {
            var buffer = new byte[256];
            int writeOffset = 0;
            buffer.WriteInt(42, ref writeOffset);
            buffer.WriteShort(1000, ref writeOffset);
            buffer.WriteLong(123456789L, ref writeOffset);
            buffer.WriteFloat(1.5f, ref writeOffset);
            buffer.WriteDouble(2.5, ref writeOffset);
            buffer.WriteByte(99, ref writeOffset);
            buffer.WriteBool(true, ref writeOffset);
            buffer.WriteString("test", ref writeOffset);

            int readOffset = 0;
            Assert.AreEqual(42, buffer.ReadInt(ref readOffset));
            Assert.AreEqual(1000, buffer.ReadShort(ref readOffset));
            Assert.AreEqual(123456789L, buffer.ReadLong(ref readOffset));
            Assert.AreEqual(1.5f, buffer.ReadFloat(ref readOffset), 0.001f);
            Assert.AreEqual(2.5, buffer.ReadDouble(ref readOffset), 0.001);
            Assert.AreEqual(99, buffer.ReadByte(ref readOffset));
            Assert.IsTrue(buffer.ReadBool(ref readOffset));
            Assert.AreEqual("test", buffer.ReadString(ref readOffset));
        }

        #endregion

        #region ToHex / ToArrayString / ToDefaultString / ToUtf8String

        [Test]
        public void ToHex_SingleByte_ReturnsHex()
        {
            Assert.AreEqual("0A", ((byte)10).ToHex());
        }

        [Test]
        public void ToHex_ByteArray_ReturnsConcatenatedHex()
        {
            var bytes = new byte[] { 0x0A, 0xFF, 0x00 };

            Assert.AreEqual("0AFF00", bytes.ToHex());
        }

        [Test]
        public void ToHex_WithFormat_UsesFormat()
        {
            var bytes = new byte[] { 0x0A, 0xFF };

            Assert.AreEqual("0aff", bytes.ToHex("x2"));
        }

        [Test]
        public void ToHex_WithOffsetAndCount_ReturnsPartialHex()
        {
            var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };

            Assert.AreEqual("0203", bytes.ToHex(1, 2));
        }

        [Test]
        public void ToArrayString_ReturnsSpaceSeparated()
        {
            var bytes = new byte[] { 10, 20, 30 };

            string result = bytes.ToArrayString();

            Assert.AreEqual("10 20 30 ", result);
        }

        [Test]
        public void ToDefaultString_ConvertsBytesToString()
        {
            var bytes = Encoding.Default.GetBytes("hello");

            Assert.AreEqual("hello", bytes.ToDefaultString());
        }

        [Test]
        public void ToDefaultString_WithRange_ConvertsPartialBytes()
        {
            var bytes = Encoding.Default.GetBytes("hello");

            Assert.AreEqual("ell", bytes.ToDefaultString(1, 3));
        }

        [Test]
        public void ToUtf8String_ConvertsUtf8Bytes()
        {
            var bytes = Encoding.UTF8.GetBytes("你好");

            Assert.AreEqual("你好", bytes.ToUtf8String());
        }

        [Test]
        public void ToUtf8String_WithRange_ConvertsPartialUtf8()
        {
            var full = Encoding.UTF8.GetBytes("ABCD");
            var partial = Encoding.UTF8.GetBytes("BC");

            Assert.AreEqual("BC", full.ToUtf8String(1, 2));
        }

        #endregion
    }
}
