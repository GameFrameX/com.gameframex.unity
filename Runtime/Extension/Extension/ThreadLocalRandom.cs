﻿using System.Threading;

namespace System
{
    /// <summary>
    /// 线程私有random对象
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class ThreadLocalRandom
    {
        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> _rng = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        /// <summary>
        /// The current random number seed available to this thread
        /// </summary>
        public static Random Current
        {
            get { return _rng.Value; }
        }
    }
}