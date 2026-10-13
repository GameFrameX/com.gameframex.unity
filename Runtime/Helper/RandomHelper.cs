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
        /// <param name="lower">下限（包含） / Lower bound (inclusive)</param>
        /// <param name="upper">上限（不包含） / Upper bound (exclusive)</param>
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
            return (float)ThreadLocalRandom.Current.NextDouble();
        }

        /// <summary>
        /// 获取Int32范围内的随机数（C27 迁入）。
        /// 随机引擎统一到 ThreadLocalRandom 单一引擎，避免双引擎并存导致的种子语义分裂。
        /// </summary>
        /// <remarks>
        /// Gets a non-negative random Int32 value.
        /// </remarks>
        /// <returns>返回一个大于等于零且小于 System.Int32.MaxValue 的 32 位带符号整数 / A non-negative 32-bit signed integer</returns>
        [UnityEngine.Scripting.Preserve]
        public static int NextInt32()
        {
            return ThreadLocalRandom.Current.Next();
        }

        /// <summary>
        /// 获取 0.0 与 1.0 之间的随机双精度浮点数（C27 迁入）。
        /// 随机引擎统一到 ThreadLocalRandom 单一引擎。
        /// </summary>
        /// <remarks>
        /// Gets a random double between 0.0 and 1.0.
        /// </remarks>
        /// <returns>返回一个大于等于 0.0 并且小于 1.0 的双精度浮点数 / A double-precision floating point number</returns>
        [UnityEngine.Scripting.Preserve]
        public static double NextDouble()
        {
            return ThreadLocalRandom.Current.NextDouble();
        }

        /// <summary>
        /// 用随机数填充指定字节数组的元素（C27 迁入）。
        /// 随机引擎统一到 ThreadLocalRandom 单一引擎。
        /// </summary>
        /// <remarks>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </remarks>
        /// <param name="buffer">包含随机数的字节数组 / The byte array to contain random numbers</param>
        [UnityEngine.Scripting.Preserve]
        public static void NextBytes(byte[] buffer)
        {
            ThreadLocalRandom.Current.NextBytes(buffer);
        }
    }
}