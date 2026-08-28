using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class StringExtensionsConvertTests
    {
        private enum TestDay
        {
            None = 0,
            Monday = 1,
            Friday = 5,
        }

        #region ToInt

        [Test]
        public void ToInt_ValidString_ReturnsValue()
        {
            Assert.AreEqual(42, "42".ToInt());
            Assert.AreEqual(-7, "-7".ToInt());
            Assert.AreEqual(int.MaxValue, int.MaxValue.ToString().ToInt());
            Assert.AreEqual(int.MinValue, int.MinValue.ToString().ToInt());
        }

        [Test]
        public void ToInt_InvalidString_ReturnsDefault()
        {
            Assert.AreEqual(0, "abc".ToInt());
        }

        [Test]
        public void ToInt_NullOrEmpty_ReturnsDefault()
        {
            Assert.AreEqual(0, ((string)null).ToInt());
            Assert.AreEqual(0, string.Empty.ToInt());
        }

        [Test]
        public void ToInt_LeadingOrTrailingWhitespace_IsParsed()
        {
            // NumberStyles.Integer 默认允许首尾空白
            Assert.AreEqual(42, " 42".ToInt());
            Assert.AreEqual(42, "42 ".ToInt());
        }

        [Test]
        public void ToInt_InternalWhitespace_ReturnsDefault()
        {
            Assert.AreEqual(0, "4 2".ToInt());
        }

        [Test]
        public void ToInt_OverflowReturnsCustomDefault()
        {
            Assert.AreEqual(-1, long.MaxValue.ToString().ToInt(-1));
        }

        [Test]
        public void ToInt_InvalidReturnsCustomDefault()
        {
            Assert.AreEqual(99, "not-a-number".ToInt(99));
        }

        #endregion

        #region ToLong / ToFloat / ToDouble

        [Test]
        public void ToLong_ValidAndInvalid()
        {
            Assert.AreEqual(long.MaxValue, long.MaxValue.ToString().ToLong());
            Assert.AreEqual(0L, "xyz".ToLong());
            Assert.AreEqual(5L, ((string)null).ToLong(5L));
        }

        [Test]
        public void ToFloat_ValidAndInvalid()
        {
            Assert.AreEqual(3.5f, "3.5".ToFloat());
            Assert.AreEqual(0f, "abc".ToFloat());
            Assert.AreEqual(1.25f, ((string)null).ToFloat(1.25f));
        }

        [Test]
        public void ToDouble_ValidAndInvalid()
        {
            Assert.AreEqual(2.718281828459045, "2.718281828459045".ToDouble());
            Assert.AreEqual(0d, "abc".ToDouble());
            Assert.AreEqual(-0.5d, string.Empty.ToDouble(-0.5d));
        }

        #endregion

        #region ToEnum

        [Test]
        public void ToEnum_ValidName_ReturnsEnum()
        {
            Assert.AreEqual(TestDay.Monday, "Monday".ToEnum<TestDay>());
        }

        [Test]
        public void ToEnum_CaseSensitiveByDefault_ReturnsDefault()
        {
            Assert.AreEqual(TestDay.None, "monday".ToEnum<TestDay>());
        }

        [Test]
        public void ToEnum_IgnoreCase_ReturnsEnum()
        {
            Assert.AreEqual(TestDay.Friday, "friday".ToEnum<TestDay>(true));
        }

        [Test]
        public void ToEnum_InvalidName_ReturnsDefault()
        {
            Assert.AreEqual(TestDay.None, "Sunday".ToEnum<TestDay>());
            Assert.AreEqual(TestDay.Friday, "Sunday".ToEnum<TestDay>(false, TestDay.Friday));
        }

        [Test]
        public void ToEnum_NullOrEmpty_ReturnsDefault()
        {
            Assert.AreEqual(TestDay.None, ((string)null).ToEnum<TestDay>());
            Assert.AreEqual(TestDay.None, string.Empty.ToEnum<TestDay>());
        }

        #endregion
    }
}
