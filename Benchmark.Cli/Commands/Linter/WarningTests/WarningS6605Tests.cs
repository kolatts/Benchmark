using BenchmarkDotNet.Attributes;
using Bogus;

namespace Benchmark.Cli.Commands.Linter.WarningTests;

[InProcess]
public class WarningS6605Tests : WarningTest
{
    public override Linter.Warnings Warning => Linter.Warnings.S6605;

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
        _searchValue = _list.Skip((N / 2)).First().Text;
    }

    private List<Item>? _list;
    private Item[]? _array;
    private string _searchValue;

    [Benchmark(Description = "Exists List")]
    public bool ExistsList() => _list!.Exists(x => x.Text == _searchValue);

    [Benchmark(Description = "Exists Array")]
    public bool ExistsArray() => Array.Exists(_array!, x => x.Text == _searchValue);

    [Benchmark(Description = "Any List")]
    public bool AnyList() => _list!.Any(x => x.Text == _searchValue);

    [Benchmark(Description = "Any Array")]
    public bool AnyArray() => _array!.Any(x => x.Text == _searchValue);

}