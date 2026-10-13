using System.IO;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class PathHelperTests
    {
        #region NormalizePath (absorbs legacy GetRegularPath semantics)

        [Test]
        public void NormalizePath_NullInput_ReturnsNull()
        {
            string result = PathHelper.NormalizePath(null);
            Assert.IsNull(result);
        }

        [Test]
        public void NormalizePath_ConvertsBackslashes()
        {
            string result = PathHelper.NormalizePath("folder\\subfolder\\file.txt");
            Assert.AreEqual("folder/subfolder/file.txt", result);
        }

        [Test]
        public void NormalizePath_NoBackslashes_Unchanged()
        {
            string path = "folder/subfolder/file.txt";
            string result = PathHelper.NormalizePath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void NormalizePath_EmptyString()
        {
            string result = PathHelper.NormalizePath("");
            Assert.AreEqual("", result);
        }

        [Test]
        public void NormalizePath_MixedSlashes()
        {
            string result = PathHelper.NormalizePath("folder/sub\\mixed/file");
            Assert.AreEqual("folder/sub/mixed/file", result);
        }

        [Test]
        public void NormalizePath_WindowsDrive()
        {
            string result = PathHelper.NormalizePath("C:\\Users\\test");
            Assert.AreEqual("C:/Users/test", result);
        }

        #endregion

        #region GetRemotePath

        [Test]
        public void GetRemotePath_NullInput_ReturnsNull()
        {
            string result = PathHelper.GetRemotePath(null);
            Assert.IsNull(result);
        }

        [Test]
        public void GetRemotePath_RelativePath_PrefixesFileProtocol()
        {
            string result = PathHelper.GetRemotePath("path/to/file.txt");
            Assert.AreEqual("file:///path/to/file.txt", result);
        }

        [Test]
        public void GetRemotePath_AlreadyHasProtocol_Unchanged()
        {
            string path = "http://example.com/file";
            string result = PathHelper.GetRemotePath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRemotePath_HttpsProtocol_Unchanged()
        {
            string path = "https://example.com/file";
            string result = PathHelper.GetRemotePath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRemotePath_FileProtocol_Unchanged()
        {
            string path = "file:///path/to/file.txt";
            string result = PathHelper.GetRemotePath(path);
            Assert.AreEqual(path, result);
        }

        [Test]
        public void GetRemotePath_WindowsPath_ConvertsAndPrefixes()
        {
            string result = PathHelper.GetRemotePath("C:\\path\\file.txt");
            Assert.AreEqual("file:///C:/path/file.txt", result);
        }

        [Test]
        public void GetRemotePath_EmptyString()
        {
            string result = PathHelper.GetRemotePath("");
            Assert.AreEqual("file:///", result);
        }

        #endregion

        #region RemoveEmptyDirectory

        [Test]
        public void RemoveEmptyDirectory_NullInput_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                PathHelper.RemoveEmptyDirectory(null);
            });
        }

        [Test]
        public void RemoveEmptyDirectory_EmptyString_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                PathHelper.RemoveEmptyDirectory("");
            });
        }

        [Test]
        public void RemoveEmptyDirectory_NonExistentPath_ReturnsFalse()
        {
            string nonExistent = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_NonExistent_" + System.Guid.NewGuid());
            bool result = PathHelper.RemoveEmptyDirectory(nonExistent);
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
                bool result = PathHelper.RemoveEmptyDirectory(tempDir);
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
                bool result = PathHelper.RemoveEmptyDirectory(tempDir);
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
                bool result = PathHelper.RemoveEmptyDirectory(tempDir);
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
                bool result = PathHelper.RemoveEmptyDirectory(tempDir);
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

        #region CreateIfNotExists / DeleteIfExists / ClearFiles (absorbs legacy directory utility semantics)

        [Test]
        public void CreateIfNotExists_CreatesMissingDirectory()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_Create_" + System.Guid.NewGuid());
            try
            {
                PathHelper.CreateIfNotExists(tempDir);
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
        public void CreateIfNotExists_ExistingDirectory_Unchanged()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_Exists_" + System.Guid.NewGuid());
            try
            {
                Directory.CreateDirectory(tempDir);
                PathHelper.CreateIfNotExists(tempDir);
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
        public void CreateIfNotExists_EmptyOrNull_DoesNothing()
        {
            Assert.DoesNotThrow(() =>
            {
                PathHelper.CreateIfNotExists(null);
                PathHelper.CreateIfNotExists("");
            });
        }

        [Test]
        public void DeleteIfExists_RemovesDirectoryWithFiles()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_Delete_" + System.Guid.NewGuid());
            Directory.CreateDirectory(tempDir);
            File.WriteAllText(Path.Combine(tempDir, "test.txt"), "data");
            PathHelper.DeleteIfExists(tempDir);
            Assert.IsFalse(Directory.Exists(tempDir));
        }

        [Test]
        public void DeleteIfExists_MissingDirectory_DoesNothing()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_DeleteMissing_" + System.Guid.NewGuid());
            Assert.DoesNotThrow(() => PathHelper.DeleteIfExists(tempDir));
        }

        [Test]
        public void ClearFiles_RemovesFilesButKeepsStructure()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_Clear_" + System.Guid.NewGuid());
            try
            {
                Directory.CreateDirectory(tempDir);
                string subDir = Path.Combine(tempDir, "sub");
                Directory.CreateDirectory(subDir);
                File.WriteAllText(Path.Combine(tempDir, "a.txt"), "data");
                File.WriteAllText(Path.Combine(subDir, "b.txt"), "data");
                PathHelper.ClearFiles(tempDir);
                Assert.IsTrue(Directory.Exists(tempDir));
                Assert.IsTrue(Directory.Exists(subDir));
                Assert.AreEqual(0, Directory.GetFiles(tempDir, "*", SearchOption.AllDirectories).Length);
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
        public void ClearFiles_MissingDirectory_DoesNothing()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_ClearMissing_" + System.Guid.NewGuid());
            Assert.DoesNotThrow(() => PathHelper.ClearFiles(tempDir));
        }

        #endregion

        #region PathHelper.Combine

        [Test]
        public void Combine_SingleSegment_ReturnsUnchanged()
        {
            string result = PathHelper.Combine("img.png");
            Assert.AreEqual("img.png", result);
        }

        [Test]
        public void Combine_TwoRelativeSegments_AddsSeparator()
        {
            string result = PathHelper.Combine("cache", "img.png");
            Assert.AreEqual("cache/img.png", result);
        }

        [Test]
        public void Combine_AbsolutePathSegment_AppendsSeparator()
        {
            string result = PathHelper.Combine("/Users/blank/cache", "img.png");
            Assert.AreEqual("/Users/blank/cache/img.png", result);
        }

        [Test]
        public void Combine_TrailingSlashSegment_DoesNotDuplicate()
        {
            string result = PathHelper.Combine("cache/", "img.png");
            Assert.AreEqual("cache/img.png", result);
        }

        [Test]
        public void Combine_PureSeparatorSegment_DoesNotDuplicate()
        {
            string result = PathHelper.Combine("/", "img.png");
            Assert.AreEqual("/img.png", result);
        }

        [Test]
        public void Combine_TrailingBackslashSegment_DoesNotDuplicate()
        {
            string result = PathHelper.Combine("dir\\", "file.txt");
            Assert.AreEqual("dir\\file.txt", result);
        }

        [Test]
        public void Combine_MultipleSegments_AbsolutePathJoint()
        {
            string result = PathHelper.Combine("/Users", "blank", "cache", "img.png");
            Assert.AreEqual("/Users/blank/cache/img.png", result);
        }

        #endregion
    }
}
