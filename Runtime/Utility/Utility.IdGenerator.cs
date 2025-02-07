using System;
using System.Threading;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        [UnityEngine.Scripting.Preserve]
        public static class IdGenerator
        {
            /// <summary>
            /// 全局UTC起始时间，用作计数器的基准时间点
            /// 设置为2020年1月1日0时0分0秒(UTC)
            /// </summary>
            public static readonly DateTime UtcTimeStart = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // 共享计数器
            private static long _counter = (long)(DateTime.UtcNow - UtcTimeStart).TotalSeconds;
            private static int _intCounter = (int)(DateTime.UtcNow - UtcTimeStart).TotalSeconds;

            /// <summary>
            /// 使用Interlocked.Increment生成唯一ID的方法
            /// </summary>
            /// <returns>返回一个唯一的长整型ID</returns>
            [UnityEngine.Scripting.Preserve]
            public static long GetNextUniqueId()
            {
                // 原子性地递增值
                return Interlocked.Increment(ref _counter);
            }

            /// <summary>
            /// 使用Interlocked.Increment生成唯一ID的方法
            /// </summary>
            /// <returns>返回一个唯一的整型ID</returns>
            [UnityEngine.Scripting.Preserve]
            public static int GetNextUniqueIntId()
            {
                return Interlocked.Increment(ref _intCounter);
            }
        }
    }
}