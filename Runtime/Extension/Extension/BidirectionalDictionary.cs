using System.Collections.Generic;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 双向字典，支持通过键查找值，也支持通过值查找键。
    /// </summary>
    /// <remarks>
    /// A bidirectional dictionary that supports looking up values by key and keys by value.
    /// </remarks>
    /// <typeparam name="TKey">键的类型 / The type of the key.</typeparam>
    /// <typeparam name="TValue">值的类型 / The type of the value.</typeparam>
    [UnityEngine.Scripting.Preserve]
    public class BidirectionalDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _forwardDictionary;
        private readonly Dictionary<TValue, TKey> _reverseDictionary;

        private int _count;

        /// <summary>
        /// 初始化双向字典的新实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the bidirectional dictionary.
        /// </remarks>
        /// <param name="capacity">初始容量 / The initial capacity.</param>
        [UnityEngine.Scripting.Preserve]
        public BidirectionalDictionary(int capacity = 8)
        {
            _count = 0;
            _forwardDictionary = new Dictionary<TKey, TValue>(capacity);
            _reverseDictionary = new Dictionary<TValue, TKey>(capacity);
        }

        /// <summary>
        /// 尝试通过值获取对应的键。
        /// </summary>
        /// <remarks>
        /// Attempts to get the corresponding key by value.
        /// </remarks>
        /// <param name="value">要查找的值 / The value to search for.</param>
        /// <param name="key">找到的键 / The found key.</param>
        /// <returns>如果找到键则返回 true，否则返回 false / Returns true if the key is found; otherwise, false.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool TryGetKey(TValue value, out TKey key)
        {
            return _reverseDictionary.TryGetValue(value, out key);
        }

        /// <summary>
        /// 尝试通过键获取对应的值。
        /// </summary>
        /// <remarks>
        /// Attempts to get the corresponding value by key.
        /// </remarks>
        /// <param name="key">要查找的键 / The key to search for.</param>
        /// <param name="value">找到的值 / The found value.</param>
        /// <returns>如果找到值则返回 true，否则返回 false / Returns true if the value is found; otherwise, false.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _forwardDictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// 获取字典中包含的键值对数量。
        /// </summary>
        /// <remarks>
        /// Gets the number of key-value pairs contained in the dictionary.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// 清空字典中的所有键值对。
        /// </summary>
        /// <remarks>
        /// Clears all key-value pairs from the dictionary.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public void Clear()
        {
            _count = 0;
            _forwardDictionary.Clear();
            _reverseDictionary.Clear();
        }

        /// <summary>
        /// 尝试向字典中添加键值对。
        /// </summary>
        /// <remarks>
        /// Attempts to add a key-value pair to the dictionary.
        /// </remarks>
        /// <param name="key">要添加的键 / The key to add.</param>
        /// <param name="value">要添加的值 / The value to add.</param>
        /// <returns>如果添加成功则返回 true，如果键已存在则返回 false / Returns true if added successfully; false if the key already exists.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool TryAdd(TKey key, TValue value)
        {
            if (!_forwardDictionary.ContainsKey(key))
            {
                _forwardDictionary.Add(key, value);
                _reverseDictionary.Add(value, key);
                _count++;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 尝试通过键移除键值对。
        /// </summary>
        /// <remarks>
        /// Attempts to remove a key-value pair by key.
        /// </remarks>
        /// <param name="key">要移除的键 / The key to remove.</param>
        /// <returns>如果移除成功则返回 true，如果键不存在则返回 false / Returns true if removed successfully; false if the key does not exist.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool TryRemoveByKey(TKey key)
        {
            if (_forwardDictionary.TryGetValue(key, out var value))
            {
                _forwardDictionary.Remove(key);
                _reverseDictionary.Remove(value);
                _count--;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 尝试通过值移除键值对。
        /// </summary>
        /// <remarks>
        /// Attempts to remove a key-value pair by value.
        /// </remarks>
        /// <param name="value">要移除的值 / The value to remove.</param>
        /// <returns>如果移除成功则返回 true，如果值不存在则返回 false / Returns true if removed successfully; false if the value does not exist.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool TryRemoveByValue(TValue value)
        {
            if (_reverseDictionary.TryGetValue(value, out var key))
            {
                _reverseDictionary.Remove(value);
                _forwardDictionary.Remove(key);
                _count--;
                return true;
            }

            return false;
        }
    }
}