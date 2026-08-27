using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityTextTests
    {
        private class StubTextHelper : GameFrameworkText.ITextHelper
        {
            public string Format(string format, params object[] args)
            {
                return "[STUB]" + string.Format(format, args);
            }

            public string Format<T>(string format, T arg)
            {
                return "[STUB]" + string.Format(format, arg);
            }

            public string Format<T1, T2>(string format, T1 arg1, T2 arg2)
            {
                return "[STUB]" + string.Format(format, arg1, arg2);
            }

            public string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3);
            }

            public string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4);
            }

            public string Format<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5);
            }

            public string Format<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6);
            }

            public string Format<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }

            public string Format<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }

            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }

            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }

            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            }

            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            }

            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            }

            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            }

            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            }

            public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
            {
                return "[STUB]" + string.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            }
        }

        [TearDown]
        public void TearDown()
        {
            GameFrameworkText.SetTextHelper(null);
        }

        #region Format without helper (default string.Format)

        [Test]
        public void Format_ParamsArgs_Works()
        {
            string result = GameFrameworkText.Format("Hello {0}", "World");
            Assert.AreEqual("Hello World", result);
        }

        [Test]
        public void Format_SingleArg_Works()
        {
            string result = GameFrameworkText.Format("Value: {0}", 42);
            Assert.AreEqual("Value: 42", result);
        }

        [Test]
        public void Format_TwoArgs_Works()
        {
            string result = GameFrameworkText.Format("{0} and {1}", "A", "B");
            Assert.AreEqual("A and B", result);
        }

        [Test]
        public void Format_ThreeArgs_Works()
        {
            string result = GameFrameworkText.Format("{0}-{1}-{2}", 1, 2, 3);
            Assert.AreEqual("1-2-3", result);
        }

        [Test]
        public void Format_FourArgs_Works()
        {
            string result = GameFrameworkText.Format("{0}{1}{2}{3}", "A", "B", "C", "D");
            Assert.AreEqual("ABCD", result);
        }

        [Test]
        public void Format_FiveArgs_Works()
        {
            string result = GameFrameworkText.Format("{0}{1}{2}{3}{4}", 1, 2, 3, 4, 5);
            Assert.AreEqual("12345", result);
        }

        [Test]
        public void Format_SixArgs_Works()
        {
            string result = GameFrameworkText.Format("{0}{1}{2}{3}{4}{5}", 1, 2, 3, 4, 5, 6);
            Assert.AreEqual("123456", result);
        }

        [Test]
        public void Format_SevenArgs_Works()
        {
            string result = GameFrameworkText.Format("{0}{1}{2}{3}{4}{5}{6}", 1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual("1234567", result);
        }

        [Test]
        public void Format_EightArgs_Works()
        {
            string result = GameFrameworkText.Format("{0}{1}{2}{3}{4}{5}{6}{7}", 1, 2, 3, 4, 5, 6, 7, 8);
            Assert.AreEqual("12345678", result);
        }

        [Test]
        public void Format_NullFormat_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                GameFrameworkText.Format(null, "arg");
            });
        }

        [Test]
        public void Format_NullFormat_SingleArg_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                GameFrameworkText.Format<int>(null, 1);
            });
        }

        [Test]
        public void Format_NullFormat_TwoArgs_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                GameFrameworkText.Format<int, int>(null, 1, 2);
            });
        }

        #endregion

        #region Format with custom helper

        [Test]
        public void Format_WithHelper_UsesHelper()
        {
            GameFrameworkText.SetTextHelper(new StubTextHelper());
            string result = GameFrameworkText.Format("Hello {0}", "World");
            Assert.AreEqual("[STUB]Hello World", result);
        }

        [Test]
        public void Format_SingleArg_WithHelper_UsesHelper()
        {
            GameFrameworkText.SetTextHelper(new StubTextHelper());
            string result = GameFrameworkText.Format("Value: {0}", 42);
            Assert.AreEqual("[STUB]Value: 42", result);
        }

        [Test]
        public void Format_TwoArgs_WithHelper_UsesHelper()
        {
            GameFrameworkText.SetTextHelper(new StubTextHelper());
            string result = GameFrameworkText.Format("{0} and {1}", "A", "B");
            Assert.AreEqual("[STUB]A and B", result);
        }

        [Test]
        public void Format_ThreeArgs_WithHelper_UsesHelper()
        {
            GameFrameworkText.SetTextHelper(new StubTextHelper());
            string result = GameFrameworkText.Format("{0}-{1}-{2}", 1, 2, 3);
            Assert.AreEqual("[STUB]1-2-3", result);
        }

        [Test]
        public void SetTextHelper_Null_ResetsHelper()
        {
            GameFrameworkText.SetTextHelper(new StubTextHelper());
            string stubResult = GameFrameworkText.Format("{0}", "test");
            Assert.IsTrue(stubResult.StartsWith("[STUB]"));

            GameFrameworkText.SetTextHelper(null);
            string defaultResult = GameFrameworkText.Format("{0}", "test");
            Assert.AreEqual("test", defaultResult);
        }

        #endregion
    }
}