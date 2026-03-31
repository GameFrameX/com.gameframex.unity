using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架异常静态方法。
    /// </summary>
    /// <remarks>
    /// Game framework guard static methods.
    /// </remarks>
    public static class GameFrameworkGuard
    {
        /// <summary>
        /// 确保指定的字符串不为 null 或空。
        /// </summary>
        /// <remarks>
        /// Ensures that the specified string is not null or empty.
        /// </remarks>
        /// <param name="value">要检查的值 / The value to check</param>
        /// <param name="name">值的名称 / The name of the value</param>
        /// <exception cref="ArgumentNullException">当值为 null 或空时引发 / Thrown when the value is null or empty</exception>
        [UnityEngine.Scripting.Preserve]
        public static void NotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(name, " can not be null.");
            }
        }


        /// <summary>
        /// 确保指定的值不为 null。
        /// </summary>
        /// <remarks>
        /// Ensures that the specified value is not null.
        /// </remarks>
        /// <typeparam name="T">值的类型 / The type of the value</typeparam>
        /// <param name="value">要检查的值 / The value to check</param>
        /// <param name="name">值的名称 / The name of the value</param>
        /// <exception cref="ArgumentNullException">当值为 null 时引发 / Thrown when the value is null</exception>
        [UnityEngine.Scripting.Preserve]
        public static void NotNull<T>(T value, string name) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name, " can not be null.");
            }
        }

        /// <summary>
        /// 检查值是否在指定范围内，如果不在范围内则抛出 <see cref="ArgumentOutOfRangeException"/> 异常。
        /// </summary>
        /// <remarks>
        /// Checks if the value is within the specified range, throws <see cref="ArgumentOutOfRangeException"/> if not.
        /// </remarks>
        /// <param name="value">要检查的值 / The value to check</param>
        /// <param name="min">允许的最小值 / The minimum allowed value</param>
        /// <param name="max">允许的最大值 / The maximum allowed value</param>
        /// <param name="name">值的名称 / The name of the value</param>
        /// <exception cref="ArgumentOutOfRangeException">当值不在指定范围内时抛出 / Thrown when the value is not within the specified range</exception>
        [UnityEngine.Scripting.Preserve]
        public static void NotRange(int value, int min, int max, string name)
        {
            if (value > max || value < min)
            {
                throw new ArgumentOutOfRangeException(name, "value must between " + min + " and " + max);
            }
        }
    }
}