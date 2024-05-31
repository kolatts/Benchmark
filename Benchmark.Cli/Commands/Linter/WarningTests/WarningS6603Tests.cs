using BenchmarkDotNet.Attributes;
using Bogus;

namespace Benchmark.Cli.Commands.Linter.WarningTests;

[InProcess]
public class WarningS6603Tests : WarningTest
{
    public override Linter.Warnings Warning => Linter.Warnings.S6603;

    [Params(10, 1000, 100000, 1000000)]
    public int N;
    private sealed class Item
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

    [Benchmark(Description = "True For All List")]
    public bool TrueForAllList() => _list!.TrueForAll(x => x.Text.Length == 10);

    [Benchmark(Description = "True For All Array")]
    public bool TrueForAllArray() => Array.TrueForAll(_array!, x => x.Text.Length == 10);

    [Benchmark(Description = "All List")]
    public bool AllList() => _list!.All(x => x.Text.Length == 10);

    [Benchmark(Description = "All Array")]
    public bool AllArray() => _array!.All(x => x.Text.Length == 10);

}