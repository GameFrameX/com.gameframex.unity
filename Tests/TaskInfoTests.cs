/*
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TaskInfoTests
    {
        #region Constructor

        [Test]
        public void Constructor_SetsAllProperties()
        {
            var info = new TaskInfo(1, "tag", 5, "userData", TaskStatus.Todo, "description");
            Assert.IsTrue(info.IsValid);
            Assert.AreEqual(1, info.SerialId);
            Assert.AreEqual("tag", info.Tag);
            Assert.AreEqual(5, info.Priority);
            Assert.AreEqual("userData", info.UserData);
            Assert.AreEqual(TaskStatus.Todo, info.Status);
            Assert.AreEqual("description", info.Description);
        }

        #endregion

        #region Default Struct

        [Test]
        public void Default_IsNotValid()
        {
            TaskInfo info = default(TaskInfo);
            Assert.IsFalse(info.IsValid);
        }

        [Test]
        public void Default_SerialId_ThrowsGameFrameworkException()
        {
            TaskInfo info = default(TaskInfo);
            Assert.Throws<GameFrameworkException>(() =>
            {
                var _ = info.SerialId;
            });
        }

        [Test]
        public void Default_Tag_ThrowsGameFrameworkException()
        {
            TaskInfo info = default(TaskInfo);
            Assert.Throws<GameFrameworkException>(() =>
            {
                var _ = info.Tag;
            });
        }

        [Test]
        public void Default_Priority_ThrowsGameFrameworkException()
        {
            TaskInfo info = default(TaskInfo);
            Assert.Throws<GameFrameworkException>(() =>
            {
                var _ = info.Priority;
            });
        }

        [Test]
        public void Default_UserData_ThrowsGameFrameworkException()
        {
            TaskInfo info = default(TaskInfo);
            Assert.Throws<GameFrameworkException>(() =>
            {
                var _ = info.UserData;
            });
        }

        [Test]
        public void Default_Status_ThrowsGameFrameworkException()
        {
            TaskInfo info = default(TaskInfo);
            Assert.Throws<GameFrameworkException>(() =>
            {
                var _ = info.Status;
            });
        }

        [Test]
        public void Default_Description_ThrowsGameFrameworkException()
        {
            TaskInfo info = default(TaskInfo);
            Assert.Throws<GameFrameworkException>(() =>
            {
                var _ = info.Description;
            });
        }

        #endregion

        #region Status Values

        [Test]
        public void Constructor_WithDoingStatus()
        {
            var info = new TaskInfo(1, "tag", 0, null, TaskStatus.Doing, null);
            Assert.AreEqual(TaskStatus.Doing, info.Status);
        }

        [Test]
        public void Constructor_WithDoneStatus()
        {
            var info = new TaskInfo(1, "tag", 0, null, TaskStatus.Done, null);
            Assert.AreEqual(TaskStatus.Done, info.Status);
        }

        #endregion
    }
}
*/
