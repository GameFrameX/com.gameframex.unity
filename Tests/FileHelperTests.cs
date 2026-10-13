using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class FileHelperTests
    {
        #region GetBytesSize

        [Test]
        public void GetBytesSize_Zero_ReturnsZeroBytes()
        {
            Assert.AreEqual("0B", FileHelper.GetBytesSize(0));
        }

        [Test]
        public void GetBytesSize_BelowOneKB_ReturnsBytes()
        {
            Assert.AreEqual("1023B", FileHelper.GetBytesSize(1023));
        }

        [Test]
        public void GetBytesSize_ExactlyOneKB_ReturnsOneKB()
        {
            Assert.AreEqual("1KB", FileHelper.GetBytesSize(1024));
        }

        [Test]
        public void GetBytesSize_OneMB()
        {
            Assert.AreEqual("1MB", FileHelper.GetBytesSize(1024 * 1024));
        }

        [Test]
        public void GetBytesSize_NonIntegralValue_TrimsTrailingZeros()
        {
            // 1536 bytes = 1.5KB
            Assert.AreEqual("1.5KB", FileHelper.GetBytesSize(1536));
        }

        [Test]
        public void GetBytesSize_FractionalValue_KeepsMeaningfulDigits()
        {
            // 1044480 bytes = 1020KB = 0.99609375MB -> "1MB"? No: 1020 < 1024, stays KB -> "1020KB"
            Assert.AreEqual("1020KB", FileHelper.GetBytesSize(1044480));
        }

        [Test]
        public void GetBytesSize_PB_UpperBound()
        {
            // 1024^5 = PB
            Assert.AreEqual("1PB", FileHelper.GetBytesSize(1024L * 1024 * 1024 * 1024 * 1024));
        }

        [Test]
        public void GetBytesSize_AbovePB_ClampsAtPBUnit()
        {
            long size = 1024L * 1024 * 1024 * 1024 * 1024 * 3; // 3 PB in bytes
            Assert.AreEqual("3PB", FileHelper.GetBytesSize(size));
        }

        #endregion
    }
}
