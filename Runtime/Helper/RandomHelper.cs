using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 随机数帮助类
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class RandomHelper
    {
        /// <summary>
        /// 设置随机种子
        /// </summary>
        /// <param name="seed">随机种子值</param>
        [UnityEngine.Scripting.Preserve]
        public static void SetSeed(int seed)
        {
            ThreadLocalRandom.SetSeed(seed);
        }

        /// <summary>
        /// 获取UInt64范围内的随机数
        /// </summary>
        /// <returns>返回一个随机的UInt64值</returns>
        [UnityEngine.Scripting.Preserve]
        public static ulong NextUInt64()
        {
            return ThreadLocalRandom.NextUInt64();
        }

        /// <summary>
        /// 获取Int64范围内的随机数
        /// </summary>
        /// <returns>返回一个随机的Int64值</returns>
        [UnityEngine.Scripting.Preserve]
        public static long NextInt64()
        {
            return ThreadLocalRandom.NextInt64();
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
            return ThreadLocalRandom.Current.Next(lower, upper);
        }

        /// <summary>
        /// 获取0与1之间的随机数
        /// </summary>
        /// <returns>返回一个在0到1之间的随机浮点数</returns>
        [UnityEngine.Scripting.Preserve]
        public static float Next()
        {
            return ThreadLocalRandom.Current.Next(0, 100_000) / 100_000f;
        }
    }
}