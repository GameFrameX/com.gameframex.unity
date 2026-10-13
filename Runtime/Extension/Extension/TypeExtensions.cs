using System;
using System.Linq;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 扩展方法，用于检查当前类型是否实现了指定的接口。
    /// </summary>
    /// <remarks>
    /// Provides extension methods for checking whether a type implements a specified interface.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class TypeExtensions
    {
        /// <summary>
        /// 判断类型是否实现了指定的接口。
        /// 此方法用于检查一个具体类型是否实现了目标接口。
        /// </summary>
        /// <remarks>
        /// Determines whether the type implements the specified interface.
        /// This method checks whether a concrete type implements the target interface.
        /// </remarks>
        /// <param name="self">要判断的类型。必须是非空的具体类型。 / The type to check. Must be a non-null concrete type.</param>
        /// <param name="target">要判断的接口类型。必须是非空的接口类型。 / The interface type to check for. Must be a non-null interface type.</param>
        /// <param name="directOnly">是否只检查直接实现的接口。
        /// 当设置为true时，只检查直接实现的接口；
        /// 当设置为false时，同时检查继承链上的所有接口。 / Whether to check only directly implemented interfaces.
        /// When set to true, only directly implemented interfaces are checked;
        /// when set to false, all interfaces in the inheritance chain are checked as well.</param>
        /// <returns>
        /// 满足以下所有条件时返回true:
        /// 1. self和target参数都不为null
        /// 2. target是接口类型
        /// 3. self不是接口类型或抽象类型
        /// 4. self实现了target接口（根据directOnly参数决定检查范围）
        /// 否则返回false
        /// Returns true when all of the following conditions are met:
        /// 1. Both self and target are not null
        /// 2. target is an interface type
        /// 3. self is neither an interface nor an abstract type
        /// 4. self implements the target interface (scope depends on directOnly)
        /// Otherwise returns false
        /// </returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsImplWithInterface(this Type self, Type target, bool directOnly = false)
        {
            // 参数有效性检查
            if (target == null || self == null)
            {
                return false;
            }

            // 确保target是接口类型
            if (!target.IsInterface)
            {
                return false;
            }

            // 检查是否是接口类型或抽象类型
            if (self.IsInterface || self.IsAbstract)
            {
                return false;
            }

            if (directOnly)
            {
                // 只检查直接声明的接口（排除通过其他接口继承的）
                var allInterfaces = self.GetInterfaces();
                return allInterfaces.Contains(target) && !allInterfaces.Any(i => i != target && i.GetInterfaces().Contains(target));
            }

            // 检查所有实现的接口（包括继承的接口）
            return self.GetInterfaces().Contains(target);
        }
    }
}