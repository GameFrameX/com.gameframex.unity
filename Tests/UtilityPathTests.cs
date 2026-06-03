using System.IO;
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
            string result = Utility.Path.GetRegularPath(null);
            Assert.IsNull(result);
        }

        [Test]
        public void GetRegularPath_ConvertsBackslashes()
        {
            string result = Utility.Path.GetRegularPath("folder\\subfolder\\file.txt");
            Assert.AreEqual("folder/subfolder/file.txt", result);
        }

        [Test]
        public void GetRegularPath_NoBackslashes_Unchanged()
        {
            string path = "folder/subfolder/file.txt";
            string result = Utility.Path.GetRegularPath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRegularPath_EmptyString()
        {
            string result = Utility.Path.GetRegularPath("");
            Assert.AreEqual("", result);
        }

        [Test]
        public void GetRegularPath_MixedSlashes()
        {
            string result = Utility.Path.GetRegularPath("folder/sub\\mixed/file");
            Assert.AreEqual("folder/sub/mixed/file", result);
        }

        [Test]
        public void GetRegularPath_WindowsDrive()
        {
            string result = Utility.Path.GetRegularPath("C:\\Users\\test");
            Assert.AreEqual("C:/Users/test", result);
        }

        #endregion

        #region GetRemotePath

        [Test]
        public void GetRemotePath_NullInput_ReturnsNull()
        {
            string result = Utility.Path.GetRemotePath(null);
            Assert.IsNull(result);
        }

        [Test]
        public void GetRemotePath_RelativePath_PrefixesFileProtocol()
        {
            string result = Utility.Path.GetRemotePath("path/to/file.txt");
            Assert.AreEqual("file:///path/to/file.txt", result);
        }

        [Test]
        public void GetRemotePath_AlreadyHasProtocol_Unchanged()
        {
            string path = "http://example.com/file";
            string result = Utility.Path.GetRemotePath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRemotePath_HttpsProtocol_Unchanged()
        {
            string path = "https://example.com/file";
            string result = Utility.Path.GetRemotePath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRemotePath_FileProtocol_Unchanged()
        {
            string path = "file:///path/to/file.txt";
            string result = Utility.Path.GetRemotePath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRemotePath_WindowsPath_ConvertsAndPrefixes()
        {
            string result = Utility.Path.GetRemotePath("C:\\path\\file.txt");
            Assert.AreEqual("file:///C:/path/file.txt", result);
        }

        [Test]
        public void GetRemotePath_EmptyString()
        {
            string result = Utility.Path.GetRemotePath("");
            Assert.AreEqual("file:///", result);
        }

        #endregion

        #region RemoveEmptyDirectory

        [Test]
        public void RemoveEmptyDirectory_NullInput_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Path.RemoveEmptyDirectory(null);
            });
        }

        [Test]
        public void RemoveEmptyDirectory_EmptyString_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                Utility.Path.RemoveEmptyDirectory("");
            });
        }

        [Test]
        public void RemoveEmptyDirectory_NonExistentPath_ReturnsFalse()
        {
            string nonExistent = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_NonExistent_" + System.Guid.NewGuid());
            bool result = Utility.Path.RemoveEmptyDirectory(nonExistent);
            Assert.IsFalse(result);
        }

        [Test]
        public void RemoveEmptyDirectory_EmptyDir_RemovesAndReturnsTrue()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_Empty_" + System.Guid.NewGuid());
            try
            {
                Directory.CreateDirectory(tempDir);
                Assert.IsTrue(Directory.Exists(tempDir));
                bool result = Utility.Path.RemoveEmptyDirectory(tempDir);
                Assert.IsTrue(result);
                Assert.IsFalse(Directory.Exists(tempDir));
            }
            finally
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        [Test]
        public void RemoveEmptyDirectory_DirWithFiles_ReturnsFalse()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_WithFiles_" + System.Guid.NewGuid());
            try
            {
                Directory.CreateDirectory(tempDir);
                File.WriteAllText(Path.Combine(tempDir, "test.txt"), "data");
                bool result = Utility.Path.RemoveEmptyDirectory(tempDir);
                Assert.IsFalse(result);
                Assert.IsTrue(Directory.Exists(tempDir));
            }
            finally
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        [Test]
        public void RemoveEmptyDirectory_DirWithEmptySubDir_RemovesBoth()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_Nested_" + System.Guid.NewGuid());
            try
            {
                Directory.CreateDirectory(tempDir);
                string subDir = Path.Combine(tempDir, "empty_sub");
                Directory.CreateDirectory(subDir);
                bool result = Utility.Path.RemoveEmptyDirectory(tempDir);
                Assert.IsTrue(result);
                Assert.IsFalse(Directory.Exists(tempDir));
            }
            finally
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        [Test]
        public void RemoveEmptyDirectory_DirWithNonEmptySubDir_ReturnsFalse()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_NestedFiles_" + System.Guid.NewGuid());
            try
            {
                Directory.CreateDirectory(tempDir);
                string subDir = Path.Combine(tempDir, "nonempty_sub");
                Directory.CreateDirectory(subDir);
                File.WriteAllText(Path.Combine(subDir, "file.txt"), "data");
                bool result = Utility.Path.RemoveEmptyDirectory(tempDir);
                Assert.IsFalse(result);
            }
            finally
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        #endregion
    }
}
