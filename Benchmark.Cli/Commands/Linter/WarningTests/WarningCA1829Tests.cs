using BenchmarkDotNet.Attributes;
using Bogus;

namespace Benchmark.Cli.Commands.Linter.WarningTests;

[InProcess]
public class WarningCA1829Tests : WarningTest
{
    public override Linter.Warnings Warning => Linter.Warnings.CA1829;

    [Params(10, 1000, 100000, 1000000)]
    public int N;
    public sealed class Item
    {
        public string Text { get; } = string.Empty;
    }

    [GlobalSetup]
    public void Setup()
    {
        _list = new Faker<Item>()
            .RuleFor(x => x.Text, x => x.Random.AlphaNumeric(10))
            .Generate(N);
        _array = _list
            .ToArray();
    }

    private List<Item>? _list;
    private Item[]? _array;

    [Benchmark(Description = "Count Property - List")]
    public int StandardList() => _list!.Count;

    [Benchmark(Description = "Length Property - Array")]
    public int StandardArray() => _array!.Length;

    [Benchmark(Description = "Enumerable.Count() - List")]
    public int VariantList() => _list!.Count();

    [Benchmark(Description = "Enumerable.Count() - Array")]
    public int VariantArray() => _array!.Count();

}