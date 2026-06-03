using System.IO;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    public class StringExtensionsCreateAsDirectoryTests
    {
        private string _tempRoot;

        [SetUp]
        public void SetUp()
        {
            _tempRoot = Path.Combine(Path.GetTempPath(), "GFX_Test_" + Path.GetRandomFileName());
            Directory.CreateDirectory(_tempRoot);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_tempRoot))
            {
                Directory.Delete(_tempRoot, true);
            }
        }

        #region 边界输入

        [Test]
        public void CreateAsDirectory_NullPath_DoesNothing()
        {
            Assert.DoesNotThrow(() =>
            {
                ((string)null).CreateAsDirectory();
            });
        }

        [Test]
        public void CreateAsDirectory_EmptyString_DoesNothing()
        {
            Assert.DoesNotThrow(() =>
            {
                string.Empty.CreateAsDirectory();
            });
        }

        [Test]
        public void CreateAsDirectory_WhitespaceString_DoesNothing()
        {
            Assert.DoesNotThrow(() =>
            {
                " ".CreateAsDirectory();
            });
        }

        #endregion

        #region 基本目录创建

        [Test]
        public void CreateAsDirectory_SingleDir_CreatesDirectory()
        {
            string dir = Path.Combine(_tempRoot, "single");
            dir.CreateAsDirectory();
            Assert.IsTrue(Directory.Exists(dir), "应创建单个目录");
        }

        [Test]
        public void CreateAsDirectory_AlreadyExists_DoesNotThrow()
        {
            string dir = Path.Combine(_tempRoot, "exists");
            Directory.CreateDirectory(dir);
            Assert.DoesNotThrow(() =>
            {
                dir.CreateAsDirectory();
            });
            Assert.IsTrue(Directory.Exists(dir));
        }

        #endregion

        #region 递归创建（核心验证）

        [Test]
        public void CreateAsDirectory_NestedThreeLevels_CreatesAll()
        {
            string dir = Path.Combine(_tempRoot, "a", "b", "c");
            dir.CreateAsDirectory();

            Assert.IsTrue(Directory.Exists(dir), "最深层目录应被创建");
            Assert.IsTrue(Directory.Exists(Path.Combine(_tempRoot, "a")), "第1层应被创建");
            Assert.IsTrue(Directory.Exists(Path.Combine(_tempRoot, "a", "b")), "第2层应被创建");
        }

        [Test]
        public void CreateAsDirectory_FiveLevelsDeep_CreatesAll()
        {
            string dir = Path.Combine(_tempRoot, "l1", "l2", "l3", "l4", "l5");
            dir.CreateAsDirectory();

            Assert.IsTrue(Directory.Exists(dir), "5层深度应全部创建");
        }

        [Test]
        public void CreateAsDirectory_PartialExists_OnlyCreatesMissing()
        {
            string existing = Path.Combine(_tempRoot, "partial");
            Directory.CreateDirectory(existing);

            string deep = Path.Combine(existing, "new1", "new2");
            deep.CreateAsDirectory();

            Assert.IsTrue(Directory.Exists(deep), "应在已有目录下继续创建");
        }

        #endregion

        #region isFile=true 分支

        [Test]
        public void CreateAsDirectory_IsFile_CreatesParentDir()
        {
            string file = Path.Combine(_tempRoot, "parent", "file.txt");
            file.CreateAsDirectory(isFile: true);

            string parent = Path.Combine(_tempRoot, "parent");
            Assert.IsTrue(Directory.Exists(parent), "应创建文件的父目录");
            Assert.IsFalse(File.Exists(file), "不应创建文件本身");
        }

        [Test]
        public void CreateAsDirectory_IsFile_DeepPath_CreatesAllParents()
        {
            string file = Path.Combine(_tempRoot, "d1", "d2", "d3", "data.bin");
            file.CreateAsDirectory(isFile: true);

            string deepest = Path.Combine(_tempRoot, "d1", "d2", "d3");
            Assert.IsTrue(Directory.Exists(deepest), "应递归创建所有父目录");
        }

        [Test]
        public void CreateAsDirectory_IsFile_OnlyFilename_DoesNothing()
        {
            // "file.txt" → GetDirectoryName 返回 ""，不应创建任何东西
            Assert.DoesNotThrow(() =>
            {
                "file.txt".CreateAsDirectory(isFile: true);
            });
        }

        [Test]
        public void CreateAsDirectory_IsFile_ParentExists_DoesNotThrow()
        {
            string parent = Path.Combine(_tempRoot, "existing_parent");
            Directory.CreateDirectory(parent);
            string file = Path.Combine(parent, "output.dat");

            Assert.DoesNotThrow(() =>
            {
                file.CreateAsDirectory(isFile: true);
            });
        }

        #endregion

        #region 路径格式

        [Test]
        public void CreateAsDirectory_TrailingSlash_CreatesDirectory()
        {
            string dir = Path.Combine(_tempRoot, "trailing") + Path.DirectorySeparatorChar;
            dir.CreateAsDirectory();
            Assert.IsTrue(Directory.Exists(dir.TrimEnd(Path.DirectorySeparatorChar)));
        }

        [Test]
        public void CreateAsDirectory_RootPath_DoesNotThrow()
        {
            // 根目录已存在，不应抛异常
            Assert.DoesNotThrow(() =>
            {
                Path.GetPathRoot(_tempRoot).CreateAsDirectory();
            });
        }

        #endregion
    }
}
