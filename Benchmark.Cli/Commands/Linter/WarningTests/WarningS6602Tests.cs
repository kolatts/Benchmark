using BenchmarkDotNet.Attributes;
using Bogus;

namespace Benchmark.Cli.Commands.Linter.WarningTests;

[InProcess]
public class WarningS6602Tests : WarningTest
{
    public override Linter.Warnings Warning => Linter.Warnings.S6602;

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
        _searchValue = _list.Skip((N/2)).First().Text;
    }

    private List<Item>? _list;
    private Item[]? _array;
    private string _searchValue = string.Empty;

    [Benchmark(Description = "Find - List")]
    public Item? FindList() => _list!.Find(x => x.Text == _searchValue);

    [Benchmark(Description = "Find - Array")]
    public Item? FindArray() => Array.Find(_array!, x => x.Text == _searchValue);

    [Benchmark(Description = "FirstOrDefault - List")]
    public Item? FirstOrDefaultList() => _list!.FirstOrDefault(x => x.Text == _searchValue);

    [Benchmark(Description = "FirstOrDefault - Array")]
    public Item? FirstOrDefaultArray() => _array!.FirstOrDefault(x => x.Text == _searchValue);

}