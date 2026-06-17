using System;
using System.Collections.Generic;
using System.Reflection;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 表示热更新 AOT 保留的构建结果。
    /// </summary>
    /// <remarks>
    /// Represents the build result of hotfix AOT preservation.
    /// </remarks>
    internal sealed class AotPreserveBuildResult
    {
        /// <summary>
        /// 获取或设置生成的 link.xml 内容。
        /// </summary>
        /// <remarks>
        /// Gets or sets the generated link.xml content.
        /// </remarks>
        public string LinkXml { get; set; }

        /// <summary>
        /// 获取或设置生成的引用代码内容。
        /// </summary>
        /// <remarks>
        /// Gets or sets the generated references code content.
        /// </remarks>
        public string ReferencesCode { get; set; }

        /// <summary>
        /// 获取生成的引用文件集合，键为文件名，值为文件内容。
        /// </summary>
        /// <remarks>
        /// Gets the collection of generated reference files, keyed by file name with file content as value.
        /// </remarks>
        public SortedDictionary<string, string> ReferenceFiles { get; } =
            new SortedDictionary<string, string>(StringComparer.Ordinal);

        /// <summary>
        /// 获取或设置生成的 asmdef 文件内容。
        /// </summary>
        /// <remarks>
        /// Gets or sets the generated asmdef file content.
        /// </remarks>
        public string GeneratedAsmdef { get; set; }

        /// <summary>
        /// 获取或设置构建报告内容。
        /// </summary>
        /// <remarks>
        /// Gets or sets the build report content.
        /// </remarks>
        public string Report { get; set; }

        /// <summary>
        /// 获取构建过程中产生的错误列表。
        /// </summary>
        /// <remarks>
        /// Gets the list of errors produced during the build process.
        /// </remarks>
        public List<string> Errors { get; } = new List<string>();

        /// <summary>
        /// 获取一个值，指示构建是否包含错误。
        /// </summary>
        /// <remarks>
        /// Gets a value indicating whether the build contains errors.
        /// </remarks>
        public bool HasErrors
        {
            get { return Errors.Count > 0; }
        }
    }

    /// <summary>
    /// 表示热更新 AOT 保留的分析结果。
    /// </summary>
    /// <remarks>
    /// Represents the analysis result of hotfix AOT preservation.
    /// </remarks>
    internal sealed class AotPreserveAnalysis
    {
        /// <summary>
        /// 获取按程序集名称索引的保留描述符集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of preserve descriptors indexed by assembly name.
        /// </remarks>
        public SortedDictionary<string, AssemblyPreserveDescriptor> Assemblies { get; } = new SortedDictionary<string, AssemblyPreserveDescriptor>(StringComparer.Ordinal);

        /// <summary>
        /// 获取引用类型名称的集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of referenced type names.
        /// </remarks>
        public SortedSet<string> ReferenceTypeNames { get; } = new SortedSet<string>(StringComparer.Ordinal);

        /// <summary>
        /// 获取引用成员信息的列表。
        /// </summary>
        /// <remarks>
        /// Gets the list of referenced member information.
        /// </remarks>
        public List<MemberInfo> ReferenceMembers { get; } = new List<MemberInfo>();

        /// <summary>
        /// 获取已保留类型名称的集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of preserved type names.
        /// </remarks>
        public SortedSet<string> PreservedTypes { get; } = new SortedSet<string>(StringComparer.Ordinal);

        /// <summary>
        /// 获取已保留成员名称的集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of preserved member names.
        /// </remarks>
        public SortedSet<string> PreservedMembers { get; } = new SortedSet<string>(StringComparer.Ordinal);

        /// <summary>
        /// 获取已保留泛型类型名称的集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of preserved generic type names.
        /// </remarks>
        public SortedSet<string> PreservedGenericTypes { get; } = new SortedSet<string>(StringComparer.Ordinal);

        /// <summary>
        /// 获取被排除项名称的集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of excluded item names.
        /// </remarks>
        public SortedSet<string> ExcludedItems { get; } = new SortedSet<string>(StringComparer.Ordinal);

        /// <summary>
        /// 获取警告信息的集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of warning messages.
        /// </remarks>
        public SortedSet<string> Warnings { get; } = new SortedSet<string>(StringComparer.Ordinal);

        /// <summary>
        /// 获取分析过程中产生的错误列表。
        /// </summary>
        /// <remarks>
        /// Gets the list of errors produced during analysis.
        /// </remarks>
        public List<string> Errors { get; } = new List<string>();

        /// <summary>
        /// 获取或创建指定类型所属程序集的描述符。
        /// </summary>
        /// <remarks>
        /// Gets or creates the assembly descriptor for the specified type.
        /// </remarks>
        /// <param name="type">目标类型 / Target type</param>
        /// <returns>程序集描述符 / Assembly descriptor</returns>
        public AssemblyPreserveDescriptor GetAssembly(Type type)
        {
            var assemblyName = type.Assembly.GetName().Name;
            if (!Assemblies.TryGetValue(assemblyName, out var descriptor))
            {
                descriptor = new AssemblyPreserveDescriptor(assemblyName);
                Assemblies.Add(assemblyName, descriptor);
            }

            return descriptor;
        }
    }

    /// <summary>
    /// 表示一个程序集的保留描述符。
    /// </summary>
    /// <remarks>
    /// Represents a preserve descriptor for an assembly.
    /// </remarks>
    internal sealed class AssemblyPreserveDescriptor
    {
        /// <summary>
        /// 初始化 <see cref="AssemblyPreserveDescriptor"/> 类的新实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="AssemblyPreserveDescriptor"/> class.
        /// </remarks>
        /// <param name="name">程序集名称 / Assembly name</param>
        public AssemblyPreserveDescriptor(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 获取程序集名称。
        /// </summary>
        /// <remarks>
        /// Gets the assembly name.
        /// </remarks>
        public string Name { get; }

        /// <summary>
        /// 获取按类型全名索引的类型保留描述符集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of type preserve descriptors indexed by full type name.
        /// </remarks>
        public SortedDictionary<string, TypePreserveDescriptor> Types { get; } =
            new SortedDictionary<string, TypePreserveDescriptor>(StringComparer.Ordinal);

        /// <summary>
        /// 获取或创建指定类型的保留描述符。
        /// </summary>
        /// <remarks>
        /// Gets or creates the preserve descriptor for the specified type.
        /// </remarks>
        /// <param name="type">目标类型 / Target type</param>
        /// <returns>类型保留描述符 / Type preserve descriptor</returns>
        public TypePreserveDescriptor GetTypeDescriptor(Type type)
        {
            var fullName = AotPreserveNameUtility.GetLinkXmlTypeName(type);
            if (!Types.TryGetValue(fullName, out var descriptor))
            {
                descriptor = new TypePreserveDescriptor(fullName);
                Types.Add(fullName, descriptor);
            }

            return descriptor;
        }
    }

    /// <summary>
    /// 表示一个类型的保留描述符。
    /// </summary>
    /// <remarks>
    /// Represents a preserve descriptor for a type.
    /// </remarks>
    internal sealed class TypePreserveDescriptor
    {
        /// <summary>
        /// 初始化 <see cref="TypePreserveDescriptor"/> 类的新实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="TypePreserveDescriptor"/> class.
        /// </remarks>
        /// <param name="fullName">类型全名 / Full type name</param>
        public TypePreserveDescriptor(string fullName)
        {
            FullName = fullName;
        }

        /// <summary>
        /// 获取类型全名。
        /// </summary>
        /// <remarks>
        /// Gets the full type name.
        /// </remarks>
        public string FullName { get; }

        /// <summary>
        /// 获取或设置一个值，指示仅保留类型本身而不保留其成员。
        /// </summary>
        /// <remarks>
        /// Gets or sets a value indicating whether to preserve only the type itself without its members.
        /// </remarks>
        public bool PreserveTypeOnly { get; set; }

        /// <summary>
        /// 获取需要保留的字段名称集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of field names to preserve.
        /// </remarks>
        public SortedSet<string> Fields { get; } = new SortedSet<string>(StringComparer.Ordinal);

        /// <summary>
        /// 获取需要保留的方法名称集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of method names to preserve.
        /// </remarks>
        public SortedSet<string> Methods { get; } = new SortedSet<string>(StringComparer.Ordinal);

        /// <summary>
        /// 获取一个值，指示是否存在需要保留的成员。
        /// </summary>
        /// <remarks>
        /// Gets a value indicating whether there are members to preserve.
        /// </remarks>
        public bool HasMembers
        {
            get { return Fields.Count > 0 || Methods.Count > 0; }
        }
    }

    /// <summary>
    /// 指定热更新 AOT 保留的成员种类。
    /// </summary>
    /// <remarks>
    /// Specifies the kind of members for hotfix AOT preservation.
    /// </remarks>
    internal enum AotPreserveMemberKind
    {
        /// <summary>
        /// 字段。
        /// </summary>
        /// <remarks>
        /// Field.
        /// </remarks>
        Field,

        /// <summary>
        /// 方法。
        /// </summary>
        /// <remarks>
        /// Method.
        /// </remarks>
        Method,
    }

    /// <summary>
    /// 表示一个已解析的热更新 AOT 保留成员。
    /// </summary>
    /// <remarks>
    /// Represents a resolved member for hotfix AOT preservation.
    /// </remarks>
    internal sealed class AotPreserveResolvedMember
    {
        /// <summary>
        /// 初始化 <see cref="AotPreserveResolvedMember"/> 类的新实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="AotPreserveResolvedMember"/> class.
        /// </remarks>
        /// <param name="declaringType">声明该成员的类型 / The type that declares the member</param>
        /// <param name="kind">成员种类 / Member kind</param>
        /// <param name="name">成员名称 / Member name</param>
        /// <param name="source">原始成员信息 / Source member information</param>
        public AotPreserveResolvedMember(Type declaringType, AotPreserveMemberKind kind, string name, MemberInfo source)
        {
            DeclaringType = declaringType;
            Kind = kind;
            Name = name;
            Source = source;
        }

        /// <summary>
        /// 获取声明该成员的类型。
        /// </summary>
        /// <remarks>
        /// Gets the type that declares the member.
        /// </remarks>
        public Type DeclaringType { get; }

        /// <summary>
        /// 获取成员种类。
        /// </summary>
        /// <remarks>
        /// Gets the member kind.
        /// </remarks>
        public AotPreserveMemberKind Kind { get; }

        /// <summary>
        /// 获取成员名称。
        /// </summary>
        /// <remarks>
        /// Gets the member name.
        /// </remarks>
        public string Name { get; }

        /// <summary>
        /// 获取原始成员信息。
        /// </summary>
        /// <remarks>
        /// Gets the source member information.
        /// </remarks>
        public MemberInfo Source { get; }
    }
}
