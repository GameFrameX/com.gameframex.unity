/*
using System.IO;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class GameFrameworkSerializerTests
    {
        private class TestSerializer : GameFrameworkSerializer<string>
        {
            private static readonly byte[] Header = new byte[] { (byte)'T', (byte)'E', (byte)'S' };

            protected override byte[] GetHeader()
            {
                return Header;
            }
        }

        private TestSerializer _serializer;

        [SetUp]
        public void SetUp()
        {
            _serializer = new TestSerializer();
        }

        #region RegisterSerializeCallback

        [Test]
        public void RegisterSerializeCallback_NullCallback_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _serializer.RegisterSerializeCallback(1, null));
        }

        [Test]
        public void RegisterSerializeCallback_ValidCallback_DoesNotThrow()
        {
            GameFrameworkSerializer<string>.SerializeCallback callback = (stream, data) => true;
            Assert.DoesNotThrow(() => _serializer.RegisterSerializeCallback(1, callback));
        }

        #endregion

        #region RegisterDeserializeCallback

        [Test]
        public void RegisterDeserializeCallback_NullCallback_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _serializer.RegisterDeserializeCallback(1, null));
        }

        [Test]
        public void RegisterDeserializeCallback_ValidCallback_DoesNotThrow()
        {
            GameFrameworkSerializer<string>.DeserializeCallback callback = (stream) => "result";
            Assert.DoesNotThrow(() => _serializer.RegisterDeserializeCallback(1, callback));
        }

        #endregion

        #region RegisterTryGetValueCallback

        [Test]
        public void RegisterTryGetValueCallback_NullCallback_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _serializer.RegisterTryGetValueCallback(1, null));
        }

        [Test]
        public void RegisterTryGetValueCallback_ValidCallback_DoesNotThrow()
        {
            GameFrameworkSerializer<string>.TryGetValueCallback callback = (stream, key, out object value) => { value = null; return false; };
            Assert.DoesNotThrow(() => _serializer.RegisterTryGetValueCallback(1, callback));
        }

        #endregion

        #region Serialize

        [Test]
        public void Serialize_NoCallbacks_ThrowsGameFrameworkException()
        {
            using (var stream = new MemoryStream())
            {
                Assert.Throws<GameFrameworkException>(() => _serializer.Serialize(stream, "test"));
            }
        }

        [Test]
        public void Serialize_WithCallback_WritesHeaderAndVersion()
        {
            _serializer.RegisterSerializeCallback(1, (stream, data) =>
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                stream.Write(bytes, 0, bytes.Length);
                return true;
            });

            using (var stream = new MemoryStream())
            {
                bool result = _serializer.Serialize(stream, "hello");
                Assert.IsTrue(result);
                stream.Position = 0;
                Assert.AreEqual((byte)'T', stream.ReadByte());
                Assert.AreEqual((byte)'E', stream.ReadByte());
                Assert.AreEqual((byte)'S', stream.ReadByte());
                Assert.AreEqual(1, stream.ReadByte());
            }
        }

        [Test]
        public void Serialize_SpecificVersion_WritesCorrectVersion()
        {
            _serializer.RegisterSerializeCallback(1, (s, d) => true);
            _serializer.RegisterSerializeCallback(5, (s, d) => true);

            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(stream, "test", 5);
                stream.Position = 3;
                Assert.AreEqual(5, stream.ReadByte());
            }
        }

        [Test]
        public void Serialize_UnknownVersion_ThrowsGameFrameworkException()
        {
            _serializer.RegisterSerializeCallback(1, (s, d) => true);

            using (var stream = new MemoryStream())
            {
                Assert.Throws<GameFrameworkException>(() => _serializer.Serialize(stream, "test", 99));
            }
        }

        [Test]
        public void Serialize_DefaultVersion_UsesLatestRegisteredVersion()
        {
            _serializer.RegisterSerializeCallback(1, (s, d) => true);
            _serializer.RegisterSerializeCallback(3, (s, d) => true);

            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(stream, "test");
                stream.Position = 3;
                Assert.AreEqual(3, stream.ReadByte());
            }
        }

        #endregion

        #region Deserialize

        [Test]
        public void Deserialize_InvalidHeader_ThrowsGameFrameworkException()
        {
            _serializer.RegisterDeserializeCallback(1, (s) => "result");
            byte[] data = new byte[] { (byte)'X', (byte)'Y', (byte)'Z', 1 };
            using (var stream = new MemoryStream(data))
            {
                Assert.Throws<GameFrameworkException>(() => _serializer.Deserialize(stream));
            }
        }

        [Test]
        public void Deserialize_ValidData_ReturnsDeserializedValue()
        {
            _serializer.RegisterSerializeCallback(1, (stream, data) =>
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                stream.Write(bytes, 0, bytes.Length);
                return true;
            });
            _serializer.RegisterDeserializeCallback(1, (stream) =>
            {
                byte[] buffer = new byte[5];
                stream.Read(buffer, 0, 5);
                return System.Text.Encoding.UTF8.GetString(buffer);
            });

            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(stream, "hello");
                stream.Position = 0;
                string result = _serializer.Deserialize(stream);
                Assert.AreEqual("hello", result);
            }
        }

        [Test]
        public void Deserialize_UnknownVersion_ThrowsGameFrameworkException()
        {
            _serializer.RegisterDeserializeCallback(1, (s) => "result");
            byte[] data = new byte[] { (byte)'T', (byte)'E', (byte)'S', 99 };
            using (var stream = new MemoryStream(data))
            {
                Assert.Throws<GameFrameworkException>(() => _serializer.Deserialize(stream));
            }
        }

        #endregion

        #region TryGetValue

        [Test]
        public void TryGetValue_InvalidHeader_ReturnsFalse()
        {
            byte[] data = new byte[] { (byte)'X', (byte)'Y', (byte)'Z', 1 };
            using (var stream = new MemoryStream(data))
            {
                bool result = _serializer.TryGetValue(stream, "key", out object value);
                Assert.IsFalse(result);
            }
        }

        [Test]
        public void TryGetValue_UnknownVersion_ReturnsFalse()
        {
            _serializer.RegisterTryGetValueCallback(1, (s, k, out object v) => { v = "found"; return true; });
            byte[] data = new byte[] { (byte)'T', (byte)'E', (byte)'S', 99 };
            using (var stream = new MemoryStream(data))
            {
                bool result = _serializer.TryGetValue(stream, "key", out object value);
                Assert.IsFalse(result);
            }
        }

        [Test]
        public void TryGetValue_ValidData_ReturnsValue()
        {
            _serializer.RegisterTryGetValueCallback(1, (s, k, out object v) =>
            {
                v = "value_for_" + k;
                return true;
            });

            using (var stream = new MemoryStream())
            {
                stream.WriteByte((byte)'T');
                stream.WriteByte((byte)'E');
                stream.WriteByte((byte)'S');
                stream.WriteByte(1);
                stream.Position = 0;

                bool result = _serializer.TryGetValue(stream, "myKey", out object value);
                Assert.IsTrue(result);
                Assert.AreEqual("value_for_myKey", value);
            }
        }

        #endregion
    }
}
*/
