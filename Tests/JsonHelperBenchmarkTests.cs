using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
    public sealed class JsonHelperBenchmarkTests
    {
        private const int WarmupIterations = 64;
        private const int ColdIterations = 1;
        private const int HotPathIterations = 10000;
        private const int SmallIterations = 5000;
        private const int MediumIterations = 1500;
        private const int LargeIterations = 200;
        private const string ReportDirectory = "Reports/json-benchmark";
        private static readonly MethodInfo s_GetAllocatedBytesForCurrentThread = typeof(GC).GetMethod("GetAllocatedBytesForCurrentThread", Type.EmptyTypes);
        private static readonly MethodInfo s_GetTotalAllocatedBytes = typeof(GC).GetMethod("GetTotalAllocatedBytes", Type.EmptyTypes) ?? typeof(GC).GetMethod("GetTotalAllocatedBytesPrecise", Type.EmptyTypes);

        [Test]
        public void JsonHelpers_Benchmark_GeneratesReport()
        {
            RunBenchmark();
        }

#if UNITY_EDITOR
        public static void RunFromCommandLine()
        {
            try
            {
                RunBenchmark();
                EditorApplication.Exit(0);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                EditorApplication.Exit(1);
            }
        }
#endif

        public static void RunBenchmark()
        {
            var helpers = new IJsonBenchmarkHelper[]
            {
                new LitJsonBenchmarkHelper(),
            };

            var cases = new[]
            {
                JsonBenchmarkCase.Create("hot-network-message", "hot-path", CreateNetworkMessagePayload(), HotPathIterations),
                JsonBenchmarkCase.Create("small-login-response", "small-dto", CreateSmallPayload(), SmallIterations),
                JsonBenchmarkCase.Create("batch-item-response", "batch-dto", CreateBatchPayload(), MediumIterations),
                JsonBenchmarkCase.Create("medium-player-profile", "medium-dto", CreateMediumPayload(), MediumIterations),
                JsonBenchmarkCase.Create("large-config-table", "large-config", CreateLargePayload(), LargeIterations),
            };

            var results = new List<JsonBenchmarkResult>();
            foreach (IJsonBenchmarkHelper helper in helpers)
            {
                foreach (JsonBenchmarkCase benchmarkCase in cases)
                {
                    results.Add(RunSerializeBenchmark(helper, benchmarkCase, "cold", ColdIterations, false));
                    results.Add(RunDeserializeBenchmark(helper, benchmarkCase, "cold", ColdIterations, false));
                    results.Add(RunSerializeBenchmark(helper, benchmarkCase, "hot", benchmarkCase.Iterations, true));
                    results.Add(RunDeserializeBenchmark(helper, benchmarkCase, "hot", benchmarkCase.Iterations, true));
                }
            }

            WriteReports(results);
            Assert.IsTrue(results.Count > 0);
        }

        private static JsonBenchmarkResult RunSerializeBenchmark(IJsonBenchmarkHelper helper, JsonBenchmarkCase benchmarkCase, string phase, int iterations, bool warmup)
        {
            if (warmup)
            {
                for (int i = 0; i < WarmupIterations; i++)
                {
                    helper.ToJson(benchmarkCase.Payload);
                }
            }

            long beforeMemory = GC.GetTotalMemory(true);
            long beforeAllocatedBytes = GetAllocatedBytes();
            var stopwatch = Stopwatch.StartNew();
            string lastJson = null;
            for (int i = 0; i < iterations; i++)
            {
                lastJson = helper.ToJson(benchmarkCase.Payload);
            }

            stopwatch.Stop();
            long afterAllocatedBytes = GetAllocatedBytes();
            long afterMemory = GC.GetTotalMemory(true);

            return JsonBenchmarkResult.Create(
                helper.Name,
                benchmarkCase.Name,
                benchmarkCase.Scenario,
                "serialize",
                phase,
                iterations,
                stopwatch.Elapsed.TotalMilliseconds,
                Math.Max(0L, afterMemory - beforeMemory),
                GetDeltaOrUnavailable(beforeAllocatedBytes, afterAllocatedBytes),
                lastJson == null ? 0 : lastJson.Length);
        }

        private static JsonBenchmarkResult RunDeserializeBenchmark(IJsonBenchmarkHelper helper, JsonBenchmarkCase benchmarkCase, string phase, int iterations, bool warmup)
        {
            string json = helper.ToJson(benchmarkCase.Payload);
            Type payloadType = benchmarkCase.Payload.GetType();

            if (warmup)
            {
                for (int i = 0; i < WarmupIterations; i++)
                {
                    helper.ToObject(payloadType, json);
                }
            }

            long beforeMemory = GC.GetTotalMemory(true);
            long beforeAllocatedBytes = GetAllocatedBytes();
            var stopwatch = Stopwatch.StartNew();
            object lastObject = null;
            for (int i = 0; i < iterations; i++)
            {
                lastObject = helper.ToObject(payloadType, json);
            }

            stopwatch.Stop();
            long afterAllocatedBytes = GetAllocatedBytes();
            long afterMemory = GC.GetTotalMemory(true);

            return JsonBenchmarkResult.Create(
                helper.Name,
                benchmarkCase.Name,
                benchmarkCase.Scenario,
                "deserialize",
                phase,
                iterations,
                stopwatch.Elapsed.TotalMilliseconds,
                Math.Max(0L, afterMemory - beforeMemory),
                GetDeltaOrUnavailable(beforeAllocatedBytes, afterAllocatedBytes),
                lastObject == null ? 0 : json.Length);
        }

        private static long GetAllocatedBytes()
        {
            MethodInfo method = s_GetAllocatedBytesForCurrentThread ?? s_GetTotalAllocatedBytes;
            if (method == null)
            {
                return -1L;
            }

            return (long)method.Invoke(null, null);
        }

        private static long GetDeltaOrUnavailable(long before, long after)
        {
            if (before < 0L || after < 0L || (before == 0L && after == 0L))
            {
                return -1L;
            }

            return Math.Max(0L, after - before);
        }

        private static void WriteReports(IReadOnlyList<JsonBenchmarkResult> results)
        {
            string directory = Path.Combine(Application.dataPath, "..", ReportDirectory);
            Directory.CreateDirectory(directory);

            string jsonPath = Path.Combine(directory, "latest.json");
            string markdownPath = Path.Combine(directory, "latest.md");

            File.WriteAllText(jsonPath, BuildJsonReport(results), Encoding.UTF8);
            File.WriteAllText(markdownPath, BuildMarkdownReport(results), Encoding.UTF8);

            Debug.Log("JSON benchmark report written to: " + jsonPath);
        }

        private static string BuildJsonReport(IReadOnlyList<JsonBenchmarkResult> results)
        {
            var builder = new StringBuilder();
            builder.AppendLine("{");
            builder.AppendLine("  \"schemaVersion\": 1,");
            builder.AppendLine("  \"generatedAtUtc\": \"" + DateTime.UtcNow.ToString("O") + "\",");
            builder.AppendLine("  \"environment\": {");
            builder.AppendLine("    \"unityVersion\": \"" + EscapeJson(Application.unityVersion) + "\",");
            builder.AppendLine("    \"platform\": \"" + EscapeJson(Application.platform.ToString()) + "\"");
            builder.AppendLine("  },");
            builder.AppendLine("  \"results\": [");

            for (int i = 0; i < results.Count; i++)
            {
                JsonBenchmarkResult result = results[i];
                builder.AppendLine("    {");
                builder.AppendLine("      \"helper\": \"" + EscapeJson(result.Helper) + "\",");
                builder.AppendLine("      \"caseName\": \"" + EscapeJson(result.CaseName) + "\",");
                builder.AppendLine("      \"scenario\": \"" + EscapeJson(result.Scenario) + "\",");
                builder.AppendLine("      \"operation\": \"" + EscapeJson(result.Operation) + "\",");
                builder.AppendLine("      \"phase\": \"" + EscapeJson(result.Phase) + "\",");
                builder.AppendLine("      \"iterations\": " + result.Iterations + ",");
                builder.AppendLine("      \"totalMilliseconds\": " + result.TotalMilliseconds.ToString("F4", System.Globalization.CultureInfo.InvariantCulture) + ",");
                builder.AppendLine("      \"averageMicroseconds\": " + result.AverageMicroseconds.ToString("F4", System.Globalization.CultureInfo.InvariantCulture) + ",");
                builder.AppendLine("      \"operationsPerSecond\": " + result.OperationsPerSecond.ToString("F2", System.Globalization.CultureInfo.InvariantCulture) + ",");
                builder.AppendLine("      \"memoryDeltaBytes\": " + result.MemoryDeltaBytes + ",");
                builder.AppendLine("      \"memoryDeltaBytesPerOperation\": " + result.MemoryDeltaBytesPerOperation.ToString("F2", System.Globalization.CultureInfo.InvariantCulture) + ",");
                builder.AppendLine("      \"allocatedBytesDelta\": " + result.AllocatedBytesDelta + ",");
                builder.AppendLine("      \"allocatedBytesPerOperation\": " + result.AllocatedBytesPerOperation.ToString("F2", System.Globalization.CultureInfo.InvariantCulture) + ",");
                builder.AppendLine("      \"jsonLength\": " + result.JsonLength);
                builder.Append("    }");
                builder.AppendLine(i == results.Count - 1 ? string.Empty : ",");
            }

            builder.AppendLine("  ]");
            builder.AppendLine("}");
            return builder.ToString();
        }

        private static string BuildMarkdownReport(IReadOnlyList<JsonBenchmarkResult> results)
        {
            var builder = new StringBuilder();
            builder.AppendLine("# JSON 辅助器性能基准报告");
            builder.AppendLine();
            builder.AppendLine("- 生成时间（UTC）：`" + DateTime.UtcNow.ToString("O") + "`");
            builder.AppendLine("- Unity 版本：`" + Application.unityVersion + "`");
            builder.AppendLine("- 运行平台：`" + Application.platform + "`");
            builder.AppendLine("- 预热次数：`" + WarmupIterations + "`");
            builder.AppendLine("- 冷启动迭代：`" + ColdIterations + "`");
            builder.AppendLine();
            builder.AppendLine("| JSON 库 | 场景 | 用例 | 阶段 | 操作 | 迭代次数 | 总耗时 ms | 平均耗时 us | 每秒操作数 | 内存差值 | 每次内存差值 | 分配字节数 | 每次分配字节 | JSON 长度 |");
            builder.AppendLine("|---|---|---|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|");

            foreach (JsonBenchmarkResult result in results)
            {
                builder.Append("| ");
                builder.Append(result.Helper);
                builder.Append(" | ");
                builder.Append(result.Scenario);
                builder.Append(" | ");
                builder.Append(result.CaseName);
                builder.Append(" | ");
                builder.Append(ToChinesePhase(result.Phase));
                builder.Append(" | ");
                builder.Append(ToChineseOperation(result.Operation));
                builder.Append(" | ");
                builder.Append(result.Iterations);
                builder.Append(" | ");
                builder.Append(result.TotalMilliseconds.ToString("F4", System.Globalization.CultureInfo.InvariantCulture));
                builder.Append(" | ");
                builder.Append(result.AverageMicroseconds.ToString("F4", System.Globalization.CultureInfo.InvariantCulture));
                builder.Append(" | ");
                builder.Append(result.OperationsPerSecond.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                builder.Append(" | ");
                builder.Append(result.MemoryDeltaBytes);
                builder.Append(" | ");
                builder.Append(result.MemoryDeltaBytesPerOperation.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                builder.Append(" | ");
                builder.Append(result.AllocatedBytesDelta);
                builder.Append(" | ");
                builder.Append(result.AllocatedBytesPerOperation.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                builder.Append(" | ");
                builder.Append(result.JsonLength);
                builder.AppendLine(" |");
            }

            return builder.ToString();
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

            return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        private static SmallLoginResponse CreateSmallPayload()
        {
            return new SmallLoginResponse
            {
                Code = 0,
                Message = "ok",
                Token = "token-abcdefghijklmnopqrstuvwxyz",
                ServerTime = 1781510400,
            };
        }

        private static NetworkMessage CreateNetworkMessagePayload()
        {
            return new NetworkMessage
            {
                Cmd = 1001,
                Seq = 9527,
                Route = "player.move",
                Body = "{\"x\":12,\"y\":8,\"z\":3}",
                SentAt = DateTime.UtcNow,
            };
        }

        private static BatchItemResponse CreateBatchPayload()
        {
            var response = new BatchItemResponse
            {
                Code = 0,
                Items = new List<ItemInfo>(),
            };

            for (int i = 0; i < 96; i++)
            {
                response.Items.Add(new ItemInfo
                {
                    ItemId = 2000 + i,
                    Count = i + 2,
                    Name = "batch-item-" + i,
                    Tags = new List<string> { "batch", "reward", "slot-" + i % 12 },
                });
            }

            return response;
        }

        private static PlayerProfile CreateMediumPayload()
        {
            var profile = new PlayerProfile
            {
                PlayerId = 10086,
                Name = "BenchmarkPlayer",
                Level = 42,
                Exp = 987654321,
                IsNew = false,
                Attributes = new Dictionary<string, int>
                {
                    { "hp", 1200 },
                    { "mp", 360 },
                    { "attack", 88 },
                    { "defense", 65 },
                },
                Items = new List<ItemInfo>(),
            };

            for (int i = 0; i < 32; i++)
            {
                profile.Items.Add(new ItemInfo
                {
                    ItemId = 1000 + i,
                    Count = i + 1,
                    Name = "item-" + i,
                    Tags = new List<string> { "bag", "stackable", "quality-" + i % 5 },
                });
            }

            return profile;
        }

        private static ConfigTable CreateLargePayload()
        {
            var table = new ConfigTable
            {
                Version = "2026.06.15",
                Rows = new List<ConfigRow>(),
            };

            for (int i = 0; i < 256; i++)
            {
                table.Rows.Add(new ConfigRow
                {
                    Id = i + 1,
                    Key = "config_key_" + i,
                    Name = "Config Name " + i,
                    Enabled = i % 3 != 0,
                    Weight = i * 0.125f,
                    Values = new List<int> { i, i + 1, i + 2, i + 3, i + 4 },
                    Metadata = new Dictionary<string, string>
                    {
                        { "group", "group-" + i % 8 },
                        { "locale", i % 2 == 0 ? "zh-CN" : "en-US" },
                        { "note", "benchmark row " + i },
                    },
                });
            }

            return table;
        }

        private interface IJsonBenchmarkHelper
        {
            string Name { get; }

            string ToJson(object obj);

            object ToObject(Type objectType, string json);
        }

        private sealed class LitJsonBenchmarkHelper : IJsonBenchmarkHelper
        {
            private readonly LitJsonHelper m_Helper = new LitJsonHelper();

            public string Name
            {
                get { return "LitJSON"; }
            }

            public string ToJson(object obj)
            {
                return m_Helper.ToJson(obj);
            }

            public object ToObject(Type objectType, string json)
            {
                return m_Helper.ToObject(objectType, json);
            }
        }

        private sealed class JsonBenchmarkCase
        {
            public string Name;
            public string Scenario;
            public object Payload;
            public int Iterations;

            public static JsonBenchmarkCase Create(string name, string scenario, object payload, int iterations)
            {
                return new JsonBenchmarkCase
                {
                    Name = name,
                    Scenario = scenario,
                    Payload = payload,
                    Iterations = iterations,
                };
            }
        }

        private sealed class JsonBenchmarkResult
        {
            public string Helper;
            public string CaseName;
            public string Scenario;
            public string Operation;
            public string Phase;
            public int Iterations;
            public double TotalMilliseconds;
            public double AverageMicroseconds;
            public double OperationsPerSecond;
            public long MemoryDeltaBytes;
            public double MemoryDeltaBytesPerOperation;
            public long AllocatedBytesDelta;
            public double AllocatedBytesPerOperation;
            public int JsonLength;

            public static JsonBenchmarkResult Create(string helper, string caseName, string scenario, string operation, string phase, int iterations, double totalMilliseconds, long memoryDeltaBytes, long allocatedBytesDelta, int jsonLength)
            {
                return new JsonBenchmarkResult
                {
                    Helper = helper,
                    CaseName = caseName,
                    Scenario = scenario,
                    Operation = operation,
                    Phase = phase,
                    Iterations = iterations,
                    TotalMilliseconds = totalMilliseconds,
                    AverageMicroseconds = totalMilliseconds * 1000d / iterations,
                    OperationsPerSecond = iterations / Math.Max(totalMilliseconds / 1000d, 0.000001d),
                    MemoryDeltaBytes = memoryDeltaBytes,
                    MemoryDeltaBytesPerOperation = memoryDeltaBytes / (double)iterations,
                    AllocatedBytesDelta = allocatedBytesDelta,
                    AllocatedBytesPerOperation = allocatedBytesDelta < 0L ? -1d : allocatedBytesDelta / (double)iterations,
                    JsonLength = jsonLength,
                };
            }
        }

        [Serializable]
        private sealed class NetworkMessage
        {
            public int Cmd { get; set; }
            public int Seq { get; set; }
            public string Route { get; set; }
            public string Body { get; set; }
            public DateTime SentAt { get; set; }
        }

        [Serializable]
        private sealed class SmallLoginResponse
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public string Token { get; set; }
            public long ServerTime { get; set; }
        }

        [Serializable]
        private sealed class PlayerProfile
        {
            public long PlayerId { get; set; }
            public string Name { get; set; }
            public int Level { get; set; }
            public long Exp { get; set; }
            public bool IsNew { get; set; }
            public Dictionary<string, int> Attributes { get; set; }
            public List<ItemInfo> Items { get; set; }
        }

        [Serializable]
        private sealed class ItemInfo
        {
            public int ItemId { get; set; }
            public int Count { get; set; }
            public string Name { get; set; }
            public List<string> Tags { get; set; }
        }

        [Serializable]
        private sealed class BatchItemResponse
        {
            public int Code { get; set; }
            public List<ItemInfo> Items { get; set; }
        }

        [Serializable]
        private sealed class ConfigTable
        {
            public string Version { get; set; }
            public List<ConfigRow> Rows { get; set; }
        }

        [Serializable]
        private sealed class ConfigRow
        {
            public int Id { get; set; }
            public string Key { get; set; }
            public string Name { get; set; }
            public bool Enabled { get; set; }
            public float Weight { get; set; }
            public List<int> Values { get; set; }
            public Dictionary<string, string> Metadata { get; set; }
        }
    }
}
