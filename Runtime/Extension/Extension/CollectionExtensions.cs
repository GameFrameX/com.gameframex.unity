using System;
using System.Collections.Generic;
using System.Text;

namespace GameFrameX.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public static class CollectionExtensions
    {
        #region DictionaryExtensions

        /// <summary>
        /// 将k和v进行合并
        /// </summary>
        /// <param name="self"></param>
        /// <param name="k"></param>
        /// <param name="v"></param>
        /// <param name="func"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        [UnityEngine.Scripting.Preserve]
        public static void Merge<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey k, TValue v, Func<TValue, TValue, TValue> func)
        {
            self[k] = self.TryGetValue(k, out var value) ? func(value, v) : v;
        }

        /// <summary>
        /// 根据key获取value值，当不存在时通过valueGetter生成value，放入Dictionary并返回value
        /// </summary>
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
        [UnityEngine.Scripting.Preserve]
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key) where TValue : new()
        {
            return GetOrAdd(self, key, k => new TValue());
        }

        /// <summary>
        /// 根据条件移除
        /// </summary>
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

        [UnityEngine.Scripting.Preserve]
        public static bool IsNullOrEmpty<T>(this ICollection<T> self)
        {
            return self == null || self.Count <= 0;
        }

        #endregion

        #region List<T>

        /// <summary>
        /// 打乱
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static void Shuffer<T>(this List<T> list)
        {
            int n = list.Count;
            var r = ThreadLocalRandom.Current;
            for (int i = 0; i < n; i++)
            {
                int rand = r.Next(i, n);
                (list[i], list[rand]) = (list[rand], list[i]);
            }
        }

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

        private static readonly StringBuilder ListToStringBuilder = new StringBuilder();

        /// <summary>
        /// 将列表转换为以指定字符串分割的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <param name="separator">默认为逗号</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [UnityEngine.Scripting.Preserve]
        public static string ListToString<T>(this List<T> list, string separator = ",")
        {
            ListToStringBuilder.Clear();
            foreach (T t in list)
            {
                ListToStringBuilder.Append(t);
                ListToStringBuilder.Append(separator);
            }

            return ListToStringBuilder.ToString();
        }

        #endregion

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