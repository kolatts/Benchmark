using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benchmark.Cli.Binders;
using Benchmark.Data;
using Microsoft.EntityFrameworkCore;

namespace Benchmark.Cli
{
    public class BaseEntityFrameworkBenchmark
    {
        protected BenchmarkDbContext Context { get; set; } = BenchmarkDbContextBinder.GetDbContextFromEnvironmentVariables();

        public void Warmup()
        {
            Context.Database.ExecuteSql($"SELECT 1");
            var result = Context.WarmupEntities.ToList();

            Context.BytePrimaryKeyEntities.SelectMany(x => x.Children).Select(x => x.Description).Take(10).ToList();
            Context.ShortPrimaryKeyEntities.SelectMany(x => x.Children).Select(x => x.Description).Take(10).ToList();
            Context.IntPrimaryKeyEntities.SelectMany(x => x.Children).Select(x => x.Description).Take(10).ToList();
            Context.LongPrimaryKeyEntities.SelectMany(x => x.Children).Select(x => x.Description).Take(10).ToList();
            Context.GuidPrimaryKeyEntities.SelectMany(x => x.Children).Select(x => x.Description).Take(10).ToList();
            Context.StringPrimaryKeyEntities.SelectMany(x => x.Children).Select(x => x.Description).Take(10).ToList();

            if (result.Any())
                Display.LogInformation("Warmed up BenchmarkDbContext.");
        }

    }
}
