using System;
using System.Collections.Generic;
using System.Text;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 集合扩展方法类
    /// </summary>
    /// <remarks>
    /// Provides extension methods for collections including Dictionary, List, HashSet, and ICollection.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class CollectionExtensions
    {
        #region DictionaryExtensions

        /// <summary>
        /// 将k和v进行合并
        /// </summary>
        /// <remarks>
        /// Merges a key-value pair into the dictionary using the specified merge function.
        /// If the key exists, the merge function is called with the existing and new values.
        /// If the key does not exist, the new value is added directly.
        /// </remarks>
        /// <param name="self">目标字典 / The target dictionary</param>
        /// <param name="k">键 / The key to merge</param>
        /// <param name="v">值 / The value to merge</param>
        /// <param name="func">合并函数 / The merge function to combine existing and new values</param>
        /// <typeparam name="TKey">键类型 / The type of the key</typeparam>
        /// <typeparam name="TValue">值类型 / The type of the value</typeparam>
        [UnityEngine.Scripting.Preserve]
        public static void Merge<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey k, TValue v, Func<TValue, TValue, TValue> func)
        {
            self[k] = self.TryGetValue(k, out var value) ? func(value, v) : v;
        }

        /// <summary>
        /// 根据key获取value值，当不存在时通过valueGetter生成value，放入Dictionary并返回value
        /// </summary>
        /// <remarks>
        /// Gets the value associated with the key, or creates and adds a new value using the valueGetter if the key does not exist.
        /// </remarks>
        /// <param name="self">目标字典 / The target dictionary</param>
        /// <param name="key">键 / The key to look up or add</param>
        /// <param name="valueGetter">值生成函数 / The function to create a value if the key does not exist</param>
        /// <typeparam name="TKey">键类型 / The type of the key</typeparam>
        /// <typeparam name="TValue">值类型 / The type of the value</typeparam>
        /// <returns>获取或创建的值 / The retrieved or created value</returns>
        [UnityEngine.Scripting.Preserve]
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, Func<TKey, TValue> valueGetter)
        {
            if (!self.TryGetValue(key, out var value))
            {
                value = valueGetter(key);
                self[key] = value;
            }

            return value;
        }

        /// <summary>
        /// 根据key获取value值，当不存在时通过默认构造函数生成value，放入Dictionary并返回value
        /// </summary>
        /// <remarks>
        /// Gets the value associated with the key, or creates and adds a new value using the default constructor if the key does not exist.
        /// </remarks>
        /// <param name="self">目标字典 / The target dictionary</param>
        /// <param name="key">键 / The key to look up or add</param>
        /// <typeparam name="TKey">键类型 / The type of the key</typeparam>
        /// <typeparam name="TValue">值类型 / The type of the value (must have a parameterless constructor)</typeparam>
        /// <returns>获取或创建的值 / The retrieved or created value</returns>
        [UnityEngine.Scripting.Preserve]
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key) where TValue : new()
        {
            return GetOrAdd(self, key, k => new TValue());
        }

        /// <summary>
        /// 根据条件移除
        /// </summary>
        /// <remarks>
        /// Removes all key-value pairs from the dictionary that match the specified predicate.
        /// </remarks>
        /// <param name="self">目标字典 / The target dictionary</param>
        /// <param name="predict">判断条件 / The predicate to determine which items to remove</param>
        /// <typeparam name="TKey">键类型 / The type of the key</typeparam>
        /// <typeparam name="TValue">值类型 / The type of the value</typeparam>
        /// <returns>移除的元素数量 / The number of items removed</returns>
        [UnityEngine.Scripting.Preserve]
        public static int RemoveIf<TKey, TValue>(this Dictionary<TKey, TValue> self, Func<TKey, TValue, bool> predict)
        {
            int count = 0;
            var remove = new HashSet<TKey>();
            foreach (var kv in self)
            {
                if (predict(kv.Key, kv.Value))
                {
                    remove.Add(kv.Key);
                    count++;
                }
            }

            foreach (var key in remove)
            {
                self.Remove(key);
            }

            return count;
        }

        #endregion


        #region ICollectionExtensions

        /// <summary>
        /// 判断集合是否为null或空
        /// </summary>
        /// <remarks>
        /// Determines whether the collection is null or contains no elements.
        /// </remarks>
        /// <param name="self">目标集合 / The target collection</param>
        /// <typeparam name="T">元素类型 / The type of elements in the collection</typeparam>
        /// <returns>如果集合为null或空则返回true，否则返回false / True if the collection is null or empty, otherwise false</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsNullOrEmpty<T>(this ICollection<T> self)
        {
            return self == null || self.Count <= 0;
        }

        #endregion

        #region List<T>

        /// <summary>
        /// 打乱列表顺序（Fisher-Yates 洗牌算法）
        /// </summary>
        /// <remarks>
        /// Shuffles the elements in the list using the Fisher-Yates algorithm.
        /// This method modifies the list in place.
        /// </remarks>
        /// <param name="list">要打乱的列表 / The list to shuffle</param>
        /// <typeparam name="T">列表元素类型 / The type of elements in the list</typeparam>
        [UnityEngine.Scripting.Preserve]
        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            var r = ThreadLocalRandom.Current;
            for (int i = 0; i < n; i++)
            {
                int rand = r.Next(i, n);
                (list[i], list[rand]) = (list[rand], list[i]);
            }
        }

        /// <summary>
        /// 根据条件移除列表中的元素
        /// </summary>
        /// <remarks>
        /// Removes all elements from the list that match the specified condition.
        /// </remarks>
        /// <param name="list">目标列表 / The target list</param>
        /// <param name="condition">判断条件 / The predicate to determine which items to remove</param>
        /// <typeparam name="T">元素类型 / The type of elements in the list</typeparam>
        [UnityEngine.Scripting.Preserve]
        public static void RemoveIf<T>(this List<T> list, Predicate<T> condition)
        {
            var idx = list.FindIndex(condition);
            while (idx >= 0)
            {
                list.RemoveAt(idx);
                idx = list.FindIndex(condition);
            }
        }

        [ThreadStatic]
        private static StringBuilder _listToStringBuilder;

        /// <summary>
        /// 将列表转换为以指定字符串分割的字符串
        /// </summary>
        /// <remarks>
        /// Converts the list to a string with elements separated by the specified separator.
        /// </remarks>
        /// <param name="list">目标列表 / The target list</param>
        /// <param name="separator">分隔符，默认为逗号 / The separator string, defaults to comma</param>
        /// <typeparam name="T">元素类型 / The type of elements in the list</typeparam>
        /// <returns>拼接后的字符串 / The concatenated string</returns>
        [UnityEngine.Scripting.Preserve]
        public static string ListToString<T>(this List<T> list, string separator = ",")
        {
            if (list == null)
            {
                return string.Empty;
            }

            _listToStringBuilder ??= new StringBuilder();
            _listToStringBuilder.Clear();
            foreach (T t in list)
            {
                _listToStringBuilder.Append(t);
                _listToStringBuilder.Append(separator);
            }

            return _listToStringBuilder.ToString();
        }

        #endregion

        /// <summary>
        /// 向HashSet中添加多个元素
        /// </summary>
        /// <remarks>
        /// Adds all elements from the enumerable to the HashSet.
        /// </remarks>
        /// <param name="c">目标HashSet / The target HashSet</param>
        /// <param name="e">要添加的元素集合 / The enumerable of elements to add</param>
        /// <typeparam name="T">元素类型 / The type of elements</typeparam>
        [UnityEngine.Scripting.Preserve]
        public static void AddRange<T>(this HashSet<T> c, IEnumerable<T> e)
        {
            foreach (var item in e)
            {
                c.Add(item);
            }
        }
    }
}