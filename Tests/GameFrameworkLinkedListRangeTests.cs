/*
using System.Collections.Generic;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class GameFrameworkLinkedListRangeTests
    {
        #region Constructor

        [Test]
        public void Constructor_ValidNodes_SetsFirstAndTerminal()
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> first = list.AddLast(1);
            list.AddLast(2);
            LinkedListNode<int> terminal = list.AddLast(default(int));
            var range = new GameFrameworkLinkedListRange<int>(first, terminal);
            Assert.AreEqual(first, range.First);
            Assert.AreEqual(terminal, range.Terminal);
        }

        [Test]
        public void Constructor_NullFirst_ThrowsGameFrameworkException()
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> terminal = list.AddLast(default(int));
            Assert.Throws<GameFrameworkException>(() => new GameFrameworkLinkedListRange<int>(null, terminal));
        }

        [Test]
        public void Constructor_NullTerminal_ThrowsGameFrameworkException()
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> first = list.AddLast(1);
            Assert.Throws<GameFrameworkException>(() => new GameFrameworkLinkedListRange<int>(first, null));
        }

        [Test]
        public void Constructor_FirstEqualsTerminal_ThrowsGameFrameworkException()
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> node = list.AddLast(1);
            Assert.Throws<GameFrameworkException>(() => new GameFrameworkLinkedListRange<int>(node, node));
        }

        #endregion

        #region IsValid

        [Test]
        public void IsValid_ValidRange_ReturnsTrue()
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> first = list.AddLast(1);
            list.AddLast(2);
            LinkedListNode<int> terminal = list.AddLast(default(int));
            var range = new GameFrameworkLinkedListRange<int>(first, terminal);
            Assert.IsTrue(range.IsValid);
        }

        [Test]
        public void IsValid_DefaultStruct_ReturnsFalse()
        {
            GameFrameworkLinkedListRange<int> range = default(GameFrameworkLinkedListRange<int>);
            Assert.IsFalse(range.IsValid);
        }

        #endregion

        #region Count

        [Test]
        public void Count_TwoElements_ReturnsTwo()
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> first = list.AddLast(1);
            list.AddLast(2);
            LinkedListNode<int> terminal = list.AddLast(default(int));
            var range = new GameFrameworkLinkedListRange<int>(first, terminal);
            Assert.AreEqual(2, range.Count);
        }

        [Test]
        public void Count_InvalidRange_ReturnsZero()
        {
            GameFrameworkLinkedListRange<int> range = default(GameFrameworkLinkedListRange<int>);
            Assert.AreEqual(0, range.Count);
        }

        [Test]
        public void Count_SingleElement_ReturnsOne()
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> first = list.AddLast(42);
            LinkedListNode<int> terminal = list.AddLast(default(int));
            var range = new GameFrameworkLinkedListRange<int>(first, terminal);
            Assert.AreEqual(1, range.Count);
        }

        #endregion

        #region Contains

        [Test]
        public void Contains_ExistingValue_ReturnsTrue()
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> first = list.AddLast(10);
            list.AddLast(20);
            LinkedListNode<int> terminal = list.AddLast(default(int));
            var range = new GameFrameworkLinkedListRange<int>(first, terminal);
            Assert.IsTrue(range.Contains(10));
            Assert.IsTrue(range.Contains(20));
        }

        [Test]
        public void Contains_NonExistingValue_ReturnsFalse()
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> first = list.AddLast(1);
            LinkedListNode<int> terminal = list.AddLast(default(int));
            var range = new GameFrameworkLinkedListRange<int>(first, terminal);
            Assert.IsFalse(range.Contains(99));
        }

        #endregion

        #region Enumeration

        [Test]
        public void Enumeration_YieldsElementsBeforeTerminal()
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedListNode<int> first = list.AddLast(10);
            list.AddLast(20);
            list.AddLast(30);
            LinkedListNode<int> terminal = list.AddLast(default(int));
            var range = new GameFrameworkLinkedListRange<int>(first, terminal);

            int sum = 0;
            foreach (int value in range)
            {
                sum += value;
            }
            Assert.AreEqual(60, sum);
        }

        [Test]
        public void GetEnumerator_InvalidRange_ThrowsGameFrameworkException()
        {
            GameFrameworkLinkedListRange<int> range = default(GameFrameworkLinkedListRange<int>);
            Assert.Throws<GameFrameworkException>(() =>
            {
                var enumerator = range.GetEnumerator();
            });
        }

        #endregion
    }
}
*/
