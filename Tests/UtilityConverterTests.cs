using System;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityConverterTests
    {
        #region IsLittleEndian

        [Test]
        public void IsLittleEndian_MatchesBitConverter()
        {
            Assert.AreEqual(BitConverter.IsLittleEndian, Utility.Converter.IsLittleEndian);
        }

        #endregion

        #region ScreenDpi / Pixel Conversion

        [Test]
        public void GetCentimetersFromPixels_Throws_WhenDpiNotSet()
        {
            Utility.Converter.ScreenDpi = 0;
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetCentimetersFromPixels(100f);
            });
        }

        [Test]
        public void GetPixelsFromCentimeters_Throws_WhenDpiNotSet()
        {
            Utility.Converter.ScreenDpi = 0;
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetPixelsFromCentimeters(10f);
            });
        }

        [Test]
        public void GetInchesFromPixels_Throws_WhenDpiNotSet()
        {
            Utility.Converter.ScreenDpi = 0;
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetInchesFromPixels(100f);
            });
        }

        [Test]
        public void GetPixelsFromInches_Throws_WhenDpiNotSet()
        {
            Utility.Converter.ScreenDpi = 0;
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetPixelsFromInches(10f);
            });
        }

        [Test]
        public void GetCentimetersFromPixels_ReturnsCorrectValue()
        {
            Utility.Converter.ScreenDpi = 96f;
            float result = Utility.Converter.GetCentimetersFromPixels(96f);
            Assert.AreEqual(2.54f, result, 0.0001f);
        }

        [Test]
        public void GetPixelsFromCentimeters_RoundTrip()
        {
            Utility.Converter.ScreenDpi = 96f;
            float pixels = 200f;
            float cm = Utility.Converter.GetCentimetersFromPixels(pixels);
            float back = Utility.Converter.GetPixelsFromCentimeters(cm);
            Assert.AreEqual(pixels, back, 0.001f);
        }

        [Test]
        public void GetInchesFromPixels_ReturnsCorrectValue()
        {
            Utility.Converter.ScreenDpi = 96f;
            float result = Utility.Converter.GetInchesFromPixels(96f);
            Assert.AreEqual(1f, result, 0.0001f);
        }

        [Test]
        public void GetPixelsFromInches_RoundTrip()
        {
            Utility.Converter.ScreenDpi = 96f;
            float inches = 2f;
            float px = Utility.Converter.GetPixelsFromInches(inches);
            float back = Utility.Converter.GetInchesFromPixels(px);
            Assert.AreEqual(inches, back, 0.001f);
        }

        #endregion

        #region Boolean

        [Test]
        public void GetBytes_Bool_True()
        {
            byte[] result = Utility.Converter.GetBytes(true);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(1, result[0]);
        }

        [Test]
        public void GetBytes_Bool_False()
        {
            byte[] result = Utility.Converter.GetBytes(false);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(0, result[0]);
        }

        [Test]
        public void GetBytes_Bool_NullBuffer_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes(true, null, 0);
            });
        }

        [Test]
        public void GetBytes_Bool_InvalidStartIndex_Throws()
        {
            byte[] buffer = new byte[1];
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes(true, buffer, -1);
            });
        }

        [Test]
        public void GetBytes_Bool_BufferTooSmall_Throws()
        {
            byte[] buffer = new byte[1];
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes(true, buffer, 1);
            });
        }

        [Test]
        public void GetBoolean_RoundTrip()
        {
            byte[] bytes = Utility.Converter.GetBytes(true);
            Assert.IsTrue(Utility.Converter.GetBoolean(bytes));

            bytes = Utility.Converter.GetBytes(false);
            Assert.IsFalse(Utility.Converter.GetBoolean(bytes));
        }

        [Test]
        public void GetBoolean_WithOffset()
        {
            byte[] buffer = new byte[3];
            Utility.Converter.GetBytes(true, buffer, 1);
            Assert.IsTrue(Utility.Converter.GetBoolean(buffer, 1));
        }

        #endregion

        #region Short / Int16

        [Test]
        public void GetBytes_Int16_RoundTrip()
        {
            short value = -12345;
            byte[] bytes = Utility.Converter.GetBytes(value);
            Assert.AreEqual(2, bytes.Length);
            Assert.AreEqual(value, Utility.Converter.GetInt16(bytes));
        }

        [Test]
        public void GetBytes_Int16_WithOffset()
        {
            short value = 1234;
            byte[] buffer = new byte[10];
            Utility.Converter.GetBytes(value, buffer, 4);
            Assert.AreEqual(value, Utility.Converter.GetInt16(buffer, 4));
        }

        [Test]
        public void GetBytes_Int16_NullBuffer_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes((short)1, null, 0);
            });
        }

        [Test]
        public void GetBytes_Int16_InvalidStartIndex_Throws()
        {
            byte[] buffer = new byte[2];
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes((short)1, buffer, -1);
            });
        }

        [Test]
        public void GetBytes_Int16_BufferTooSmall_Throws()
        {
            byte[] buffer = new byte[1];
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes((short)1, buffer, 0);
            });
        }

        #endregion

        #region UInt16

        [Test]
        public void GetBytes_UInt16_RoundTrip()
        {
            ushort value = 65000;
            byte[] bytes = Utility.Converter.GetBytes(value);
            Assert.AreEqual(value, Utility.Converter.GetUInt16(bytes));
        }

        [Test]
        public void GetBytes_UInt16_WithOffset()
        {
            ushort value = 54321;
            byte[] buffer = new byte[10];
            Utility.Converter.GetBytes(value, buffer, 3);
            Assert.AreEqual(value, Utility.Converter.GetUInt16(buffer, 3));
        }

        #endregion

        #region Int32

        [Test]
        public void GetBytes_Int32_RoundTrip()
        {
            int value = -987654;
            byte[] bytes = Utility.Converter.GetBytes(value);
            Assert.AreEqual(4, bytes.Length);
            Assert.AreEqual(value, Utility.Converter.GetInt32(bytes));
        }

        [Test]
        public void GetBytes_Int32_Zero()
        {
            Assert.AreEqual(0, Utility.Converter.GetInt32(Utility.Converter.GetBytes(0)));
        }

        [Test]
        public void GetBytes_Int32_MaxValue()
        {
            Assert.AreEqual(int.MaxValue, Utility.Converter.GetInt32(Utility.Converter.GetBytes(int.MaxValue)));
        }

        [Test]
        public void GetBytes_Int32_MinValue()
        {
            Assert.AreEqual(int.MinValue, Utility.Converter.GetInt32(Utility.Converter.GetBytes(int.MinValue)));
        }

        [Test]
        public void GetBytes_Int32_NullBuffer_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes(1, null, 0);
            });
        }

        [Test]
        public void GetBytes_Int32_BufferTooSmall_Throws()
        {
            byte[] buffer = new byte[3];
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes(1, buffer, 0);
            });
        }

        #endregion

        #region UInt32

        [Test]
        public void GetBytes_UInt32_RoundTrip()
        {
            uint value = 3000000000;
            byte[] bytes = Utility.Converter.GetBytes(value);
            Assert.AreEqual(value, Utility.Converter.GetUInt32(bytes));
        }

        #endregion

        #region Int64

        [Test]
        public void GetBytes_Int64_RoundTrip()
        {
            long value = -9876543210L;
            byte[] bytes = Utility.Converter.GetBytes(value);
            Assert.AreEqual(8, bytes.Length);
            Assert.AreEqual(value, Utility.Converter.GetInt64(bytes));
        }

        [Test]
        public void GetBytes_Int64_MaxValue()
        {
            Assert.AreEqual(long.MaxValue, Utility.Converter.GetInt64(Utility.Converter.GetBytes(long.MaxValue)));
        }

        [Test]
        public void GetBytes_Int64_NullBuffer_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes(1L, null, 0);
            });
        }

        #endregion

        #region UInt64

        [Test]
        public void GetBytes_UInt64_RoundTrip()
        {
            ulong value = 18000000000000000UL;
            byte[] bytes = Utility.Converter.GetBytes(value);
            Assert.AreEqual(value, Utility.Converter.GetUInt64(bytes));
        }

        #endregion

        #region Char

        [Test]
        public void GetBytes_Char_RoundTrip()
        {
            char value = 'A';
            byte[] bytes = Utility.Converter.GetBytes(value);
            Assert.AreEqual(2, bytes.Length);
            Assert.AreEqual(value, Utility.Converter.GetChar(bytes));
        }

        [Test]
        public void GetBytes_Char_WithOffset()
        {
            char value = '中';
            byte[] buffer = new byte[10];
            Utility.Converter.GetBytes(value, buffer, 5);
            Assert.AreEqual(value, Utility.Converter.GetChar(buffer, 5));
        }

        #endregion

        #region Float / Single

        [Test]
        public void GetBytes_Single_RoundTrip()
        {
            float value = 3.14f;
            byte[] bytes = Utility.Converter.GetBytes(value);
            Assert.AreEqual(4, bytes.Length);
            Assert.AreEqual(value, Utility.Converter.GetSingle(bytes), 0.0001f);
        }

        [Test]
        public void GetBytes_Single_Zero()
        {
            Assert.AreEqual(0f, Utility.Converter.GetSingle(Utility.Converter.GetBytes(0f)), 0.0001f);
        }

        [Test]
        public void GetBytes_Single_NegativeInfinity()
        {
            float value = float.NegativeInfinity;
            Assert.AreEqual(value, Utility.Converter.GetSingle(Utility.Converter.GetBytes(value)));
        }

        [Test]
        public void GetBytes_Single_NaN()
        {
            float value = float.NaN;
            Assert.IsNaN(Utility.Converter.GetSingle(Utility.Converter.GetBytes(value)));
        }

        [Test]
        public void GetBytes_Single_WithOffset()
        {
            float value = -999.5f;
            byte[] buffer = new byte[10];
            Utility.Converter.GetBytes(value, buffer, 3);
            Assert.AreEqual(value, Utility.Converter.GetSingle(buffer, 3), 0.0001f);
        }

        #endregion

        #region Double

        [Test]
        public void GetBytes_Double_RoundTrip()
        {
            double value = 2.718281828;
            byte[] bytes = Utility.Converter.GetBytes(value);
            Assert.AreEqual(8, bytes.Length);
            Assert.AreEqual(value, Utility.Converter.GetDouble(bytes), 0.0000001);
        }

        [Test]
        public void GetBytes_Double_WithOffset()
        {
            double value = -123.456;
            byte[] buffer = new byte[20];
            Utility.Converter.GetBytes(value, buffer, 5);
            Assert.AreEqual(value, Utility.Converter.GetDouble(buffer, 5), 0.0000001);
        }

        #endregion

        #region String

        [Test]
        public void GetBytes_String_UTF8_RoundTrip()
        {
            string value = "Hello World";
            byte[] bytes = Utility.Converter.GetBytes(value);
            string result = Utility.Converter.GetString(bytes);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void GetBytes_String_Unicode_RoundTrip()
        {
            string value = "你好世界";
            byte[] bytes = Utility.Converter.GetBytes(value, Encoding.Unicode);
            string result = Utility.Converter.GetString(bytes, Encoding.Unicode);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void GetBytes_String_NullValue_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes(null, Encoding.UTF8);
            });
        }

        [Test]
        public void GetBytes_String_NullEncoding_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes("test", (System.Text.Encoding)null);
            });
        }

        [Test]
        public void GetString_NullValue_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetString(null);
            });
        }

        [Test]
        public void GetString_NullEncoding_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetString(new byte[0], null);
            });
        }

        [Test]
        public void GetString_WithOffsetAndLength()
        {
            string value = "ABCDEFGH";
            byte[] bytes = Utility.Converter.GetBytes(value);
            string result = Utility.Converter.GetString(bytes, 0, 5);
            Assert.AreEqual("ABCDE", result);
        }

        [Test]
        public void GetBytes_String_ToBuffer()
        {
            string value = "Test";
            byte[] buffer = new byte[10];
            int written = Utility.Converter.GetBytes(value, buffer, 0);
            Assert.AreEqual(Encoding.UTF8.GetByteCount(value), written);
            Assert.AreEqual(value, Utility.Converter.GetString(buffer, 0, written));
        }

        [Test]
        public void GetBytes_String_ToBuffer_NullValue_Throws()
        {
            byte[] buffer = new byte[10];
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetBytes(null, Encoding.UTF8, buffer, 0);
            });
        }

        [Test]
        public void GetString_NullValueWithOffset_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Converter.GetString(null, 0, 0, Encoding.UTF8);
            });
        }

        #endregion

        #region Multi-value packed buffer

        [Test]
        public void MultipleValues_PackedInSingleBuffer()
        {
            byte[] buffer = new byte[100];

            Utility.Converter.GetBytes(true, buffer, 0);
            Utility.Converter.GetBytes((short)1234, buffer, 1);
            Utility.Converter.GetBytes(56789, buffer, 3);
            Utility.Converter.GetBytes(3.14f, buffer, 7);

            Assert.IsTrue(Utility.Converter.GetBoolean(buffer, 0));
            Assert.AreEqual((short)1234, Utility.Converter.GetInt16(buffer, 1));
            Assert.AreEqual(56789, Utility.Converter.GetInt32(buffer, 3));
            Assert.AreEqual(3.14f, Utility.Converter.GetSingle(buffer, 7), 0.001f);
        }

        #endregion
    }
}
