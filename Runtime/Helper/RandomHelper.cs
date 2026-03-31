using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 随机数帮助类。
    /// </summary>
    /// <remarks>
    /// Random number helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class RandomHelper
    {
        /// <summary>
        /// 设置随机种子。
        /// </summary>
        /// <remarks>
        /// Sets the random seed for the random number generator.
        /// </remarks>
        /// <param name="seed">随机种子值 / Random seed value</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetSeed(int seed)
        {
            ThreadLocalRandom.SetSeed(seed);
        }

        /// <summary>
        /// 获取UInt64范围内的随机数。
        /// </summary>
        /// <remarks>
        /// Gets a random number within the UInt64 range.
        /// </remarks>
        /// <returns>返回一个随机的UInt64值 / A random UInt64 value</returns>
        [UnityEngine.Scripting.Preserve]
        public static ulong NextUInt64()
        {
            return ThreadLocalRandom.NextUInt64();
        }

        /// <summary>
        /// 获取Int64范围内的随机数。
        /// </summary>
        /// <remarks>
        /// Gets a random number within the Int64 range.
        /// </remarks>
        /// <returns>返回一个随机的Int64值 / A random Int64 value</returns>
        [UnityEngine.Scripting.Preserve]
        public static long NextInt64()
        {
            return ThreadLocalRandom.NextInt64();
        }

        /// <summary>
        /// 获取指定范围内的随机整数。
        /// </summary>
        /// <remarks>
        /// Gets a random integer within the specified range.
        /// </remarks>
        /// <param name="lower">下限（包含）/ Lower bound (inclusive)</param>
        /// <param name="upper">上限（不包含）/ Upper bound (exclusive)</param>
        /// <returns>返回一个在指定范围内的随机整数 / A random integer within the specified range</returns>
        [UnityEngine.Scripting.Preserve]
        public static int Next(int lower, int upper)
        {
            return ThreadLocalRandom.Current.Next(lower, upper);
        }

        /// <summary>
        /// 获取0与1之间的随机浮点数。
        /// </summary>
        /// <remarks>
        /// Gets a random float between 0 and 1.
        /// </remarks>
        /// <returns>返回一个在0到1之间的随机浮点数 / A random float between 0 and 1</returns>
        [UnityEngine.Scripting.Preserve]
        public static float Next()
        {
            return ThreadLocalRandom.Current.Next(0, 100_000) / 100_000f;
        }
    }
}