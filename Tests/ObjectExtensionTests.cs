using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class ObjectExtensionTests
    {
        #region IsNull

        [Test]
        public void IsNull_NullObject_ReturnsTrue()
        {
            object obj = null;

            Assert.IsTrue(obj.IsNull());
        }

        [Test]
        public void IsNull_NonNullObject_ReturnsFalse()
        {
            object obj = new object();

            Assert.IsFalse(obj.IsNull());
        }

        [Test]
        public void IsNull_BoxedInt_ReturnsFalse()
        {
            object obj = 42;

            Assert.IsFalse(obj.IsNull());
        }

        [Test]
        public void IsNull_EmptyString_ReturnsFalse()
        {
            object obj = string.Empty;

            Assert.IsFalse(obj.IsNull());
        }

        #endregion

        #region IsNotNull

        [Test]
        public void IsNotNull_NullObject_ReturnsFalse()
        {
            object obj = null;

            Assert.IsFalse(obj.IsNotNull());
        }

        [Test]
        public void IsNotNull_NonNullObject_ReturnsTrue()
        {
            object obj = new object();

            Assert.IsTrue(obj.IsNotNull());
        }

        [Test]
        public void IsNotNull_BoxedValue_ReturnsTrue()
        {
            object obj = 0;

            Assert.IsTrue(obj.IsNotNull());
        }

        #endregion

        #region CheckNull

        [Test]
        public void CheckNull_NullObject_ThrowsArgumentNullException()
        {
            object obj = null;

            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                obj.CheckNull("paramName");
            });

            Assert.AreEqual("paramName", ex.ParamName);
        }

        [Test]
        public void CheckNull_NonNullObject_DoesNotThrow()
        {
            object obj = new object();

            Assert.DoesNotThrow(() =>
            {
                obj.CheckNull("paramName");
            });
        }

        [Test]
        public void CheckNull_BoxedInt_DoesNotThrow()
        {
            object obj = 0;

            Assert.DoesNotThrow(() =>
            {
                obj.CheckNull("test");
            });
        }

        #endregion
    }
}
