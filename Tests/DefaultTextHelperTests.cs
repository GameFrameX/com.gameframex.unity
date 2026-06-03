using GameFrameX;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class DefaultTextHelperTests
    {
        private DefaultTextHelper _helper;

        [SetUp]
        public void SetUp()
        {
            _helper = new DefaultTextHelper();
        }

        #region Format (params object[])

        [Test]
        public void Format_Params_SingleArg_ReturnsFormatted()
        {
            string result = _helper.Format("Hello {0}", "World");

            Assert.AreEqual("Hello World", result);
        }

        [Test]
        public void Format_Params_MultipleArgs_ReturnsFormatted()
        {
            string result = _helper.Format("{0} + {1} = {2}", 1, 2, 3);

            Assert.AreEqual("1 + 2 = 3", result);
        }

        [Test]
        public void Format_Params_NoArgs_ReturnsFormatString()
        {
            string result = _helper.Format("No placeholders");

            Assert.AreEqual("No placeholders", result);
        }

        [Test]
        public void Format_Params_NullArg_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format(null, "arg");
            });
        }

        [Test]
        public void Format_Params_MixedTypes_ReturnsFormatted()
        {
            string result = _helper.Format("Name: {0}, Age: {1}, Active: {2}", "Alice", 30, true);

            Assert.AreEqual("Name: Alice, Age: 30, Active: True", result);
        }

        #endregion

        #region Format<T>

        [Test]
        public void Format_SingleGeneric_IntArg_ReturnsFormatted()
        {
            string result = _helper.Format<int>("Value: {0}", 42);

            Assert.AreEqual("Value: 42", result);
        }

        [Test]
        public void Format_SingleGeneric_StringArg_ReturnsFormatted()
        {
            string result = _helper.Format<string>("Hello {0}!", "Test");

            Assert.AreEqual("Hello Test!", result);
        }

        [Test]
        public void Format_SingleGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int>(null, 1);
            });
        }

        #endregion

        #region Format<T1, T2>

        [Test]
        public void Format_TwoGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, string>("{0}: {1}", 1, "one");

            Assert.AreEqual("1: one", result);
        }

        [Test]
        public void Format_TwoGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int>(null, 1, 2);
            });
        }

        #endregion

        #region Format<T1, T2, T3>

        [Test]
        public void Format_ThreeGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int>("{0}-{1}-{2}", 1, 2, 3);

            Assert.AreEqual("1-2-3", result);
        }

        [Test]
        public void Format_ThreeGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int>(null, 1, 2, 3);
            });
        }

        #endregion

        #region Format<T1, T2, T3, T4>

        [Test]
        public void Format_FourGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int>("{0}{1}{2}{3}", 1, 2, 3, 4);

            Assert.AreEqual("1234", result);
        }

        [Test]
        public void Format_FourGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int>(null, 1, 2, 3, 4);
            });
        }

        #endregion

        #region Format<T1, T2, T3, T4, T5>

        [Test]
        public void Format_FiveGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int>("{0},{1},{2},{3},{4}", 1, 2, 3, 4, 5);

            Assert.AreEqual("1,2,3,4,5", result);
        }

        [Test]
        public void Format_FiveGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int>(null, 1, 2, 3, 4, 5);
            });
        }

        #endregion

        #region Format<T1..T6>

        [Test]
        public void Format_SixGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}", 1, 2, 3, 4, 5, 6);

            Assert.AreEqual("123456", result);
        }

        [Test]
        public void Format_SixGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int>(null, 1, 2, 3, 4, 5, 6);
            });
        }

        #endregion

        #region Format<T1..T7>

        [Test]
        public void Format_SevenGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}{6}", 1, 2, 3, 4, 5, 6, 7);

            Assert.AreEqual("1234567", result);
        }

        [Test]
        public void Format_SevenGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int, int>(null, 1, 2, 3, 4, 5, 6, 7);
            });
        }

        #endregion

        #region Format<T1..T8>

        [Test]
        public void Format_EightGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}{6}{7}", 1, 2, 3, 4, 5, 6, 7, 8);

            Assert.AreEqual("12345678", result);
        }

        [Test]
        public void Format_EightGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int, int, int>(null, 1, 2, 3, 4, 5, 6, 7, 8);
            });
        }

        #endregion

        #region Format<T1..T9>

        [Test]
        public void Format_NineGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}{6}{7}{8}", 1, 2, 3, 4, 5, 6, 7, 8, 9);

            Assert.AreEqual("123456789", result);
        }

        [Test]
        public void Format_NineGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int, int, int, int>(null, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            });
        }

        #endregion

        #region Format<T1..T10>

        [Test]
        public void Format_TenGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            Assert.AreEqual("12345678910", result);
        }

        [Test]
        public void Format_TenGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int, int, int, int, int>(null, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            });
        }

        #endregion

        #region Format<T1..T11>

        [Test]
        public void Format_ElevenGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);

            Assert.AreEqual("1234567891011", result);
        }

        [Test]
        public void Format_ElevenGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int, int, int, int, int, int>(
                    null, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
            });
        }

        #endregion

        #region Format<T1..T12>

        [Test]
        public void Format_TwelveGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int, int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

            Assert.AreEqual("123456789101112", result);
        }

        [Test]
        public void Format_TwelveGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int, int, int, int, int, int, int>(
                    null, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            });
        }

        #endregion

        #region Format<T1..T13>

        [Test]
        public void Format_ThirteenGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int, int, int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13);

            Assert.AreEqual("12345678910111213", result);
        }

        [Test]
        public void Format_ThirteenGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int, int, int, int, int, int, int, int>(
                    null, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13);
            });
        }

        #endregion

        #region Format<T1..T14>

        [Test]
        public void Format_FourteenGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int, int, int, int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14);

            Assert.AreEqual("1234567891011121314", result);
        }

        [Test]
        public void Format_FourteenGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int, int, int, int, int, int, int, int, int>(
                    null, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14);
            });
        }

        #endregion

        #region Format<T1..T15>

        [Test]
        public void Format_FifteenGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}",
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

            Assert.AreEqual("123456789101112131415", result);
        }

        [Test]
        public void Format_FifteenGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>(
                    null, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);
            });
        }

        #endregion

        #region Format<T1..T16>

        [Test]
        public void Format_SixteenGeneric_ReturnsFormatted()
        {
            string result = _helper.Format<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>(
                "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Assert.AreEqual("12345678910111213141516", result);
        }

        [Test]
        public void Format_SixteenGeneric_NullFormat_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.Format<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>(
                    null, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            });
        }

        #endregion

        #region Repeated calls reuse cached StringBuilder

        [Test]
        public void Format_CalledMultipleTimes_DoesNotAccumulatePreviousOutput()
        {
            _helper.Format("First: {0}", "AAAA");
            string result = _helper.Format("Second: {0}", "BBBB");

            Assert.AreEqual("Second: BBBB", result);
        }

        #endregion
    }
}
