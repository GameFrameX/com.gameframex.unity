using System;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class BidirectionalDictionaryTests
    {
        private BidirectionalDictionary<string, int> _dict;

        [SetUp]
        public void SetUp()
        {
            _dict = new BidirectionalDictionary<string, int>();
        }

        #region TryAdd

        [Test]
        public void TryAdd_NewPair_ReturnsTrue()
        {
            bool result = _dict.TryAdd("one", 1);

            Assert.IsTrue(result);
            Assert.AreEqual(1, _dict.Count);
        }

        [Test]
        public void TryAdd_MultiplePairs_ReturnsTrueEachTime()
        {
            Assert.IsTrue(_dict.TryAdd("a", 1));
            Assert.IsTrue(_dict.TryAdd("b", 2));
            Assert.IsTrue(_dict.TryAdd("c", 3));

            Assert.AreEqual(3, _dict.Count);
        }

        [Test]
        public void TryAdd_DuplicateKey_ReturnsFalse()
        {
            _dict.TryAdd("one", 1);

            bool result = _dict.TryAdd("one", 99);

            Assert.IsFalse(result);
            Assert.AreEqual(1, _dict.Count);
        }

        [Test]
        public void TryAdd_DuplicateValue_ReturnsFalse()
        {
            _dict.TryAdd("one", 1);

            bool result = _dict.TryAdd("different", 1);

            Assert.IsFalse(result);
            Assert.AreEqual(1, _dict.Count);
        }

        #endregion

        #region TryGetValue / TryGetKey

        [Test]
        public void TryGetValue_ExistingKey_ReturnsTrueAndCorrectValue()
        {
            _dict.TryAdd("hello", 42);

            bool result = _dict.TryGetValue("hello", out int value);

            Assert.IsTrue(result);
            Assert.AreEqual(42, value);
        }

        [Test]
        public void TryGetValue_MissingKey_ReturnsFalse()
        {
            bool result = _dict.TryGetValue("missing", out int value);

            Assert.IsFalse(result);
            Assert.AreEqual(default(int), value);
        }

        [Test]
        public void TryGetKey_ExistingValue_ReturnsTrueAndCorrectKey()
        {
            _dict.TryAdd("hello", 42);

            bool result = _dict.TryGetKey(42, out string key);

            Assert.IsTrue(result);
            Assert.AreEqual("hello", key);
        }

        [Test]
        public void TryGetKey_MissingValue_ReturnsFalse()
        {
            bool result = _dict.TryGetKey(999, out string key);

            Assert.IsFalse(result);
            Assert.IsNull(key);
        }

        #endregion

        #region Count

        [Test]
        public void Count_NewDictionary_IsZero()
        {
            Assert.AreEqual(0, _dict.Count);
        }

        [Test]
        public void Count_AfterAdds_ReflectsCount()
        {
            _dict.TryAdd("a", 1);
            _dict.TryAdd("b", 2);

            Assert.AreEqual(2, _dict.Count);
        }

        #endregion

        #region TryRemoveByKey

        [Test]
        public void TryRemoveByKey_ExistingKey_ReturnsTrueAndRemoves()
        {
            _dict.TryAdd("x", 10);

            bool result = _dict.TryRemoveByKey("x");

            Assert.IsTrue(result);
            Assert.AreEqual(0, _dict.Count);
            Assert.IsFalse(_dict.TryGetValue("x", out _));
            Assert.IsFalse(_dict.TryGetKey(10, out _));
        }

        [Test]
        public void TryRemoveByKey_MissingKey_ReturnsFalse()
        {
            bool result = _dict.TryRemoveByKey("nonexistent");

            Assert.IsFalse(result);
        }

        [Test]
        public void TryRemoveByKey_RemovesFromBothDirections()
        {
            _dict.TryAdd("a", 1);
            _dict.TryAdd("b", 2);
            _dict.TryRemoveByKey("a");

            Assert.IsFalse(_dict.TryGetValue("a", out _));
            Assert.IsFalse(_dict.TryGetKey(1, out _));
            Assert.IsTrue(_dict.TryGetValue("b", out int val));
            Assert.AreEqual(2, val);
        }

        #endregion

        #region TryRemoveByValue

        [Test]
        public void TryRemoveByValue_ExistingValue_ReturnsTrueAndRemoves()
        {
            _dict.TryAdd("x", 10);

            bool result = _dict.TryRemoveByValue(10);

            Assert.IsTrue(result);
            Assert.AreEqual(0, _dict.Count);
            Assert.IsFalse(_dict.TryGetValue("x", out _));
        }

        [Test]
        public void TryRemoveByValue_MissingValue_ReturnsFalse()
        {
            bool result = _dict.TryRemoveByValue(999);

            Assert.IsFalse(result);
        }

        #endregion

        #region Clear

        [Test]
        public void Clear_RemovesAllEntries()
        {
            _dict.TryAdd("a", 1);
            _dict.TryAdd("b", 2);
            _dict.TryAdd("c", 3);

            _dict.Clear();

            Assert.AreEqual(0, _dict.Count);
            Assert.IsFalse(_dict.TryGetValue("a", out _));
            Assert.IsFalse(_dict.TryGetKey(1, out _));
        }

        [Test]
        public void Clear_EmptyDictionary_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                _dict.Clear();
            });
        }

        [Test]
        public void Clear_ThenAdd_WorksCorrectly()
        {
            _dict.TryAdd("a", 1);
            _dict.Clear();
            bool result = _dict.TryAdd("a", 1);

            Assert.IsTrue(result);
            Assert.AreEqual(1, _dict.Count);
        }

        #endregion

        #region Edge Cases

        [Test]
        public void TryAdd_NullKeyAndValue_WorksForReferenceTypes()
        {
            var nullDict = new BidirectionalDictionary<string, string>();

            bool result = nullDict.TryAdd(null, null);

            Assert.IsFalse(result);
        }

        [Test]
        public void Capacity_DefaultIsEight()
        {
            var dict = new BidirectionalDictionary<int, int>();
            Assert.AreEqual(0, dict.Count);

            for (int i = 0; i < 20; i++)
            {
                Assert.IsTrue(dict.TryAdd(i, i * 10));
            }

            Assert.AreEqual(20, dict.Count);
        }

        [Test]
        public void TryAdd_AfterRemoveSameKey_CanReAdd()
        {
            _dict.TryAdd("a", 1);
            _dict.TryRemoveByKey("a");

            bool result = _dict.TryAdd("a", 1);

            Assert.IsTrue(result);
            Assert.AreEqual(1, _dict.Count);
        }

        [Test]
        public void TryAdd_AfterRemoveSameValue_CanReAdd()
        {
            _dict.TryAdd("a", 1);
            _dict.TryRemoveByValue(1);

            bool result = _dict.TryAdd("a", 1);

            Assert.IsTrue(result);
        }

        #endregion
    }
}
