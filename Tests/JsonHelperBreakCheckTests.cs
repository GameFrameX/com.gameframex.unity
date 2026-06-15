using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameFrameX.Runtime;
using NUnit.Framework;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GameFrameX.Tests
{
    [TestFixture]
    public sealed class JsonHelperBreakCheckTests
    {
        private const string ReportDirectory = "Reports/json-breakcheck";

        [Test]
        public void JsonHelpers_BreakCheck_GeneratesReport()
        {
            RunBreakCheck();
        }

#if UNITY_EDITOR
        public static void RunFromCommandLine()
        {
            try
            {
                RunBreakCheck();
                EditorApplication.Exit(0);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                EditorApplication.Exit(1);
            }
        }
#endif

        public static void RunBreakCheck()
        {
            var helper = new LitJsonHelper();
            IReadOnlyList<BreakCheckCase> cases = CreateCases();
            var results = new List<BreakCheckResult>();

            foreach (BreakCheckCase breakCheckCase in cases)
            {
                results.Add(breakCheckCase.Execute(helper));
            }

            WriteReports(results);
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(0, CountNonMatches(results));
        }

        private static IReadOnlyList<BreakCheckCase> CreateCases()
        {
            return new[]
            {
                BreakCheckCase.Serialize("serialize-null-object", "顶层 null 对象序列化为 null。", null, "null"),
                BreakCheckCase.Serialize("serialize-simple-object", "基础 public 属性对象是常见运行时载荷形态。", new SimplePayload { Name = "Test", Value = 42 }, "{\"Name\":\"Test\",\"Value\":42}"),
                BreakCheckCase.Serialize("serialize-null-field", "null 成员不能被静默丢弃。", new SimplePayload { Name = null, Value = 1 }, "{\"Name\":null,\"Value\":1}"),
                BreakCheckCase.Serialize("serialize-primitive-array", "数组输出需要与客户端协议保持兼容。", new[] { 1, 2, 3 }, "[1,2,3]"),
                BreakCheckCase.Serialize("serialize-dictionary", "Dictionary 常用于配置类数据。", new Dictionary<string, int> { { "hp", 100 }, { "mp", 20 } }, "{\"hp\":100,\"mp\":20}"),
                BreakCheckCase.Serialize("serialize-list-of-objects", "List 对象集合常用于响应 DTO。", new List<SimplePayload> { new SimplePayload { Name = "A", Value = 1 }, new SimplePayload { Name = "B", Value = 2 } }, "[{\"Name\":\"A\",\"Value\":1},{\"Name\":\"B\",\"Value\":2}]"),
                BreakCheckCase.Serialize("serialize-enum", "枚举保持数字表示。", new EnumPayload { Mode = SampleMode.Beta }, "{\"Mode\":1}"),
                BreakCheckCase.Serialize("serialize-datetime", "DateTime 使用 UTC ISO 格式。", new DatePayload { Time = new DateTime(2026, 6, 15, 12, 30, 45, DateTimeKind.Utc) }, "{\"Time\":\"2026-06-15T12:30:45Z\"}"),
                BreakCheckCase.Serialize("serialize-json-ignore", "LitJSON JsonIgnore 字段不应被序列化。", new IgnorePayload { Visible = "yes", Hidden = "secret" }, "{\"Visible\":\"yes\"}"),
                BreakCheckCase.Serialize("serialize-json-property", "LitJSON JsonProperty 应改写 JSON 字段名。", new PropertyNamePayload { PlayerId = 10086, DisplayName = "tester" }, "{\"player_id\":10086,\"display_name\":\"tester\"}"),
                BreakCheckCase.Serialize("serialize-private-setter", "private setter 可被序列化。", PrivateSetterPayload.Create(7, "locked"), "{\"Id\":7,\"Name\":\"locked\"}"),
                BreakCheckCase.Deserialize<SimplePayload>("deserialize-simple-object", "基础对象反序列化必须保留 public 属性。", "{\"Name\":\"Test\",\"Value\":42}", "{\"Name\":\"Test\",\"Value\":42}"),
                BreakCheckCase.Deserialize<SimplePayload>("deserialize-empty-string", "空字符串反序列化返回 null。", string.Empty, "<null>"),
                BreakCheckCase.DeserializeThrows<SimplePayload>("deserialize-null-json", "null 输入抛出 ArgumentNullException。", null, typeof(ArgumentNullException)),
                BreakCheckCase.DeserializeThrows<SimplePayload>("deserialize-invalid-json", "非法 JSON 抛出 GameFrameworkException。", "not valid json", typeof(GameFrameworkException)),
                BreakCheckCase.Deserialize<Dictionary<string, int>>("deserialize-dictionary", "Dictionary 反序列化必须保留键和值。", "{\"hp\":100,\"mp\":20}", "{\"hp\":100,\"mp\":20}"),
                BreakCheckCase.Deserialize<EnumPayload>("deserialize-enum-number", "数字枚举输入保持兼容。", "{\"Mode\":1}", "{\"Mode\":1}"),
                BreakCheckCase.Deserialize<DatePayload>("deserialize-datetime", "DateTime 解析为 UTC 语义。", "{\"Time\":\"2026-06-15T12:30:45Z\"}", "{\"Time\":\"2026-06-15T12:30:45Z\"}"),
                BreakCheckCase.Deserialize<IgnorePayload>("deserialize-json-ignore", "被忽略字段不应被填充。", "{\"Visible\":\"yes\",\"Hidden\":\"secret\"}", "{\"Visible\":\"yes\"}"),
                BreakCheckCase.Deserialize<PropertyNamePayload>("deserialize-json-property", "LitJSON JsonProperty 应使用 JSON 字段名填充成员。", "{\"player_id\":10086,\"display_name\":\"tester\"}", "{\"player_id\":10086,\"display_name\":\"tester\"}"),
                BreakCheckCase.Deserialize<PrivateSetterPayload>("deserialize-private-setter", "private setter 不应被反序列化填充。", "{\"Id\":7,\"Name\":\"locked\"}", "{\"Id\":0,\"Name\":null}"),
            };
        }

        private static void WriteReports(IReadOnlyList<BreakCheckResult> results)
        {
            string directory = Path.Combine(Application.dataPath, "..", ReportDirectory);
            Directory.CreateDirectory(directory);

            File.WriteAllText(Path.Combine(directory, "latest.json"), BuildJsonReport(results), Encoding.UTF8);
            File.WriteAllText(Path.Combine(directory, "latest.md"), BuildMarkdownReport(results), Encoding.UTF8);
        }

        private static string BuildJsonReport(IReadOnlyList<BreakCheckResult> results)
        {
            var builder = new StringBuilder();
            builder.AppendLine("{");
            builder.AppendLine("  \"schemaVersion\": 2,");
            builder.AppendLine("  \"generatedAtUtc\": \"" + DateTime.UtcNow.ToString("O") + "\",");
            builder.AppendLine("  \"environment\": {");
            builder.AppendLine("    \"unityVersion\": \"" + EscapeJson(Application.unityVersion) + "\",");
            builder.AppendLine("    \"platform\": \"" + EscapeJson(Application.platform.ToString()) + "\"");
            builder.AppendLine("  },");
            builder.AppendLine("  \"summary\": {");
            builder.AppendLine("    \"total\": " + results.Count + ",");
            builder.AppendLine("    \"matches\": " + CountStatus(results, BreakCheckStatus.Match) + ",");
            builder.AppendLine("    \"differences\": " + CountNonMatches(results));
            builder.AppendLine("  },");
            builder.AppendLine("  \"results\": [");

            for (int i = 0; i < results.Count; i++)
            {
                BreakCheckResult result = results[i];
                builder.AppendLine("    {");
                builder.AppendLine("      \"caseName\": \"" + EscapeJson(result.CaseName) + "\",");
                builder.AppendLine("      \"operation\": \"" + EscapeJson(result.Operation) + "\",");
                builder.AppendLine("      \"status\": \"" + result.Status + "\",");
                builder.AppendLine("      \"severity\": \"" + result.Severity + "\",");
                builder.AppendLine("      \"description\": \"" + EscapeJson(result.Description) + "\",");
                builder.AppendLine("      \"expected\": \"" + EscapeJson(result.Expected) + "\",");
                builder.AppendLine("      \"actual\": \"" + EscapeJson(result.Actual) + "\"");
                builder.Append("    }");
                builder.AppendLine(i == results.Count - 1 ? string.Empty : ",");
            }

            builder.AppendLine("  ]");
            builder.AppendLine("}");
            return builder.ToString();
        }

        private static string BuildMarkdownReport(IReadOnlyList<BreakCheckResult> results)
        {
            var builder = new StringBuilder();
            builder.AppendLine("# LitJSON 辅助器契约检查报告");
            builder.AppendLine();
            builder.AppendLine("- 生成时间（UTC）：`" + DateTime.UtcNow.ToString("O") + "`");
            builder.AppendLine("- Unity 版本：`" + Application.unityVersion + "`");
            builder.AppendLine("- 运行平台：`" + Application.platform + "`");
            builder.AppendLine("- 检查总数：`" + results.Count + "`");
            builder.AppendLine("- 匹配数量：`" + CountStatus(results, BreakCheckStatus.Match) + "`");
            builder.AppendLine("- 差异数量：`" + CountNonMatches(results) + "`");
            builder.AppendLine();
            builder.AppendLine("| 状态 | 严重级别 | 用例 | 操作 | 期望 | 实际 |");
            builder.AppendLine("|---|---|---|---|---|---|");

            foreach (BreakCheckResult result in results)
            {
                builder.Append("| ");
                builder.Append(ToChineseStatus(result.Status));
                builder.Append(" | ");
                builder.Append(ToChineseSeverity(result.Severity));
                builder.Append(" | ");
                builder.Append(result.CaseName);
                builder.Append(" | ");
                builder.Append(ToChineseOperation(result.Operation));
                builder.Append(" | ");
                builder.Append(ToMarkdownCell(result.Expected));
                builder.Append(" | ");
                builder.Append(ToMarkdownCell(result.Actual));
                builder.AppendLine(" |");
            }

            return builder.ToString();
        }

        private static int CountStatus(IReadOnlyList<BreakCheckResult> results, BreakCheckStatus status)
        {
            int count = 0;
            foreach (BreakCheckResult result in results)
            {
                if (result.Status == status)
                {
                    count++;
                }
            }

            return count;
        }

        private static int CountNonMatches(IReadOnlyList<BreakCheckResult> results)
        {
            return results.Count - CountStatus(results, BreakCheckStatus.Match);
        }

        private static string NormalizeJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return json;
            }

            return json.Replace(" ", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        private static string ToChineseStatus(BreakCheckStatus status)
        {
            return status == BreakCheckStatus.Match ? "匹配" : "差异";
        }

        private static string ToChineseSeverity(BreakCheckSeverity severity)
        {
            return severity == BreakCheckSeverity.Info ? "信息" : "高";
        }

        private static string ToChineseOperation(string operation)
        {
            return operation == "serialize" ? "序列化" : "反序列化";
        }

        private static string ToMarkdownCell(string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value.Replace("|", "\\|");
        }

        private static string EscapeJson(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n");
        }

        private sealed class BreakCheckCase
        {
            private readonly Func<LitJsonHelper, string> m_Execute;

            private BreakCheckCase(string name, string operation, string description, string expected, Func<LitJsonHelper, string> execute)
            {
                Name = name;
                Operation = operation;
                Description = description;
                Expected = expected;
                m_Execute = execute;
            }

            public string Name { get; }
            public string Operation { get; }
            public string Description { get; }
            public string Expected { get; }

            public BreakCheckResult Execute(LitJsonHelper helper)
            {
                string actual;
                try
                {
                    actual = m_Execute(helper);
                }
                catch (Exception exception)
                {
                    actual = exception.GetType().Name;
                }

                return new BreakCheckResult
                {
                    CaseName = Name,
                    Operation = Operation,
                    Description = Description,
                    Expected = Expected,
                    Actual = actual,
                    Status = string.Equals(Expected, actual, StringComparison.Ordinal) ? BreakCheckStatus.Match : BreakCheckStatus.Difference,
                    Severity = string.Equals(Expected, actual, StringComparison.Ordinal) ? BreakCheckSeverity.Info : BreakCheckSeverity.High,
                };
            }

            public static BreakCheckCase Serialize(string name, string description, object payload, string expected)
            {
                return new BreakCheckCase(name, "serialize", description, expected, helper => NormalizeJson(helper.ToJson(payload)));
            }

            public static BreakCheckCase Deserialize<T>(string name, string description, string json, string expected)
            {
                return new BreakCheckCase(name, "deserialize", description, expected, helper =>
                {
                    object obj = helper.ToObject<T>(json);
                    return obj == null ? "<null>" : NormalizeJson(helper.ToJson(obj));
                });
            }

            public static BreakCheckCase DeserializeThrows<T>(string name, string description, string json, Type exceptionType)
            {
                return new BreakCheckCase(name, "deserialize", description, exceptionType.Name, helper =>
                {
                    helper.ToObject<T>(json);
                    return "<no-exception>";
                });
            }
        }

        private sealed class BreakCheckResult
        {
            public string CaseName;
            public string Operation;
            public string Description;
            public string Expected;
            public string Actual;
            public BreakCheckStatus Status;
            public BreakCheckSeverity Severity;
        }

        private enum BreakCheckStatus
        {
            Match,
            Difference,
        }

        private enum BreakCheckSeverity
        {
            Info,
            High,
        }

        private enum SampleMode
        {
            Alpha,
            Beta,
        }

        [Serializable]
        private sealed class SimplePayload
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        [Serializable]
        private sealed class EnumPayload
        {
            public SampleMode Mode { get; set; }
        }

        [Serializable]
        private sealed class DatePayload
        {
            public DateTime Time { get; set; }
        }

        [Serializable]
        private sealed class IgnorePayload
        {
            public string Visible { get; set; }

            [GameFrameX.LitJSON.Runtime.JsonIgnore]
            public string Hidden { get; set; }
        }

        [Serializable]
        private sealed class PropertyNamePayload
        {
            [GameFrameX.LitJSON.Runtime.JsonProperty("player_id")]
            public int PlayerId { get; set; }

            [GameFrameX.LitJSON.Runtime.JsonProperty("display_name")]
            public string DisplayName { get; set; }
        }

        [Serializable]
        private sealed class PrivateSetterPayload
        {
            public int Id { get; private set; }
            public string Name { get; private set; }

            public static PrivateSetterPayload Create(int id, string name)
            {
                return new PrivateSetterPayload
                {
                    Id = id,
                    Name = name,
                };
            }
        }
    }
}
