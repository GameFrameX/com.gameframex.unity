/*
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TaskBaseTests
    {
        private class ConcreteTask : TaskBase
        {
            public override string Description
            {
                get { return "ConcreteTestTask"; }
            }
        }

        #region Default Values

        [Test]
        public void Default_SerialId_IsZero()
        {
            var task = new ConcreteTask();
            Assert.AreEqual(0, task.SerialId);
        }

        [Test]
        public void Default_Tag_IsNull()
        {
            var task = new ConcreteTask();
            Assert.IsNull(task.Tag);
        }

        [Test]
        public void Default_Priority_IsDefaultPriority()
        {
            var task = new ConcreteTask();
            Assert.AreEqual(TaskBase.DefaultPriority, task.Priority);
            Assert.AreEqual(0, task.Priority);
        }

        [Test]
        public void Default_UserData_IsNull()
        {
            var task = new ConcreteTask();
            Assert.IsNull(task.UserData);
        }

        [Test]
        public void Default_Done_IsFalse()
        {
            var task = new ConcreteTask();
            Assert.IsFalse(task.Done);
        }

        [Test]
        public void DefaultPriority_IsZero()
        {
            Assert.AreEqual(0, TaskBase.DefaultPriority);
        }

        #endregion

        #region Initialize

        [Test]
        public void Initialize_SetsAllProperties()
        {
            var task = new ConcreteTask();
            task.Initialize(42, "myTag", 10, "userData");
            Assert.AreEqual(42, task.SerialId);
            Assert.AreEqual("myTag", task.Tag);
            Assert.AreEqual(10, task.Priority);
            Assert.AreEqual("userData", task.UserData);
            Assert.IsFalse(task.Done);
        }

        [Test]
        public void Initialize_SetsDoneToFalse()
        {
            var task = new ConcreteTask();
            task.Done = true;
            task.Initialize(1, "tag", 0, null);
            Assert.IsFalse(task.Done);
        }

        #endregion

        #region Done

        [Test]
        public void Done_CanBeSetToTrue()
        {
            var task = new ConcreteTask();
            task.Done = true;
            Assert.IsTrue(task.Done);
        }

        [Test]
        public void Done_CanBeToggled()
        {
            var task = new ConcreteTask();
            task.Done = true;
            task.Done = false;
            Assert.IsFalse(task.Done);
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_ResetsAllProperties()
        {
            var task = new ConcreteTask();
            task.Initialize(42, "tag", 10, "data");
            task.Done = true;
            task.Clear();
            Assert.AreEqual(0, task.SerialId);
            Assert.IsNull(task.Tag);
            Assert.AreEqual(TaskBase.DefaultPriority, task.Priority);
            Assert.IsNull(task.UserData);
            Assert.IsFalse(task.Done);
        }

        #endregion

        #region Description

        [Test]
        public void Description_ReturnsOverriddenValue()
        {
            var task = new ConcreteTask();
            Assert.AreEqual("ConcreteTestTask", task.Description);
        }

        #endregion
    }
}
*/
