/*
using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class ReferencePoolInfoTests
    {
        #region Constructor

        [Test]
        public void Constructor_SetsAllProperties()
        {
            var info = new ReferencePoolInfo(
                typeof(string),
                unusedReferenceCount: 10,
                usingReferenceCount: 5,
                acquireReferenceCount: 100,
                releaseReferenceCount: 95,
                addReferenceCount: 15,
                removeReferenceCount: 3
            );

            Assert.AreEqual(typeof(string), info.Type);
            Assert.AreEqual(10, info.UnusedReferenceCount);
            Assert.AreEqual(5, info.UsingReferenceCount);
            Assert.AreEqual(100, info.AcquireReferenceCount);
            Assert.AreEqual(95, info.ReleaseReferenceCount);
            Assert.AreEqual(15, info.AddReferenceCount);
            Assert.AreEqual(3, info.RemoveReferenceCount);
        }

        [Test]
        public void Constructor_WithZeroCounts()
        {
            var info = new ReferencePoolInfo(typeof(int), 0, 0, 0, 0, 0, 0);
            Assert.AreEqual(typeof(int), info.Type);
            Assert.AreEqual(0, info.UnusedReferenceCount);
            Assert.AreEqual(0, info.UsingReferenceCount);
        }

        #endregion
    }
}
*/
