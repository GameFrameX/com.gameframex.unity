/*
using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class GameFrameworkGuardTests
    {
        #region NotNull

        [Test]
        public void NotNull_NonNullValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => GameFrameworkGuard.NotNull("hello", "param"));
        }

        [Test]
        public void NotNull_NullValue_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => GameFrameworkGuard.NotNull<string>(null, "param"));
        }

        [Test]
        public void NotNull_NullValue_ExceptionContainsParamName()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => GameFrameworkGuard.NotNull<string>(null, "myParam"));
            Assert.AreEqual("myParam", ex.ParamName);
        }

        #endregion

        #region NotNullOrEmpty

        [Test]
        public void NotNullOrEmpty_ValidString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => GameFrameworkGuard.NotNullOrEmpty("hello", "param"));
        }

        [Test]
        public void NotNullOrEmpty_NullString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => GameFrameworkGuard.NotNullOrEmpty(null, "param"));
        }

        [Test]
        public void NotNullOrEmpty_EmptyString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => GameFrameworkGuard.NotNullOrEmpty(string.Empty, "param"));
        }

        [Test]
        public void NotNullOrEmpty_NullString_ExceptionContainsParamName()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => GameFrameworkGuard.NotNullOrEmpty(null, "myParam"));
            Assert.AreEqual("myParam", ex.ParamName);
        }

        #endregion

        #region NotRange

        [Test]
        public void NotRange_ValueInRange_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => GameFrameworkGuard.NotRange(5, 1, 10, "param"));
        }

        [Test]
        public void NotRange_ValueAtMin_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => GameFrameworkGuard.NotRange(1, 1, 10, "param"));
        }

        [Test]
        public void NotRange_ValueAtMax_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => GameFrameworkGuard.NotRange(10, 1, 10, "param"));
        }

        [Test]
        public void NotRange_ValueBelowMin_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GameFrameworkGuard.NotRange(0, 1, 10, "param"));
        }

        [Test]
        public void NotRange_ValueAboveMax_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GameFrameworkGuard.NotRange(11, 1, 10, "param"));
        }

        [Test]
        public void NotRange_OutOfRange_ExceptionContainsParamName()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => GameFrameworkGuard.NotRange(99, 1, 10, "myParam"));
            Assert.AreEqual("myParam", ex.ParamName);
        }

        #endregion
    }
}
*/
