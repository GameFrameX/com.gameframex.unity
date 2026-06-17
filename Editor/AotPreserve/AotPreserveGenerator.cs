using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 提供热更新 AOT 元数据保留文件的生成与校验能力。
    /// </summary>
    /// <remarks>
    /// Provides generation and validation capabilities for hotfix AOT preserve files.
    /// </remarks>
    internal static class AotPreserveGenerator
    {
        /// <summary>
        /// 基于当前配置分析结果构建所有生成文件内容（不写入磁盘）。
        /// </summary>
        /// <remarks>
        /// Builds all generated file contents based on the current configuration analysis (without writing to disk).
        /// </remarks>
        /// <returns>包含 link.xml、引用代码、asmdef 与报告的构建结果 / Build result containing link.xml, reference code, asmdef and report</returns>
        public static AotPreserveBuildResult Build()
        {
            var analysis = AotPreserveAnalyzer.Analyze();
            return Build(analysis);
        }

        /// <summary>
        /// 基于给定的分析结果构建所有生成文件内容（不写入磁盘）。
        /// </summary>
        /// <remarks>
        /// Builds all generated file contents based on the given analysis result (without writing to disk).
        /// </remarks>
        /// <param name="analysis">分析结果 / Analysis result</param>
        /// <returns>包含 link.xml、引用代码、asmdef 与报告的构建结果 / Build result containing link.xml, reference code, asmdef and report</returns>
        internal static AotPreserveBuildResult Build(AotPreserveAnalysis analysis)
        {
            var referenceFiles = GenerateReferenceFiles(analysis);
            var result = new AotPreserveBuildResult
            {
                LinkXml = GenerateLinkXml(analysis),
                ReferencesCode = referenceFiles[AotPreserveConstants.ReferencesPath],
                GeneratedAsmdef = GenerateAsmdef(analysis),
                Report = GenerateReport(analysis, referenceFiles.Keys),
            };

            foreach (var referenceFile in referenceFiles)
            {
                result.ReferenceFiles.Add(referenceFile.Key, referenceFile.Value);
            }

            result.Errors.AddRange(analysis.Errors);
            return result;
        }

        /// <summary>
        /// 生成并写入所有热更新 AOT 保留文件。
        /// </summary>
        /// <remarks>
        /// Generates and writes all hotfix AOT preserve files.
        /// </remarks>
        /// <exception cref="InvalidOperationException">当生成过程出现错误时抛出 / Thrown when errors occur during generation</exception>
        /// <exception cref="IOException">当文件写入失败时抛出 / Thrown when file writing fails</exception>
        /// <exception cref="UnauthorizedAccessException">当无写入权限时抛出 / Thrown when write permission is denied</exception>
        public static void GenerateAndWrite()
        {
            var result = Build();
            if (result.HasErrors)
            {
                throw new InvalidOperationException("Hotfix AOT preserve generation failed:\n" + string.Join("\n", result.Errors.ToArray()));
            }

            if (Directory.Exists(AotPreserveConstants.GeneratedDirectory))
            {
                Directory.Delete(AotPreserveConstants.GeneratedDirectory, true);
            }
            Directory.CreateDirectory(AotPreserveConstants.GeneratedDirectory);
            File.WriteAllText(AotPreserveConstants.LinkXmlPath, result.LinkXml, Encoding.UTF8);
            foreach (var referenceFile in result.ReferenceFiles)
            {
                File.WriteAllText(referenceFile.Key, referenceFile.Value, Encoding.UTF8);
            }

            File.WriteAllText(AotPreserveConstants.GeneratedAsmdefPath, result.GeneratedAsmdef, Encoding.UTF8);
            File.WriteAllText(AotPreserveConstants.ReportPath, result.Report, Encoding.UTF8);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 校验已生成的文件是否与当前配置一致。
        /// </summary>
        /// <remarks>
        /// Validates whether the generated files are consistent with the current configuration.
        /// </remarks>
        /// <returns>错误信息数组；如果数组为空则表示校验通过 / Array of error messages; empty array indicates validation passed</returns>
        public static string[] ValidateGeneratedFiles()
        {
            var errors = Build().Errors.ToList();
            if (errors.Count > 0)
            {
                return errors.ToArray();
            }

            var result = Build();
            CompareFile(errors, AotPreserveConstants.LinkXmlPath, result.LinkXml);
            foreach (var referenceFile in result.ReferenceFiles)
            {
                CompareFile(errors, referenceFile.Key, referenceFile.Value);
            }

            FindStaleReferenceFiles(errors, result.ReferenceFiles.Keys);
            CompareFile(errors, AotPreserveConstants.GeneratedAsmdefPath, result.GeneratedAsmdef);
            CompareFile(errors, AotPreserveConstants.ReportPath, result.Report);
            return errors.ToArray();
        }

        /// <summary>
        /// 扫描生成目录中所有文件，将不在预期路径集合内的文件作为陈旧文件错误追加到错误列表。
        /// </summary>
        /// <remarks>
        /// Scans all files in the generated directory and appends an error for each file
        /// that is not in the expected path set.
        /// </remarks>
        /// <param name="errors">错误信息收集列表 / Error message collection list</param>
        /// <param name="expectedPaths">预期保留的文件路径集合 / Collection of expected file paths to keep</param>
        private static void FindStaleReferenceFiles(List<string> errors, IEnumerable<string> expectedPaths)
        {
            if (!Directory.Exists(AotPreserveConstants.GeneratedDirectory))
            {
                return;
            }

            var expected = new HashSet<string>(expectedPaths, StringComparer.Ordinal)
            {
                AotPreserveConstants.LinkXmlPath,
                AotPreserveConstants.ReportPath,
            };

            foreach (var path in Directory.GetFiles(AotPreserveConstants.GeneratedDirectory, "*", SearchOption.AllDirectories))
            {
                var normalizedPath = path.Replace('\\', '/');
                if (!expected.Contains(normalizedPath))
                {
                    errors.Add("Generated file is stale: " + normalizedPath);
                }
            }
        }

        /// <summary>
        /// 比较磁盘文件内容与预期内容，将缺失或不一致的错误追加到错误列表。
        /// </summary>
        /// <remarks>
        /// Compares the on-disk file content with the expected content, appending missing or mismatched errors to the error list.
        /// </remarks>
        /// <param name="errors">错误信息收集列表 / Error message collection list</param>
        /// <param name="path">磁盘文件路径 / On-disk file path</param>
        /// <param name="expected">预期文件内容 / Expected file content</param>
        private static void CompareFile(System.Collections.Generic.List<string> errors, string path, string expected)
        {
            if (!File.Exists(path))
            {
                errors.Add("Generated file is missing: " + path);
                return;
            }

            var actual = File.ReadAllText(path, Encoding.UTF8);
            if (!string.Equals(NormalizeLineEndings(actual), NormalizeLineEndings(expected), StringComparison.Ordinal))
            {
                errors.Add("Generated file is out of date: " + path);
            }
        }

        /// <summary>
        /// 将字符串中的换行符统一规范化为 LF（\n）。
        /// </summary>
        /// <remarks>
        /// Normalizes line endings in the string to LF (\n).
        /// </remarks>
        /// <param name="value">原始字符串 / Original string</param>
        /// <returns>换行符被统一为 LF 的字符串 / String with line endings unified to LF</returns>
        private static string NormalizeLineEndings(string value)
        {
            return value.Replace("\r\n", "\n");
        }

        /// <summary>
        /// 生成 link.xml 文件内容。
        /// </summary>
        /// <remarks>
        /// Generates the link.xml file content.
        /// </remarks>
        /// <param name="analysis">分析结果 / Analysis result</param>
        /// <returns>link.xml 文件内容 / link.xml file content</returns>
        private static string GenerateLinkXml(AotPreserveAnalysis analysis)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<linker>");

            foreach (var assembly in analysis.Assemblies.Values)
            {
                builder.Append("  <assembly fullname=\"").Append(XmlEscape(assembly.Name)).AppendLine("\">");
                foreach (var type in assembly.Types.Values)
                {
                    if (!type.HasMembers)
                    {
                        builder.Append("    <type fullname=\"").Append(XmlEscape(type.FullName)).AppendLine("\" preserve=\"nothing\" />");
                        continue;
                    }

                    builder.Append("    <type fullname=\"").Append(XmlEscape(type.FullName)).AppendLine("\" preserve=\"nothing\">");
                    foreach (var field in type.Fields)
                    {
                        builder.Append("      <field name=\"").Append(XmlEscape(field)).AppendLine("\" />");
                    }

                    foreach (var method in type.Methods)
                    {
                        builder.Append("      <method name=\"").Append(XmlEscape(method)).AppendLine("\" />");
                    }

                    builder.AppendLine("    </type>");
                }

                builder.AppendLine("  </assembly>");
            }

            builder.AppendLine("</linker>");
            return builder.ToString();
        }

        /// <summary>
        /// 生成所有引用占位文件，键为文件路径，值为文件内容。
        /// </summary>
        /// <remarks>
        /// Generates all reference placeholder files, keyed by file path with file content as value.
        /// </remarks>
        /// <param name="analysis">分析结果 / Analysis result</param>
        /// <returns>文件路径到文件内容的有序映射 / Sorted mapping from file path to file content</returns>
        private static SortedDictionary<string, string> GenerateReferenceFiles(AotPreserveAnalysis analysis)
        {
            var typeReferences = CreateTypeReferences(analysis);
            var files = new SortedDictionary<string, string>(StringComparer.Ordinal)
            {
                { AotPreserveConstants.ReferencesPath, GenerateReferencesEntryCode(typeReferences) },
            };

            foreach (var typeReference in typeReferences)
            {
                files.Add(typeReference.Path, GenerateReferenceTypeCode(typeReference));
            }

            return files;
        }

        /// <summary>
        /// 基于分析结果构造每个引用类型对应的文件描述信息。
        /// </summary>
        /// <remarks>
        /// Builds the per-reference-type file descriptors based on the analysis result.
        /// </remarks>
        /// <param name="analysis">分析结果 / Analysis result</param>
        /// <returns>引用类型文件描述数组 / Array of reference type file descriptors</returns>
        private static ReferenceTypeFile[] CreateTypeReferences(AotPreserveAnalysis analysis)
        {
            var references = new SortedDictionary<string, ReferenceTypeFile>(StringComparer.Ordinal);

            foreach (var typeName in analysis.ReferenceTypeNames)
            {
                var type = Type.GetType(typeName, false);
                var reference = GetOrCreateTypeReference(references, typeName, type);
                reference.TypeNames.Add(typeName);
            }

            var index = 0;
            foreach (var member in analysis.ReferenceMembers.OrderBy(GetMemberSortKey, StringComparer.Ordinal))
            {
                var statements = CreateMemberReferenceStatements(member, index);
                index++;
                if (statements.Length == 0 || member.DeclaringType == null)
                {
                    continue;
                }

                var typeName = AotPreserveNameUtility.GetStableTypeName(member.DeclaringType);
                var reference = GetOrCreateTypeReference(references, typeName, member.DeclaringType);
                reference.MemberStatementBlocks.Add(statements);
            }

            return references.Values.ToArray();
        }

        /// <summary>
        /// 获取或创建指定类型对应的引用文件描述。
        /// </summary>
        /// <remarks>
        /// Gets or creates the reference file descriptor for the specified type.
        /// </remarks>
        /// <param name="references">引用文件描述集合 / Reference file descriptor collection</param>
        /// <param name="typeName">类型名称 / Type name</param>
        /// <param name="type">类型对象，可能为空 / Type object, may be null</param>
        /// <returns>对应的引用文件描述 / The corresponding reference file descriptor</returns>
        private static ReferenceTypeFile GetOrCreateTypeReference(SortedDictionary<string, ReferenceTypeFile> references, string typeName, Type type)
        {
            var key = type != null ? AotPreserveNameUtility.GetStableTypeName(type) : typeName;
            if (!references.TryGetValue(key, out var reference))
            {
                reference = new ReferenceTypeFile(typeName, type);
                references.Add(key, reference);
            }

            return reference;
        }

        /// <summary>
        /// 生成引用入口类的代码内容（聚合调用各类型的 Preserve 方法）。
        /// </summary>
        /// <remarks>
        /// Generates the code content of the reference entry class (aggregating Preserve calls for all types).
        /// </remarks>
        /// <param name="typeReferences">引用类型文件描述数组 / Array of reference type file descriptors</param>
        /// <returns>入口类代码内容 / Entry class code content</returns>
        private static string GenerateReferencesEntryCode(ReferenceTypeFile[] typeReferences)
        {
            var builder = CreateReferenceCodeBuilder(true, true);
            AppendReferenceClassStart(builder, true);
            builder.AppendLine("        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]");
            builder.AppendLine("        [Preserve]");
            builder.AppendLine("        public static void Preserve()");
            builder.AppendLine("        {");
            foreach (var typeReference in typeReferences)
            {
                builder.Append("            ").Append(typeReference.ClassName).AppendLine(".Preserve();");
            }

            builder.AppendLine("        }");
            AppendReferenceClassEnd(builder);
            return builder.ToString();
        }

        /// <summary>
        /// 生成单个引用类型占位文件的代码内容。
        /// </summary>
        /// <remarks>
        /// Generates the code content of a single reference type placeholder file.
        /// </remarks>
        /// <param name="typeReference">引用类型文件描述 / Reference type file descriptor</param>
        /// <returns>类型占位文件代码内容 / Type placeholder file code content</returns>
        private static string GenerateReferenceTypeCode(ReferenceTypeFile typeReference)
        {
            var builder = CreateReferenceCodeBuilder(true);
            builder.AppendLine("    [Preserve]");
            builder.Append("    public static partial class ").Append(typeReference.ClassName).AppendLine();
            builder.AppendLine("    {");
            builder.AppendLine("        private static readonly Type[] Types =");
            builder.AppendLine("        {");
            if (typeReference.Type != null)
            {
                builder.Append("            typeof(").Append(AotPreserveNameUtility.GetCSharpTypeName(typeReference.Type)).AppendLine("),");
            }

            builder.AppendLine("        };");
            builder.AppendLine();
            builder.AppendLine("        private static readonly string[] TypeNames =");
            builder.AppendLine("        {");
            foreach (var typeName in typeReference.TypeNames)
            {
                builder.Append("            \"").Append(EscapeString(typeName)).AppendLine("\",");
            }

            builder.AppendLine("        };");
            builder.AppendLine();
            builder.AppendLine("        [Preserve]");
            builder.AppendLine("        public static void Preserve()");
            builder.AppendLine("        {");
            AppendTypeReferenceLoop(builder, "Types", "TypeNames");
            AppendMemberReferenceStatements(builder, typeReference);
            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }

        /// <summary>
        /// 创建引用代码文件的 StringBuilder，并写入文件头、using 与命名空间起始。
        /// </summary>
        /// <remarks>
        /// Creates a StringBuilder for the reference code file, writing the file header, usings and namespace start.
        /// </remarks>
        /// <param name="includeSystemUsing">是否包含 using System / Whether to include using System</param>
        /// <param name="includeUnityEngineUsing">是否包含 using UnityEngine / Whether to include using UnityEngine</param>
        /// <returns>已写入文件头的 StringBuilder / StringBuilder with file header written</returns>
        private static StringBuilder CreateReferenceCodeBuilder(bool includeSystemUsing, bool includeUnityEngineUsing = false)
        {
            var builder = new StringBuilder();
            builder.AppendLine("// <auto-generated />");
            if (includeSystemUsing)
            {
                builder.AppendLine("using System;");
            }

            if (includeUnityEngineUsing)
            {
                builder.AppendLine("using UnityEngine;");
            }

            builder.AppendLine("using UnityEngine.Scripting;");
            builder.AppendLine();
            builder.AppendLine("namespace AotPreserve.Generated");
            builder.AppendLine("{");
            return builder;
        }

        /// <summary>
        /// 向构建器追加引用入口类的起始定义。
        /// </summary>
        /// <remarks>
        /// Appends the start definition of the reference entry class to the builder.
        /// </remarks>
        /// <param name="builder">字符串构建器 / String builder</param>
        /// <param name="includeAttribute">是否追加 [Preserve] 特性 / Whether to append the [Preserve] attribute</param>
        private static void AppendReferenceClassStart(StringBuilder builder, bool includeAttribute)
        {
            if (includeAttribute)
            {
                builder.AppendLine("    [Preserve]");
            }

            builder.AppendLine("    public static partial class AotPreserveReferences");
            builder.AppendLine("    {");
        }

        /// <summary>
        /// 向构建器追加引用入口类的结尾括号。
        /// </summary>
        /// <remarks>
        /// Appends the closing braces of the reference entry class to the builder.
        /// </remarks>
        /// <param name="builder">字符串构建器 / String builder</param>
        private static void AppendReferenceClassEnd(StringBuilder builder)
        {
            builder.AppendLine("    }");
            builder.AppendLine("}");
        }

        /// <summary>
        /// 向构建器追加遍历类型与类型名称数组的占位访问代码。
        /// </summary>
        /// <remarks>
        /// Appends placeholder access code that iterates over the type and type name arrays to the builder.
        /// </remarks>
        /// <param name="builder">字符串构建器 / String builder</param>
        /// <param name="typeArrayName">类型数组字段名 / Type array field name</param>
        /// <param name="typeNameArrayName">类型名称数组字段名 / Type name array field name</param>
        private static void AppendTypeReferenceLoop(StringBuilder builder, string typeArrayName, string typeNameArrayName)
        {
            builder.Append("            foreach (var type in ").Append(typeArrayName).AppendLine(")");
            builder.AppendLine("            {");
            builder.AppendLine("                _ = type;");
            builder.AppendLine("            }");
            builder.AppendLine();
            builder.Append("            foreach (var typeName in ").Append(typeNameArrayName).AppendLine(")");
            builder.AppendLine("            {");
            builder.AppendLine("                _ = Type.GetType(typeName, false);");
            builder.AppendLine("            }");
        }

        /// <summary>
        /// 向构建器追加成员引用占位语句（包在恒不成立的条件块中以防实际执行）。
        /// </summary>
        /// <remarks>
        /// Appends member reference placeholder statements to the builder
        /// (wrapped in an always-false condition block to prevent actual execution).
        /// </remarks>
        /// <param name="builder">字符串构建器 / String builder</param>
        /// <param name="typeReference">引用类型文件描述 / Reference type file descriptor</param>
        private static void AppendMemberReferenceStatements(StringBuilder builder, ReferenceTypeFile typeReference)
        {
            builder.AppendLine();
            if (typeReference.MemberStatementBlocks.Count == 0)
            {
                builder.AppendLine("            _ = 0;");
                return;
            }

            builder.AppendLine("            if (DateTime.UtcNow.Ticks == long.MinValue)");
            builder.AppendLine("            {");
            foreach (var statements in typeReference.MemberStatementBlocks)
            {
                foreach (var statement in statements)
                {
                    builder.Append("                ").AppendLine(statement);
                }
            }

            builder.AppendLine("            }");
        }

        /// <summary>
        /// 为单个成员（字段、构造函数、方法、属性访问器、事件访问器）生成引用占位语句。
        /// </summary>
        /// <remarks>
        /// Generates reference placeholder statements for a single member
        /// (field, constructor, method, property accessor, or event accessor).
        /// </remarks>
        /// <param name="member">反射成员信息 / Reflected member info</param>
        /// <param name="index">用于生成实例变量名的索引 / Index used to generate instance variable names</param>
        /// <returns>引用占位语句数组，空数组表示不生成 / Array of reference placeholder statements; empty array means none</returns>
        private static string[] CreateMemberReferenceStatements(MemberInfo member, int index)
        {
            if (member is FieldInfo field)
            {
                if (!field.IsPublic ||
                    field.IsLiteral ||
                    field.Name == "value__" ||
                    AotPreserveNameUtility.IsObsolete(field) ||
                    !ShouldGenerateMemberReferences(field.DeclaringType) ||
                    !IsReferenceableType(field.FieldType))
                {
                    return Array.Empty<string>();
                }

                var target = GetMemberTargetStatements(field.DeclaringType, field.IsStatic, index, out var targetExpression);
                return target.Concat(new[] { "_ = " + targetExpression + "." + field.Name + ";" }).ToArray();
            }

            if (member is ConstructorInfo constructor)
            {
                if (!constructor.IsPublic ||
                    constructor.GetParameters().Length != 0 ||
                    constructor.DeclaringType == null ||
                    constructor.DeclaringType.IsAbstract ||
                    AotPreserveNameUtility.IsObsolete(constructor) ||
                    !ShouldGenerateMemberReferences(constructor.DeclaringType))
                {
                    return Array.Empty<string>();
                }

                return new[] { "_ = new " + AotPreserveNameUtility.GetCSharpTypeName(constructor.DeclaringType) + "();" };
            }

            if (member is MethodInfo method)
            {
                return CreateMethodReferenceStatements(method, index);
            }

            return Array.Empty<string>();
        }

        /// <summary>
        /// 为方法成员（含属性与事件访问器）生成引用占位语句。
        /// </summary>
        /// <remarks>
        /// Generates reference placeholder statements for a method member (including property and event accessors).
        /// </remarks>
        /// <param name="method">反射方法信息 / Reflected method info</param>
        /// <param name="index">用于生成实例变量名的索引 / Index used to generate instance variable names</param>
        /// <returns>引用占位语句数组，空数组表示不生成 / Array of reference placeholder statements; empty array means none</returns>
        private static string[] CreateMethodReferenceStatements(MethodInfo method, int index)
        {
            if (!method.IsPublic ||
                method.ContainsGenericParameters ||
                method.DeclaringType == null ||
                method.GetBaseDefinition() != method ||
                AotPreserveNameUtility.IsObsolete(method) ||
                !ShouldGenerateMemberReferences(method.DeclaringType))
            {
                return Array.Empty<string>();
            }

            var property = FindPropertyForAccessor(method);
            if (property != null)
            {
                if (property.GetIndexParameters().Length != 0 ||
                    AotPreserveNameUtility.IsObsolete(property) ||
                    !IsReferenceableType(property.PropertyType))
                {
                    return Array.Empty<string>();
                }

                var target = GetMemberTargetStatements(method.DeclaringType, method.IsStatic, index, out var targetExpression);
                if (method.Name.StartsWith("get_", StringComparison.Ordinal))
                {
                    return target.Concat(new[] { "_ = " + targetExpression + "." + property.Name + ";" }).ToArray();
                }

                if (method.Name.StartsWith("set_", StringComparison.Ordinal))
                {
                    return target.Concat(new[] { targetExpression + "." + property.Name + " = " + GetDefaultValueExpression(property.PropertyType) + ";" }).ToArray();
                }
            }

            var eventInfo = FindEventForAccessor(method);
            if (eventInfo != null)
            {
                if (AotPreserveNameUtility.IsObsolete(eventInfo) ||
                    eventInfo.EventHandlerType == null ||
                    !IsReferenceableType(eventInfo.EventHandlerType))
                {
                    return Array.Empty<string>();
                }

                var target = GetMemberTargetStatements(method.DeclaringType, method.IsStatic, index, out var targetExpression);
                var operation = method.Name.StartsWith("add_", StringComparison.Ordinal) ? "+=" : "-=";
                return target.Concat(new[] { targetExpression + "." + eventInfo.Name + " " + operation + " " + GetDefaultValueExpression(eventInfo.EventHandlerType) + ";" }).ToArray();
            }

            if (!method.IsSpecialName &&
                IsReferenceableType(method.ReturnType) &&
                method.GetParameters().All(parameter => IsReferenceableType(parameter.ParameterType)))
            {
                var target = GetMemberTargetStatements(method.DeclaringType, method.IsStatic, index, out var targetExpression);
                var arguments = string.Join(", ", method.GetParameters().Select(parameter => GetDefaultValueExpression(parameter.ParameterType)).ToArray());
                var call = targetExpression + "." + method.Name + "(" + arguments + ")";
                var statement = method.ReturnType == typeof(void) ? call + ";" : "_ = " + call + ";";
                return target.Concat(new[] { statement }).ToArray();
            }

            return Array.Empty<string>();
        }

        /// <summary>
        /// 查找方法所属的属性（如果该方法为属性的 get/set 访问器）。
        /// </summary>
        /// <remarks>
        /// Finds the property that owns the method, if it is the property's get/set accessor.
        /// </remarks>
        /// <param name="method">反射方法信息 / Reflected method info</param>
        /// <returns>对应的属性；如果不存在则为空 / The corresponding property; null if none</returns>
        private static PropertyInfo FindPropertyForAccessor(MethodInfo method)
        {
            return method.DeclaringType
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .FirstOrDefault(property => property.GetGetMethod(true) == method || property.GetSetMethod(true) == method);
        }

        /// <summary>
        /// 查找方法所属的事件（如果该方法为事件的 add/remove 访问器）。
        /// </summary>
        /// <remarks>
        /// Finds the event that owns the method, if it is the event's add/remove accessor.
        /// </remarks>
        /// <param name="method">反射方法信息 / Reflected method info</param>
        /// <returns>对应的事件；如果不存在则为空 / The corresponding event; null if none</returns>
        private static EventInfo FindEventForAccessor(MethodInfo method)
        {
            return method.DeclaringType
                .GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .FirstOrDefault(eventInfo => eventInfo.GetAddMethod(true) == method || eventInfo.GetRemoveMethod(true) == method);
        }

        /// <summary>
        /// 构造成员访问前的目标表达式与必要的实例化语句。
        /// </summary>
        /// <remarks>
        /// Builds the target expression and any necessary instantiation statements before member access.
        /// </remarks>
        /// <param name="declaringType">声明该成员的类型 / Type that declares the member</param>
        /// <param name="isStatic">成员是否为静态 / Whether the member is static</param>
        /// <param name="index">用于生成实例变量名的索引 / Index used to generate instance variable names</param>
        /// <param name="targetExpression">输出的目标表达式 / Output target expression</param>
        /// <returns>目标前置语句数组；静态成员返回空数组 / Array of target prefix statements; empty array for static members</returns>
        private static string[] GetMemberTargetStatements(Type declaringType, bool isStatic, int index, out string targetExpression)
        {
            var typeName = AotPreserveNameUtility.GetCSharpTypeName(declaringType);
            if (isStatic)
            {
                targetExpression = typeName;
                return Array.Empty<string>();
            }

            var variableName = "instance" + index.ToString(System.Globalization.CultureInfo.InvariantCulture);
            targetExpression = variableName;
            return new[] { "var " + variableName + " = default(" + typeName + ");" };
        }

        /// <summary>
        /// 生成指定类型的 default(T) 表达式。
        /// </summary>
        /// <remarks>
        /// Generates the default(T) expression for the specified type.
        /// </remarks>
        /// <param name="type">目标类型 / Target type</param>
        /// <returns>default(T) 形式的表达式字符串 / Expression string in the form default(T)</returns>
        private static string GetDefaultValueExpression(Type type)
        {
            return "default(" + AotPreserveNameUtility.GetCSharpTypeName(type) + ")";
        }

        /// <summary>
        /// 判断类型是否可以用于生成引用代码。
        /// </summary>
        /// <remarks>
        /// Determines whether the type can be used to generate reference code.
        /// </remarks>
        /// <param name="type">目标类型 / Target type</param>
        /// <returns>可引用返回 true，否则返回 false / Returns true if referenceable; otherwise false</returns>
        private static bool IsReferenceableType(Type type)
        {
            return type != null &&
                   type != typeof(void) &&
                   !type.IsPointer &&
                   !type.IsByRef &&
                   !type.IsGenericParameter &&
                   !type.ContainsGenericParameters;
        }

        /// <summary>
        /// 判断该类型的成员是否应该生成引用代码（仅限 UnityEngine 与 GameFrameX 程序集）。
        /// </summary>
        /// <remarks>
        /// Determines whether members of the type should generate reference code
        /// (restricted to UnityEngine and GameFrameX assemblies).
        /// </remarks>
        /// <param name="type">目标类型 / Target type</param>
        /// <returns>需要生成返回 true，否则返回 false / Returns true if generation is needed; otherwise false</returns>
        private static bool ShouldGenerateMemberReferences(Type type)
        {
            if (!IsReferenceableType(type))
            {
                return false;
            }

            var assemblyName = type.Assembly.GetName().Name;
            return assemblyName.StartsWith("UnityEngine", StringComparison.Ordinal) ||
                   assemblyName.StartsWith("GameFrameX", StringComparison.Ordinal);
        }

        /// <summary>
        /// 为成员生成稳定的排序键（声明类型全名 : 成员名）。
        /// </summary>
        /// <remarks>
        /// Generates a stable sort key for the member (declaring type full name : member name).
        /// </remarks>
        /// <param name="member">反射成员信息 / Reflected member info</param>
        /// <returns>排序键字符串 / Sort key string</returns>
        private static string GetMemberSortKey(MemberInfo member)
        {
            return (member.DeclaringType != null ? member.DeclaringType.FullName : string.Empty) + ":" + member.Name;
        }

        /// <summary>
        /// 生成生成程序集的 asmdef 文件内容。
        /// </summary>
        /// <remarks>
        /// Generates the asmdef file content for the generated assembly.
        /// </remarks>
        /// <param name="analysis">分析结果 / Analysis result</param>
        /// <returns>asmdef 文件内容 / asmdef file content</returns>
        private static string GenerateAsmdef(AotPreserveAnalysis analysis)
        {
            var references = analysis.Assemblies.Keys
                .Where(assemblyName => !AotPreserveNameUtility.IsFrameworkAssembly(assemblyName))
                .OrderBy(assemblyName => assemblyName, StringComparer.Ordinal)
                .ToArray();

            var builder = new StringBuilder();
            builder.AppendLine("{");
            builder.AppendLine("    \"name\": \"AotPreserve.Generated\",");
            builder.AppendLine("    \"rootNamespace\": \"AotPreserve.Generated\",");
            builder.AppendLine("    \"references\": [");
            for (var i = 0; i < references.Length; i++)
            {
                builder.Append("        \"").Append(EscapeString(references[i])).Append("\"");
                if (i < references.Length - 1)
                {
                    builder.Append(',');
                }

                builder.AppendLine();
            }

            builder.AppendLine("    ],");
            builder.AppendLine("    \"includePlatforms\": [],");
            builder.AppendLine("    \"excludePlatforms\": [],");
            builder.AppendLine("    \"allowUnsafeCode\": false,");
            builder.AppendLine("    \"overrideReferences\": false,");
            builder.AppendLine("    \"precompiledReferences\": [],");
            builder.AppendLine("    \"autoReferenced\": true,");
            builder.AppendLine("    \"defineConstraints\": [],");
            builder.AppendLine("    \"versionDefines\": [],");
            builder.AppendLine("    \"noEngineReferences\": false");
            builder.AppendLine("}");
            return builder.ToString();
        }

        /// <summary>
        /// 生成 markdown 形式的生成报告内容。
        /// </summary>
        /// <remarks>
        /// Generates the markdown-formatted report content.
        /// </remarks>
        /// <param name="analysis">分析结果 / Analysis result</param>
        /// <param name="referenceFiles">生成的引用文件路径集合 / Collection of generated reference file paths</param>
        /// <returns>报告 markdown 内容 / Report markdown content</returns>
        private static string GenerateReport(AotPreserveAnalysis analysis, IEnumerable<string> referenceFiles)
        {
            var builder = new StringBuilder();
            builder.AppendLine("# Hotfix AOT Preserve Report");
            builder.AppendLine();
            builder.AppendLine("> Generated by `GameFrameX/HybridCLR Generate Hotfix AOT Preserve`.");
            builder.AppendLine();
            AppendSection(builder, "Assemblies", analysis.Assemblies.Keys.ToArray());
            AppendSection(builder, "Types", analysis.PreservedTypes.ToArray());
            AppendSection(builder, "Members", analysis.PreservedMembers.ToArray());
            AppendSection(builder, "Generic And Delegate Types", analysis.PreservedGenericTypes.ToArray());
            AppendSection(builder, "Excluded Items", analysis.ExcludedItems.ToArray());
            AppendSection(builder, "Warnings", analysis.Warnings.ToArray());
            AppendSection(builder, "Errors", analysis.Errors.ToArray());
            builder.AppendLine("## Generated Files");
            builder.AppendLine();
            builder.AppendLine("- `" + AotPreserveConstants.LinkXmlPath + "`");
            foreach (var referenceFile in referenceFiles)
            {
                builder.AppendLine("- `" + referenceFile + "`");
            }

            builder.AppendLine("- `" + AotPreserveConstants.GeneratedAsmdefPath + "`");
            builder.AppendLine("- `" + AotPreserveConstants.ReportPath + "`");
            return builder.ToString();
        }

        /// <summary>
        /// 向报告追加一个标题分节，按字典序列出所有条目（为空则输出 None）。
        /// </summary>
        /// <remarks>
        /// Appends a titled section to the report, listing all items in lexical order (None if empty).
        /// </remarks>
        /// <param name="builder">字符串构建器 / String builder</param>
        /// <param name="title">分节标题 / Section title</param>
        /// <param name="items">分节条目数组 / Array of section items</param>
        private static void AppendSection(StringBuilder builder, string title, string[] items)
        {
            builder.Append("## ").AppendLine(title);
            builder.AppendLine();
            if (items.Length == 0)
            {
                builder.AppendLine("- None");
            }
            else
            {
                foreach (var item in items.OrderBy(item => item, StringComparer.Ordinal))
                {
                    builder.Append("- `").Append(item.Replace("`", "\\`")).AppendLine("`");
                }
            }

            builder.AppendLine();
        }

        /// <summary>
        /// 对字符串进行 XML 转义。
        /// </summary>
        /// <remarks>
        /// Escapes the string for use in XML.
        /// </remarks>
        /// <param name="value">原始字符串 / Original string</param>
        /// <returns>转义后的字符串 / Escaped string</returns>
        private static string XmlEscape(string value)
        {
            return SecurityElement.Escape(value);
        }

        /// <summary>
        /// 对字符串进行 C# 字符串字面量转义（反斜杠与双引号）。
        /// </summary>
        /// <remarks>
        /// Escapes the string for use in a C# string literal (backslash and double quotes).
        /// </remarks>
        /// <param name="value">原始字符串 / Original string</param>
        /// <returns>转义后的字符串 / Escaped string</returns>
        private static string EscapeString(string value)
        {
            return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        /// <summary>
        /// 描述单个引用类型对应的生成文件信息。
        /// </summary>
        /// <remarks>
        /// Describes the generation metadata of a single reference type's file.
        /// </remarks>
        private sealed class ReferenceTypeFile
        {
            /// <summary>
            /// 初始化引用类型文件描述。
            /// </summary>
            /// <remarks>
            /// Initializes the reference type file descriptor.
            /// </remarks>
            /// <param name="stableTypeName">稳定类型名称 / Stable type name</param>
            /// <param name="type">类型对象，可能为空 / Type object, may be null</param>
            public ReferenceTypeFile(string stableTypeName, Type type)
            {
                StableTypeName = stableTypeName;
                Type = type;

                var safeName = GetSafeGeneratedIdentifier(type != null ? AotPreserveNameUtility.GetReadableTypeName(type) : stableTypeName);
                var suffix = GetStableHash(stableTypeName);
                ClassName = AotPreserveConstants.ReferenceTypeFilePrefix + safeName + "_" + suffix;
                Path = AotPreserveConstants.GeneratedDirectory + "/" + ClassName + ".cs";
            }

            /// <summary>
            /// 获取稳定类型名称。
            /// </summary>
            /// <remarks>
            /// Gets the stable type name.
            /// </remarks>
            /// <value>稳定类型名称 / Stable type name</value>
            public string StableTypeName { get; }

            /// <summary>
            /// 获取类型对象，可能为空。
            /// </summary>
            /// <remarks>
            /// Gets the type object, may be null.
            /// </remarks>
            /// <value>类型对象 / Type object</value>
            public Type Type { get; }

            /// <summary>
            /// 获取生成的类名（含稳定哈希后缀以避免冲突）。
            /// </summary>
            /// <remarks>
            /// Gets the generated class name (with a stable hash suffix to avoid collisions).
            /// </remarks>
            /// <value>生成类名 / Generated class name</value>
            public string ClassName { get; }

            /// <summary>
            /// 获取生成文件的相对路径。
            /// </summary>
            /// <remarks>
            /// Gets the relative path of the generated file.
            /// </remarks>
            /// <value>生成文件路径 / Generated file path</value>
            public string Path { get; }

            /// <summary>
            /// 获取需要保留的类型名称集合。
            /// </summary>
            /// <remarks>
            /// Gets the set of type names to preserve.
            /// </remarks>
            /// <value>类型名称集合 / Set of type names</value>
            public SortedSet<string> TypeNames { get; } = new SortedSet<string>(StringComparer.Ordinal);

            /// <summary>
            /// 获取成员引用占位语句块列表。
            /// </summary>
            /// <remarks>
            /// Gets the list of member reference placeholder statement blocks.
            /// </remarks>
            /// <value>成员语句块列表 / List of member statement blocks</value>
            public List<string[]> MemberStatementBlocks { get; } = new List<string[]>();
        }

        /// <summary>
        /// 将任意字符串转换为合法且长度受限的 C# 标识符。
        /// </summary>
        /// <remarks>
        /// Converts an arbitrary string into a valid, length-limited C# identifier.
        /// </remarks>
        /// <param name="value">原始字符串 / Original string</param>
        /// <returns>安全的标识符 / Safe identifier</returns>
        private static string GetSafeGeneratedIdentifier(string value)
        {
            var safeName = Regex.Replace(value, "[^A-Za-z0-9_]+", "_").Trim('_');
            if (string.IsNullOrEmpty(safeName))
            {
                safeName = "Type";
            }

            if (safeName.Length > 80)
            {
                safeName = safeName.Substring(0, 80).Trim('_');
            }

            if (char.IsDigit(safeName[0]))
            {
                safeName = "_" + safeName;
            }

            return safeName;
        }

        /// <summary>
        /// 基于字符串计算稳定的 SHA1 哈希前缀（6 个十六进制字符）。
        /// </summary>
        /// <remarks>
        /// Computes a stable SHA1 hash prefix (6 hex characters) from the string.
        /// </remarks>
        /// <param name="value">原始字符串 / Original string</param>
        /// <returns>6 字符哈希前缀 / 6-character hash prefix</returns>
        private static string GetStableHash(string value)
        {
            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(value));
                var builder = new StringBuilder();
                for (var i = 0; i < 6; i++)
                {
                    builder.Append(hash[i].ToString("x2", System.Globalization.CultureInfo.InvariantCulture));
                }

                return builder.ToString();
            }
        }
    }
}
