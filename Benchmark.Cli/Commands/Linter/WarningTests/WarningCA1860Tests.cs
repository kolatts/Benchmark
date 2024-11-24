using BenchmarkDotNet.Attributes;
using Bogus;


namespace Benchmark.Cli.Commands.Linter.WarningTests;


[InProcess]
public class WarningCA1860Tests : WarningTest
{
    public override Linter.Warnings Warning => Linter.Warnings.CA1860;

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

    [Benchmark(Description = "Count == 0 - List")]
    public bool ListCountIsZero() => _list!.Count == 0;

    [Benchmark(Description = "Length == 0 - Array")]
    public bool ArrayCountIsZero() => _array!.Length == 0;

    [Benchmark(Description = "Enumerable.Any() - List")]
    public bool VariantList() => _list!.Any();

    [Benchmark(Description = "Enumerable.Any() - Array")]
    public bool VariantArray() => _array!.Any();

}