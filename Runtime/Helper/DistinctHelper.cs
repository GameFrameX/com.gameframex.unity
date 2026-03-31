using System;
using System.Collections.Generic;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 去重帮助类。
    /// </summary>
    /// <remarks>
    /// Distinct helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class DistinctHelper
    {
        /// <summary>
        /// 根据指定键选择器对集合进行去重。
        /// </summary>
        /// <remarks>
        /// Returns distinct elements from a collection based on a key selector.
        /// </remarks>
        /// <typeparam name="TSource">源集合元素类型 / Source collection element type</typeparam>
        /// <typeparam name="TKey">键选择器返回的键类型 / Key type returned by the key selector</typeparam>
        /// <param name="source">要去重的源集合 / Source collection to deduplicate</param>
        /// <param name="keySelector">用于选择比较键的函数 / Function to select the comparison key</param>
        /// <returns>去重后的集合 / Deduplicated collection</returns>
        [UnityEngine.Scripting.Preserve]
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var identifiedKeys = new HashSet<TKey>();

            foreach (var item in source)
            {
                if (identifiedKeys.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }
    }
}