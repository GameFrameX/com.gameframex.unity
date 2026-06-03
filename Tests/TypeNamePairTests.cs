/*
using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TypeNamePairTests
    {
        #region Constructor

        [Test]
        public void Constructor_WithTypeOnly_SetsNameToEmpty()
        {
            var pair = new TypeNamePair(typeof(int));
            Assert.AreEqual(typeof(int), pair.Type);
            Assert.AreEqual(string.Empty, pair.Name);
        }

        [Test]
        public void Constructor_WithTypeAndName_SetsBoth()
        {
            var pair = new TypeNamePair(typeof(string), "TestName");
            Assert.AreEqual(typeof(string), pair.Type);
            Assert.AreEqual("TestName", pair.Name);
        }

        [Test]
        public void Constructor_NullType_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => new TypeNamePair(null));
        }

        [Test]
        public void Constructor_NullTypeWithName_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => new TypeNamePair(null, "name"));
        }

        [Test]
        public void Constructor_NullName_SetsNameToEmpty()
        {
            var pair = new TypeNamePair(typeof(int), null);
            Assert.AreEqual(string.Empty, pair.Name);
        }

        #endregion

        #region ToString

        [Test]
        public void ToString_NoName_ReturnsTypeFullName()
        {
            var pair = new TypeNamePair(typeof(int));
            string result = pair.ToString();
            Assert.AreEqual(typeof(int).FullName, result);
        }

        [Test]
        public void ToString_WithName_ReturnsTypeDotName()
        {
            var pair = new TypeNamePair(typeof(int), "MyName");
            string result = pair.ToString();
            Assert.AreEqual(string.Format("{0}.{1}", typeof(int).FullName, "MyName"), result);
        }

        [Test]
        public void ToString_EmptyName_ReturnsTypeFullName()
        {
            var pair = new TypeNamePair(typeof(string), string.Empty);
            string result = pair.ToString();
            Assert.AreEqual(typeof(string).FullName, result);
        }

        #endregion

        #region Equality

        [Test]
        public void Equals_SameTypeAndName_ReturnsTrue()
        {
            var a = new TypeNamePair(typeof(int), "Test");
            var b = new TypeNamePair(typeof(int), "Test");
            Assert.IsTrue(a.Equals(b));
        }

        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            var a = new TypeNamePair(typeof(int), "Test");
            var b = new TypeNamePair(typeof(string), "Test");
            Assert.IsFalse(a.Equals(b));
        }

        [Test]
        public void Equals_DifferentName_ReturnsFalse()
        {
            var a = new TypeNamePair(typeof(int), "Test1");
            var b = new TypeNamePair(typeof(int), "Test2");
            Assert.IsFalse(a.Equals(b));
        }

        [Test]
        public void Equals_ObjectBoxed_ReturnsTrue()
        {
            var a = new TypeNamePair(typeof(int), "Test");
            object b = new TypeNamePair(typeof(int), "Test");
            Assert.IsTrue(a.Equals(b));
        }

        [Test]
        public void Equals_NullObject_ReturnsFalse()
        {
            var a = new TypeNamePair(typeof(int), "Test");
            Assert.IsFalse(a.Equals(null));
        }

        [Test]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            var a = new TypeNamePair(typeof(int), "Test");
            Assert.IsFalse(a.Equals("not a TypeNamePair"));
        }

        #endregion

        #region Operators

        [Test]
        public void OperatorEquality_SameValues_ReturnsTrue()
        {
            var a = new TypeNamePair(typeof(int), "Test");
            var b = new TypeNamePair(typeof(int), "Test");
            Assert.IsTrue(a == b);
        }

        [Test]
        public void OperatorInequality_DifferentValues_ReturnsTrue()
        {
            var a = new TypeNamePair(typeof(int), "Test1");
            var b = new TypeNamePair(typeof(int), "Test2");
            Assert.IsTrue(a != b);
        }

        [Test]
        public void OperatorEquality_SameValuesNoName_ReturnsTrue()
        {
            var a = new TypeNamePair(typeof(int));
            var b = new TypeNamePair(typeof(int));
            Assert.IsTrue(a == b);
        }

        #endregion

        #region GetHashCode

        [Test]
        public void GetHashCode_SameValues_ReturnsSameHash()
        {
            var a = new TypeNamePair(typeof(int), "Test");
            var b = new TypeNamePair(typeof(int), "Test");
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void GetHashCode_DifferentValues_ReturnsDifferentHash()
        {
            var a = new TypeNamePair(typeof(int), "Test1");
            var b = new TypeNamePair(typeof(int), "Test2");
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        #endregion
    }
}
*/
