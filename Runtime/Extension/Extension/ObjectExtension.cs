using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 对象扩展函数
    /// </summary>
    /// <remarks>
    /// Provides extension functions for objects.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class ObjectExtension
    {
        /// <summary>
        /// 检查对象是否为null
        /// </summary>
        /// <remarks>
        /// Checks whether the object is null.
        /// </remarks>
        /// <param name="self">当前对象 / The current object</param>
        /// <returns>是否为null / Whether the object is null</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsNull(this object self)
        {
            return self == null;
        }

        /// <summary>
        /// 检查对象是否不为null
        /// </summary>
        /// <remarks>
        /// Checks whether the object is not null.
        /// </remarks>
        /// <param name="self">当前对象 / The current object</param>
        /// <returns>是否不为null / Whether the object is not null</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsNotNull(this object self)
        {
            return !self.IsNull();
        }

        /// <summary>
        /// 检查对象是否为null,当为null时抛出异常
        /// </summary>
        /// <remarks>
        /// Checks whether the object is null and throws an exception when it is.
        /// </remarks>
        /// <param name="self">对象值 / The object value</param>
        /// <param name="name">异常信息 / The exception message</param>
        /// <exception cref="ArgumentNullException">参数为空的异常 / Thrown when the argument is null</exception>
        [UnityEngine.Scripting.Preserve]
        public static void CheckNull(this object self, string name)
        {
            if (self.IsNull())
            {
                throw new ArgumentNullException(name, " can not be null.");
            }
        }
    }
}