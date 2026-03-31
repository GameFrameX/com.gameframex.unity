namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// Object 对象工具类。
        /// </summary>
        /// <remarks>
        /// Object utility class.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static class Object
        {
            /// <summary>
            /// 交换两个对象的值。
            /// </summary>
            /// <remarks>
            /// Swaps the values of two objects.
            /// </remarks>
            /// <param name="t1">第一个对象的引用 / Reference to the first object</param>
            /// <param name="t2">第二个对象的引用 / Reference to the second object</param>
            /// <typeparam name="T">对象的类型 / The type of the objects</typeparam>
            [UnityEngine.Scripting.Preserve]
            public static void Swap<T>(ref T t1, ref T t2)
            {
                (t1, t2) = (t2, t1);
            }
        }
    }
}