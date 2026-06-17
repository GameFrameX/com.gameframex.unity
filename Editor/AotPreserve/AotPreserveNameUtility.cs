using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 提供热更新 AOT 保留场景下的类型与成员名称处理工具。
    /// </summary>
    /// <remarks>
    /// Provides name processing utilities for types and members in hotfix AOT preserve scenarios.
    /// </remarks>
    internal static class AotPreserveNameUtility
    {
        /// <summary>
        /// 获取类型的稳定字符串表示，包含程序集名，用于去重和排序。
        /// </summary>
        /// <remarks>
        /// Gets the stable string representation of a type, including the assembly name, for deduplication and sorting.
        /// </remarks>
        /// <param name="type">目标类型 / Target type</param>
        /// <returns>类型全名加程序集名的稳定字符串 / Stable string of type full name plus assembly name</returns>
        public static string GetStableTypeName(Type type)
        {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }

        /// <summary>
        /// 获取适用于 link.xml 的类型全名，将嵌套类型分隔符 '+' 替换为 '/'。
        /// </summary>
        /// <remarks>
        /// Gets the type full name suitable for link.xml, replacing the nested type separator '+' with '/'.
        /// </remarks>
        /// <param name="type">目标类型 / Target type</param>
        /// <returns>link.xml 格式的类型全名 / Type full name in link.xml format</returns>
        public static string GetLinkXmlTypeName(Type type)
        {
            return type.FullName.Replace('+', '/');
        }

        /// <summary>
        /// 获取成员的显示名称，由声明类型的稳定名与成员名组合而成。
        /// </summary>
        /// <remarks>
        /// Gets the display name of a member, composed of the declaring type's stable name and the member name.
        /// </remarks>
        /// <param name="member">目标成员 / Target member</param>
        /// <returns>成员显示名称字符串 / Member display name string</returns>
        public static string GetMemberDisplayName(MemberInfo member)
        {
            return GetStableTypeName(member.DeclaringType) + ":" + member.Name;
        }

        /// <summary>
        /// 判断指定类型是否属于热更新类型。
        /// </summary>
        /// <remarks>
        /// Determines whether the specified type belongs to a hotfix type.
        /// </remarks>
        /// <param name="type">目标类型 / Target type</param>
        /// <returns>如果是热更新类型返回 true，否则返回 false / true if the type is a hotfix type; otherwise false</returns>
        public static bool IsHotfixType(Type type)
        {
            var assemblyName = type.Assembly.GetName().Name;
            if (string.Equals(assemblyName, "Unity.HotFix", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return type.FullName != null && type.FullName.StartsWith("Hotfix.", StringComparison.Ordinal);
        }

        /// <summary>
        /// 判断指定成员是否已标记为弃用（Obsolete）。
        /// </summary>
        /// <remarks>
        /// Determines whether the specified member is marked as obsolete.
        /// </remarks>
        /// <param name="member">目标成员 / Target member</param>
        /// <returns>如果成员已弃用返回 true，否则返回 false / true if the member is obsolete; otherwise false</returns>
        public static bool IsObsolete(MemberInfo member)
        {
            return member.GetCustomAttributes(typeof(ObsoleteAttribute), false).Length > 0;
        }

        /// <summary>
        /// 使用通配符模式匹配字符串，支持 '*' 与 '?'。
        /// </summary>
        /// <remarks>
        /// Matches a string against a wildcard pattern supporting '*' and '?'.
        /// </remarks>
        /// <param name="value">待匹配的字符串 / The string to match</param>
        /// <param name="pattern">通配符模式 / The wildcard pattern</param>
        /// <returns>如果匹配成功返回 true，否则返回 false / true if the value matches the pattern; otherwise false</returns>
        public static bool IsMatch(string value, string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return false;
            }

            var regex = "^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
            return Regex.IsMatch(value, regex, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 获取方法参数列表的签名字符串，形如 "(Type1,Type2)"。
        /// </summary>
        /// <remarks>
        /// Gets the parameter signature string of a method, formatted as "(Type1,Type2)".
        /// </remarks>
        /// <param name="method">目标方法 / Target method</param>
        /// <returns>参数签名字符串 / Parameter signature string</returns>
        public static string GetParameterSignature(MethodBase method)
        {
            var parameters = method.GetParameters();
            if (parameters.Length == 0)
            {
                return "()";
            }

            var builder = new StringBuilder();
            builder.Append('(');
            for (var i = 0; i < parameters.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append(',');
                }

                builder.Append(GetReadableTypeName(parameters[i].ParameterType));
            }

            builder.Append(')');
            return builder.ToString();
        }

        /// <summary>
        /// 获取类型的可读名称，将内置类型映射为 C# 关键字，并展开泛型参数。
        /// </summary>
        /// <remarks>
        /// Gets a readable name for a type, mapping built-in types to C# keywords and expanding generic arguments.
        /// </remarks>
        /// <param name="type">目标类型 / Target type</param>
        /// <returns>可读的类型名称字符串 / Readable type name string</returns>
        public static string GetReadableTypeName(Type type)
        {
            if (type == typeof(void))
            {
                return "void";
            }

            if (type == typeof(int))
            {
                return "int";
            }

            if (type == typeof(string))
            {
                return "string";
            }

            if (type == typeof(bool))
            {
                return "bool";
            }

            if (type == typeof(object))
            {
                return "object";
            }

            if (type == typeof(float))
            {
                return "float";
            }

            if (type == typeof(double))
            {
                return "double";
            }

            if (type == typeof(long))
            {
                return "long";
            }

            if (type == typeof(byte))
            {
                return "byte";
            }

            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();
                var tickIndex = genericType.FullName.IndexOf('`');
                var name = tickIndex >= 0 ? genericType.FullName.Substring(0, tickIndex) : genericType.FullName;
                return name + "<" + string.Join(",", type.GetGenericArguments().Select(GetReadableTypeName).ToArray()) + ">";
            }

            return type.FullName ?? type.Name;
        }

        /// <summary>
        /// 获取类型的 C# 形式名称，映射内置类型为关键字，处理数组、泛型参数与泛型定义，并添加 global:: 前缀。
        /// </summary>
        /// <remarks>
        /// Gets the C# form name of a type, mapping built-in types to keywords, handling arrays, generic parameters and generic definitions, and adding a global:: prefix.
        /// </remarks>
        /// <param name="type">目标类型 / Target type</param>
        /// <returns>C# 形式的类型名称字符串 / Type name string in C# form</returns>
        public static string GetCSharpTypeName(Type type)
        {
            if (type == typeof(void))
            {
                return "void";
            }

            if (type == typeof(int))
            {
                return "int";
            }

            if (type == typeof(string))
            {
                return "string";
            }

            if (type == typeof(bool))
            {
                return "bool";
            }

            if (type == typeof(object))
            {
                return "object";
            }

            if (type == typeof(float))
            {
                return "float";
            }

            if (type == typeof(double))
            {
                return "double";
            }

            if (type == typeof(long))
            {
                return "long";
            }

            if (type == typeof(byte))
            {
                return "byte";
            }

            if (type.IsArray)
            {
                return GetCSharpTypeName(type.GetElementType()) + "[]";
            }

            if (type.IsGenericParameter)
            {
                return type.Name;
            }

            if (type.IsGenericType)
            {
                var genericDefinition = type.GetGenericTypeDefinition();
                var tickIndex = genericDefinition.FullName.IndexOf('`');
                var name = tickIndex >= 0 ? genericDefinition.FullName.Substring(0, tickIndex) : genericDefinition.FullName;
                name = "global::" + name.Replace('+', '.');

                if (type.IsGenericTypeDefinition)
                {
                    var commas = new string(',', genericDefinition.GetGenericArguments().Length - 1);
                    return name + "<" + commas + ">";
                }

                return name + "<" + string.Join(", ", type.GetGenericArguments().Select(GetCSharpTypeName).ToArray()) + ">";
            }

            return "global::" + (type.FullName ?? type.Name).Replace('+', '.');
        }

        /// <summary>
        /// 判断指定程序集是否属于框架程序集（mscorlib、netstandard、System 系列、UnityEngine、UnityEditor）。
        /// </summary>
        /// <remarks>
        /// Determines whether the specified assembly is a framework assembly (mscorlib, netstandard, System family, UnityEngine, UnityEditor).
        /// </remarks>
        /// <param name="assemblyName">程序集名称 / Assembly name</param>
        /// <returns>如果是框架程序集返回 true，否则返回 false / true if the assembly is a framework assembly; otherwise false</returns>
        public static bool IsFrameworkAssembly(string assemblyName)
        {
            return string.Equals(assemblyName, "mscorlib", StringComparison.Ordinal) ||
                   string.Equals(assemblyName, "netstandard", StringComparison.Ordinal) ||
                   string.Equals(assemblyName, "System", StringComparison.Ordinal) ||
                   string.Equals(assemblyName, "System.Core", StringComparison.Ordinal) ||
                   assemblyName.StartsWith("UnityEngine", StringComparison.Ordinal) ||
                   assemblyName.StartsWith("UnityEditor", StringComparison.Ordinal);
        }
    }
}
