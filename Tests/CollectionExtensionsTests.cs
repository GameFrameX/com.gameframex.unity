using System;
using System.Collections.Generic;
using System.Linq;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class CollectionExtensionsTests
    {
        #region Dictionary.Merge

        [Test]
        public void Merge_NewKey_AddsValue()
        {
            var dict = new Dictionary<string, int>();

            dict.Merge("key", 10, (oldVal, newVal) => oldVal + newVal);

            Assert.AreEqual(10, dict["key"]);
        }

        [Test]
        public void Merge_ExistingKey_AppliesMergeFunction()
        {
            var dict = new Dictionary<string, int> { { "key", 5 } };

            dict.Merge("key", 10, (oldVal, newVal) => oldVal + newVal);

            Assert.AreEqual(15, dict["key"]);
        }

        [Test]
        public void Merge_ExistingKey_ReplaceStrategy()
        {
            var dict = new Dictionary<string, string> { { "key", "old" } };

            dict.Merge("key", "new", (oldVal, newVal) => newVal);

            Assert.AreEqual("new", dict["key"]);
        }

        #endregion

        #region Dictionary.GetOrAdd (with factory)

        [Test]
        public void GetOrAdd_ExistingKey_ReturnsExistingValue()
        {
            var dict = new Dictionary<string, int> { { "key", 42 } };

            int result = dict.GetOrAdd("key", k => 99);

            Assert.AreEqual(42, result);
        }

        [Test]
        public void GetOrAdd_MissingKey_CreatesAndReturnsValue()
        {
            var dict = new Dictionary<string, int>();

            int result = dict.GetOrAdd("key", k => 100);

            Assert.AreEqual(100, result);
            Assert.AreEqual(100, dict["key"]);
        }

        [Test]
        public void GetOrAdd_FactoryReceivesKey()
        {
            var dict = new Dictionary<string, string>();

            string result = dict.GetOrAdd("myKey", k => "value_" + k);

            Assert.AreEqual("value_myKey", result);
        }

        #endregion

        #region Dictionary.GetOrAdd (default ctor)

        [Test]
        public void GetOrAdd_DefaultCtor_ExistingKey_ReturnsExistingValue()
        {
            var dict = new Dictionary<string, List<int>> { { "key", new List<int> { 1, 2 } } };

            var result = dict.GetOrAdd<string, List<int>>("key");

            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void GetOrAdd_DefaultCtor_MissingKey_CreatesNewInstance()
        {
            var dict = new Dictionary<string, List<int>>();

            var result = dict.GetOrAdd<string, List<int>>("key");

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
            Assert.AreEqual(1, dict.Count);
        }

        #endregion

        #region Dictionary.RemoveIf

        [Test]
        public void RemoveIf_MatchingCondition_RemovesAndReturnsCount()
        {
            var dict = new Dictionary<string, int> { { "a", 1 }, { "b", 2 }, { "c", 3 } };

            int removed = dict.RemoveIf((k, v) => v > 1);

            Assert.AreEqual(2, removed);
            Assert.AreEqual(1, dict.Count);
            Assert.IsTrue(dict.ContainsKey("a"));
        }

        [Test]
        public void RemoveIf_NoMatch_ReturnsZero()
        {
            var dict = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } };

            int removed = dict.RemoveIf((k, v) => v > 100);

            Assert.AreEqual(0, removed);
            Assert.AreEqual(2, dict.Count);
        }

        [Test]
        public void RemoveIf_AllMatch_ClearsDictionary()
        {
            var dict = new Dictionary<int, int> { { 1, 10 }, { 2, 20 } };

            int removed = dict.RemoveIf((k, v) => true);

            Assert.AreEqual(2, removed);
            Assert.AreEqual(0, dict.Count);
        }

        [Test]
        public void RemoveIf_EmptyDictionary_ReturnsZero()
        {
            var dict = new Dictionary<int, int>();

            int removed = dict.RemoveIf((k, v) => true);

            Assert.AreEqual(0, removed);
        }

        #endregion

        #region ICollection.IsNullOrEmpty

        [Test]
        public void IsNullOrEmpty_NullCollection_ReturnsTrue()
        {
            List<int> list = null;

            Assert.IsTrue(list.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrEmpty_EmptyCollection_ReturnsTrue()
        {
            var list = new List<int>();

            Assert.IsTrue(list.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrEmpty_NonEmptyCollection_ReturnsFalse()
        {
            var list = new List<int> { 1 };

            Assert.IsFalse(list.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrEmpty_NullArray_ReturnsTrue()
        {
            int[] arr = null;

            Assert.IsTrue(arr.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrEmpty_EmptyArray_ReturnsTrue()
        {
            var arr = new int[0];

            Assert.IsTrue(arr.IsNullOrEmpty());
        }

        #endregion

        #region List.Shuffle

        [Test]
        public void Shuffle_PreservesElements()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var original = list.ToList();

            list.Shuffle();

            CollectionAssert.AreEquivalent(original, list);
        }

        [Test]
        public void Shuffle_SingleElement_NoChange()
        {
            var list = new List<int> { 42 };

            list.Shuffle();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(42, list[0]);
        }

        [Test]
        public void Shuffle_EmptyList_DoesNotThrow()
        {
            var list = new List<int>();

            Assert.DoesNotThrow(() =>
            {
                list.Shuffle();
            });
        }

        [Test]
        public void Shuffle_PreservesCount()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            int countBefore = list.Count;

            list.Shuffle();

            Assert.AreEqual(countBefore, list.Count);
        }

        #endregion

        #region List.RemoveIf

        [Test]
        public void ListRemoveIf_MatchingElements_RemovesThem()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            list.RemoveIf(x => x % 2 == 0);

            CollectionAssert.AreEqual(new[] { 1, 3, 5 }, list);
        }

        [Test]
        public void ListRemoveIf_NoMatch_DoesNotRemove()
        {
            var list = new List<int> { 1, 3, 5 };

            list.RemoveIf(x => x % 2 == 0);

            CollectionAssert.AreEqual(new[] { 1, 3, 5 }, list);
        }

        [Test]
        public void ListRemoveIf_AllMatch_ClearsList()
        {
            var list = new List<int> { 2, 4, 6 };

            list.RemoveIf(x => true);

            Assert.IsEmpty(list);
        }

        [Test]
        public void ListRemoveIf_EmptyList_DoesNotThrow()
        {
            var list = new List<int>();

            Assert.DoesNotThrow(() =>
            {
                list.RemoveIf(x => true);
            });
        }

        [Test]
        public void ListRemoveIf_ConsecutiveMatches_RemovesAll()
        {
            var list = new List<int> { 1, 1, 2, 1, 3 };

            list.RemoveIf(x => x == 1);

            CollectionAssert.AreEqual(new[] { 2, 3 }, list);
        }

        #endregion

        #region List.ListToString

        [Test]
        public void ListToString_DefaultSeparator_CommaSeparated()
        {
            var list = new List<int> { 1, 2, 3 };

            string result = list.ListToString();

            Assert.AreEqual("1,2,3", result);
        }

        [Test]
        public void ListToString_CustomSeparator_UsesSeparator()
        {
            var list = new List<string> { "a", "b", "c" };

            string result = list.ListToString("-");

            Assert.AreEqual("a-b-c", result);
        }

        [Test]
        public void ListToString_SingleElement_NoSeparator()
        {
            var list = new List<int> { 42 };

            string result = list.ListToString();

            Assert.AreEqual("42", result);
        }

        [Test]
        public void ListToString_NullList_ReturnsEmptyString()
        {
            List<int> list = null;

            string result = list.ListToString();

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void ListToString_EmptyList_ReturnsEmptyString()
        {
            var list = new List<int>();

            string result = list.ListToString();

            Assert.AreEqual(string.Empty, result);
        }

        #endregion

        #region HashSet.AddRange

        [Test]
        public void AddRange_AddsAllElements()
        {
            var set = new HashSet<int> { 1 };
            var items = new List<int> { 2, 3, 4 };

            set.AddRange(items);

            Assert.AreEqual(4, set.Count);
            Assert.IsTrue(set.Contains(1));
            Assert.IsTrue(set.Contains(2));
            Assert.IsTrue(set.Contains(3));
            Assert.IsTrue(set.Contains(4));
        }

        [Test]
        public void AddRange_Duplicates_IgnoresDuplicates()
        {
            var set = new HashSet<int> { 1, 2 };
            var items = new List<int> { 2, 3 };

            set.AddRange(items);

            Assert.AreEqual(3, set.Count);
        }

        [Test]
        public void AddRange_EmptySource_DoesNotChange()
        {
            var set = new HashSet<int> { 1 };
            var items = new List<int>();

            set.AddRange(items);

            Assert.AreEqual(1, set.Count);
        }

        #endregion

        #region DistinctBy

        [Test]
        public void DistinctBy_IntKey_RemovesDuplicates()
        {
            var items = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("a", 1),
                new KeyValuePair<string, int>("b", 2),
                new KeyValuePair<string, int>("c", 1),
                new KeyValuePair<string, int>("d", 2),
                new KeyValuePair<string, int>("e", 3),
            };

            var result = items.DistinctBy(kv => kv.Value).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("a", result[0].Key);
            Assert.AreEqual("b", result[1].Key);
            Assert.AreEqual("e", result[2].Key);
        }

        [Test]
        public void DistinctBy_StringKey_RemovesDuplicates()
        {
            var items = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("a", 1),
                new KeyValuePair<string, int>("a", 2),
                new KeyValuePair<string, int>("b", 3),
                new KeyValuePair<string, int>("b", 4),
            };

            var result = items.DistinctBy(kv => kv.Key).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Value);
            Assert.AreEqual(3, result[1].Value);
        }

        [Test]
        public void DistinctBy_NoDuplicates_ReturnsAll()
        {
            var items = new List<int> { 1, 2, 3, 4, 5 };

            var result = items.DistinctBy(x => x).ToList();

            Assert.AreEqual(5, result.Count);
            CollectionAssert.AreEqual(items, result);
        }

        [Test]
        public void DistinctBy_AllDuplicates_ReturnsSingle()
        {
            var items = new List<int> { 7, 7, 7, 7 };

            var result = items.DistinctBy(x => x).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(7, result[0]);
        }

        [Test]
        public void DistinctBy_KeepsFirstOccurrence()
        {
            var items = new List<string> { "first", "second", "first", "third", "second" };

            var result = items.DistinctBy(x => x).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("first", result[0]);
            Assert.AreEqual("second", result[1]);
            Assert.AreEqual("third", result[2]);
        }

        [Test]
        public void DistinctBy_ComplexKeySelector_WorksCorrectly()
        {
            var items = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(2, 4),
                new Tuple<int, int>(3, 6),
                new Tuple<int, int>(4, 8),
            };

            var result = items.DistinctBy(t => t.Item2 % 3).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, result[0].Item2 % 3);
            Assert.AreEqual(1, result[1].Item2 % 3);
            Assert.AreEqual(0, result[2].Item2 % 3);
        }

        [Test]
        public void DistinctBy_EmptyCollection_ReturnsEmpty()
        {
            var items = new List<int>();

            var result = items.DistinctBy(x => x).ToList();

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void DistinctBy_SingleItem_ReturnsSingle()
        {
            var items = new List<int> { 42 };

            var result = items.DistinctBy(x => x).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(42, result[0]);
        }

        [Test]
        public void DistinctBy_NullKeys_PreservesFirstNull()
        {
            var items = new List<string> { null, "a", null, "b" };

            var result = items.DistinctBy(x => x).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.IsNull(result[0]);
            Assert.AreEqual("a", result[1]);
            Assert.AreEqual("b", result[2]);
        }

        [Test]
        public void DistinctBy_AllNullKeys_ReturnsSingleNull()
        {
            var items = new List<string> { null, null, null };

            var result = items.DistinctBy(x => x).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.IsNull(result[0]);
        }

        [Test]
        public void DistinctBy_LargeCollection_PerformsCorrectly()
        {
            var items = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                items.Add(i % 10);
            }

            var result = items.DistinctBy(x => x).ToList();

            Assert.AreEqual(10, result.Count);
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, result[i]);
            }
        }

        [Test]
        public void DistinctBy_ReferenceTypeKey_UsesEqualityComparer()
        {
            var items = new List<string[]>
            {
                new string[] { "a", "b" },
                new string[] { "a", "b" },
                new string[] { "c" },
            };

            var result = items.DistinctBy(x => x.Length).ToList();

            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void DistinctBy_ReturnsLazyEnumerable()
        {
            int callCount = 0;
            var items = new List<int> { 1, 2, 1, 3 };
            Func<int, int> selector = x =>
            {
                callCount++;
                return x;
            };

            var enumerable = items.DistinctBy(selector);

            Assert.AreEqual(0, callCount, "DistinctBy should be lazy and not evaluate until enumerated");

            var result = enumerable.ToList();

            Assert.Greater(callCount, 0, "After enumeration, selector should have been called");
        }

        #endregion
    }
}
