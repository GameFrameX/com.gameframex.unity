/*
using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class VariableTests
    {
        private class IntVariable : Variable<int>
        {
        }

        private class StringVariable : Variable<string>
        {
        }

        #region Type

        [Test]
        public void Type_IntVariable_ReturnsInt32()
        {
            var v = new IntVariable();
            Assert.AreEqual(typeof(int), v.Type);
        }

        [Test]
        public void Type_StringVariable_ReturnsString()
        {
            var v = new StringVariable();
            Assert.AreEqual(typeof(string), v.Type);
        }

        #endregion

        #region Value Get/Set

        [Test]
        public void Value_DefaultInt_IsZero()
        {
            var v = new IntVariable();
            Assert.AreEqual(0, v.Value);
        }

        [Test]
        public void Value_DefaultString_IsNull()
        {
            var v = new StringVariable();
            Assert.IsNull(v.Value);
        }

        [Test]
        public void Value_SetInt_ReturnsSetValue()
        {
            var v = new IntVariable();
            v.Value = 42;
            Assert.AreEqual(42, v.Value);
        }

        [Test]
        public void Value_SetString_ReturnsSetValue()
        {
            var v = new StringVariable();
            v.Value = "hello";
            Assert.AreEqual("hello", v.Value);
        }

        #endregion

        #region GetValue

        [Test]
        public void GetValue_ReturnsBoxedValue()
        {
            var v = new IntVariable();
            v.Value = 99;
            object result = v.GetValue();
            Assert.AreEqual(99, result);
            Assert.IsInstanceOf<int>(result);
        }

        [Test]
        public void GetValue_ReturnsNullForDefaultReferenceType()
        {
            var v = new StringVariable();
            Assert.IsNull(v.GetValue());
        }

        #endregion

        #region SetValue

        [Test]
        public void SetValue_CorrectType_SetsValue()
        {
            var v = new IntVariable();
            v.SetValue(42);
            Assert.AreEqual(42, v.Value);
        }

        [Test]
        public void SetValue_NullForReferenceType_SetsNull()
        {
            var v = new StringVariable();
            v.Value = "test";
            v.SetValue(null);
            Assert.IsNull(v.Value);
        }

        [Test]
        public void SetValue_WrongType_ThrowsGameFrameworkException()
        {
            var v = new IntVariable();
            Assert.Throws<GameFrameworkException>(() => v.SetValue("not an int"));
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_ResetsToDefaultValue()
        {
            var v = new IntVariable();
            v.Value = 100;
            v.Clear();
            Assert.AreEqual(0, v.Value);
        }

        [Test]
        public void Clear_ResetsStringToNull()
        {
            var v = new StringVariable();
            v.Value = "test";
            v.Clear();
            Assert.IsNull(v.Value);
        }

        #endregion

        #region ToString

        [Test]
        public void ToString_WithValue_ReturnsValueString()
        {
            var v = new IntVariable();
            v.Value = 42;
            Assert.AreEqual("42", v.ToString());
        }

        [Test]
        public void ToString_WithNullValue_ReturnsNullString()
        {
            var v = new StringVariable();
            Assert.AreEqual("<Null>", v.ToString());
        }

        [Test]
        public void ToString_WithStringValue_ReturnsValue()
        {
            var v = new StringVariable();
            v.Value = "hello";
            Assert.AreEqual("hello", v.ToString());
        }

        #endregion
    }
}
*/
