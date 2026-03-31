using System;
using System.Threading;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 线程私有random对象
    /// </summary>
    /// <remarks>
    /// Thread-local random number generator that provides thread-safe random number generation.
    /// Each thread gets its own Random instance to avoid contention and ensure thread safety.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class ThreadLocalRandom
    {
        private static int _seed = Environment.TickCount;
        private static int? _customSeed;

        private static readonly ThreadLocal<Random> _rng = new ThreadLocal<Random>(() => new Random(_customSeed ?? Interlocked.Increment(ref _seed)));

        /// <summary>
        /// The current random number seed available to this thread
        /// </summary>
        /// <remarks>
        /// 获取当前线程可用的随机数生成器实例。
        /// Gets the Random instance available to the current thread.
        /// </remarks>
        public static Random Current
        {
            get { return _rng.Value; }
        }

        /// <summary>
        /// 设置随机种子（用于测试或可重现的随机序列）
        /// </summary>
        /// <remarks>
        /// Sets a custom seed for the random number generator, useful for testing or reproducible random sequences.
        /// </remarks>
        /// <param name="seed">随机种子值 / The seed value for the random number generator</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetSeed(int seed)
        {
            _customSeed = seed;
        }

        /// <summary>
        /// 获取Int64范围内的随机数
        /// </summary>
        /// <remarks>
        /// Generates a random number within the Int64 range.
        /// </remarks>
        /// <returns>返回一个随机的Int64值 / A random Int64 value</returns>
        [UnityEngine.Scripting.Preserve]
        public static long NextInt64()
        {
            var bytes = new byte[8];
            Current.NextBytes(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// 获取UInt64范围内的随机数
        /// </summary>
        /// <remarks>
        /// Generates a random number within the UInt64 range.
        /// </remarks>
        /// <returns>返回一个随机的UInt64值 / A random UInt64 value</returns>
        [UnityEngine.Scripting.Preserve]
        public static ulong NextUInt64()
        {
            var bytes = new byte[8];
            Current.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }
    }
}