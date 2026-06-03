/*
using System.Collections.Generic;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class GameFrameworkMultiDictionaryTests
    {
        private GameFrameworkMultiDictionary<string, int> _dict;

        [SetUp]
        public void SetUp()
        {
            _dict = new GameFrameworkMultiDictionary<string, int>();
        }

        #region Count

        [Test]
        public void Count_NewDictionary_ReturnsZero()
        {
            Assert.AreEqual(0, _dict.Count);
        }

        [Test]
        public void Count_AfterAdd_ReturnsOne()
        {
            _dict.Add("key1", 1);
            Assert.AreEqual(1, _dict.Count);
        }

        #endregion

        #region Add

        [Test]
        public void Add_NewKey_CreatesRange()
        {
            _dict.Add("key1", 10);
            Assert.IsTrue(_dict.Contains("key1"));
            Assert.IsTrue(_dict.Contains("key1", 10));
        }

        [Test]
        public void Add_SameKeyMultipleValues_AllStored()
        {
            _dict.Add("key1", 1);
            _dict.Add("key1", 2);
            _dict.Add("key1", 3);
            Assert.AreEqual(1, _dict.Count);
            Assert.IsTrue(_dict.Contains("key1", 1));
            Assert.IsTrue(_dict.Contains("key1", 2));
            Assert.IsTrue(_dict.Contains("key1", 3));
        }

        [Test]
        public void Add_DifferentKeys_TrackedSeparately()
        {
            _dict.Add("key1", 1);
            _dict.Add("key2", 2);
            Assert.AreEqual(2, _dict.Count);
        }

        #endregion

        #region Contains

        [Test]
        public void Contains_KeyExists_ReturnsTrue()
        {
            _dict.Add("key1", 1);
            Assert.IsTrue(_dict.Contains("key1"));
        }

        [Test]
        public void Contains_KeyNotExists_ReturnsFalse()
        {
            Assert.IsFalse(_dict.Contains("nonexistent"));
        }

        [Test]
        public void Contains_KeyAndValueExist_ReturnsTrue()
        {
            _dict.Add("key1", 42);
            Assert.IsTrue(_dict.Contains("key1", 42));
        }

        [Test]
        public void Contains_KeyExistsValueNot_ReturnsFalse()
        {
            _dict.Add("key1", 42);
            Assert.IsFalse(_dict.Contains("key1", 99));
        }

        [Test]
        public void Contains_KeyNotExistsValueAny_ReturnsFalse()
        {
            Assert.IsFalse(_dict.Contains("nonexistent", 1));
        }

        #endregion

        #region Remove

        [Test]
        public void Remove_ExistingValue_ReturnsTrue()
        {
            _dict.Add("key1", 1);
            _dict.Add("key1", 2);
            bool result = _dict.Remove("key1", 1);
            Assert.IsTrue(result);
            Assert.IsFalse(_dict.Contains("key1", 1));
            Assert.IsTrue(_dict.Contains("key1", 2));
        }

        [Test]
        public void Remove_NonExistingValue_ReturnsFalse()
        {
            _dict.Add("key1", 1);
            bool result = _dict.Remove("key1", 99);
            Assert.IsFalse(result);
        }

        [Test]
        public void Remove_NonExistingKey_ReturnsFalse()
        {
            bool result = _dict.Remove("nonexistent", 1);
            Assert.IsFalse(result);
        }

        [Test]
        public void Remove_LastValueForKey_RemovesKeyEntirely()
        {
            _dict.Add("key1", 1);
            bool result = _dict.Remove("key1", 1);
            Assert.IsTrue(result);
            Assert.IsFalse(_dict.Contains("key1"));
            Assert.AreEqual(0, _dict.Count);
        }

        #endregion

        #region RemoveAll

        [Test]
        public void RemoveAll_ExistingKey_ReturnsTrue()
        {
            _dict.Add("key1", 1);
            _dict.Add("key1", 2);
            _dict.Add("key1", 3);
            bool result = _dict.RemoveAll("key1");
            Assert.IsTrue(result);
            Assert.IsFalse(_dict.Contains("key1"));
            Assert.AreEqual(0, _dict.Count);
        }

        [Test]
        public void RemoveAll_NonExistingKey_ReturnsFalse()
        {
            bool result = _dict.RemoveAll("nonexistent");
            Assert.IsFalse(result);
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_ResetsDictionary()
        {
            _dict.Add("key1", 1);
            _dict.Add("key2", 2);
            _dict.Clear();
            Assert.AreEqual(0, _dict.Count);
            Assert.IsFalse(_dict.Contains("key1"));
            Assert.IsFalse(_dict.Contains("key2"));
        }

        #endregion

        #region indexer

        [Test]
        public void Indexer_ExistingKey_ReturnsRange()
        {
            _dict.Add("key1", 42);
            GameFrameworkLinkedListRange<int> range = _dict["key1"];
            Assert.IsTrue(range.IsValid);
            Assert.IsTrue(range.Contains(42));
        }

        [Test]
        public void Indexer_NonExistingKey_ReturnsInvalidRange()
        {
            GameFrameworkLinkedListRange<int> range = _dict["nonexistent"];
            Assert.IsFalse(range.IsValid);
        }

        #endregion

        #region TryGetValue

        [Test]
        public void TryGetValue_ExistingKey_ReturnsTrue()
        {
            _dict.Add("key1", 42);
            bool result = _dict.TryGetValue("key1", out GameFrameworkLinkedListRange<int> range);
            Assert.IsTrue(result);
            Assert.IsTrue(range.IsValid);
        }

        [Test]
        public void TryGetValue_NonExistingKey_ReturnsFalse()
        {
            bool result = _dict.TryGetValue("nonexistent", out GameFrameworkLinkedListRange<int> range);
            Assert.IsFalse(result);
        }

        #endregion

        #region Enumeration

        [Test]
        public void Enumeration_YieldsAllKeys()
        {
            _dict.Add("a", 1);
            _dict.Add("b", 2);
            int count = 0;
            foreach (var kvp in _dict)
            {
                count++;
            }
            Assert.AreEqual(2, count);
        }

        #endregion
    }
}
*/
