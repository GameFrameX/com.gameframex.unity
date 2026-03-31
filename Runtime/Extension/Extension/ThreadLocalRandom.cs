using System;
using System.Threading;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 线程私有random对象
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class ThreadLocalRandom
    {
        private static int _seed = Environment.TickCount;
        private static int? _customSeed;

        private static readonly ThreadLocal<Random> _rng = new ThreadLocal<Random>(() => new Random(_customSeed ?? Interlocked.Increment(ref _seed)));

        /// <summary>
        /// The current random number seed available to this thread
        /// </summary>
        public static Random Current
        {
            get { return _rng.Value; }
        }

        /// <summary>
        /// 设置随机种子（用于测试或可重现的随机序列）
        /// </summary>
        /// <param name="seed">随机种子值</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetSeed(int seed)
        {
            _customSeed = seed;
        }

        /// <summary>
        /// 获取Int64范围内的随机数
        /// </summary>
        /// <returns>返回一个随机的Int64值</returns>
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
        /// <returns>返回一个随机的UInt64值</returns>
        [UnityEngine.Scripting.Preserve]
        public static ulong NextUInt64()
        {
            var bytes = new byte[8];
            Current.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }
    }
}