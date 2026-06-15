using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class LitJsonHelperTests
    {
        private LitJsonHelper _helper;

        [SetUp]
        public void SetUp()
        {
            _helper = new LitJsonHelper();
        }

        [Test]
        public void ToJson_NullObject_ReturnsNullString()
        {
            string json = _helper.ToJson(null);

            Assert.AreEqual("null", json);
        }

        [Test]
        public void ToJson_ObjectWithLitJsonIgnore_ExcludesIgnoredField()
        {
            var payload = new IgnorePayload { Visible = "yes", Hidden = "secret" };

            string json = _helper.ToJson(payload);

            Assert.AreEqual("{\"Visible\":\"yes\"}", json);
        }

        [Test]
        public void ToJson_DateTime_UsesUtcFormat()
        {
            var payload = new DatePayload { Time = new DateTime(2026, 6, 15, 12, 30, 45, DateTimeKind.Utc) };

            string json = _helper.ToJson(payload);

            Assert.AreEqual("{\"Time\":\"2026-06-15T12:30:45Z\"}", json);
        }

        [Test]
        public void ToJson_JsonProperty_UsesCustomPropertyName()
        {
            var payload = new PropertyNamePayload { PlayerId = 10086, DisplayName = "tester" };

            string json = _helper.ToJson(payload);

            Assert.AreEqual("{\"player_id\":10086,\"display_name\":\"tester\"}", json);
        }

        [Test]
        public void ToObject_Generic_EmptyString_ReturnsDefault()
        {
            var result = _helper.ToObject<JsonTestObject>(string.Empty);

            Assert.IsNull(result);
        }

        [Test]
        public void ToObject_Generic_NullJson_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _helper.ToObject<JsonTestObject>(null);
            });
        }

        [Test]
        public void ToObject_Generic_InvalidJson_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                _helper.ToObject<JsonTestObject>("not valid json");
            });
        }

        [Test]
        public void ToObject_DateTime_ParsesUtcPayload()
        {
            var result = _helper.ToObject<DatePayload>("{\"Time\":\"2026-06-15T12:30:45Z\"}");

            Assert.AreEqual(DateTimeKind.Utc, result.Time.Kind);
            Assert.AreEqual(new DateTime(2026, 6, 15, 12, 30, 45, DateTimeKind.Utc), result.Time);
        }

        [Test]
        public void ToObject_JsonProperty_UsesCustomPropertyName()
        {
            var result = _helper.ToObject<PropertyNamePayload>("{\"player_id\":10086,\"display_name\":\"tester\"}");

            Assert.AreEqual(10086, result.PlayerId);
            Assert.AreEqual("tester", result.DisplayName);
        }

        [Test]
        public void ToObject_PrivateSetter_DoesNotPopulatePrivateSetters()
        {
            var result = _helper.ToObject<PrivateSetterPayload>("{\"Id\":7,\"Name\":\"locked\"}");

            Assert.AreEqual(0, result.Id);
            Assert.IsNull(result.Name);
        }

        [Test]
        public void RoundTrip_SimpleObject_PreservesData()
        {
            var original = new JsonTestObject { Name = "RoundTrip", Value = 100 };

            string json = _helper.ToJson(original);
            var deserialized = _helper.ToObject<JsonTestObject>(json);

            Assert.AreEqual(original.Name, deserialized.Name);
            Assert.AreEqual(original.Value, deserialized.Value);
        }

        private sealed class JsonTestObject
        {
            public string Name;
            public int Value;
        }

        private sealed class IgnorePayload
        {
            public string Visible { get; set; }

            [GameFrameX.LitJSON.Runtime.JsonIgnore]
            public string Hidden { get; set; }
        }

        private sealed class DatePayload
        {
            public DateTime Time { get; set; }
        }

        private sealed class PropertyNamePayload
        {
            [GameFrameX.LitJSON.Runtime.JsonProperty("player_id")]
            public int PlayerId { get; set; }

            [GameFrameX.LitJSON.Runtime.JsonProperty("display_name")]
            public string DisplayName { get; set; }
        }

        private sealed class PrivateSetterPayload
        {
            public int Id { get; private set; }
            public string Name { get; private set; }
        }
    }
}
