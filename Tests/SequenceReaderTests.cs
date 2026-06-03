using System;
using System.Buffers;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class SequenceReaderTests
    {
        #region Construction

        [Test]
        public void Constructor_FromMemory_SetsProperties()
        {
            var data = new byte[] { 1, 2, 3, 4, 5 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            Assert.AreEqual(5, reader.Length);
            Assert.AreEqual(0, reader.Consumed);
            Assert.IsFalse(reader.End);
            Assert.AreEqual(5, reader.Remaining);
        }

        [Test]
        public void Constructor_FromSequence_SetsProperties()
        {
            var data = new byte[] { 1, 2, 3 };
            var sequence = new ReadOnlySequence<byte>(data);
            var reader = new SequenceReader<byte>(sequence);

            Assert.AreEqual(3, reader.Length);
            Assert.AreEqual(0, reader.Consumed);
            Assert.IsFalse(reader.End);
        }

        [Test]
        public void Constructor_EmptyMemory_EndIsTrue()
        {
            var reader = new SequenceReader<byte>(ReadOnlyMemory<byte>.Empty);

            Assert.IsTrue(reader.End);
            Assert.AreEqual(0, reader.Length);
        }

        [Test]
        public void Constructor_EmptySequence_EndIsTrue()
        {
            var reader = new SequenceReader<byte>(ReadOnlySequence<byte>.Empty);

            Assert.IsTrue(reader.End);
        }

        #endregion

        #region TryPeek

        [Test]
        public void TryPeek_HasData_ReturnsFirstByte()
        {
            var data = new byte[] { 42, 43, 44 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            bool result = reader.TryPeek(out byte value);

            Assert.IsTrue(result);
            Assert.AreEqual(42, value);
            Assert.AreEqual(0, reader.Consumed);
        }

        [Test]
        public void TryPeek_CalledTwice_ReturnsSameValue()
        {
            var data = new byte[] { 99 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            reader.TryPeek(out byte first);
            reader.TryPeek(out byte second);

            Assert.AreEqual(first, second);
        }

        [Test]
        public void TryPeek_Empty_ReturnsFalse()
        {
            var reader = new SequenceReader<byte>(ReadOnlyMemory<byte>.Empty);

            bool result = reader.TryPeek(out byte value);

            Assert.IsFalse(result);
            Assert.AreEqual(0, value);
        }

        #endregion

        #region TryRead

        [Test]
        public void TryRead_HasData_ReturnsAndAdvances()
        {
            var data = new byte[] { 10, 20, 30 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            Assert.IsTrue(reader.TryRead(out byte v1));
            Assert.AreEqual(10, v1);
            Assert.AreEqual(1, reader.Consumed);

            Assert.IsTrue(reader.TryRead(out byte v2));
            Assert.AreEqual(20, v2);
            Assert.AreEqual(2, reader.Consumed);
        }

        [Test]
        public void TryRead_ReadAllData_EndIsTrue()
        {
            var data = new byte[] { 1 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            reader.TryRead(out _);

            Assert.IsTrue(reader.End);
        }

        [Test]
        public void TryRead_PastEnd_ReturnsFalse()
        {
            var data = new byte[] { 1 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            reader.TryRead(out _);

            Assert.IsFalse(reader.TryRead(out byte value));
            Assert.AreEqual(0, value);
        }

        #endregion

        #region Advance

        [Test]
        public void Advance_ByCount_SkipsBytes()
        {
            var data = new byte[] { 10, 20, 30, 40, 50 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            reader.Advance(2);

            Assert.AreEqual(2, reader.Consumed);
            Assert.IsTrue(reader.TryRead(out byte value));
            Assert.AreEqual(30, value);
        }

        [Test]
        public void Advance_ToEnd_ReachesEnd()
        {
            var data = new byte[] { 1, 2, 3 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            reader.Advance(3);

            Assert.IsTrue(reader.End);
        }

        [Test]
        public void Advance_PastEnd_ThrowsArgumentOutOfRangeException()
        {
            var data = new byte[] { 1, 2, 3 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            bool threw = false;
            try
            {
                reader.Advance(100);
            }
            catch (ArgumentOutOfRangeException)
            {
                threw = true;
            }
            Assert.IsTrue(threw, "Should have thrown ArgumentOutOfRangeException");
        }

        #endregion

        #region Rewind

        [Test]
        public void Rewind_AfterRead_GoesBack()
        {
            var data = new byte[] { 10, 20, 30 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            reader.TryRead(out _);
            reader.TryRead(out _);
            reader.Rewind(1);

            Assert.AreEqual(1, reader.Consumed);
            Assert.IsTrue(reader.TryRead(out byte value));
            Assert.AreEqual(20, value);
        }

        [Test]
        public void Rewind_ToStart_ResetsToBeginning()
        {
            var data = new byte[] { 10, 20, 30 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            reader.TryRead(out _);
            reader.TryRead(out _);
            reader.Rewind(2);

            Assert.AreEqual(0, reader.Consumed);
            Assert.IsTrue(reader.TryRead(out byte value));
            Assert.AreEqual(10, value);
        }

        [Test]
        public void Rewind_NegativeCount_ThrowsArgumentOutOfRangeException()
        {
            var data = new byte[] { 1, 2, 3 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            bool threw1 = false;
            try
            {
                reader.Rewind(-1);
            }
            catch (ArgumentOutOfRangeException)
            {
                threw1 = true;
            }
            Assert.IsTrue(threw1, "Should have thrown ArgumentOutOfRangeException");
        }

        [Test]
        public void Rewind_PastStart_ThrowsArgumentOutOfRangeException()
        {
            var data = new byte[] { 1, 2, 3 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            bool threw2 = false;
            try
            {
                reader.Rewind(5);
            }
            catch (ArgumentOutOfRangeException)
            {
                threw2 = true;
            }
            Assert.IsTrue(threw2, "Should have thrown ArgumentOutOfRangeException");
        }

        #endregion

        #region Remaining / Length / Consumed

        [Test]
        public void Remaining_DecreasesAsDataIsRead()
        {
            var data = new byte[] { 1, 2, 3 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            Assert.AreEqual(3, reader.Remaining);
            reader.TryRead(out _);
            Assert.AreEqual(2, reader.Remaining);
            reader.TryRead(out _);
            Assert.AreEqual(1, reader.Remaining);
            reader.TryRead(out _);
            Assert.AreEqual(0, reader.Remaining);
        }

        [Test]
        public void Consumed_IncreasesAsDataIsRead()
        {
            var data = new byte[] { 1, 2, 3 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            Assert.AreEqual(0, reader.Consumed);
            reader.TryRead(out _);
            Assert.AreEqual(1, reader.Consumed);
            reader.TryRead(out _);
            Assert.AreEqual(2, reader.Consumed);
        }

        #endregion

        #region TryCopyTo

        [Test]
        public void TryCopyTo_EnoughData_CopiesCorrectly()
        {
            var data = new byte[] { 1, 2, 3, 4, 5 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));
            var dest = new byte[3];

            bool result = reader.TryCopyTo(dest);

            Assert.IsTrue(result);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, dest);
            Assert.AreEqual(0, reader.Consumed);
        }

        [Test]
        public void TryCopyTo_NotEnoughData_ReturnsFalse()
        {
            var data = new byte[] { 1, 2 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));
            var dest = new byte[5];

            bool result = reader.TryCopyTo(dest);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryCopyTo_DoesNotAdvanceReader()
        {
            var data = new byte[] { 10, 20, 30 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));
            var dest = new byte[2];

            reader.TryCopyTo(dest);

            Assert.AreEqual(0, reader.Consumed);
            Assert.AreEqual(10, reader.TryRead(out byte b) ? b : (byte)0);
        }

        #endregion

        #region SequenceReaderExtensions - TryReadBigEndian

        [Test]
        public void TryReadBigEndian_Short_ValidData_RoundTrip()
        {
            short original = 0x1234;
            byte[] bytes = BitConverter.GetBytes(original);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(bytes));

            bool result = reader.TryReadBigEndian(out short value);

            Assert.IsTrue(result);
            Assert.AreEqual(original, value);
        }

        [Test]
        public void TryReadBigEndian_Int_ValidData_RoundTrip()
        {
            int original = 0x12345678;
            byte[] bytes = BitConverter.GetBytes(original);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(bytes));

            bool result = reader.TryReadBigEndian(out int value);

            Assert.IsTrue(result);
            Assert.AreEqual(original, value);
        }

        [Test]
        public void TryReadBigEndian_Long_ValidData_RoundTrip()
        {
            long original = 0x0123456789ABCDEF;
            byte[] bytes = BitConverter.GetBytes(original);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(bytes));

            bool result = reader.TryReadBigEndian(out long value);

            Assert.IsTrue(result);
            Assert.AreEqual(original, value);
        }

        [Test]
        public void TryReadBigEndian_UShort_ValidData_RoundTrip()
        {
            ushort original = 0xABCD;
            byte[] bytes = BitConverter.GetBytes(original);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(bytes));

            bool result = reader.TryReadBigEndian(out ushort value);

            Assert.IsTrue(result);
            Assert.AreEqual(original, value);
        }

        [Test]
        public void TryReadBigEndian_UInt_ValidData_RoundTrip()
        {
            uint original = 0x12345678;
            byte[] bytes = BitConverter.GetBytes(original);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(bytes));

            bool result = reader.TryReadBigEndian(out uint value);

            Assert.IsTrue(result);
            Assert.AreEqual(original, value);
        }

        [Test]
        public void TryReadBigEndian_ULong_ValidData_RoundTrip()
        {
            ulong original = 0x0123456789ABCDEF;
            byte[] bytes = BitConverter.GetBytes(original);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(bytes));

            bool result = reader.TryReadBigEndian(out ulong value);

            Assert.IsTrue(result);
            Assert.AreEqual(original, value);
        }

        [Test]
        public void TryReadBigEndian_Float_ValidData_RoundTrip()
        {
            float original = 3.14f;
            byte[] bytes = BitConverter.GetBytes(original);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(bytes));

            bool result = reader.TryReadBigEndian(out float value);

            Assert.IsTrue(result);
            Assert.AreEqual(original, value, 0.001f);
        }

        [Test]
        public void TryReadBigEndian_Double_ValidData_RoundTrip()
        {
            double original = 3.14159265358979;
            byte[] bytes = BitConverter.GetBytes(original);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(bytes));

            bool result = reader.TryReadBigEndian(out double value);

            Assert.IsTrue(result);
            Assert.AreEqual(original, value, 0.0000001);
        }

        [Test]
        public void TryReadBigEndian_InsufficientData_ReturnsFalse()
        {
            var data = new byte[] { 0x01 };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            Assert.IsFalse(reader.TryReadBigEndian(out short _));
            Assert.IsFalse(reader.TryReadBigEndian(out int _));
            Assert.IsFalse(reader.TryReadBigEndian(out long _));
        }

        #endregion

        #region SequenceReaderExtensions - TryRead sbyte

        [Test]
        public void TryRead_SByte_ValidData()
        {
            var data = new byte[] { 0xFF };
            var reader = new SequenceReader<byte>(new ReadOnlyMemory<byte>(data));

            bool result = reader.TryRead(out sbyte value);

            Assert.IsTrue(result);
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TryRead_SByte_Empty_ReturnsFalse()
        {
            var reader = new SequenceReader<byte>(ReadOnlyMemory<byte>.Empty);

            bool result = reader.TryRead(out sbyte value);

            Assert.IsFalse(result);
            Assert.AreEqual(0, value);
        }

        #endregion

        #region Multi-segment sequence

        [Test]
        public void TryRead_MultiSegmentSequence_ReadsAcrossSegments()
        {
            var segment1 = new byte[] { 1, 2 };
            var segment2 = new byte[] { 3, 4 };
            var segment3 = new byte[] { 5 };

            var sequence = CreateMultiSegmentSequence(segment1, segment2, segment3);
            var reader = new SequenceReader<byte>(sequence);

            for (int i = 1; i <= 5; i++)
            {
                Assert.IsTrue(reader.TryRead(out byte value));
                Assert.AreEqual(i, value);
            }

            Assert.IsTrue(reader.End);
        }

        [Test]
        public void TryReadBigEndian_Int_MultiSegment_ReadsAcrossBoundary()
        {
            var segment1 = new byte[] { 0x12, 0x34 };
            var segment2 = new byte[] { 0x56, 0x78 };

            var sequence = CreateMultiSegmentSequence(segment1, segment2);
            var reader = new SequenceReader<byte>(sequence);

            bool result = reader.TryReadBigEndian(out int value);

            Assert.IsTrue(result);
            Assert.AreEqual(0x12345678, value);
        }

        private static ReadOnlySequence<byte> CreateMultiSegmentSequence(params byte[][] segments)
        {
            var start = new Segment(segments[0], null);
            var current = start;

            for (int i = 1; i < segments.Length; i++)
            {
                current = new Segment(segments[i], current);
            }

            return new ReadOnlySequence<byte>(start, 0, current, current.Memory.Length);
        }

        private class Segment : ReadOnlySequenceSegment<byte>
        {
            public Segment(byte[] data, Segment previous)
            {
                Memory = data;

                if (previous != null)
                {
                    RunningIndex = previous.RunningIndex + previous.Memory.Length;
                    previous.SetNext(this);
                }
            }

            public void SetNext(Segment next)
            {
                Next = next;
            }
        }

        #endregion
    }
}
