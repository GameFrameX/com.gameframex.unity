using System;
using System.Collections.Generic;
using System.Threading;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class UtilityIdGeneratorTests
    {
        #region UtcTimeStart

        [Test]
        public void UtcTimeStart_Is2020Jan1()
        {
            Assert.AreEqual(2020, Utility.IdGenerator.UtcTimeStart.Year);
            Assert.AreEqual(1, Utility.IdGenerator.UtcTimeStart.Month);
            Assert.AreEqual(1, Utility.IdGenerator.UtcTimeStart.Day);
            Assert.AreEqual(DateTimeKind.Utc, Utility.IdGenerator.UtcTimeStart.Kind);
        }

        #endregion

        #region GetNextUniqueId (long)

        [Test]
        public void GetNextUniqueId_ReturnsSequential()
        {
            long id1 = Utility.IdGenerator.GetNextUniqueId();
            long id2 = Utility.IdGenerator.GetNextUniqueId();
            Assert.AreEqual(id1 + 1, id2, "Sequential calls should produce consecutive IDs");
        }

        [Test]
        public void GetNextUniqueId_ReturnsPositive()
        {
            long id = Utility.IdGenerator.GetNextUniqueId();
            Assert.Greater(id, 0);
        }

        [Test]
        public void GetNextUniqueId_MultipleCalls_AllUnique()
        {
            const int count = 1000;
            HashSet<long> ids = new HashSet<long>();
            for (int i = 0; i < count; i++)
            {
                long id = Utility.IdGenerator.GetNextUniqueId();
                Assert.IsTrue(ids.Add(id), $"Duplicate ID found: {id}");
            }
            Assert.AreEqual(count, ids.Count);
        }

        [Test]
        public void GetNextUniqueId_Concurrent_AllUnique()
        {
            const int threadCount = 4;
            const int perThread = 500;
            HashSet<long> allIds = new HashSet<long>();
            object lockObj = new object();

            Thread[] threads = new Thread[threadCount];
            for (int t = 0; t < threadCount; t++)
            {
                threads[t] = new Thread(() =>
                {
                    List<long> localIds = new List<long>();
                    for (int i = 0; i < perThread; i++)
                    {
                        localIds.Add(Utility.IdGenerator.GetNextUniqueId());
                    }
                    lock (lockObj)
                    {
                        foreach (long id in localIds)
                        {
                            allIds.Add(id);
                        }
                    }
                });
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Assert.AreEqual(threadCount * perThread, allIds.Count);
        }

        #endregion

        #region GetNextUniqueIntId (int)

        [Test]
        public void GetNextUniqueIntId_ReturnsSequential()
        {
            int id1 = Utility.IdGenerator.GetNextUniqueIntId();
            int id2 = Utility.IdGenerator.GetNextUniqueIntId();
            Assert.AreEqual(id1 + 1, id2);
        }

        [Test]
        public void GetNextUniqueIntId_MultipleCalls_AllUnique()
        {
            const int count = 1000;
            HashSet<int> ids = new HashSet<int>();
            for (int i = 0; i < count; i++)
            {
                int id = Utility.IdGenerator.GetNextUniqueIntId();
                Assert.IsTrue(ids.Add(id), $"Duplicate int ID found: {id}");
            }
            Assert.AreEqual(count, ids.Count);
        }

        #endregion
    }
}
