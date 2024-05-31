using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;

namespace Benchmark.Cli.Commands.Linter
{
    public static class Linter
    {
        public enum Warnings
        {
            S6603
        }
        public static RootCommand AddLinter(this RootCommand rootCommand)
        {
            var warningArgument = new Argument<Warnings>("warning", () => Warnings.S6603,"The warning to test the performance of");
            var command = new Command("linter", "Tests performance implications of linter warnings") { warningArgument};
            command.SetHandler((warning) =>
            {
                switch (warning)
                {
                    case Warnings.S6603:
                        BenchmarkRunner.Run<WarningS6603Tests>();
                        break;
                    default:
                        throw new NotSupportedException($"Warning {warning} is not supported yet.");
                }
            }, warningArgument);
            rootCommand.Add(command);
            return rootCommand;
        }
    }
    [InProcess]
    public class WarningS6603Tests
    {
        private sealed class Item
        {
            public string Text { get;  } = string.Empty;
        }
        private List<Item> _list = new Faker<Item>()
            .RuleFor(x=>x.Text, x=>x.Random.AlphaNumeric(10))
            .Generate(1000000);
        private Item[] _array = new Faker<Item>()
            .RuleFor(x => x.Text, x => x.Random.AlphaNumeric(10))
            .Generate(1000000)
            .ToArray();

        [Benchmark(Description="True For All List")]
        public bool TrueForAllList() => _list.TrueForAll(x => x.Text.Length == 10);

        [Benchmark(Description="True For All Array")]
        public bool TrueForAllArray() => Array.TrueForAll(_array, x => x.Text.Length == 10);

        [Benchmark(Description = "All List")]
        public bool AllList() => _list.All(x => x.Text.Length == 10);

        [Benchmark(Description = "All Array")]
        public bool AllArray() => _array.All(x => x.Text.Length == 10);
    }
}
