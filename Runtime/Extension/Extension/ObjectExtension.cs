using System;
using UnityEngine.Scripting; // Added this line for the Preserve attribute

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 对象扩展函数
    /// </summary>
    [Preserve]
    public static class ObjectExtension
    {
        /// <summary>
        /// 检查对象是否为null
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        [Preserve]
        public static bool IsNull(this object self)
        {
            return self == null;
        }

        /// <summary>
        /// 检查对象是否不为null
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        [Preserve]
        public static bool IsNotNull(this object self)
        {
            return !self.IsNull();
        }

        /// <summary>
        /// 检查对象是否为null,当为null时抛出异常
        /// </summary>
        /// <param name="self">对象值</param>
        /// <param name="name">异常信息</param>
        /// <exception cref="ArgumentNullException">参数为空的异常</exception>
        [Preserve]
        public static void CheckNull(this object self, string name)
        {
            if (self.IsNull())
            {
                throw new ArgumentNullException(name, " can not be null.");
            }
        }
    }
}