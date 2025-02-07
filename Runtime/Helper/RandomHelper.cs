using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 随机数帮助类
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class RandomHelper
    {
        private static Random _random = new Random((int) DateTime.UtcNow.Ticks);

        /// <summary>
        /// 设置随机种子
        /// </summary>
        /// <param name="seed">随机种子值</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetSeed(int seed)
        {
            _random = new Random(seed);
        }

        /// <summary>
        /// 获取UInt64范围内的随机数
        /// </summary>
        /// <returns>返回一个随机的UInt64值</returns>
        [UnityEngine.Scripting.Preserve]
        public static ulong NextUInt64()
        {
            var bytes = new byte[8];
            _random.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        /// <summary>
        /// 获取Int64范围内的随机数
        /// </summary>
        /// <returns>返回一个随机的Int64值</returns>
        [UnityEngine.Scripting.Preserve]
        public static long NextInt64()
        {
            var bytes = new byte[8];
            _random.NextBytes(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// 获取lower与Upper之间的随机数
        /// </summary>
        /// <param name="lower">下限</param>
        /// <param name="upper">上限</param>
        /// <returns>返回一个在指定范围内的随机整数</returns>
        [UnityEngine.Scripting.Preserve]
        public static int Next(int lower, int upper)
        {
            return _random.Next(lower, upper);
        }

        /// <summary>
        /// 获取0与1之间的随机数
        /// </summary>
        /// <returns>返回一个在0到1之间的随机浮点数</returns>
        [UnityEngine.Scripting.Preserve]
        public static float Next()
        {
            return _random.Next(0, 100_000) / 100_000f;
        }
    }
}