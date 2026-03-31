namespace GameFrameX.Runtime
{
    /// <summary>
    /// 对象帮助类。
    /// </summary>
    /// <remarks>
    /// Object helper class.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class ObjectHelper
    {
        /// <summary>
        /// 交换两个变量的值。
        /// </summary>
        /// <remarks>
        /// Swaps the values of two variables.
        /// </remarks>
        /// <typeparam name="T">变量类型 / Variable type</typeparam>
        /// <param name="t1">第一个变量 / First variable</param>
        /// <param name="t2">第二个变量 / Second variable</param>
        [UnityEngine.Scripting.Preserve]
        public static void Swap<T>(ref T t1, ref T t2)
        {
            (t1, t2) = (t2, t1);
        }
    }
}