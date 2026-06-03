/*
using System.Collections.Generic;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class TaskPoolTests
    {
        private class TestTask : TaskBase
        {
            public TestTask()
            {
            }

            public override string Description
            {
                get { return "TestTask"; }
            }
        }

        private class TestTaskAgent : ITaskAgent<TestTask>
        {
            public TestTask Task { get; private set; }
            public bool IsInitialized { get; private set; }
            public bool IsShutdown { get; private set; }
            public StartTaskStatus StartResult { get; set; }

            public TestTaskAgent()
            {
                Task = null;
                IsInitialized = false;
                IsShutdown = false;
                StartResult = StartTaskStatus.CanResume;
            }

            public void Initialize()
            {
                IsInitialized = true;
            }

            public void Update(float elapseSeconds, float realElapseSeconds)
            {
            }

            public void Shutdown()
            {
                IsShutdown = true;
            }

            public StartTaskStatus Start(TestTask task)
            {
                Task = task;
                return StartResult;
            }

            public void Reset()
            {
                Task = null;
            }
        }

        private TaskPool<TestTask> _taskPool;

        [SetUp]
        public void SetUp()
        {
            ReferencePool.ClearAll();
            _taskPool = new TaskPool<TestTask>();
        }

        [TearDown]
        public void TearDown()
        {
            _taskPool.RemoveAllTasks();
            _taskPool.Shutdown();
            ReferencePool.ClearAll();
        }

        #region Constructor

        [Test]
        public void Constructor_InitialValues()
        {
            Assert.AreEqual(0, _taskPool.TotalAgentCount);
            Assert.AreEqual(0, _taskPool.FreeAgentCount);
            Assert.AreEqual(0, _taskPool.WorkingAgentCount);
            Assert.AreEqual(0, _taskPool.WaitingTaskCount);
            Assert.IsFalse(_taskPool.Paused);
        }

        #endregion

        #region AddAgent

        [Test]
        public void AddAgent_NullAgent_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _taskPool.AddAgent(null));
        }

        [Test]
        public void AddAgent_ValidAgent_IncrementsTotalCount()
        {
            var agent = new TestTaskAgent();
            _taskPool.AddAgent(agent);
            Assert.AreEqual(1, _taskPool.TotalAgentCount);
            Assert.AreEqual(1, _taskPool.FreeAgentCount);
            Assert.IsTrue(agent.IsInitialized);
        }

        #endregion

        #region AddTask

        [Test]
        public void AddTask_IncrementsWaitingTaskCount()
        {
            var task = ReferencePool.Acquire<TestTask>();
            task.Initialize(1, "tag1", 0, null);
            _taskPool.AddTask(task);
            Assert.AreEqual(1, _taskPool.WaitingTaskCount);
        }

        [Test]
        public void AddTask_MultipleTasks_OrderedByPriority()
        {
            var task1 = ReferencePool.Acquire<TestTask>();
            task1.Initialize(1, "tag1", 1, null);
            var task2 = ReferencePool.Acquire<TestTask>();
            task2.Initialize(2, "tag1", 5, null);
            var task3 = ReferencePool.Acquire<TestTask>();
            task3.Initialize(3, "tag1", 3, null);

            _taskPool.AddTask(task1);
            _taskPool.AddTask(task2);
            _taskPool.AddTask(task3);

            TaskInfo[] infos = _taskPool.GetAllTaskInfos();
            Assert.AreEqual(3, infos.Length);
            Assert.AreEqual(5, infos[0].Priority);
            Assert.AreEqual(3, infos[1].Priority);
            Assert.AreEqual(1, infos[2].Priority);
        }

        #endregion

        #region Update - ProcessWaitingTasks

        [Test]
        public void Update_AssignsTasksToFreeAgents()
        {
            var agent = new TestTaskAgent();
            _taskPool.AddAgent(agent);
            var task = ReferencePool.Acquire<TestTask>();
            task.Initialize(1, "tag1", 0, null);
            _taskPool.AddTask(task);
            _taskPool.Update(0f, 0f);
            Assert.AreEqual(1, _taskPool.WorkingAgentCount);
            Assert.AreEqual(0, _taskPool.FreeAgentCount);
            Assert.AreEqual(0, _taskPool.WaitingTaskCount);
        }

        [Test]
        public void Update_CompletedTask_ReturnsAgentToFree()
        {
            var agent = new TestTaskAgent();
            _taskPool.AddAgent(agent);
            var task = ReferencePool.Acquire<TestTask>();
            task.Initialize(1, "tag1", 0, null);
            _taskPool.AddTask(task);
            _taskPool.Update(0f, 0f);
            task.Done = true;
            _taskPool.Update(0f, 0f);
            Assert.AreEqual(0, _taskPool.WorkingAgentCount);
            Assert.AreEqual(1, _taskPool.FreeAgentCount);
        }

        #endregion

        #region Paused

        [Test]
        public void Paused_True_DoesNotProcessTasks()
        {
            _taskPool.Paused = true;
            var agent = new TestTaskAgent();
            _taskPool.AddAgent(agent);
            var task = ReferencePool.Acquire<TestTask>();
            task.Initialize(1, "tag1", 0, null);
            _taskPool.AddTask(task);
            _taskPool.Update(0f, 0f);
            Assert.AreEqual(1, _taskPool.WaitingTaskCount);
            Assert.AreEqual(1, _taskPool.FreeAgentCount);
        }

        #endregion

        #region RemoveTask

        [Test]
        public void RemoveTask_ExistingWaitingTask_ReturnsTrue()
        {
            var task = ReferencePool.Acquire<TestTask>();
            task.Initialize(1, "tag1", 0, null);
            _taskPool.AddTask(task);
            bool result = _taskPool.RemoveTask(1);
            Assert.IsTrue(result);
            Assert.AreEqual(0, _taskPool.WaitingTaskCount);
        }

        [Test]
        public void RemoveTask_NonExistingTask_ReturnsFalse()
        {
            bool result = _taskPool.RemoveTask(999);
            Assert.IsFalse(result);
        }

        #endregion

        #region RemoveTasks

        [Test]
        public void RemoveTasks_ByTag_RemovesMatchingTasks()
        {
            var task1 = ReferencePool.Acquire<TestTask>();
            task1.Initialize(1, "a", 0, null);
            var task2 = ReferencePool.Acquire<TestTask>();
            task2.Initialize(2, "b", 0, null);
            var task3 = ReferencePool.Acquire<TestTask>();
            task3.Initialize(3, "a", 0, null);
            _taskPool.AddTask(task1);
            _taskPool.AddTask(task2);
            _taskPool.AddTask(task3);
            int removed = _taskPool.RemoveTasks("a");
            Assert.AreEqual(2, removed);
            Assert.AreEqual(1, _taskPool.WaitingTaskCount);
        }

        #endregion

        #region RemoveAllTasks

        [Test]
        public void RemoveAllTasks_ClearsEverything()
        {
            var task1 = ReferencePool.Acquire<TestTask>();
            task1.Initialize(1, "a", 0, null);
            var task2 = ReferencePool.Acquire<TestTask>();
            task2.Initialize(2, "b", 0, null);
            _taskPool.AddTask(task1);
            _taskPool.AddTask(task2);
            int removed = _taskPool.RemoveAllTasks();
            Assert.AreEqual(2, removed);
            Assert.AreEqual(0, _taskPool.WaitingTaskCount);
        }

        #endregion

        #region GetTaskInfo

        [Test]
        public void GetTaskInfo_ExistingTask_ReturnsValidInfo()
        {
            var task = ReferencePool.Acquire<TestTask>();
            task.Initialize(42, "myTag", 5, "userData");
            _taskPool.AddTask(task);
            TaskInfo info = _taskPool.GetTaskInfo(42);
            Assert.IsTrue(info.IsValid);
            Assert.AreEqual(42, info.SerialId);
            Assert.AreEqual("myTag", info.Tag);
            Assert.AreEqual(5, info.Priority);
            Assert.AreEqual("userData", info.UserData);
            Assert.AreEqual(TaskStatus.Todo, info.Status);
        }

        [Test]
        public void GetTaskInfo_NonExistingTask_ReturnsInvalidInfo()
        {
            TaskInfo info = _taskPool.GetTaskInfo(999);
            Assert.IsFalse(info.IsValid);
        }

        #endregion

        #region GetTaskInfos

        [Test]
        public void GetTaskInfos_ByTag_ReturnsMatchingTasks()
        {
            var task1 = ReferencePool.Acquire<TestTask>();
            task1.Initialize(1, "a", 0, null);
            var task2 = ReferencePool.Acquire<TestTask>();
            task2.Initialize(2, "b", 0, null);
            var task3 = ReferencePool.Acquire<TestTask>();
            task3.Initialize(3, "a", 0, null);
            _taskPool.AddTask(task1);
            _taskPool.AddTask(task2);
            _taskPool.AddTask(task3);
            TaskInfo[] infos = _taskPool.GetTaskInfos("a");
            Assert.AreEqual(2, infos.Length);
        }

        [Test]
        public void GetTaskInfos_NullResults_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _taskPool.GetTaskInfos("tag", null));
        }

        #endregion

        #region GetAllTaskInfos

        [Test]
        public void GetAllTaskInfos_ReturnsAllTasks()
        {
            var task1 = ReferencePool.Acquire<TestTask>();
            task1.Initialize(1, "a", 0, null);
            var task2 = ReferencePool.Acquire<TestTask>();
            task2.Initialize(2, "b", 0, null);
            _taskPool.AddTask(task1);
            _taskPool.AddTask(task2);
            TaskInfo[] infos = _taskPool.GetAllTaskInfos();
            Assert.AreEqual(2, infos.Length);
        }

        [Test]
        public void GetAllTaskInfos_NullResults_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _taskPool.GetAllTaskInfos(null as List<TaskInfo>));
        }

        #endregion

        #region Shutdown

        [Test]
        public void Shutdown_ClearsAgentsAndTasks()
        {
            var agent = new TestTaskAgent();
            _taskPool.AddAgent(agent);
            var task = ReferencePool.Acquire<TestTask>();
            task.Initialize(1, "tag", 0, null);
            _taskPool.AddTask(task);
            _taskPool.Shutdown();
            Assert.IsTrue(agent.IsShutdown);
        }

        #endregion
    }
}
*/
