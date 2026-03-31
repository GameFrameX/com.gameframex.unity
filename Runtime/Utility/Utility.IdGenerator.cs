using System;
using System.Threading;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// ID 生成器相关的实用函数。
        /// </summary>
        /// <remarks>
        /// ID generator related utility functions.
        /// </remarks>
        /// <summary>
        /// ID 生成器相关的实用函数。
        /// </summary>
        /// <remarks>
        /// ID generator related utility functions.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static class IdGenerator
        {
            /// <summary>
            /// 全局UTC起始时间，用作计数器的基准时间点。
            /// 设置为2020年1月1日0时0分0秒(UTC)。
            /// </summary>
            /// <remarks>
            /// Global UTC start time, used as the reference point for the counter.
            /// Set to January 1, 2020, 00:00:00 (UTC).
            /// </remarks>
            public static readonly DateTime UtcTimeStart = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // 共享计数器
            private static long _counter = (long)(DateTime.UtcNow - UtcTimeStart).TotalSeconds;
            private static int _intCounter = (int)(DateTime.UtcNow - UtcTimeStart).TotalSeconds;

            /// <summary>
            /// 使用 Interlocked.Increment 生成唯一的长整型ID。
            /// </summary>
            /// <remarks>
            /// Generates a unique long integer ID using Interlocked.Increment.
            /// </remarks>
            /// <returns>返回一个唯一的长整型ID / A unique long integer ID</returns>
            [UnityEngine.Scripting.Preserve]
            public static long GetNextUniqueId()
            {
                // 原子性地递增值
                return Interlocked.Increment(ref _counter);
            }

            /// <summary>
            /// 使用 Interlocked.Increment 生成唯一的整型ID。
            /// </summary>
            /// <remarks>
            /// Generates a unique integer ID using Interlocked.Increment.
            /// </remarks>
            /// <returns>返回一个唯一的整型ID / A unique integer ID</returns>
            [UnityEngine.Scripting.Preserve]
            public static int GetNextUniqueIntId()
            {
                return Interlocked.Increment(ref _intCounter);
            }
        }
    }
}