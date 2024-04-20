using Benchmark.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmark.Cli
{
    public static class BenchmarkClassHelper
    {
        /// <summary>
        /// We want a simple run of the methods - with no warmup.
        /// SQL and/or EF Context caching will likely skew the results of the benchmark dotnet typical runs.
        /// </summary>
        /// <typeparam name="TBenchmarkClass"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, TimeSpan> RunBenchmarkMethods<TBenchmarkClass>()
        {
            Dictionary<string, TimeSpan> benchmarkResults = new Dictionary<string, TimeSpan>();

            Type benchmarkType = typeof(TBenchmarkClass);
            MethodInfo[] methods = benchmarkType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetCustomAttributes(typeof(BenchmarkAttribute), false).Length > 0 && m.GetParameters().Length == 0)
                .ToArray();
            var benchmarkInstance = Activator.CreateInstance(benchmarkType);
            if (benchmarkInstance is BaseEntityFrameworkBenchmark efBase)
            {
                efBase.Warmup();
            }
            foreach (MethodInfo method in methods)
            {
                BenchmarkAttribute? benchmarkAttribute = method.GetCustomAttribute<BenchmarkAttribute>();
                string? description = benchmarkAttribute?.Description;

                DateTime startTime = DateTime.Now;
                try
                {
                    method.Invoke(benchmarkInstance, null);
                    TimeSpan executionTime = DateTime.Now - startTime;
                    benchmarkResults.Add(description ?? "Unknown", executionTime);
                }
                catch (Exception ex)
                {
                    Display.LogError(ex, $"Failed to run initial test on {description}");
                    benchmarkResults.Add($"{description} (failed)", TimeSpan.Zero);

                }
               
            }
            Display.LogInformation("Completed initial dry run of tests, now starting benchmark dotnet tests.");
            BenchmarkRunner.Run<TBenchmarkClass>();
            Display.LogToTable(benchmarkResults, $"Initial Run Results - {benchmarkType.Name}");
            return benchmarkResults;
        }

    }

}
