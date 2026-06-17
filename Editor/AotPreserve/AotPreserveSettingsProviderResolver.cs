using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 解析并合并所有热更新 AOT 保留设置提供者。
    /// </summary>
    /// <remarks>
    /// Resolves and merges all hotfix AOT preserve settings providers.
    /// </remarks>
    internal static class AotPreserveSettingsProviderResolver
    {
        /// <summary>
        /// 解析所有可用的设置提供者并返回合并后的提供者。
        /// </summary>
        /// <remarks>
        /// Resolves all available settings providers and returns the merged provider.
        /// </remarks>
        /// <returns>合并后的设置提供者 / The merged settings provider</returns>
        public static IAotPreserveSettingsProvider Resolve()
        {
            var providers = new List<IAotPreserveSettingsProvider>
            {
                new DefaultAotPreserveSettingsProvider(),
            };

            providers.AddRange(AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => SafeGetTypes(assembly))
                .Where(type =>
                    typeof(IAotPreserveSettingsProvider).IsAssignableFrom(type) &&
                    !type.IsAbstract &&
                    !type.IsInterface &&
                    type != typeof(DefaultAotPreserveSettingsProvider) &&
                    type.GetConstructor(Type.EmptyTypes) != null)
                .OrderBy(type => type.FullName, StringComparer.Ordinal)
                .Select(type => (IAotPreserveSettingsProvider)Activator.CreateInstance(type)));

            return Merge(providers);
        }

        /// <summary>
        /// 合并多个设置提供者为一个统一的提供者。
        /// </summary>
        /// <remarks>
        /// Merges multiple settings providers into a single unified provider.
        /// </remarks>
        /// <param name="providers">待合并的设置提供者集合 / The collection of settings providers to merge</param>
        /// <returns>合并后的设置提供者 / The merged settings provider</returns>
        internal static IAotPreserveSettingsProvider Merge(IEnumerable<IAotPreserveSettingsProvider> providers)
        {
            return new MergedAotPreserveSettingsProvider(providers);
        }

        /// <summary>
        /// 安全地获取程序集中的所有类型，遇到加载异常时返回已成功加载的类型。
        /// </summary>
        /// <remarks>
        /// Safely gets all types in the assembly, returning successfully loaded types on load exceptions.
        /// </remarks>
        /// <param name="assembly">目标程序集 / Target assembly</param>
        /// <returns>成功加载的类型集合 / The collection of successfully loaded types</returns>
        private static IEnumerable<Type> SafeGetTypes(System.Reflection.Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(type => type != null);
            }
        }

        /// <summary>
        /// 表示合并多个设置提供者后的统一设置提供者。
        /// </summary>
        /// <remarks>
        /// Represents a unified settings provider that merges multiple settings providers.
        /// </remarks>
        private sealed class MergedAotPreserveSettingsProvider : IAotPreserveSettingsProvider
        {
            /// <summary>
            /// 初始化 <see cref="MergedAotPreserveSettingsProvider"/> 类的新实例。
            /// </summary>
            /// <remarks>
            /// Initializes a new instance of the <see cref="MergedAotPreserveSettingsProvider"/> class.
            /// </remarks>
            /// <param name="providers">待合并的设置提供者集合 / The collection of settings providers to merge</param>
            public MergedAotPreserveSettingsProvider(IEnumerable<IAotPreserveSettingsProvider> providers)
            {
                var providerList = providers.Where(provider => provider != null).ToArray();
                PreserveTypes = MergeTypes(providerList.Select(provider => provider.PreserveTypes));
                WrapTypes = MergeTypes(providerList.Select(provider => provider.WrapTypes));
                PreserveMembers = MergeStrings(providerList.Select(provider => provider.PreserveMembers));
                PreserveGenericTypes = MergeTypes(providerList.Select(provider => provider.PreserveGenericTypes));
                TypeWhiteList = MergeStrings(providerList.Select(provider => provider.TypeWhiteList));
                TypeBlackList = MergeStrings(providerList.Select(provider => provider.TypeBlackList));
                MemberWhiteList = MergeStrings(providerList.Select(provider => provider.MemberWhiteList));
                MemberBlackList = MergeStrings(providerList.Select(provider => provider.MemberBlackList));
            }

            /// <summary>
            /// 获取合并后需要保留的 Unity 类型数组。
            /// </summary>
            /// <remarks>
            /// Gets the merged array of Unity types to preserve.
            /// </remarks>
            public Type[] PreserveTypes { get; }

            /// <summary>
            /// 获取合并后需要生成引用包装代码的类型数组。
            /// </summary>
            /// <remarks>
            /// Gets the merged array of types that require reference wrapper code generation.
            /// </remarks>
            public Type[] WrapTypes { get; }

            /// <summary>
            /// 获取合并后需要保留的成员名称数组。
            /// </summary>
            /// <remarks>
            /// Gets the merged array of member names to preserve.
            /// </remarks>
            public string[] PreserveMembers { get; }

            /// <summary>
            /// 获取合并后需要保留的泛型类型数组。
            /// </summary>
            /// <remarks>
            /// Gets the merged array of generic types to preserve.
            /// </remarks>
            public Type[] PreserveGenericTypes { get; }

            /// <summary>
            /// 获取合并后的类型白名单数组。
            /// </summary>
            /// <remarks>
            /// Gets the merged array of type whitelist patterns.
            /// </remarks>
            public string[] TypeWhiteList { get; }

            /// <summary>
            /// 获取合并后的类型黑名单数组。
            /// </summary>
            /// <remarks>
            /// Gets the merged array of type blacklist patterns.
            /// </remarks>
            public string[] TypeBlackList { get; }

            /// <summary>
            /// 获取合并后的成员白名单数组。
            /// </summary>
            /// <remarks>
            /// Gets the merged array of member whitelist patterns.
            /// </remarks>
            public string[] MemberWhiteList { get; }

            /// <summary>
            /// 获取合并后的成员黑名单数组。
            /// </summary>
            /// <remarks>
            /// Gets the merged array of member blacklist patterns.
            /// </remarks>
            public string[] MemberBlackList { get; }

            /// <summary>
            /// 合并多个类型数组并按稳定类型名去重排序。
            /// </summary>
            /// <remarks>
            /// Merges multiple type arrays and deduplicates by stable type name with ordering.
            /// </remarks>
            /// <param name="values">待合并的类型数组集合 / The collection of type arrays to merge</param>
            /// <returns>合并去重后的类型数组 / The merged and deduplicated type array</returns>
            private static Type[] MergeTypes(IEnumerable<Type[]> values)
            {
                return values
                    .Where(items => items != null)
                    .SelectMany(items => items)
                    .Where(type => type != null)
                    .GroupBy(type => AotPreserveNameUtility.GetStableTypeName(type), StringComparer.Ordinal)
                    .OrderBy(group => group.Key, StringComparer.Ordinal)
                    .Select(group => group.First())
                    .ToArray();
            }

            /// <summary>
            /// 合并多个字符串数组并去重排序。
            /// </summary>
            /// <remarks>
            /// Merges multiple string arrays and deduplicates with ordering.
            /// </summary>
            /// <param name="values">待合并的字符串数组集合 / The collection of string arrays to merge</param>
            /// <returns>合并去重后的字符串数组 / The merged and deduplicated string array</returns>
            private static string[] MergeStrings(IEnumerable<string[]> values)
            {
                return values
                    .Where(items => items != null)
                    .SelectMany(items => items)
                    .Where(value => !string.IsNullOrWhiteSpace(value))
                    .Distinct(StringComparer.Ordinal)
                    .OrderBy(value => value, StringComparer.Ordinal)
                    .ToArray();
            }
        }
    }
}
