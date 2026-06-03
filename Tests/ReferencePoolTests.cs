/*
using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class ReferencePoolTests
    {
        private class TestReference : IReference
        {
            public int Value { get; private set; }

            public TestReference()
            {
                Value = 0;
            }

            public void SetValue(int value)
            {
                Value = value;
            }

            public void Clear()
            {
                Value = 0;
            }
        }

        [SetUp]
        public void SetUp()
        {
            ReferencePool.ClearAll();
            ReferencePool.EnableStrictCheck = true;
        }

        [TearDown]
        public void TearDown()
        {
            ReferencePool.ClearAll();
            ReferencePool.EnableStrictCheck = false;
        }

        #region Acquire

        [Test]
        public void Acquire_ReturnsNewInstance()
        {
            TestReference reference = ReferencePool.Acquire<TestReference>();
            Assert.IsNotNull(reference);
            Assert.AreEqual(0, reference.Value);
        }

        [Test]
        public void Acquire_AfterRelease_ReturnsPooledInstance()
        {
            TestReference original = ReferencePool.Acquire<TestReference>();
            original.SetValue(42);
            ReferencePool.Release(original);
            TestReference reused = ReferencePool.Acquire<TestReference>();
            Assert.AreSame(original, reused);
            Assert.AreEqual(0, reused.Value);
        }

        [Test]
        public void Acquire_WithType_ReturnsInstance()
        {
            IReference reference = ReferencePool.Acquire(typeof(TestReference));
            Assert.IsNotNull(reference);
            Assert.IsInstanceOf<TestReference>(reference);
        }

        #endregion

        #region Release

        [Test]
        public void Release_NullReference_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => ReferencePool.Release(null));
        }

        [Test]
        public void Release_CallsClearOnReference()
        {
            TestReference reference = ReferencePool.Acquire<TestReference>();
            reference.SetValue(100);
            ReferencePool.Release(reference);
            Assert.AreEqual(0, reference.Value);
        }

        #endregion

        #region Add

        [Test]
        public void Add_PreAllocatesReferences()
        {
            ReferencePool.Add<TestReference>(5);
            ReferencePoolInfo[] infos = ReferencePool.GetAllReferencePoolInfos();
            Assert.AreEqual(1, infos.Length);
            Assert.AreEqual(5, infos[0].UnusedReferenceCount);
        }

        [Test]
        public void Add_WithType_PreAllocatesReferences()
        {
            ReferencePool.Add(typeof(TestReference), 3);
            ReferencePoolInfo[] infos = ReferencePool.GetAllReferencePoolInfos();
            Assert.AreEqual(3, infos[0].UnusedReferenceCount);
        }

        #endregion

        #region Remove

        [Test]
        public void Remove_DecreasesPoolSize()
        {
            ReferencePool.Add<TestReference>(5);
            ReferencePool.Remove<TestReference>(2);
            ReferencePoolInfo[] infos = ReferencePool.GetAllReferencePoolInfos();
            Assert.AreEqual(3, infos[0].UnusedReferenceCount);
        }

        [Test]
        public void Remove_MoreThanAvailable_RemovesAll()
        {
            ReferencePool.Add<TestReference>(2);
            ReferencePool.Remove<TestReference>(10);
            ReferencePoolInfo[] infos = ReferencePool.GetAllReferencePoolInfos();
            Assert.AreEqual(0, infos[0].UnusedReferenceCount);
        }

        [Test]
        public void Remove_WithType_DecreasesPoolSize()
        {
            ReferencePool.Add(typeof(TestReference), 5);
            ReferencePool.Remove(typeof(TestReference), 3);
            ReferencePoolInfo[] infos = ReferencePool.GetAllReferencePoolInfos();
            Assert.AreEqual(2, infos[0].UnusedReferenceCount);
        }

        #endregion

        #region RemoveAll

        [Test]
        public void RemoveAll_ClearsUnusedReferences()
        {
            ReferencePool.Add<TestReference>(5);
            ReferencePool.RemoveAll<TestReference>();
            ReferencePoolInfo[] infos = ReferencePool.GetAllReferencePoolInfos();
            Assert.AreEqual(0, infos[0].UnusedReferenceCount);
        }

        #endregion

        #region ClearAll

        [Test]
        public void ClearAll_ResetsEntirePool()
        {
            ReferencePool.Add<TestReference>(5);
            ReferencePool.ClearAll();
            Assert.AreEqual(0, ReferencePool.Count);
        }

        #endregion

        #region Count

        [Test]
        public void Count_NoPools_ReturnsZero()
        {
            Assert.AreEqual(0, ReferencePool.Count);
        }

        [Test]
        public void Count_AfterFirstAcquire_ReturnsOne()
        {
            ReferencePool.Acquire<TestReference>();
            Assert.AreEqual(1, ReferencePool.Count);
        }

        #endregion

        #region Statistics

        [Test]
        public void GetAllReferencePoolInfos_TracksAcquireCount()
        {
            ReferencePool.Acquire<TestReference>();
            ReferencePool.Acquire<TestReference>();
            ReferencePoolInfo[] infos = ReferencePool.GetAllReferencePoolInfos();
            Assert.AreEqual(2, infos[0].AcquireReferenceCount);
        }

        [Test]
        public void GetAllReferencePoolInfos_TracksReleaseCount()
        {
            TestReference reference = ReferencePool.Acquire<TestReference>();
            ReferencePool.Release(reference);
            ReferencePoolInfo[] infos = ReferencePool.GetAllReferencePoolInfos();
            Assert.AreEqual(1, infos[0].ReleaseReferenceCount);
        }

        [Test]
        public void GetAllReferencePoolInfos_TracksUsingCount()
        {
            ReferencePool.Acquire<TestReference>();
            ReferencePool.Acquire<TestReference>();
            TestReference released = ReferencePool.Acquire<TestReference>();
            ReferencePool.Release(released);
            ReferencePoolInfo[] infos = ReferencePool.GetAllReferencePoolInfos();
            Assert.AreEqual(2, infos[0].UsingReferenceCount);
        }

        [Test]
        public void GetAllReferencePoolInfos_TracksAddCount()
        {
            ReferencePool.Add<TestReference>(3);
            ReferencePoolInfo[] infos = ReferencePool.GetAllReferencePoolInfos();
            Assert.AreEqual(3, infos[0].AddReferenceCount);
        }

        #endregion

        #region StrictCheck

        [Test]
        public void EnableStrictCheck_CanBeToggled()
        {
            ReferencePool.EnableStrictCheck = false;
            Assert.IsFalse(ReferencePool.EnableStrictCheck);
            ReferencePool.EnableStrictCheck = true;
            Assert.IsTrue(ReferencePool.EnableStrictCheck);
        }

        #endregion
    }
}
*/
