using System;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        #region EqualsFast

        [Test]
        public void EqualsFast_SameStrings_ReturnsTrue()
        {
            Assert.IsTrue("hello".EqualsFast("hello"));
        }

        [Test]
        public void EqualsFast_DifferentStrings_ReturnsFalse()
        {
            Assert.IsFalse("hello".EqualsFast("world"));
        }

        [Test]
        public void EqualsFast_DifferentLength_ReturnsFalse()
        {
            Assert.IsFalse("hi".EqualsFast("hello"));
        }

        [Test]
        public void EqualsFast_BothNull_ReturnsTrue()
        {
            string a = null;
            string b = null;

            Assert.IsTrue(a.EqualsFast(b));
        }

        [Test]
        public void EqualsFast_SelfNull_TargetNonNull_ReturnsFalse()
        {
            string a = null;

            Assert.IsFalse(a.EqualsFast("hello"));
        }

        [Test]
        public void EqualsFast_TargetNull_ReturnsFalse()
        {
            Assert.IsFalse("hello".EqualsFast(null));
        }

        [Test]
        public void EqualsFast_EmptyStrings_ReturnsTrue()
        {
            Assert.IsTrue(string.Empty.EqualsFast(string.Empty));
        }

        #endregion

        #region EndsWithFast

        [Test]
        public void EndsWithFast_MatchingSuffix_ReturnsTrue()
        {
            Assert.IsTrue("hello world".EndsWithFast("world"));
        }

        [Test]
        public void EndsWithFast_NonMatchingSuffix_ReturnsFalse()
        {
            Assert.IsFalse("hello world".EndsWithFast("hello"));
        }

        [Test]
        public void EndsWithFast_NullSelf_ReturnsFalse()
        {
            string self = null;
            Assert.IsFalse(self.EndsWithFast("test"));
        }

        [Test]
        public void EndsWithFast_NullTarget_ReturnsFalse()
        {
            Assert.IsFalse("test".EndsWithFast(null));
        }

        [Test]
        public void EndsWithFast_TargetLongerThanSelf_ReturnsFalse()
        {
            Assert.IsFalse("hi".EndsWithFast("hello"));
        }

        [Test]
        public void EndsWithFast_EmptyTarget_ReturnsTrue()
        {
            Assert.IsTrue("hello".EndsWithFast(string.Empty));
        }

        [Test]
        public void EndsWithFast_ExactMatch_ReturnsTrue()
        {
            Assert.IsTrue("hello".EndsWithFast("hello"));
        }

        #endregion

        #region StartsWithFast

        [Test]
        public void StartsWithFast_MatchingPrefix_ReturnsTrue()
        {
            Assert.IsTrue("hello world".StartsWithFast("hello"));
        }

        [Test]
        public void StartsWithFast_NonMatchingPrefix_ReturnsFalse()
        {
            Assert.IsFalse("hello world".StartsWithFast("world"));
        }

        [Test]
        public void StartsWithFast_NullSelf_ReturnsFalse()
        {
            string self = null;
            Assert.IsFalse(self.StartsWithFast("test"));
        }

        [Test]
        public void StartsWithFast_NullTarget_ReturnsFalse()
        {
            Assert.IsFalse("test".StartsWithFast(null));
        }

        [Test]
        public void StartsWithFast_TargetLongerThanSelf_ReturnsFalse()
        {
            Assert.IsFalse("hi".StartsWithFast("hello"));
        }

        [Test]
        public void StartsWithFast_EmptyTarget_ReturnsTrue()
        {
            Assert.IsTrue("hello".StartsWithFast(string.Empty));
        }

        [Test]
        public void StartsWithFast_ExactMatch_ReturnsTrue()
        {
            Assert.IsTrue("hello".StartsWithFast("hello"));
        }

        #endregion

        #region ToBytes / ToByteArray / ToUtf8

        [Test]
        public void ToBytes_ReturnsByteArray()
        {
            var result = "AB".ToBytes();

            CollectionAssert.AreEqual(new byte[] { 65, 66 }, result);
        }

        [Test]
        public void ToByteArray_ReturnsByteArray()
        {
            var result = "AB".ToByteArray();

            CollectionAssert.AreEqual(new byte[] { 65, 66 }, result);
        }

        [Test]
        public void ToUtf8_ReturnsUtf8Bytes()
        {
            var result = "hello".ToUtf8();

            Assert.AreEqual(Encoding.UTF8.GetBytes("hello"), result);
        }

        [Test]
        public void ToUtf8_ChineseCharacters_EncodesCorrectly()
        {
            var result = "中文".ToUtf8();

            Assert.AreEqual(Encoding.UTF8.GetBytes("中文"), result);
        }

        #endregion

        #region HexToBytes

        [Test]
        public void HexToBytes_ValidHex_ReturnsCorrectBytes()
        {
            var result = "0A1F".HexToBytes();

            CollectionAssert.AreEqual(new byte[] { 0x0A, 0x1F }, result);
        }

        [Test]
        public void HexToBytes_AllZeros_ReturnsZeroBytes()
        {
            var result = "0000".HexToBytes();

            CollectionAssert.AreEqual(new byte[] { 0x00, 0x00 }, result);
        }

        [Test]
        public void HexToBytes_OddLength_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                "ABC".HexToBytes();
            });
        }

        [Test]
        public void HexToBytes_LowercaseHex_Works()
        {
            var result = "0a1f".HexToBytes();

            CollectionAssert.AreEqual(new byte[] { 0x0A, 0x1F }, result);
        }

        [Test]
        public void HexToBytes_EmptyString_ReturnsEmptyArray()
        {
            var result = "".HexToBytes();

            CollectionAssert.AreEqual(new byte[0], result);
        }

        #endregion

        #region IsNullOrWhiteSpace / IsNullOrEmpty / IsNotNull*

        [Test]
        public void IsNullOrWhiteSpace_NullString_ReturnsTrue()
        {
            string s = null;
            Assert.IsTrue(s.IsNullOrWhiteSpace());
        }

        [Test]
        public void IsNullOrWhiteSpace_EmptyString_ReturnsTrue()
        {
            Assert.IsTrue(string.Empty.IsNullOrWhiteSpace());
        }

        [Test]
        public void IsNullOrWhiteSpace_WhitespaceString_ReturnsTrue()
        {
            Assert.IsTrue("   \t\n".IsNullOrWhiteSpace());
        }

        [Test]
        public void IsNullOrWhiteSpace_NonEmptyString_ReturnsFalse()
        {
            Assert.IsFalse("hello".IsNullOrWhiteSpace());
        }

        [Test]
        public void IsNullOrEmpty_NullString_ReturnsTrue()
        {
            string s = null;
            Assert.IsTrue(s.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrEmpty_EmptyString_ReturnsTrue()
        {
            Assert.IsTrue(string.Empty.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrEmpty_WhitespaceString_ReturnsFalse()
        {
            Assert.IsFalse("   ".IsNullOrEmpty());
        }

        [Test]
        public void IsNotNullOrWhiteSpace_ValidString_ReturnsTrue()
        {
            Assert.IsTrue("hello".IsNotNullOrWhiteSpace());
        }

        [Test]
        public void IsNotNullOrWhiteSpace_NullString_ReturnsFalse()
        {
            string s = null;
            Assert.IsFalse(s.IsNotNullOrWhiteSpace());
        }

        [Test]
        public void IsNotNullOrEmpty_ValidString_ReturnsTrue()
        {
            Assert.IsTrue("hello".IsNotNullOrEmpty());
        }

        [Test]
        public void IsNotNullOrEmpty_NullString_ReturnsFalse()
        {
            string s = null;
            Assert.IsFalse(s.IsNotNullOrEmpty());
        }

        #endregion

        #region Format

        [Test]
        public void Format_WithArgs_FormatsCorrectly()
        {
            string result = "Hello {0}, you are {1} years old.".Format("Alice", 25);

            Assert.AreEqual("Hello Alice, you are 25 years old.", result);
        }

        [Test]
        public void Format_NoArgs_ReturnsOriginal()
        {
            string result = "Hello World".Format();

            Assert.AreEqual("Hello World", result);
        }

        #endregion

        #region TrimEmpty

        [Test]
        public void TrimEmpty_RemovesWhitespaceChars()
        {
            string result = "a b\tc\nd\re".TrimEmpty();

            Assert.AreEqual("abcde", result);
        }

        [Test]
        public void TrimEmpty_NoWhitespace_ReturnsOriginal()
        {
            string result = "hello".TrimEmpty();

            Assert.AreEqual("hello", result);
        }

        [Test]
        public void TrimEmpty_AllWhitespace_ReturnsEmpty()
        {
            string result = " \t\n\r ".TrimEmpty();

            Assert.AreEqual(string.Empty, result);
        }

        #endregion

        #region ConvertToSnakeCase

        [Test]
        public void ConvertToSnakeCase_PascalCase_Converts()
        {
            Assert.AreEqual("hello_world", "HelloWorld".ConvertToSnakeCase());
        }

        [Test]
        public void ConvertToSnakeCase_CamelCase_Converts()
        {
            Assert.AreEqual("hello_world", "helloWorld".ConvertToSnakeCase());
        }

        [Test]
        public void ConvertToSnakeCase_AlreadySnakeCase_NoChange()
        {
            Assert.AreEqual("hello_world", "hello_world".ConvertToSnakeCase());
        }

        [Test]
        public void ConvertToSnakeCase_NullString_ReturnsNull()
        {
            string input = null;
            Assert.IsNull(input.ConvertToSnakeCase());
        }

        [Test]
        public void ConvertToSnakeCase_EmptyString_ReturnsEmpty()
        {
            Assert.AreEqual(string.Empty, string.Empty.ConvertToSnakeCase());
        }

        [Test]
        public void ConvertToSnakeCase_LeadingUnderscores_Preserved()
        {
            Assert.AreEqual("__hello_world", "__HelloWorld".ConvertToSnakeCase());
        }

        [Test]
        public void ConvertToSnakeCase_Alphanumeric_PreservesNumbers()
        {
            Assert.AreEqual("test123_abc", "Test123Abc".ConvertToSnakeCase());
        }

        #endregion

        #region TrimZhCn

        [Test]
        public void TrimZhCn_RemovesChineseChars()
        {
            string result = "hello你好world世界".TrimZhCn();

            Assert.AreEqual("helloworld", result);
        }

        [Test]
        public void TrimZhCn_NoChinese_ReturnsOriginal()
        {
            string result = "hello world 123".TrimZhCn();

            Assert.AreEqual("hello world 123", result);
        }

        [Test]
        public void TrimZhCn_AllChinese_ReturnsEmpty()
        {
            string result = "你好世界".TrimZhCn();

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void TrimZhCn_MixedContent_OnlyRemovesChinese()
        {
            string result = "abc123你好".TrimZhCn();

            Assert.AreEqual("abc123", result);
        }

        #endregion

        #region SplitToIntArray

        [Test]
        public void SplitToIntArray_ValidInput_ReturnsIntArray()
        {
            int[] result = "1+2+3".SplitToIntArray();

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result);
        }

        [Test]
        public void SplitToIntArray_CustomSeparator_ReturnsIntArray()
        {
            int[] result = "10,20,30".SplitToIntArray(',');

            CollectionAssert.AreEqual(new[] { 10, 20, 30 }, result);
        }

        [Test]
        public void SplitToIntArray_NullString_ReturnsEmptyArray()
        {
            string input = null;
            int[] result = input.SplitToIntArray();

            Assert.IsEmpty(result);
        }

        [Test]
        public void SplitToIntArray_EmptyString_ReturnsEmptyArray()
        {
            int[] result = string.Empty.SplitToIntArray();

            Assert.IsEmpty(result);
        }

        [Test]
        public void SplitToIntArray_InvalidValues_YieldZero()
        {
            int[] result = "1+abc+3".SplitToIntArray();

            CollectionAssert.AreEqual(new[] { 1, 0, 3 }, result);
        }

        [Test]
        public void SplitToIntArray_SingleValue_ReturnsSingleElement()
        {
            int[] result = "42".SplitToIntArray();

            CollectionAssert.AreEqual(new[] { 42 }, result);
        }

        #endregion

        #region SplitTo2IntArray

        [Test]
        public void SplitTo2IntArray_ValidInput_Returns2DArray()
        {
            int[][] result = "1+2;3+4".SplitTo2IntArray();

            Assert.AreEqual(2, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 2 }, result[0]);
            CollectionAssert.AreEqual(new[] { 3, 4 }, result[1]);
        }

        [Test]
        public void SplitTo2IntArray_NullString_ReturnsEmptyArray()
        {
            string input = null;
            int[][] result = input.SplitTo2IntArray();

            Assert.IsEmpty(result);
        }

        [Test]
        public void SplitTo2IntArray_EmptyString_ReturnsEmptyArray()
        {
            int[][] result = string.Empty.SplitTo2IntArray();

            Assert.IsEmpty(result);
        }

        [Test]
        public void SplitTo2IntArray_SingleSegment_ReturnsSingleSubArray()
        {
            int[][] result = "1+2+3".SplitTo2IntArray();

            Assert.AreEqual(1, result.Length);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result[0]);
        }

        #endregion

        #region ReadLine

        [Test]
        public void ReadLine_SingleLine_ReturnsLineAndAdvances()
        {
            int pos = 0;
            string result = "hello world".ReadLine(ref pos);

            Assert.AreEqual("hello world", result);
            Assert.AreEqual(11, pos);
        }

        [Test]
        public void ReadLine_TwoLinesLF_ReturnsFirstLine()
        {
            int pos = 0;
            string result = "line1\nline2".ReadLine(ref pos);

            Assert.AreEqual("line1", result);
        }

        [Test]
        public void ReadLine_TwoLinesCRLF_ReturnsFirstLine()
        {
            int pos = 0;
            string result = "line1\r\nline2".ReadLine(ref pos);

            Assert.AreEqual("line1", result);
        }

        [Test]
        public void ReadLine_SequentialReads_ReturnsAllLines()
        {
            string text = "a\nb\nc";
            int pos = 0;

            Assert.AreEqual("a", text.ReadLine(ref pos));
            Assert.AreEqual("b", text.ReadLine(ref pos));
            Assert.AreEqual("c", text.ReadLine(ref pos));
        }

        [Test]
        public void ReadLine_NegativePosition_ReturnsNull()
        {
            int pos = -1;

            string result = "hello".ReadLine(ref pos);

            Assert.IsNull(result);
        }

        [Test]
        public void ReadLine_PastEnd_ReturnsNull()
        {
            int pos = 5;

            string result = "hello".ReadLine(ref pos);

            Assert.IsNull(result);
        }

        [Test]
        public void ReadLine_EmptyLine_ReturnsEmptyString()
        {
            string text = "\nhello";
            int pos = 0;

            string result = text.ReadLine(ref pos);

            Assert.AreEqual(string.Empty, result);
        }

        #endregion
    }
}
