using System;
using GameFrameX.Runtime;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class NewtonsoftJsonHelperTests
    {
        private NewtonsoftJsonHelper _helper;

        [SetUp]
        public void SetUp()
        {
            _helper = new NewtonsoftJsonHelper();
        }

        #region ToJson

        [Test]
        public void ToJson_SimpleObject_ReturnsValidJson()
        {
            var obj = new JsonTestObject { Name = "Test", Value = 42 };

            string json = _helper.ToJson(obj);

            Assert.IsNotNull(json);
            Assert.IsTrue(json.Contains("\"Name\""));
            Assert.IsTrue(json.Contains("\"Test\""));
            Assert.IsTrue(json.Contains("\"Value\""));
            Assert.IsTrue(json.Contains("42"));
        }

        [Test]
        public void ToJson_NullObject_ReturnsNullString()
        {
            string json = _helper.ToJson(null);

            Assert.AreEqual("null", json);
        }

        [Test]
        public void ToJson_IntValue_ReturnsNumberString()
        {
            string json = _helper.ToJson(42);

            Assert.AreEqual("42", json);
        }

        [Test]
        public void ToJson_StringValue_ReturnsQuotedString()
        {
            string json = _helper.ToJson("hello");

            Assert.AreEqual("\"hello\"", json);
        }

        [Test]
        public void ToJson_BoolValue_ReturnsLowercaseBool()
        {
            string json = _helper.ToJson(true);

            Assert.AreEqual("true", json);
        }

        [Test]
        public void ToJson_Array_ReturnsJsonArray()
        {
            var arr = new int[] { 1, 2, 3 };

            string json = _helper.ToJson(arr);

            Assert.AreEqual("[1,2,3]", json);
        }

        [Test]
        public void ToJson_ObjectWithNullField_IncludesNullInOutput()
        {
            var obj = new JsonTestObject { Name = null, Value = 1 };

            string json = _helper.ToJson(obj);

            Assert.IsTrue(json.Contains("\"Name\":null"));
        }

        #endregion

        #region ToObject<T>

        [Test]
        public void ToObject_Generic_ValidJson_ReturnsObject()
        {
            string json = "{\"Name\":\"Test\",\"Value\":42}";

            var result = _helper.ToObject<JsonTestObject>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Name);
            Assert.AreEqual(42, result.Value);
        }

        [Test]
        public void ToObject_Generic_NullJson_ReturnsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _helper.ToObject<JsonTestObject>(null);
            });
        }

        [Test]
        public void ToObject_Generic_EmptyString_ReturnsNull()
        {
            var result = _helper.ToObject<JsonTestObject>("");
            Assert.IsNull(result);
        }

        [Test]
        public void ToObject_Generic_InvalidJson_Throws()
        {
            Assert.Throws<JsonReaderException>(() =>
            {
                _helper.ToObject<JsonTestObject>("not valid json");
            });
        }

        [Test]
        public void ToObject_Generic_IntType_ReturnsValue()
        {
            string json = "42";

            int result = _helper.ToObject<int>(json);

            Assert.AreEqual(42, result);
        }

        [Test]
        public void ToObject_Generic_StringType_ReturnsValue()
        {
            string json = "\"hello\"";

            string result = _helper.ToObject<string>(json);

            Assert.AreEqual("hello", result);
        }

        [Test]
        public void ToObject_Generic_ArrayType_ReturnsArray()
        {
            string json = "[1,2,3]";

            int[] result = _helper.ToObject<int[]>(json);

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
        }

        #endregion

        #region ToObject(Type, string)

        [Test]
        public void ToObject_NonGeneric_ValidJson_ReturnsObject()
        {
            string json = "{\"Name\":\"Test\",\"Value\":42}";

            object result = _helper.ToObject(typeof(JsonTestObject), json);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<JsonTestObject>(result);
            var typed = (JsonTestObject)result;
            Assert.AreEqual("Test", typed.Name);
            Assert.AreEqual(42, typed.Value);
        }

        [Test]
        public void ToObject_NonGeneric_NullType_ReturnsNull()
        {
            Assert.DoesNotThrow(() =>
            {
                _helper.ToObject(null, "{}");
            });
        }

        [Test]
        public void ToObject_NonGeneric_NullJson_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _helper.ToObject(typeof(JsonTestObject), null);
            });
        }

        [Test]
        public void ToObject_NonGeneric_IntType_ReturnsValue()
        {
            string json = "99";

            object result = _helper.ToObject(typeof(int), json);

            Assert.AreEqual(99, result);
        }

        #endregion

        #region Round-trip tests

        [Test]
        public void RoundTrip_SimpleObject_PreservesData()
        {
            var original = new JsonTestObject { Name = "RoundTrip", Value = 100 };

            string json = _helper.ToJson(original);
            var deserialized = _helper.ToObject<JsonTestObject>(json);

            Assert.AreEqual(original.Name, deserialized.Name);
            Assert.AreEqual(original.Value, deserialized.Value);
        }

        [Test]
        public void RoundTrip_NestedObject_PreservesData()
        {
            var original = new JsonNestedObject
            {
                Id = 1,
                Inner = new JsonTestObject { Name = "Inner", Value = 55 }
            };

            string json = _helper.ToJson(original);
            var deserialized = _helper.ToObject<JsonNestedObject>(json);

            Assert.AreEqual(original.Id, deserialized.Id);
            Assert.IsNotNull(deserialized.Inner);
            Assert.AreEqual("Inner", deserialized.Inner.Name);
            Assert.AreEqual(55, deserialized.Inner.Value);
        }

        [Test]
        public void RoundTrip_ArrayOfObjects_PreservesData()
        {
            var original = new JsonTestObject[]
            {
                new JsonTestObject { Name = "A", Value = 1 },
                new JsonTestObject { Name = "B", Value = 2 },
            };

            string json = _helper.ToJson(original);
            var deserialized = _helper.ToObject<JsonTestObject[]>(json);

            Assert.AreEqual(2, deserialized.Length);
            Assert.AreEqual("A", deserialized[0].Name);
            Assert.AreEqual("B", deserialized[1].Name);
        }

        [Test]
        public void RoundTrip_ObjectWithDefaultValues_PreservesDefaults()
        {
            var original = new JsonTestObject { Name = "Defaults", Value = 0 };

            string json = _helper.ToJson(original);
            var deserialized = _helper.ToObject<JsonTestObject>(json);

            Assert.AreEqual("Defaults", deserialized.Name);
            Assert.AreEqual(0, deserialized.Value);
        }

        #endregion

        #region Test data types

        private class JsonTestObject
        {
            public string Name;
            public int Value;
        }

        private class JsonNestedObject
        {
            public int Id;
            public JsonTestObject Inner;
        }

        #endregion
    }
}
