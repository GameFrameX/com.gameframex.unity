using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 热更新 AOT 元数据保留分析器，负责扫描配置的类型与成员并结合白名单/黑名单规则生成保留描述符集合。
    /// </summary>
    /// <remarks>
    /// Hotfix AOT metadata preserve analyzer. Scans configured types and members,
    /// applies whitelist/blacklist rules, and produces a set of preserve descriptors.
    /// </remarks>
    internal static class AotPreserveAnalyzer
    {
        private const BindingFlags MemberFlags =
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.DeclaredOnly;

        /// <summary>
        /// 从默认设置提供者读取配置并执行保留分析。
        /// </summary>
        /// <remarks>
        /// Reads configuration from the default settings provider and performs the preserve analysis.
        /// </remarks>
        /// <returns>分析结果 / The analysis result</returns>
        public static AotPreserveAnalysis Analyze()
        {
            var provider = AotPreserveSettingsProviderResolver.Resolve();
            return Analyze(
                provider.PreserveTypes,
                provider.WrapTypes,
                provider.PreserveMembers,
                provider.PreserveGenericTypes,
                provider.TypeWhiteList,
                provider.TypeBlackList,
                provider.MemberWhiteList,
                provider.MemberBlackList);
        }

        /// <summary>
        /// 使用简化的过滤参数集合执行保留分析（仅指定成员黑名单，其余过滤条件为空）。
        /// </summary>
        /// <remarks>
        /// Performs the preserve analysis with a reduced filter set (only a member blacklist;
        /// other filter lists default to empty).
        /// </remarks>
        /// <param name="preserveTypes">需要保留的类型集合 / The set of types to preserve</param>
        /// <param name="wrapTypes">需要保留且包含成员的类型集合 / The set of types to preserve including their members</param>
        /// <param name="preserveMembers">需要保留的成员描述集合 / The collection of member specifications to preserve</param>
        /// <param name="preserveGenericTypes">需要保留的泛型类型集合 / The set of generic types to preserve</param>
        /// <param name="memberBlacklist">成员黑名单模式集合 / The collection of member blacklist patterns</param>
        /// <returns>分析结果 / The analysis result</returns>
        internal static AotPreserveAnalysis Analyze(
            Type[] preserveTypes,
            Type[] wrapTypes,
            string[] preserveMembers,
            Type[] preserveGenericTypes,
            string[] memberBlacklist)
        {
            return Analyze(
                preserveTypes,
                wrapTypes,
                preserveMembers,
                preserveGenericTypes,
                Array.Empty<string>(),
                Array.Empty<string>(),
                Array.Empty<string>(),
                memberBlacklist);
        }

        /// <summary>
        /// 使用完整的过滤参数集合执行保留分析，依次处理保留类型、保留类型(含成员)、保留成员与保留泛型类型。
        /// </summary>
        /// <remarks>
        /// Performs the preserve analysis with the full set of filters. Processes preserve types,
        /// wrap types (with members), preserve members, and preserve generic types in order.
        /// </remarks>
        /// <param name="preserveTypes">需要保留的类型集合 / The set of types to preserve</param>
        /// <param name="wrapTypes">需要保留且包含成员的类型集合 / The set of types to preserve including their members</param>
        /// <param name="preserveMembers">需要保留的成员描述集合 / The collection of member specifications to preserve</param>
        /// <param name="preserveGenericTypes">需要保留的泛型类型集合 / The set of generic types to preserve</param>
        /// <param name="typeWhiteList">类型白名单模式集合 / The collection of type whitelist patterns</param>
        /// <param name="typeBlackList">类型黑名单模式集合 / The collection of type blacklist patterns</param>
        /// <param name="memberWhiteList">成员白名单模式集合 / The collection of member whitelist patterns</param>
        /// <param name="memberBlackList">成员黑名单模式集合 / The collection of member blacklist patterns</param>
        /// <returns>分析结果 / The analysis result</returns>
        internal static AotPreserveAnalysis Analyze(
            Type[] preserveTypes,
            Type[] wrapTypes,
            string[] preserveMembers,
            Type[] preserveGenericTypes,
            string[] typeWhiteList,
            string[] typeBlackList,
            string[] memberWhiteList,
            string[] memberBlackList)
        {
            var analysis = new AotPreserveAnalysis();
            var filters = new PreserveFilters(typeWhiteList, typeBlackList, memberWhiteList, memberBlackList);

            foreach (var type in SafeTypes(preserveTypes))
            {
                PreserveType(analysis, type, false, filters);
            }

            foreach (var type in SafeTypes(wrapTypes))
            {
                PreserveType(analysis, type, true, filters);
            }

            foreach (var memberSpec in SafeStrings(preserveMembers))
            {
                PreserveMember(analysis, memberSpec, filters);
            }

            foreach (var type in SafeTypes(preserveGenericTypes))
            {
                PreserveGenericType(analysis, type, filters);
            }

            return analysis;
        }

        private static IEnumerable<Type> SafeTypes(Type[] types)
        {
            return types ?? Array.Empty<Type>();
        }

        private static IEnumerable<string> SafeStrings(string[] values)
        {
            return values ?? Array.Empty<string>();
        }

        /// <summary>
        /// 将指定类型加入保留结果，并根据参数决定是否扫描并保留其字段、方法与构造函数。
        /// </summary>
        /// <remarks>
        /// Adds the specified type to the preserve result and, depending on the flag,
        /// scans and preserves its fields, methods, and constructors.
        /// </remarks>
        /// <param name="analysis">分析结果实例 / The analysis result instance</param>
        /// <param name="type">要保留的类型 / The type to preserve</param>
        /// <param name="includeMembers">是否同时保留类型的成员 / Whether to also preserve members of the type</param>
        /// <param name="filters">过滤规则 / The filter rules</param>
        private static void PreserveType(AotPreserveAnalysis analysis, Type type, bool includeMembers, PreserveFilters filters)
        {
            if (type == null)
            {
                analysis.Errors.Add("Preserve type entry is null.");
                return;
            }

            if (!ValidateType(analysis, type, "type", filters))
            {
                return;
            }

            var assembly = analysis.GetAssembly(type);
            var descriptor = assembly.GetTypeDescriptor(type);
            descriptor.PreserveTypeOnly = true;
            analysis.ReferenceTypeNames.Add(AotPreserveNameUtility.GetStableTypeName(type));
            analysis.PreservedTypes.Add(AotPreserveNameUtility.GetStableTypeName(type) + (includeMembers ? " (members)" : string.Empty));

            if (!includeMembers)
            {
                return;
            }

            foreach (var field in type.GetFields(MemberFlags))
            {
                if (ShouldExclude(analysis, field, filters))
                {
                    continue;
                }

                descriptor.Fields.Add(field.Name);
                analysis.ReferenceMembers.Add(field);
                analysis.PreservedMembers.Add(AotPreserveNameUtility.GetMemberDisplayName(field));
            }

            foreach (var method in type.GetMethods(MemberFlags))
            {
                if (ShouldExclude(analysis, method, filters))
                {
                    continue;
                }

                descriptor.Methods.Add(method.Name);
                analysis.ReferenceMembers.Add(method);
                analysis.PreservedMembers.Add(AotPreserveNameUtility.GetMemberDisplayName(method));
            }

            foreach (var constructor in type.GetConstructors(MemberFlags))
            {
                if (ShouldExclude(analysis, constructor, filters))
                {
                    continue;
                }

                descriptor.Methods.Add(constructor.Name);
                analysis.ReferenceMembers.Add(constructor);
                analysis.PreservedMembers.Add(AotPreserveNameUtility.GetMemberDisplayName(constructor));
            }
        }

        /// <summary>
        /// 将泛型类型加入保留结果，并递归保留其非泛型参数的实际类型参数。
        /// </summary>
        /// <remarks>
        /// Adds the generic type to the preserve result and recursively preserves its
        /// concrete type arguments that are not generic parameters.
        /// </remarks>
        /// <param name="analysis">分析结果实例 / The analysis result instance</param>
        /// <param name="type">要保留的泛型类型 / The generic type to preserve</param>
        /// <param name="filters">过滤规则 / The filter rules</param>
        private static void PreserveGenericType(AotPreserveAnalysis analysis, Type type, PreserveFilters filters)
        {
            if (type == null)
            {
                analysis.Errors.Add("Preserve generic type entry is null.");
                return;
            }

            if (!ValidateType(analysis, type, "generic type", filters))
            {
                return;
            }

            analysis.ReferenceTypeNames.Add(AotPreserveNameUtility.GetStableTypeName(type));
            analysis.PreservedGenericTypes.Add(AotPreserveNameUtility.GetStableTypeName(type));

            if (type.IsGenericType)
            {
                foreach (var argument in type.GetGenericArguments())
                {
                    if (!argument.IsGenericParameter && ValidateType(analysis, argument, "generic argument", filters))
                    {
                        PreserveType(analysis, argument, false, filters);
                    }
                }
            }
        }

        /// <summary>
        /// 解析形如"Namespace.Type:MemberName"的成员描述字符串，并将匹配的字段或方法加入保留结果。
        /// </summary>
        /// <remarks>
        /// Parses a member specification of the form "Namespace.Type:MemberName" and adds
        /// any matching fields or methods to the preserve result.
        /// </remarks>
        /// <param name="analysis">分析结果实例 / The analysis result instance</param>
        /// <param name="memberSpec">成员描述字符串 / The member specification string</param>
        /// <param name="filters">过滤规则 / The filter rules</param>
        private static void PreserveMember(AotPreserveAnalysis analysis, string memberSpec, PreserveFilters filters)
        {
            if (string.IsNullOrWhiteSpace(memberSpec))
            {
                analysis.Errors.Add("Preserve member entry is empty.");
                return;
            }

            var colonIndex = memberSpec.LastIndexOf(':');
            if (colonIndex <= 0 || colonIndex == memberSpec.Length - 1)
            {
                analysis.Errors.Add("Invalid member entry '" + memberSpec + "'. Expected 'Namespace.Type:MemberName'.");
                return;
            }

            var typeName = memberSpec.Substring(0, colonIndex).Trim();
            var memberName = memberSpec.Substring(colonIndex + 1).Trim();
            var type = ResolveType(typeName);
            if (type == null)
            {
                analysis.Errors.Add("Cannot resolve type '" + typeName + "' from member entry '" + memberSpec + "'.");
                return;
            }

            if (!ValidateType(analysis, type, "member declaring type", filters))
            {
                return;
            }

            var resolvedMembers = ResolveMembers(analysis, type, memberName, memberSpec, filters);
            if (resolvedMembers.Count == 0)
            {
                return;
            }

            var descriptor = analysis.GetAssembly(type).GetTypeDescriptor(type);
            descriptor.PreserveTypeOnly = true;
            analysis.ReferenceTypeNames.Add(AotPreserveNameUtility.GetStableTypeName(type));

            foreach (var resolvedMember in resolvedMembers)
            {
                if (ShouldExclude(analysis, resolvedMember.Source, filters))
                {
                    continue;
                }

                if (resolvedMember.Kind == AotPreserveMemberKind.Field)
                {
                    descriptor.Fields.Add(resolvedMember.Name);
                }
                else
                {
                    descriptor.Methods.Add(resolvedMember.Name);
                }

                analysis.ReferenceMembers.Add(resolvedMember.Source);
                analysis.PreservedMembers.Add(AotPreserveNameUtility.GetStableTypeName(resolvedMember.DeclaringType) + ":" + resolvedMember.Name);
            }
        }

        /// <summary>
        /// 在指定类型上解析成员名称，支持通配符"*"、属性、事件、字段以及带参数签名的方法重载。
        /// </summary>
        /// <remarks>
        /// Resolves a member name on the given type, supporting the wildcard "*", properties,
        /// events, fields, and method overloads with explicit parameter signatures.
        /// </remarks>
        /// <param name="analysis">分析结果实例 / The analysis result instance</param>
        /// <param name="type">声明成员的类型 / The type that declares the member</param>
        /// <param name="memberName">成员名称（可包含参数签名） / The member name (may include a parameter signature)</param>
        /// <param name="originalSpec">原始描述字符串，用于错误信息 / The original specification string used for error messages</param>
        /// <param name="filters">过滤规则 / The filter rules</param>
        /// <returns>解析出的成员描述符列表 / The list of resolved member descriptors</returns>
        private static List<AotPreserveResolvedMember> ResolveMembers(
            AotPreserveAnalysis analysis,
            Type type,
            string memberName,
            string originalSpec,
            PreserveFilters filters)
        {
            var result = new List<AotPreserveResolvedMember>();

            if (memberName == "*")
            {
                PreserveType(analysis, type, true, filters);
                return result;
            }

            var parsedName = memberName;
            string[] parameterNames = null;
            var openParen = memberName.IndexOf('(');
            if (openParen >= 0)
            {
                var closeParen = memberName.LastIndexOf(')');
                if (closeParen < openParen)
                {
                    analysis.Errors.Add("Invalid method signature '" + originalSpec + "'.");
                    return result;
                }

                parsedName = memberName.Substring(0, openParen);
                var parametersText = memberName.Substring(openParen + 1, closeParen - openParen - 1);
                parameterNames = string.IsNullOrWhiteSpace(parametersText)
                    ? Array.Empty<string>()
                    : parametersText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()).ToArray();
            }

            var property = type.GetProperty(parsedName, MemberFlags);
            if (property != null && parameterNames == null)
            {
                var getter = property.GetGetMethod(true);
                if (getter != null)
                {
                    result.Add(new AotPreserveResolvedMember(type, AotPreserveMemberKind.Method, getter.Name, getter));
                }

                var setter = property.GetSetMethod(true);
                if (setter != null)
                {
                    result.Add(new AotPreserveResolvedMember(type, AotPreserveMemberKind.Method, setter.Name, setter));
                }

                return result;
            }

            var eventInfo = type.GetEvent(parsedName, MemberFlags);
            if (eventInfo != null && parameterNames == null)
            {
                var addMethod = eventInfo.GetAddMethod(true);
                if (addMethod != null)
                {
                    result.Add(new AotPreserveResolvedMember(type, AotPreserveMemberKind.Method, addMethod.Name, addMethod));
                }

                var removeMethod = eventInfo.GetRemoveMethod(true);
                if (removeMethod != null)
                {
                    result.Add(new AotPreserveResolvedMember(type, AotPreserveMemberKind.Method, removeMethod.Name, removeMethod));
                }

                return result;
            }

            var field = type.GetField(parsedName, MemberFlags);
            if (field != null && parameterNames == null)
            {
                result.Add(new AotPreserveResolvedMember(type, AotPreserveMemberKind.Field, field.Name, field));
                return result;
            }

            var methods = type.GetMethods(MemberFlags).Where(method => method.Name == parsedName).ToArray();
            if (parameterNames != null)
            {
                methods = methods.Where(method => ParameterSignatureMatches(method, parameterNames)).ToArray();
            }

            if (methods.Length == 1)
            {
                result.Add(new AotPreserveResolvedMember(type, AotPreserveMemberKind.Method, methods[0].Name, methods[0]));
                return result;
            }

            if (methods.Length > 1)
            {
                analysis.Errors.Add(
                    "Member entry '" + originalSpec + "' matches multiple overloads. Use an explicit signature such as '" +
                    type.FullName + ":" + parsedName + AotPreserveNameUtility.GetParameterSignature(methods[0]) + "'.");
                return result;
            }

            analysis.Errors.Add("Cannot resolve member '" + originalSpec + "'.");
            return result;
        }

        /// <summary>
        /// 判断方法的参数签名是否与指定的参数类型名称序列匹配（支持全名、短名与可读名）。
        /// </summary>
        /// <remarks>
        /// Determines whether the method's parameter signature matches the specified sequence of
        /// parameter type names (supports full name, short name, and readable name).
        /// </remarks>
        /// <param name="method">待比较的方法 / The method to compare</param>
        /// <param name="parameterNames">期望的参数类型名称序列 / The expected sequence of parameter type names</param>
        /// <returns>匹配返回 true，否则返回 false / true if matched; otherwise false</returns>
        private static bool ParameterSignatureMatches(MethodBase method, string[] parameterNames)
        {
            var parameters = method.GetParameters();
            if (parameters.Length != parameterNames.Length)
            {
                return false;
            }

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;
                var expected = parameterNames[i];
                if (string.Equals(parameterType.FullName, expected, StringComparison.Ordinal) ||
                    string.Equals(parameterType.Name, expected, StringComparison.Ordinal) ||
                    string.Equals(AotPreserveNameUtility.GetReadableTypeName(parameterType), expected, StringComparison.Ordinal))
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// 通过名称解析类型，先尝试 <see cref="Type.GetType(string,bool)"/>，再遍历当前应用程序域的所有程序集。
        /// </summary>
        /// <remarks>
        /// Resolves a type by name. First tries <see cref="Type.GetType(string,bool)"/>, then iterates
        /// over all assemblies in the current application domain.
        /// </remarks>
        /// <param name="typeName">类型全名 / The full type name</param>
        /// <returns>解析到的类型实例；若未找到则为 null / The resolved type, or null if not found</returns>
        private static Type ResolveType(string typeName)
        {
            var type = Type.GetType(typeName, false);
            if (type != null)
            {
                return type;
            }

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName, false);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }

        /// <summary>
        /// 校验类型是否允许保留，包括热更新类型检测、黑名单过滤以及白名单命中检查。
        /// </summary>
        /// <remarks>
        /// Validates whether a type is allowed to be preserved, including hotfix type detection,
        /// blacklist filtering, and whitelist hit checks.
        /// </remarks>
        /// <param name="analysis">分析结果实例 / The analysis result instance</param>
        /// <param name="type">待校验的类型 / The type to validate</param>
        /// <param name="source">来源说明，用于错误信息 / The source label used in error messages</param>
        /// <param name="filters">过滤规则 / The filter rules</param>
        /// <returns>允许保留返回 true，否则返回 false / true if the type is allowed; otherwise false</returns>
        private static bool ValidateType(AotPreserveAnalysis analysis, Type type, string source, PreserveFilters filters)
        {
            var stableName = AotPreserveNameUtility.GetStableTypeName(type);
            var fullName = type.FullName ?? type.Name;
            if (AotPreserveNameUtility.IsHotfixType(type))
            {
                analysis.Errors.Add("Hotfix type is not allowed in AOT preserve " + source + ": " + stableName);
                return false;
            }

            if (MatchesAny(stableName, filters.TypeBlackList) ||
                MatchesAny(fullName, filters.TypeBlackList))
            {
                analysis.ExcludedItems.Add("Type excluded by blacklist: " + stableName);
                return false;
            }

            if (HasPatterns(filters.TypeWhiteList) &&
                !MatchesAny(stableName, filters.TypeWhiteList) &&
                !MatchesAny(fullName, filters.TypeWhiteList))
            {
                analysis.ExcludedItems.Add("Type excluded by whitelist: " + stableName);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断成员是否应被排除，依据 ObsoleteAttribute、黑名单与白名单规则。
        /// </summary>
        /// <remarks>
        /// Determines whether a member should be excluded based on the ObsoleteAttribute,
        /// the blacklist, and the whitelist rules.
        /// </remarks>
        /// <param name="analysis">分析结果实例 / The analysis result instance</param>
        /// <param name="member">待检查的成员 / The member to inspect</param>
        /// <param name="filters">过滤规则 / The filter rules</param>
        /// <returns>应被排除返回 true，否则返回 false / true if the member should be excluded; otherwise false</returns>
        private static bool ShouldExclude(AotPreserveAnalysis analysis, MemberInfo member, PreserveFilters filters)
        {
            var displayName = AotPreserveNameUtility.GetMemberDisplayName(member);
            if (AotPreserveNameUtility.IsObsolete(member))
            {
                analysis.ExcludedItems.Add("Member excluded by ObsoleteAttribute: " + displayName);
                return true;
            }

            if (MatchesAny(displayName, filters.MemberBlackList) ||
                MatchesAny(member.Name, filters.MemberBlackList))
            {
                analysis.ExcludedItems.Add("Member excluded by blacklist: " + displayName);
                return true;
            }

            if (HasPatterns(filters.MemberWhiteList) &&
                !MatchesAny(displayName, filters.MemberWhiteList) &&
                !MatchesAny(member.Name, filters.MemberWhiteList))
            {
                analysis.ExcludedItems.Add("Member excluded by whitelist: " + displayName);
                return true;
            }

            return false;
        }

        private static bool HasPatterns(string[] patterns)
        {
            return SafeStrings(patterns).Any(pattern => !string.IsNullOrWhiteSpace(pattern));
        }

        private static bool MatchesAny(string value, string[] patterns)
        {
            foreach (var pattern in SafeStrings(patterns))
            {
                if (AotPreserveNameUtility.IsMatch(value, pattern))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 保留分析的过滤规则集合，封装类型与成员的白名单及黑名单。
        /// </summary>
        /// <remarks>
        /// A collection of filter rules used by the preserve analysis, encapsulating
        /// whitelist and blacklist patterns for both types and members.
        /// </remarks>
        private sealed class PreserveFilters
        {
            /// <summary>
            /// 使用指定的白名单与黑名单初始化过滤规则实例。
            /// </summary>
            /// <remarks>
            /// Initializes a new instance of the filter rules with the specified whitelist and blacklist.
            /// </remarks>
            /// <param name="typeWhiteList">类型白名单模式集合 / The collection of type whitelist patterns</param>
            /// <param name="typeBlackList">类型黑名单模式集合 / The collection of type blacklist patterns</param>
            /// <param name="memberWhiteList">成员白名单模式集合 / The collection of member whitelist patterns</param>
            /// <param name="memberBlackList">成员黑名单模式集合 / The collection of member blacklist patterns</param>
            public PreserveFilters(string[] typeWhiteList, string[] typeBlackList, string[] memberWhiteList, string[] memberBlackList)
            {
                TypeWhiteList = typeWhiteList ?? Array.Empty<string>();
                TypeBlackList = typeBlackList ?? Array.Empty<string>();
                MemberWhiteList = memberWhiteList ?? Array.Empty<string>();
                MemberBlackList = memberBlackList ?? Array.Empty<string>();
            }

            /// <value>类型白名单模式集合 / The collection of type whitelist patterns</value>
            public string[] TypeWhiteList { get; }

            /// <value>类型黑名单模式集合 / The collection of type blacklist patterns</value>
            public string[] TypeBlackList { get; }

            /// <value>成员白名单模式集合 / The collection of member whitelist patterns</value>
            public string[] MemberWhiteList { get; }

            /// <value>成员黑名单模式集合 / The collection of member blacklist patterns</value>
            public string[] MemberBlackList { get; }
        }
    }
}
