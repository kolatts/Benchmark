using BenchmarkDotNet.Attributes;
using Bogus;

namespace Benchmark.Cli.Commands.Linter.WarningTests;

[InProcess]
public class WarningS6608Tests : WarningTest
{
    public override Linter.Warnings Warning => Linter.Warnings.S6608;

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

    [Benchmark(Description = "Index 0 - List")]
    public Item FindList() => _list![0];

    [Benchmark(Description = "Index 0 - Array")]
    public Item FindArray() => _array![0];

    [Benchmark(Description = "First - List")]
    public Item FirstOrDefaultList() => _list!.First(x => x.Text == _searchValue);

    [Benchmark(Description = "First - Array")]
    public Item FirstOrDefaultArray() => _array!.First(x => x.Text == _searchValue);

}