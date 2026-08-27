using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityPathTests
    {
        #region GetRegularPath

        [Test]
        public void GetRegularPath_NullInput_ReturnsNull()
        {
            string result = PathUtility.GetRegularPath(null);
            Assert.IsNull(result);
        }

        [Test]
        public void GetRegularPath_ConvertsBackslashes()
        {
            string result = PathUtility.GetRegularPath("folder\\subfolder\\file.txt");
            Assert.AreEqual("folder/subfolder/file.txt", result);
        }

        [Test]
        public void GetRegularPath_NoBackslashes_Unchanged()
        {
            string path = "folder/subfolder/file.txt";
            string result = PathUtility.GetRegularPath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRegularPath_EmptyString()
        {
            string result = PathUtility.GetRegularPath("");
            Assert.AreEqual("", result);
        }

        [Test]
        public void GetRegularPath_MixedSlashes()
        {
            string result = PathUtility.GetRegularPath("folder/sub\\mixed/file");
            Assert.AreEqual("folder/sub/mixed/file", result);
        }

        [Test]
        public void GetRegularPath_WindowsDrive()
        {
            string result = PathUtility.GetRegularPath("C:\\Users\\test");
            Assert.AreEqual("C:/Users/test", result);
        }

        #endregion

        #region GetRemotePath

        [Test]
        public void GetRemotePath_NullInput_ReturnsNull()
        {
            string result = PathUtility.GetRemotePath(null);
            Assert.IsNull(result);
        }

        [Test]
        public void GetRemotePath_RelativePath_PrefixesFileProtocol()
        {
            string result = PathUtility.GetRemotePath("path/to/file.txt");
            Assert.AreEqual("file:///path/to/file.txt", result);
        }

        [Test]
        public void GetRemotePath_AlreadyHasProtocol_Unchanged()
        {
            string path = "http://example.com/file";
            string result = PathUtility.GetRemotePath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRemotePath_HttpsProtocol_Unchanged()
        {
            string path = "https://example.com/file";
            string result = PathUtility.GetRemotePath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRemotePath_FileProtocol_Unchanged()
        {
            string path = "file:///path/to/file.txt";
            string result = PathUtility.GetRemotePath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRemotePath_WindowsPath_ConvertsAndPrefixes()
        {
            string result = PathUtility.GetRemotePath("C:\\path\\file.txt");
            Assert.AreEqual("file:///C:/path/file.txt", result);
        }

        [Test]
        public void GetRemotePath_EmptyString()
        {
            string result = PathUtility.GetRemotePath("");
            Assert.AreEqual("file:///", result);
        }

        #endregion
    }
}