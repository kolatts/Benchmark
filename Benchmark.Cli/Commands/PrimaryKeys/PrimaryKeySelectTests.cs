using Benchmark.Cli.Binders;
using Benchmark.Data;
using BenchmarkDotNet.Attributes;

namespace  Benchmark.Cli.Commands.PrimaryKeys;

public class PrimaryKeySelectTests : BaseEntityFrameworkBenchmark
{


    public const string StaticText = "Static Text";
   
    [Benchmark(Description = "Short(16) - Select from child (join)")]
    public List<string?> ShortSelectWithChild() => Context.ShortPrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .ToList();
    [Benchmark(Description = "Int(32) - Select from child (join)")]
    public List<string?> IntSelectWithChild() => Context.IntPrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .ToList();
    [Benchmark(Description = "Long(64) - Select from child (join)")]
    public List<string?> LongSelectWithChild() => Context.LongPrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .ToList();
    [Benchmark(Description = "Guid - Select from child (join)")]
    public List<string?> GuidSelectWithChild() => Context.GuidPrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .ToList();
    [Benchmark(Description = "String - Select from child (join)")]
    public List<string?> StringSelectWithChild() => Context.StringPrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .ToList();
    [Benchmark(Description = "Byte(8) - Select from child (join)")]
    public List<string?> ByteSelectWithChild() => Context.BytePrimaryKeyEntities
        .SelectMany(x => x.Children.Select(y => y.Description))
        .ToList();
}