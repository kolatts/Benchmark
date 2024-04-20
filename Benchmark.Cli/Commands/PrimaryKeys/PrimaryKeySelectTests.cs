using Benchmark.Cli.Binders;
using Benchmark.Data;
using BenchmarkDotNet.Attributes;

namespace  Benchmark.Cli.Commands.PrimaryKeys;

[InProcess]
public class PrimaryKeySelectTests : BaseEntityFrameworkBenchmark
{



    [Benchmark(Description = "Additional Warmup")]
    public List<string?> AdditionalWarmup() => Context.ShortPrimaryKeyEntities.Select(x => x.Description).Take(1)
        .Union(Context.IntPrimaryKeyEntities.Select(x => x.Description).Take(1))
        .Union(Context.LongPrimaryKeyEntities.Select(x => x.Description).Take(1))
        .Union(Context.GuidPrimaryKeyEntities.Select(x => x.Description).Take(1))
        .Union(Context.StringPrimaryKeyEntities.Select(x => x.Description).Take(1))
        .Union(Context.BytePrimaryKeyEntities.Select(x => x.Description).Take(1))
        .ToList();
    [Benchmark(Description = "Int(32) - Select from child (join)")]
    public List<string?> IntSelectWithChild() => Context.IntPrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .Take(255)
        .ToList();
    [Benchmark(Description = "Short(16) - Select from child (join)")]
    public List<string?> ShortSelectWithChild() => Context.ShortPrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .Take(255)
        .ToList();

    [Benchmark(Description = "Long(64) - Select from child (join)")]
    public List<string?> LongSelectWithChild() => Context.LongPrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .Take(255)
        .ToList();
    [Benchmark(Description = "Guid - Select from child (join)")]
    public List<string?> GuidSelectWithChild() => Context.GuidPrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .Take(255)
        .ToList();
    [Benchmark(Description = "String - Select from child (join)")]
    public List<string?> StringSelectWithChild() => Context.StringPrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .Take(255)
        .ToList();
    [Benchmark(Description = "Byte(8) - Select from child (join)")]
    public List<string?> ByteSelectWithChild() => Context.BytePrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .Take(255)
        .ToList();
}