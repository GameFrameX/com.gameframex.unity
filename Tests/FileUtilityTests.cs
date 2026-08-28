using System.IO;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class FileUtilityTests
    {
        #region GetBytesSize

        [Test]
        public void GetBytesSize_Zero_ReturnsPlainBytes()
        {
            Assert.AreEqual("0B", FileUtility.GetBytesSize(0));
        }

        [Test]
        public void GetBytesSize_BelowOneKB_ReturnsBytesWithoutFormatting()
        {
            Assert.AreEqual("1B", FileUtility.GetBytesSize(1));
            Assert.AreEqual("1000B", FileUtility.GetBytesSize(1000));
            Assert.AreEqual("1023B", FileUtility.GetBytesSize(1023));
        }

        [Test]
        public void GetBytesSize_ExactlyOneKB_ReturnsOneKB()
        {
            Assert.AreEqual("1KB", FileUtility.GetBytesSize(1024));
        }

        [Test]
        public void GetBytesSize_FractionalKB_KeepsTwoDecimals()
        {
            Assert.AreEqual("1.5KB", FileUtility.GetBytesSize(1536));
            Assert.AreEqual("1.25KB", FileUtility.GetBytesSize(1280));
            Assert.AreEqual("10.01KB", FileUtility.GetBytesSize(10250));
        }

        [Test]
        public void GetBytesSize_TrailingZerosAreTrimmed()
        {
            // 10240 / 1024 = 10, "10.00" 去尾零后应为 "10" 而非 "10." 或 "10.00"
            Assert.AreEqual("10KB", FileUtility.GetBytesSize(10240));
        }

        [Test]
        public void GetBytesSize_HigherUnits()
        {
            Assert.AreEqual("1MB", FileUtility.GetBytesSize(1024L * 1024));
            Assert.AreEqual("1GB", FileUtility.GetBytesSize(1024L * 1024 * 1024));
            Assert.AreEqual("1TB", FileUtility.GetBytesSize(1024L * 1024 * 1024 * 1024));
            Assert.AreEqual("1PB", FileUtility.GetBytesSize(1024L * 1024 * 1024 * 1024 * 1024));
        }

        [Test]
        public void GetBytesSize_AbovePetaByte_CapsAtPetaUnit()
        {
            // 单位表上限为 PB, 超出后不再进位
            Assert.AreEqual("1024PB", FileUtility.GetBytesSize(1024L * 1024 * 1024 * 1024 * 1024 * 1024));
        }

        [Test]
        public void GetBytesSize_NegativeValue_ReturnsNegativeBytes()
        {
            Assert.AreEqual("-1B", FileUtility.GetBytesSize(-1));
        }

        #endregion

        #region DeleteIfExists

        [Test]
        public void DeleteIfExists_NonExistentFile_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                FileUtility.DeleteIfExists(Path.Combine(Path.GetTempPath(), "gfx_test_no_such_file.tmp"));
            });
        }

        [Test]
        public void DeleteIfExists_ExistingFile_DeletesFile()
        {
            string path = Path.Combine(Path.GetTempPath(), "gfx_test_delete_if_exists.tmp");
            File.WriteAllText(path, "x");

            FileUtility.DeleteIfExists(path);

            Assert.IsFalse(File.Exists(path));
        }

        #endregion
    }
}
