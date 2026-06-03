/*
using System;
using System.Collections.Generic;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class EventPoolTests
    {
        private class TestEventArgs : BaseEventArgs
        {
            public override string Id { get { return "TestEvent"; } }

            public int Value { get; private set; }

            public TestEventArgs()
            {
                Value = 0;
            }

            public TestEventArgs(int value)
            {
                Value = value;
            }

            public override void Clear()
            {
                Value = 0;
            }
        }

        private EventPool<TestEventArgs> _pool;
        private List<string> _receivedEvents;

        [SetUp]
        public void SetUp()
        {
            ReferencePool.ClearAll();
            _pool = new EventPool<TestEventArgs>(EventPoolMode.AllowMultiHandler | EventPoolMode.AllowDuplicateHandler);
            _receivedEvents = new List<string>();
        }

        [TearDown]
        public void TearDown()
        {
            _pool.Shutdown();
            ReferencePool.ClearAll();
        }

        #region Subscribe

        [Test]
        public void Subscribe_NullHandler_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _pool.Subscribe("TestEvent", null));
        }

        [Test]
        public void Subscribe_ValidHandler_IncrementsHandlerCount()
        {
            EventHandler<TestEventArgs> handler = (s, e) => { };
            _pool.Subscribe("TestEvent", handler);
            Assert.AreEqual(1, _pool.EventHandlerCount);
        }

        [Test]
        public void Subscribe_DefaultMode_SecondHandlerThrows()
        {
            var defaultPool = new EventPool<TestEventArgs>(EventPoolMode.Default);
            EventHandler<TestEventArgs> handler1 = (s, e) => { };
            EventHandler<TestEventArgs> handler2 = (s, e) => { };
            defaultPool.Subscribe("TestEvent", handler1);
            Assert.Throws<GameFrameworkException>(() => defaultPool.Subscribe("TestEvent", handler2));
            defaultPool.Shutdown();
        }

        [Test]
        public void Subscribe_MultiHandler_AllowsMultipleHandlers()
        {
            EventHandler<TestEventArgs> handler1 = (s, e) => { _receivedEvents.Add("h1"); };
            EventHandler<TestEventArgs> handler2 = (s, e) => { _receivedEvents.Add("h2"); };
            _pool.Subscribe("TestEvent", handler1);
            _pool.Subscribe("TestEvent", handler2);
            Assert.AreEqual(1, _pool.EventHandlerCount);
        }

        #endregion

        #region Unsubscribe

        [Test]
        public void Unsubscribe_NullHandler_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _pool.Unsubscribe("TestEvent", null));
        }

        [Test]
        public void Unsubscribe_ExistingHandler_DecrementsHandlerCount()
        {
            EventHandler<TestEventArgs> handler = (s, e) => { };
            _pool.Subscribe("TestEvent", handler);
            _pool.Unsubscribe("TestEvent", handler);
            Assert.AreEqual(0, _pool.EventHandlerCount);
        }

        [Test]
        public void Unsubscribe_NonExistingHandler_ThrowsGameFrameworkException()
        {
            EventHandler<TestEventArgs> handler = (s, e) => { };
            Assert.Throws<GameFrameworkException>(() => _pool.Unsubscribe("TestEvent", handler));
        }

        #endregion

        #region Fire

        [Test]
        public void Fire_NullEvent_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _pool.Fire(this, null));
        }

        [Test]
        public void Fire_QueuesEvent_ProcessesOnUpdate()
        {
            EventHandler<TestEventArgs> handler = (s, e) => { _receivedEvents.Add("fired"); };
            _pool.Subscribe("TestEvent", handler);
            _pool.Fire(this, new TestEventArgs(42));
            Assert.AreEqual(0, _receivedEvents.Count);
            _pool.Update(0f, 0f);
            Assert.AreEqual(1, _receivedEvents.Count);
        }

        #endregion

        #region FireNow

        [Test]
        public void FireNow_NullEvent_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _pool.FireNow(this, null));
        }

        [Test]
        public void FireNow_InvokesHandlerImmediately()
        {
            EventHandler<TestEventArgs> handler = (s, e) => { _receivedEvents.Add("fired:" + e.Value); };
            _pool.Subscribe("TestEvent", handler);
            _pool.FireNow(this, new TestEventArgs(42));
            Assert.AreEqual(1, _receivedEvents.Count);
            Assert.AreEqual("fired:42", _receivedEvents[0]);
        }

        [Test]
        public void FireNow_MultipleHandlers_InvokesAll()
        {
            EventHandler<TestEventArgs> handler1 = (s, e) => { _receivedEvents.Add("h1"); };
            EventHandler<TestEventArgs> handler2 = (s, e) => { _receivedEvents.Add("h2"); };
            _pool.Subscribe("TestEvent", handler1);
            _pool.Subscribe("TestEvent", handler2);
            _pool.FireNow(this, new TestEventArgs(1));
            Assert.AreEqual(2, _receivedEvents.Count);
        }

        #endregion

        #region Check

        [Test]
        public void Check_NullHandler_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _pool.Check("TestEvent", null));
        }

        [Test]
        public void Check_ExistingHandler_ReturnsTrue()
        {
            EventHandler<TestEventArgs> handler = (s, e) => { };
            _pool.Subscribe("TestEvent", handler);
            Assert.IsTrue(_pool.Check("TestEvent", handler));
        }

        [Test]
        public void Check_NonExistingHandler_ReturnsFalse()
        {
            EventHandler<TestEventArgs> handler = (s, e) => { };
            Assert.IsFalse(_pool.Check("TestEvent", handler));
        }

        #endregion

        #region Count

        [Test]
        public void Count_NoHandlers_ReturnsZero()
        {
            Assert.AreEqual(0, _pool.Count("TestEvent"));
        }

        [Test]
        public void Count_WithHandlers_ReturnsHandlerCount()
        {
            EventHandler<TestEventArgs> handler = (s, e) => { };
            _pool.Subscribe("TestEvent", handler);
            Assert.AreEqual(1, _pool.Count("TestEvent"));
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_RemovesQueuedEvents()
        {
            _pool.Fire(this, new TestEventArgs(1));
            _pool.Fire(this, new TestEventArgs(2));
            _pool.Clear();
            Assert.AreEqual(0, _pool.EventCount);
        }

        #endregion

        #region DefaultHandler

        [Test]
        public void DefaultHandler_InvokedWhenNoHandlersRegistered()
        {
            var pool = new EventPool<TestEventArgs>(EventPoolMode.AllowNoHandler);
            string received = null;
            pool.SetDefaultHandler((s, e) => { received = "default:" + e.Value; });
            pool.FireNow(this, new TestEventArgs(99));
            Assert.AreEqual("default:99", received);
            pool.Shutdown();
        }

        #endregion

        #region NoHandler

        [Test]
        public void FireNoHandler_DefaultMode_ThrowsGameFrameworkException()
        {
            var pool = new EventPool<TestEventArgs>(EventPoolMode.Default);
            Assert.Throws<GameFrameworkException>(() => pool.FireNow(this, new TestEventArgs(1)));
            pool.Shutdown();
        }

        [Test]
        public void FireNoHandler_AllowNoHandlerMode_DoesNotThrow()
        {
            var pool = new EventPool<TestEventArgs>(EventPoolMode.AllowNoHandler);
            Assert.DoesNotThrow(() => pool.FireNow(this, new TestEventArgs(1)));
            pool.Shutdown();
        }

        #endregion

        #region Shutdown

        [Test]
        public void Shutdown_ClearsHandlers()
        {
            EventHandler<TestEventArgs> handler = (s, e) => { };
            _pool.Subscribe("TestEvent", handler);
            _pool.Shutdown();
        }

        #endregion

        #region EventCount

        [Test]
        public void EventCount_AfterFire_ReturnsQueuedCount()
        {
            _pool.Fire(this, new TestEventArgs(1));
            _pool.Fire(this, new TestEventArgs(2));
            Assert.AreEqual(2, _pool.EventCount);
        }

        [Test]
        public void EventCount_AfterUpdate_ReturnsZero()
        {
            EventHandler<TestEventArgs> handler = (s, e) => { };
            _pool.Subscribe("TestEvent", handler);
            _pool.Fire(this, new TestEventArgs(1));
            _pool.Update(0f, 0f);
            Assert.AreEqual(0, _pool.EventCount);
        }

        #endregion
    }
}
*/
