using System;
using System.IO;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class DefaultCompressionHelperTests
    {
        private DefaultCompressionHelper _helper;

        [SetUp]
        public void SetUp()
        {
            _helper = new DefaultCompressionHelper();
        }

        #region Compress/Decompress byte[] round-trip

        [Test]
        public void Compress_ThenDecompress_ByteRoundTrip()
        {
            byte[] original = Encoding.UTF8.GetBytes("Hello GameFrameX compression test!");
            var compressedStream = new MemoryStream();

            bool compressOk = _helper.Compress(original, 0, original.Length, compressedStream);
            Assert.IsTrue(compressOk, "Compress should succeed");

            compressedStream.Position = 0;
            var decompressedStream = new MemoryStream();

            bool decompressOk = _helper.Decompress(compressedStream.ToArray(), 0, (int)compressedStream.Length, decompressedStream);
            Assert.IsTrue(decompressOk, "Decompress should succeed");

            byte[] result = decompressedStream.ToArray();
            CollectionAssert.AreEqual(original, result, "Decompressed data should match original");
        }

        [Test]
        public void Compress_ThenDecompress_LargeData_RoundTrip()
        {
            byte[] original = new byte[10000];
            for (int i = 0; i < original.Length; i++)
            {
                original[i] = (byte)(i % 256);
            }

            var compressedStream = new MemoryStream();

            bool compressOk = _helper.Compress(original, 0, original.Length, compressedStream);
            Assert.IsTrue(compressOk);

            compressedStream.Position = 0;
            var decompressedStream = new MemoryStream();

            bool decompressOk = _helper.Decompress(compressedStream.ToArray(), 0, (int)compressedStream.Length, decompressedStream);
            Assert.IsTrue(decompressOk);

            CollectionAssert.AreEqual(original, decompressedStream.ToArray());
        }

        [Test]
        public void Compress_ThenDecompress_EmptyData_RoundTrip()
        {
            byte[] original = new byte[0];
            var compressedStream = new MemoryStream();

            bool compressOk = _helper.Compress(original, 0, 0, compressedStream);
            Assert.IsTrue(compressOk);

            var decompressedStream = new MemoryStream();

            bool decompressOk = _helper.Decompress(compressedStream.ToArray(), 0, (int)compressedStream.Length, decompressedStream);
            Assert.IsTrue(decompressOk);

            CollectionAssert.AreEqual(original, decompressedStream.ToArray());
        }

        #endregion

        #region Compress byte[] - validation

        [Test]
        public void Compress_Bytes_NullBytes_ReturnsFalse()
        {
            var output = new MemoryStream();

            bool result = _helper.Compress(null, 0, 0, output);

            Assert.IsFalse(result);
        }

        [Test]
        public void Compress_Bytes_NullStream_ReturnsFalse()
        {
            byte[] data = new byte[] { 1, 2, 3 };

            bool result = _helper.Compress(data, 0, data.Length, null);

            Assert.IsFalse(result);
        }

        [Test]
        public void Compress_Bytes_NegativeOffset_ReturnsFalse()
        {
            byte[] data = new byte[] { 1, 2, 3 };
            var output = new MemoryStream();

            bool result = _helper.Compress(data, -1, data.Length, output);

            Assert.IsFalse(result);
        }

        [Test]
        public void Compress_Bytes_NegativeLength_ReturnsFalse()
        {
            byte[] data = new byte[] { 1, 2, 3 };
            var output = new MemoryStream();

            bool result = _helper.Compress(data, 0, -1, output);

            Assert.IsFalse(result);
        }

        [Test]
        public void Compress_Bytes_OffsetPlusLengthExceedsArray_ReturnsFalse()
        {
            byte[] data = new byte[] { 1, 2, 3 };
            var output = new MemoryStream();

            bool result = _helper.Compress(data, 2, 5, output);

            Assert.IsFalse(result);
        }

        [Test]
        public void Compress_Bytes_PartialData_CompressesCorrectly()
        {
            byte[] data = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var compressedStream = new MemoryStream();

            bool compressOk = _helper.Compress(data, 3, 4, compressedStream);
            Assert.IsTrue(compressOk);

            compressedStream.Position = 0;
            var decompressedStream = new MemoryStream();

            bool decompressOk = _helper.Decompress(compressedStream.ToArray(), 0, (int)compressedStream.Length, decompressedStream);
            Assert.IsTrue(decompressOk);

            byte[] result = decompressedStream.ToArray();
            CollectionAssert.AreEqual(new byte[] { 3, 4, 5, 6 }, result);
        }

        #endregion

        #region Compress Stream - round-trip

        [Test]
        public void Compress_Stream_ThenDecompress_Stream_RoundTrip()
        {
            byte[] original = Encoding.UTF8.GetBytes("Stream compress test data");
            var inputStream = new MemoryStream(original);
            var compressedStream = new MemoryStream();

            bool compressOk = _helper.Compress(inputStream, compressedStream);
            Assert.IsTrue(compressOk);

            compressedStream.Position = 0;
            var decompressedStream = new MemoryStream();

            bool decompressOk = _helper.Decompress(compressedStream, decompressedStream);
            Assert.IsTrue(decompressOk);

            CollectionAssert.AreEqual(original, decompressedStream.ToArray());
        }

        [Test]
        public void Compress_Stream_NullStream_ReturnsFalse()
        {
            var output = new MemoryStream();

            bool result = _helper.Compress((Stream)null, output);

            Assert.IsFalse(result);
        }

        [Test]
        public void Compress_Stream_NullCompressedStream_ReturnsFalse()
        {
            var input = new MemoryStream(new byte[] { 1, 2, 3 });

            bool result = _helper.Compress(input, null);

            Assert.IsFalse(result);
        }

        #endregion

        #region Decompress byte[] - validation

        [Test]
        public void Decompress_Bytes_NullBytes_ReturnsFalse()
        {
            var output = new MemoryStream();

            bool result = _helper.Decompress(null, 0, 0, output);

            Assert.IsFalse(result);
        }

        [Test]
        public void Decompress_Bytes_NullStream_ReturnsFalse()
        {
            byte[] data = new byte[] { 1, 2, 3 };

            bool result = _helper.Decompress(data, 0, data.Length, null);

            Assert.IsFalse(result);
        }

        [Test]
        public void Decompress_Bytes_NegativeOffset_ReturnsFalse()
        {
            byte[] data = new byte[] { 1, 2, 3 };
            var output = new MemoryStream();

            bool result = _helper.Decompress(data, -1, data.Length, output);

            Assert.IsFalse(result);
        }

        [Test]
        public void Decompress_Bytes_NegativeLength_ReturnsFalse()
        {
            byte[] data = new byte[] { 1, 2, 3 };
            var output = new MemoryStream();

            bool result = _helper.Decompress(data, 0, -1, output);

            Assert.IsFalse(result);
        }

        [Test]
        public void Decompress_Bytes_OffsetPlusLengthExceedsArray_ReturnsFalse()
        {
            byte[] data = new byte[] { 1, 2, 3 };
            var output = new MemoryStream();

            bool result = _helper.Decompress(data, 1, 10, output);

            Assert.IsFalse(result);
        }

        [Test]
        public void Decompress_Bytes_InvalidGzipData_ReturnsFalse()
        {
            byte[] invalidData = new byte[] { 1, 2, 3, 4, 5 };
            var output = new MemoryStream();

            bool result = _helper.Decompress(invalidData, 0, invalidData.Length, output);

            Assert.IsFalse(result);
        }

        #endregion

        #region Decompress Stream - validation

        [Test]
        public void Decompress_Stream_NullStream_ReturnsFalse()
        {
            var output = new MemoryStream();

            bool result = _helper.Decompress((Stream)null, output);

            Assert.IsFalse(result);
        }

        [Test]
        public void Decompress_Stream_NullDecompressedStream_ReturnsFalse()
        {
            var input = new MemoryStream(new byte[] { 1, 2, 3 });

            bool result = _helper.Decompress(input, null);

            Assert.IsFalse(result);
        }

        [Test]
        public void Decompress_Stream_InvalidGzipData_ReturnsFalse()
        {
            var invalidStream = new MemoryStream(new byte[] { 1, 2, 3, 4, 5 });
            var output = new MemoryStream();

            bool result = _helper.Decompress(invalidStream, output);

            Assert.IsFalse(result);
        }

        #endregion

        #region Full byte[] compress/decompress round-trip using Decompress method

        [Test]
        public void Compress_Bytes_ThenDecompress_Bytes_FullRoundTrip()
        {
            byte[] original = Encoding.UTF8.GetBytes("Full round trip with separate compress and decompress");
            var compressedStream = new MemoryStream();

            bool compressOk = _helper.Compress(original, 0, original.Length, compressedStream);
            Assert.IsTrue(compressOk);

            byte[] compressedData = compressedStream.ToArray();
            var decompressedStream = new MemoryStream();

            bool decompressOk = _helper.Decompress(compressedData, 0, compressedData.Length, decompressedStream);
            Assert.IsTrue(decompressOk);

            CollectionAssert.AreEqual(original, decompressedStream.ToArray());
        }

        [Test]
        public void Compress_Bytes_ThenDecompress_Stream_FullRoundTrip()
        {
            byte[] original = Encoding.UTF8.GetBytes("Stream-based decompress round trip");
            var compressedStream = new MemoryStream();

            bool compressOk = _helper.Compress(original, 0, original.Length, compressedStream);
            Assert.IsTrue(compressOk);

            compressedStream.Position = 0;
            var decompressedStream = new MemoryStream();

            bool decompressOk = _helper.Decompress(compressedStream, decompressedStream);
            Assert.IsTrue(decompressOk);

            CollectionAssert.AreEqual(original, decompressedStream.ToArray());
        }

        #endregion
    }
}
