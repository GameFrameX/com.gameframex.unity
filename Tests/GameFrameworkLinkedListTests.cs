/*
using System.Collections.Generic;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class GameFrameworkLinkedListTests
    {
        private GameFrameworkLinkedList<int> _list;

        [SetUp]
        public void SetUp()
        {
            _list = new GameFrameworkLinkedList<int>();
        }

        [TearDown]
        public void TearDown()
        {
            _list.Clear();
            _list.ClearCachedNodes();
        }

        #region Constructor and Count

        [Test]
        public void Count_NewList_ReturnsZero()
        {
            Assert.AreEqual(0, _list.Count);
        }

        [Test]
        public void CachedNodeCount_NewList_ReturnsZero()
        {
            Assert.AreEqual(0, _list.CachedNodeCount);
        }

        #endregion

        #region AddFirst

        [Test]
        public void AddFirst_EmptyList_CountIsOne()
        {
            LinkedListNode<int> node = _list.AddFirst(10);
            Assert.AreEqual(1, _list.Count);
            Assert.AreEqual(10, node.Value);
            Assert.AreEqual(10, _list.First.Value);
        }

        [Test]
        public void AddFirst_MultipleItems_FirstIsLatest()
        {
            _list.AddFirst(1);
            _list.AddFirst(2);
            _list.AddFirst(3);
            Assert.AreEqual(3, _list.Count);
            Assert.AreEqual(3, _list.First.Value);
            Assert.AreEqual(1, _list.Last.Value);
        }

        #endregion

        #region AddLast

        [Test]
        public void AddLast_EmptyList_CountIsOne()
        {
            LinkedListNode<int> node = _list.AddLast(10);
            Assert.AreEqual(1, _list.Count);
            Assert.AreEqual(10, node.Value);
        }

        [Test]
        public void AddLast_MultipleItems_LastIsLatest()
        {
            _list.AddLast(1);
            _list.AddLast(2);
            _list.AddLast(3);
            Assert.AreEqual(3, _list.Count);
            Assert.AreEqual(1, _list.First.Value);
            Assert.AreEqual(3, _list.Last.Value);
        }

        #endregion

        #region AddAfter

        [Test]
        public void AddAfter_InsertsBetween()
        {
            LinkedListNode<int> first = _list.AddLast(1);
            _list.AddLast(3);
            _list.AddAfter(first, 2);
            Assert.AreEqual(3, _list.Count);
            Assert.AreEqual(2, first.Next.Value);
        }

        #endregion

        #region AddBefore

        [Test]
        public void AddBefore_InsertsBeforeNode()
        {
            _list.AddLast(1);
            LinkedListNode<int> last = _list.AddLast(3);
            _list.AddBefore(last, 2);
            Assert.AreEqual(3, _list.Count);
            Assert.AreEqual(2, last.Previous.Value);
        }

        #endregion

        #region Remove

        [Test]
        public void Remove_ValueExists_ReturnsTrue()
        {
            _list.AddLast(1);
            _list.AddLast(2);
            _list.AddLast(3);
            bool result = _list.Remove(2);
            Assert.IsTrue(result);
            Assert.AreEqual(2, _list.Count);
            Assert.IsFalse(_list.Contains(2));
        }

        [Test]
        public void Remove_ValueNotExists_ReturnsFalse()
        {
            _list.AddLast(1);
            bool result = _list.Remove(99);
            Assert.IsFalse(result);
            Assert.AreEqual(1, _list.Count);
        }

        [Test]
        public void Remove_Node_RemovesSpecificNode()
        {
            LinkedListNode<int> node = _list.AddLast(1);
            _list.AddLast(2);
            _list.Remove(node);
            Assert.AreEqual(1, _list.Count);
            Assert.IsFalse(_list.Contains(1));
        }

        #endregion

        #region RemoveFirst / RemoveLast

        [Test]
        public void RemoveFirst_RemovesHead()
        {
            _list.AddLast(1);
            _list.AddLast(2);
            _list.RemoveFirst();
            Assert.AreEqual(1, _list.Count);
            Assert.AreEqual(2, _list.First.Value);
        }

        [Test]
        public void RemoveFirst_EmptyList_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _list.RemoveFirst());
        }

        [Test]
        public void RemoveLast_RemovesTail()
        {
            _list.AddLast(1);
            _list.AddLast(2);
            _list.RemoveLast();
            Assert.AreEqual(1, _list.Count);
            Assert.AreEqual(1, _list.Last.Value);
        }

        [Test]
        public void RemoveLast_EmptyList_ThrowsGameFrameworkException()
        {
            Assert.Throws<GameFrameworkException>(() => _list.RemoveLast());
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_ResetsCountToZero()
        {
            _list.AddLast(1);
            _list.AddLast(2);
            _list.AddLast(3);
            _list.Clear();
            Assert.AreEqual(0, _list.Count);
        }

        [Test]
        public void Clear_PopulatesCachedNodes()
        {
            _list.AddLast(1);
            _list.AddLast(2);
            _list.Clear();
            Assert.AreEqual(2, _list.CachedNodeCount);
        }

        #endregion

        #region Contains

        [Test]
        public void Contains_ExistingValue_ReturnsTrue()
        {
            _list.AddLast(42);
            Assert.IsTrue(_list.Contains(42));
        }

        [Test]
        public void Contains_NonExistingValue_ReturnsFalse()
        {
            _list.AddLast(1);
            Assert.IsFalse(_list.Contains(99));
        }

        #endregion

        #region Find / FindLast

        [Test]
        public void Find_ExistingValue_ReturnsNode()
        {
            _list.AddLast(1);
            _list.AddLast(2);
            LinkedListNode<int> node = _list.Find(2);
            Assert.IsNotNull(node);
            Assert.AreEqual(2, node.Value);
        }

        [Test]
        public void Find_NonExistingValue_ReturnsNull()
        {
            _list.AddLast(1);
            Assert.IsNull(_list.Find(99));
        }

        [Test]
        public void FindLast_DuplicateValue_ReturnsLastNode()
        {
            _list.AddLast(1);
            _list.AddLast(2);
            _list.AddLast(2);
            LinkedListNode<int> node = _list.FindLast(2);
            Assert.IsNotNull(node);
            Assert.AreEqual(_list.Last, node);
        }

        #endregion

        #region First / Last

        [Test]
        public void First_EmptyList_ReturnsNull()
        {
            Assert.IsNull(_list.First);
        }

        [Test]
        public void Last_EmptyList_ReturnsNull()
        {
            Assert.IsNull(_list.Last);
        }

        #endregion

        #region Node Caching

        [Test]
        public void NodeCaching_ReusesCachedNodes()
        {
            _list.AddLast(1);
            _list.AddLast(2);
            _list.Clear();
            Assert.AreEqual(2, _list.CachedNodeCount);
            _list.AddLast(3);
            Assert.AreEqual(1, _list.CachedNodeCount);
        }

        [Test]
        public void ClearCachedNodes_ResetsCache()
        {
            _list.AddLast(1);
            _list.Clear();
            Assert.AreEqual(1, _list.CachedNodeCount);
            _list.ClearCachedNodes();
            Assert.AreEqual(0, _list.CachedNodeCount);
        }

        #endregion

        #region Enumeration

        [Test]
        public void Enumeration_YieldsAllElements()
        {
            _list.AddLast(10);
            _list.AddLast(20);
            _list.AddLast(30);
            int sum = 0;
            foreach (int value in _list)
            {
                sum += value;
            }
            Assert.AreEqual(60, sum);
        }

        #endregion

        #region CopyTo

        [Test]
        public void CopyTo_CopiesAllElements()
        {
            _list.AddLast(1);
            _list.AddLast(2);
            _list.AddLast(3);
            int[] array = new int[3];
            _list.CopyTo(array, 0);
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(2, array[1]);
            Assert.AreEqual(3, array[2]);
        }

        #endregion
    }
}
*/
