using System;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class SpanExtensionTests
    {
        #region Constants

        [Test]
        public void Constants_HaveCorrectValues()
        {
            Assert.AreEqual(4, SpanExtension.IntSize);
            Assert.AreEqual(2, SpanExtension.ShortSize);
            Assert.AreEqual(8, SpanExtension.LongSize);
            Assert.AreEqual(4, SpanExtension.FloatSize);
            Assert.AreEqual(8, SpanExtension.DoubleSize);
            Assert.AreEqual(1, SpanExtension.ByteSize);
            Assert.AreEqual(1, SpanExtension.SbyteSize);
            Assert.AreEqual(1, SpanExtension.BoolSize);
        }

        #endregion

        #region Write/Read Int

        [Test]
        public void WriteAndReadInt_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteInt(42, ref offset);

            Assert.AreEqual(4, offset);

            offset = 0;
            int result = buffer.ReadInt(ref offset);

            Assert.AreEqual(42, result);
        }

        [Test]
        public void WriteAndReadInt_MaxValue_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteInt(int.MaxValue, ref offset);

            offset = 0;
            int result = buffer.ReadInt(ref offset);

            Assert.AreEqual(int.MaxValue, result);
        }

        [Test]
        public void WriteAndReadInt_MinValue_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteInt(int.MinValue, ref offset);

            offset = 0;
            int result = buffer.ReadInt(ref offset);

            Assert.AreEqual(int.MinValue, result);
        }

        [Test]
        public void WriteInt_OutOfBounds_ThrowsArgumentOutOfRangeException()
        {
            Span<byte> buffer = stackalloc byte[2];
            int offset = 0;

            bool threw1 = false;
            try
            {
                buffer.WriteInt(1, ref offset);
            }
            catch (ArgumentOutOfRangeException)
            {
                threw1 = true;
            }
            Assert.IsTrue(threw1, "Should have thrown ArgumentOutOfRangeException");
        }

        [Test]
        public void ReadInt_OutOfBounds_ThrowsArgumentOutOfRangeException()
        {
            Span<byte> buffer = stackalloc byte[2];
            int offset = 0;

            bool threw2 = false;
            try
            {
                buffer.ReadInt(ref offset);
            }
            catch (ArgumentOutOfRangeException)
            {
                threw2 = true;
            }
            Assert.IsTrue(threw2, "Should have thrown ArgumentOutOfRangeException");
        }

        [Test]
        public void WriteInt_NegativeOffset_ThrowsArgumentOutOfRangeException()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = -1;

            bool threw3 = false;
            try
            {
                buffer.WriteInt(1, ref offset);
            }
            catch (ArgumentOutOfRangeException)
            {
                threw3 = true;
            }
            Assert.IsTrue(threw3, "Should have thrown ArgumentOutOfRangeException");
        }

        #endregion

        #region Write/Read Short

        [Test]
        public void WriteAndReadShort_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteShort(1234, ref offset);

            offset = 0;
            short result = buffer.ReadShort(ref offset);

            Assert.AreEqual(1234, result);
        }

        [Test]
        public void WriteAndReadShort_Negative_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteShort(-1000, ref offset);

            offset = 0;
            short result = buffer.ReadShort(ref offset);

            Assert.AreEqual(-1000, result);
        }

        #endregion

        #region Write/Read Long

        [Test]
        public void WriteAndReadLong_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteLong(1234567890123L, ref offset);

            offset = 0;
            long result = buffer.ReadLong(ref offset);

            Assert.AreEqual(1234567890123L, result);
        }

        [Test]
        public void WriteAndReadLong_MaxValue_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[16];
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
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteFloat(3.14f, ref offset);

            offset = 0;
            float result = buffer.ReadFloat(ref offset);

            Assert.AreEqual(3.14f, result, 0.001f);
        }

        [Test]
        public void WriteAndReadFloat_Negative_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteFloat(-99.5f, ref offset);

            offset = 0;
            float result = buffer.ReadFloat(ref offset);

            Assert.AreEqual(-99.5f, result, 0.001f);
        }

        #endregion

        #region Write/Read Double

        [Test]
        public void WriteAndReadDouble_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteDouble(2.718281828, ref offset);

            offset = 0;
            double result = buffer.ReadDouble(ref offset);

            Assert.AreEqual(2.718281828, result, 0.0000001);
        }

        #endregion

        #region Write/Read Byte

        [Test]
        public void WriteAndReadByte_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[4];
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
            Span<byte> buffer = stackalloc byte[4];
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
            Span<byte> buffer = stackalloc byte[4];
            int offset = 0;
            buffer.WriteBool(true, ref offset);

            offset = 0;
            bool result = buffer.ReadBool(ref offset);

            Assert.IsTrue(result);
        }

        [Test]
        public void WriteAndReadBool_False_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[4];
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
            Span<byte> buffer = stackalloc byte[32];
            int offset = 0;
            buffer.WriteBytes(new byte[] { 10, 20, 30 }, ref offset);

            offset = 0;
            byte[] result = buffer.ReadBytes(ref offset);

            CollectionAssert.AreEqual(new byte[] { 10, 20, 30 }, result);
        }

        [Test]
        public void WriteBytes_Null_WritesZeroLength()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteBytes(null, ref offset);

            Assert.AreEqual(4, offset);

            offset = 0;
            byte[] result = buffer.ReadBytes(ref offset);

            Assert.IsEmpty(result);
        }

        #endregion

        #region WriteBytesWithoutLength

        [Test]
        public void WriteBytesWithoutLength_WritesRawData()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteBytesWithoutLength(new byte[] { 0xAA, 0xBB }, ref offset);

            Assert.AreEqual(2, offset);
            Assert.AreEqual(0xAA, buffer[0]);
            Assert.AreEqual(0xBB, buffer[1]);
        }

        [Test]
        public void WriteBytesWithoutLength_Null_WritesZeroInt()
        {
            Span<byte> buffer = stackalloc byte[16];
            int offset = 0;
            buffer.WriteBytesWithoutLength(null, ref offset);

            Assert.AreEqual(4, offset);
        }

        #endregion

        #region Write/Read String

        [Test]
        public void WriteAndReadString_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[64];
            int offset = 0;
            buffer.WriteString("hello", ref offset);

            offset = 0;
            string result = buffer.ReadString(ref offset);

            Assert.AreEqual("hello", result);
        }

        [Test]
        public void WriteString_Null_WritesEmpty()
        {
            Span<byte> buffer = stackalloc byte[64];
            int offset = 0;
            buffer.WriteString(null, ref offset);

            offset = 0;
            string result = buffer.ReadString(ref offset);

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void WriteAndReadString_Unicode_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[128];
            int offset = 0;
            buffer.WriteString("中文", ref offset);

            offset = 0;
            string result = buffer.ReadString(ref offset);

            Assert.AreEqual("中文", result);
        }

        #endregion

        #region Sequential Writes/Reads

        [Test]
        public void SequentialWriteAndRead_AllTypes_RoundTrip()
        {
            Span<byte> buffer = stackalloc byte[256];
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

        #region GetArray

        [Test]
        public void GetArray_FromArrayBackedMemory_ReturnsArraySegment()
        {
            var data = new byte[] { 1, 2, 3, 4, 5 };
            ReadOnlyMemory<byte> memory = data.AsMemory();

            ArraySegment<byte> segment = memory.GetArray();

            Assert.AreEqual(data, segment.Array);
            Assert.AreEqual(0, segment.Offset);
            Assert.AreEqual(5, segment.Count);
        }

        #endregion
    }
}
