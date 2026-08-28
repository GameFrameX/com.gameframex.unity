using System.IO;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityDirectoryTests
    {
        #region RemoveEmptyDirectory

        [Test]
        public void RemoveEmptyDirectory_NullInput_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                DirectoryUtility.RemoveEmptyDirectory(null);
            });
        }

        [Test]
        public void RemoveEmptyDirectory_EmptyString_Throws()
        {
            Assert.Throws<GameFrameworkException>(() =>
            {
                DirectoryUtility.RemoveEmptyDirectory("");
            });
        }

        [Test]
        public void RemoveEmptyDirectory_NonExistentPath_ReturnsFalse()
        {
            string nonExistent = Path.Combine(Path.GetTempPath(), "GameFrameX_Test_NonExistent_" + System.Guid.NewGuid());
            bool result = DirectoryUtility.RemoveEmptyDirectory(nonExistent);
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
                bool result = DirectoryUtility.RemoveEmptyDirectory(tempDir);
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
                bool result = DirectoryUtility.RemoveEmptyDirectory(tempDir);
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
                bool result = DirectoryUtility.RemoveEmptyDirectory(tempDir);
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
                bool result = DirectoryUtility.RemoveEmptyDirectory(tempDir);
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