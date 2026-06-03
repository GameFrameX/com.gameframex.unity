using System;
using System.Collections.Generic;
using System.Linq;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class DistinctHelperTests
    {
        #region DistinctBy - Happy Path

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

        #endregion

        #region DistinctBy - Edge Cases

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
