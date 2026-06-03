using System;
using GameFrameX.ObjectPool;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class ObjectInfoTests
    {
        #region Construction and Properties

        [Test]
        public void Constructor_SetsAllProperties()
        {
            var lastUseTime = DateTime.UtcNow;
            var info = new ObjectInfo("TestObj", true, false, 5, lastUseTime, 3);

            Assert.AreEqual("TestObj", info.Name);
            Assert.IsTrue(info.Locked);
            Assert.IsFalse(info.CustomCanReleaseFlag);
            Assert.AreEqual(5, info.Priority);
            Assert.AreEqual(lastUseTime, info.LastUseTime);
            Assert.AreEqual(3, info.SpawnCount);
        }

        [Test]
        public void Constructor_WithNullName_SetsNameToNull()
        {
            var info = new ObjectInfo(null, false, false, 0, DateTime.UtcNow, 0);
            Assert.IsNull(info.Name);
        }

        [Test]
        public void Constructor_WithEmptyName_SetsNameToEmpty()
        {
            var info = new ObjectInfo("", false, false, 0, DateTime.UtcNow, 0);
            Assert.AreEqual("", info.Name);
        }

        #endregion

        #region IsInUse

        [Test]
        public void IsInUse_SpawnCountZero_IsFalse()
        {
            var info = new ObjectInfo("test", false, false, 0, DateTime.UtcNow, 0);
            Assert.IsFalse(info.IsInUse);
        }

        [Test]
        public void IsInUse_SpawnCountPositive_IsTrue()
        {
            var info = new ObjectInfo("test", false, false, 0, DateTime.UtcNow, 1);
            Assert.IsTrue(info.IsInUse);
        }

        [Test]
        public void IsInUse_SpawnCountGreaterThanOne_IsTrue()
        {
            var info = new ObjectInfo("test", false, false, 0, DateTime.UtcNow, 10);
            Assert.IsTrue(info.IsInUse);
        }

        #endregion

        #region Locked

        [Test]
        public void Locked_True()
        {
            var info = new ObjectInfo("test", true, false, 0, DateTime.UtcNow, 0);
            Assert.IsTrue(info.Locked);
        }

        [Test]
        public void Locked_False()
        {
            var info = new ObjectInfo("test", false, false, 0, DateTime.UtcNow, 0);
            Assert.IsFalse(info.Locked);
        }

        #endregion

        #region CustomCanReleaseFlag

        [Test]
        public void CustomCanReleaseFlag_True()
        {
            var info = new ObjectInfo("test", false, true, 0, DateTime.UtcNow, 0);
            Assert.IsTrue(info.CustomCanReleaseFlag);
        }

        [Test]
        public void CustomCanReleaseFlag_False()
        {
            var info = new ObjectInfo("test", false, false, 0, DateTime.UtcNow, 0);
            Assert.IsFalse(info.CustomCanReleaseFlag);
        }

        #endregion

        #region Priority

        [Test]
        public void Priority_Positive()
        {
            var info = new ObjectInfo("test", false, false, 100, DateTime.UtcNow, 0);
            Assert.AreEqual(100, info.Priority);
        }

        [Test]
        public void Priority_Negative()
        {
            var info = new ObjectInfo("test", false, false, -10, DateTime.UtcNow, 0);
            Assert.AreEqual(-10, info.Priority);
        }

        [Test]
        public void Priority_Zero()
        {
            var info = new ObjectInfo("test", false, false, 0, DateTime.UtcNow, 0);
            Assert.AreEqual(0, info.Priority);
        }

        #endregion

        #region SpawnCount

        [Test]
        public void SpawnCount_Zero()
        {
            var info = new ObjectInfo("test", false, false, 0, DateTime.UtcNow, 0);
            Assert.AreEqual(0, info.SpawnCount);
        }

        [Test]
        public void SpawnCount_Positive()
        {
            var info = new ObjectInfo("test", false, false, 0, DateTime.UtcNow, 5);
            Assert.AreEqual(5, info.SpawnCount);
        }

        #endregion

        #region LastUseTime

        [Test]
        public void LastUseTime_ReturnsSetTime()
        {
            var time = new DateTime(2025, 6, 15, 10, 30, 0, DateTimeKind.Utc);
            var info = new ObjectInfo("test", false, false, 0, time, 0);
            Assert.AreEqual(time, info.LastUseTime);
        }

        #endregion

        #region Struct Behavior

        [Test]
        public void ObjectInfo_IsStruct()
        {
            Assert.IsTrue(typeof(ObjectInfo).IsValueType);
        }

        [Test]
        public void ObjectInfo_TwoInstancesWithSameValues_AreEqualViaProperties()
        {
            var time = DateTime.UtcNow;
            var info1 = new ObjectInfo("test", true, false, 5, time, 2);
            var info2 = new ObjectInfo("test", true, false, 5, time, 2);

            Assert.AreEqual(info1.Name, info2.Name);
            Assert.AreEqual(info1.Locked, info2.Locked);
            Assert.AreEqual(info1.CustomCanReleaseFlag, info2.CustomCanReleaseFlag);
            Assert.AreEqual(info1.Priority, info2.Priority);
            Assert.AreEqual(info1.LastUseTime, info2.LastUseTime);
            Assert.AreEqual(info1.SpawnCount, info2.SpawnCount);
            Assert.AreEqual(info1.IsInUse, info2.IsInUse);
        }

        [Test]
        public void ObjectInfo_CopyCreatesIndependentInstance()
        {
            var time = DateTime.UtcNow;
            var info1 = new ObjectInfo("test", true, false, 5, time, 2);
            var info2 = info1;

            Assert.AreEqual(info1.Name, info2.Name);
            Assert.AreEqual(info1.SpawnCount, info2.SpawnCount);
        }

        #endregion
    }
}
