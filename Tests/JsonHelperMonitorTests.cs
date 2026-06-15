using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using GameFrameX.LitJSON.Runtime;
using NUnit.Framework;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GameFrameX.Tests
{
    [TestFixture]
    public sealed class JsonHelperMonitorTests
    {
        private const string BenchmarkReportPath = "Reports/json-benchmark/latest.json";
        private const string BreakCheckReportPath = "Reports/json-breakcheck/latest.json";
        private const string MonitorReportDirectory = "Reports/json-monitor";

        [Test]
        public void JsonHelpers_Monitor_GeneratesReport()
        {
            RunMonitor(false);
        }

#if UNITY_EDITOR
        public static void RunFromCommandLine()
        {
            try
            {
                MonitorResult result = RunMonitor(true);
                EditorApplication.Exit(result.Passed ? 0 : 2);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                EditorApplication.Exit(1);
            }
        }
#endif

        public static MonitorResult RunMonitor(bool enforceGate)
        {
            JsonHelperBenchmarkTests.RunBenchmark();
            JsonHelperBreakCheckTests.RunBreakCheck();

            string root = Path.Combine(Application.dataPath, "..");
            JsonData benchmarkReport = JsonMapper.ToObject(File.ReadAllText(Path.Combine(root, BenchmarkReportPath), Encoding.UTF8));
            JsonData breakCheckReport = JsonMapper.ToObject(File.ReadAllText(Path.Combine(root, BreakCheckReportPath), Encoding.UTF8));

            MonitorResult result = Evaluate(benchmarkReport, breakCheckReport);
            WriteReports(result, enforceGate);
            return result;
        }

        private static MonitorResult Evaluate(JsonData benchmarkReport, JsonData breakCheckReport)
        {
            var result = new MonitorResult();
            result.GeneratedAtUtc = DateTime.UtcNow.ToString("O");
            result.UnityVersion = Application.unityVersion;
            result.Platform = Application.platform.ToString();

            foreach (JsonData item in (JsonData)breakCheckReport["results"])
            {
                string status = (string)item["status"];
                string severity = (string)item["severity"];
                if (status == "Match")
                {
                    continue;
                }

                result.BreakCheckDifferences++;
                if (severity == "High")
                {
                    result.HighSeverityDifferences++;
                    result.Failures.Add("高危兼容性差异：" + (string)item["caseName"] + "（" + ToChineseStatus(status) + "）");
                }
            }

            foreach (JsonData item in (JsonData)benchmarkReport["results"])
            {
                string helper = (string)item["helper"];
                if (helper != "LitJSON")
                {
                    continue;
                }

                var comparison = new BenchmarkComparison
                {
                    CaseName = (string)item["caseName"],
                    Scenario = (string)item["scenario"],
                    Operation = (string)item["operation"],
                    Phase = (string)item["phase"],
                    LitJsonAverageMicroseconds = GetDouble(item["averageMicroseconds"]),
                    LitJsonMemoryDeltaBytesPerOperation = GetDouble(item["memoryDeltaBytesPerOperation"]),
                    LitJsonAllocatedBytesPerOperation = GetDouble(item["allocatedBytesPerOperation"]),
                    Iterations = GetInt(item["iterations"]),
                    JsonLength = GetInt(item["jsonLength"]),
                };
                result.BenchmarkComparisons.Add(comparison);
            }

            result.Passed = result.BreakCheckDifferences == 0;
            return result;
        }

        private static double GetDouble(JsonData value)
        {
            return Convert.ToDouble(value.ToString(), CultureInfo.InvariantCulture);
        }

        private static int GetInt(JsonData value)
        {
            return Convert.ToInt32(value.ToString(), CultureInfo.InvariantCulture);
        }

        private static void WriteReports(MonitorResult result, bool enforceGate)
        {
            string directory = Path.Combine(Application.dataPath, "..", MonitorReportDirectory);
            Directory.CreateDirectory(directory);

            string jsonPath = Path.Combine(directory, "latest.json");
            string markdownPath = Path.Combine(directory, "latest.md");

            File.WriteAllText(jsonPath, BuildJsonReport(result, enforceGate), Encoding.UTF8);
            File.WriteAllText(markdownPath, BuildMarkdownReport(result, enforceGate), Encoding.UTF8);

            Debug.Log("JSON monitor report written to: " + jsonPath);
        }

        private static string BuildJsonReport(MonitorResult result, bool enforceGate)
        {
            var builder = new StringBuilder();
            builder.AppendLine("{");
            builder.AppendLine("  \"schemaVersion\": 1,");
            builder.AppendLine("  \"generatedAtUtc\": \"" + EscapeJson(result.GeneratedAtUtc) + "\",");
            builder.AppendLine("  \"enforceGate\": " + (enforceGate ? "true" : "false") + ",");
            builder.AppendLine("  \"passed\": " + (result.Passed ? "true" : "false") + ",");
            builder.AppendLine("  \"policy\": {");
            builder.AppendLine("    \"performanceGateEnabled\": false,");
            builder.AppendLine("    \"compatibilityDifferencesAllowed\": 0");
            builder.AppendLine("  },");
            builder.AppendLine("  \"summary\": {");
            builder.AppendLine("    \"breakCheckDifferences\": " + result.BreakCheckDifferences + ",");
            builder.AppendLine("    \"highSeverityDifferences\": " + result.HighSeverityDifferences + ",");
            builder.AppendLine("    \"performanceRegressions\": " + result.PerformanceRegressions);
            builder.AppendLine("  },");
            builder.AppendLine("  \"failures\": [");
            for (int i = 0; i < result.Failures.Count; i++)
            {
                builder.Append("    \"");
                builder.Append(EscapeJson(result.Failures[i]));
                builder.AppendLine(i == result.Failures.Count - 1 ? "\"" : "\",");
            }

            builder.AppendLine("  ],");
            builder.AppendLine("  \"benchmarkComparisons\": [");
            for (int i = 0; i < result.BenchmarkComparisons.Count; i++)
            {
                BenchmarkComparison comparison = result.BenchmarkComparisons[i];
                builder.AppendLine("    {");
                builder.AppendLine("      \"caseName\": \"" + EscapeJson(comparison.CaseName) + "\",");
                builder.AppendLine("      \"scenario\": \"" + EscapeJson(comparison.Scenario) + "\",");
                builder.AppendLine("      \"operation\": \"" + EscapeJson(comparison.Operation) + "\",");
                builder.AppendLine("      \"phase\": \"" + EscapeJson(comparison.Phase) + "\",");
                builder.AppendLine("      \"iterations\": " + comparison.Iterations + ",");
                builder.AppendLine("      \"litJsonAverageMicroseconds\": " + comparison.LitJsonAverageMicroseconds.ToString("F4", CultureInfo.InvariantCulture) + ",");
                builder.AppendLine("      \"litJsonMemoryDeltaBytesPerOperation\": " + comparison.LitJsonMemoryDeltaBytesPerOperation.ToString("F2", CultureInfo.InvariantCulture) + ",");
                builder.AppendLine("      \"litJsonAllocatedBytesPerOperation\": " + comparison.LitJsonAllocatedBytesPerOperation.ToString("F2", CultureInfo.InvariantCulture) + ",");
                builder.AppendLine("      \"jsonLength\": " + comparison.JsonLength);
                builder.Append("    }");
                builder.AppendLine(i == result.BenchmarkComparisons.Count - 1 ? string.Empty : ",");
            }

            builder.AppendLine("  ]");
            builder.AppendLine("}");
            return builder.ToString();
        }

        private static string BuildMarkdownReport(MonitorResult result, bool enforceGate)
        {
            var builder = new StringBuilder();
            builder.AppendLine("# JSON 辅助器监控门禁报告");
            builder.AppendLine();
            builder.AppendLine("- 生成时间（UTC）：`" + result.GeneratedAtUtc + "`");
            builder.AppendLine("- Unity 版本：`" + result.UnityVersion + "`");
            builder.AppendLine("- 运行平台：`" + result.Platform + "`");
            builder.AppendLine("- 是否启用门禁：`" + (enforceGate ? "是" : "否") + "`");
            builder.AppendLine("- 结果：`" + (result.Passed ? "通过" : "失败") + "`");
            builder.AppendLine("- 策略：兼容性差异 = `0`；性能仅观察，不作为门禁");
            builder.AppendLine();
            builder.AppendLine("## 汇总");
            builder.AppendLine();
            builder.AppendLine("| 指标 | 数值 |");
            builder.AppendLine("|---|---:|");
            builder.AppendLine("| 兼容性差异 | " + result.BreakCheckDifferences + " |");
            builder.AppendLine("| 高危兼容性差异 | " + result.HighSeverityDifferences + " |");
            builder.AppendLine("| 性能退化项 | " + result.PerformanceRegressions + " |");
            builder.AppendLine();
            builder.AppendLine("## 性能观察");
            builder.AppendLine();
            builder.AppendLine("| 场景 | 用例 | 阶段 | 操作 | 迭代次数 | LitJSON 平均 us | 每次内存差值 | 每次分配字节 | JSON 长度 |");
            builder.AppendLine("|---|---|---|---|---:|---:|---:|---:|---:|");

            foreach (BenchmarkComparison comparison in result.BenchmarkComparisons)
            {
                builder.Append("| ");
                builder.Append(comparison.Scenario);
                builder.Append(" | ");
                builder.Append(comparison.CaseName);
                builder.Append(" | ");
                builder.Append(ToChinesePhase(comparison.Phase));
                builder.Append(" | ");
                builder.Append(ToChineseOperation(comparison.Operation));
                builder.Append(" | ");
                builder.Append(comparison.Iterations);
                builder.Append(" | ");
                builder.Append(comparison.LitJsonAverageMicroseconds.ToString("F4", CultureInfo.InvariantCulture));
                builder.Append(" | ");
                builder.Append(comparison.LitJsonMemoryDeltaBytesPerOperation.ToString("F2", CultureInfo.InvariantCulture));
                builder.Append(" | ");
                builder.Append(comparison.LitJsonAllocatedBytesPerOperation.ToString("F2", CultureInfo.InvariantCulture));
                builder.Append(" | ");
                builder.Append(comparison.JsonLength);
                builder.AppendLine(" |");
            }

            if (result.Failures.Count > 0)
            {
                builder.AppendLine();
                builder.AppendLine("## 失败原因");
                builder.AppendLine();
                foreach (string failure in result.Failures)
                {
                    builder.Append("- ");
                    builder.AppendLine(failure);
                }
            }

            return builder.ToString();
        }

        private static string ToChineseStatus(string status)
        {
            if (status == "ValueDifference")
            {
                return "值差异";
            }

            if (status == "ExceptionDifference")
            {
                return "异常差异";
            }

            if (status == "LitJsonFailed")
            {
                return "LitJSON 失败";
            }

            if (status == "Match")
            {
                return "匹配";
            }

            return status;
        }

        private static string ToChineseOperation(string operation)
        {
            if (operation == "serialize")
            {
                return "序列化";
            }

            if (operation == "deserialize")
            {
                return "反序列化";
            }

            return operation;
        }

        private static string ToChinesePhase(string phase)
        {
            if (phase == "cold")
            {
                return "冷启动";
            }

            if (phase == "hot")
            {
                return "热路径";
            }

            return phase;
        }

        private static string EscapeJson(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n");
        }

        public sealed class MonitorResult
        {
            public string GeneratedAtUtc;
            public string UnityVersion;
            public string Platform;
            public bool Passed;
            public int BreakCheckDifferences;
            public int HighSeverityDifferences;
            public int PerformanceRegressions;
            public readonly List<string> Failures = new List<string>();
            public readonly List<BenchmarkComparison> BenchmarkComparisons = new List<BenchmarkComparison>();
        }

        public sealed class BenchmarkComparison
        {
            public string CaseName;
            public string Scenario;
            public string Operation;
            public string Phase;
            public int Iterations;
            public double LitJsonAverageMicroseconds;
            public double LitJsonMemoryDeltaBytesPerOperation;
            public double LitJsonAllocatedBytesPerOperation;
            public int JsonLength;
        }
    }
}
