using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benchmark.Cli.Binders;
using Benchmark.Data;
using Benchmark.Data.Seeding;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;

namespace Benchmark.Cli.Commands
{
    public static class PrimaryKeyCommands
    {
        public static RootCommand AddPrimaryKeyCommand(this RootCommand rootCommand)
        {
            var countArgument = new Argument<int>("count", () => 1000, "Number of parent entities to seed.");
            var seedOption = new Option<int>("--use-seed", () => 1,
                "Uses a specified seed value for randomization of text.");
            var descriptionLengthOption = new Option<int>("--textLength", () => 100,
                "Specifies the length of how much text in the Description fields.");
            var childrenCount =
                new Option<int>("--childrenCount", () => 1, "Number of children to seed with each parent.");
            var command = new Command("primary-keys",
                "Runs tests to evaluate performance of different primary key types.")
            {
                countArgument, seedOption, descriptionLengthOption, childrenCount,
            };
            command.SetHandler(RunPrimaryKeyTest, new BenchmarkDbContextBinder(), countArgument, seedOption, descriptionLengthOption, childrenCount);
            rootCommand.Add(command);
            return rootCommand;
        }

        public static void RunPrimaryKeyTest(BenchmarkDbContext context, int count, int seed, int descriptionLength,
            int childrenCount)
        {
            var parameterTable = new Table() { Title = new TableTitle("Parameters") };
            parameterTable.AddColumns("Name", "Value");
            parameterTable.AddRow("Count", count.ToString());
            parameterTable.AddRow("Seed", seed.ToString());
            parameterTable.AddRow("Description Length", descriptionLength.ToString());
            parameterTable.AddRow("Children Count", childrenCount.ToString());
            AnsiConsole.Write(parameterTable);
            AnsiConsole.Progress().SimpleColumns().Start(progress =>
            {
                var seedTask = progress.AddTask("Seed entities (delete existing)");
                context.SeedPrimaryKeyEntities(true, count, seed, descriptionLength, childrenCount);
                seedTask.Complete();
            });
            BenchmarkRunner.Run<PrimaryKeyTest>();
        }
        [InProcess]
        public class PrimaryKeyTest
        {
            public PrimaryKeyTest()
            {
                if (BenchmarkDbContextBinder.ContextInstance == null)
                    throw new ArgumentException(
                        "The BenchmarkDbContextBinder did not initialize the context instance for use by benchmark classes.");
                _context = BenchmarkDbContextBinder.ContextInstance;
            }
            private readonly BenchmarkDbContext _context;
            public const string StaticText = "Static Text";

            [Benchmark(Description = "Int(32) - Join to Child - Select child data")]
            public List<string?> IntPrimaryKeyJoinToSelectChildData() => _context.IntPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => y.Description))
                .ToList();
            [Benchmark(Description = "Int(32) - Join to Child - Select static text")]
            public List<string> IntPrimaryKeyJoinToSelectStaticText() => _context.IntPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => StaticText))
                .ToList();
            [Benchmark(Description = "Guid - Join to Child - Select child text")]
            public List<string?> GuidPrimaryKeyJoinToSelectChildData() => _context.GuidPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => y.Description))
                .ToList();
            [Benchmark(Description = "Guid - Join to Child - Select static text")]
            public List<string> GuidPrimaryKeyJoinToSelectStaticText() => _context.GuidPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => StaticText))
                .ToList();
            [Benchmark(Description = "String - Join to Child - Select child text")]
            public List<string?> StringPrimaryKeyJoinToSelectChildData() => _context.StringPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => y.Description))
                .ToList();
            [Benchmark(Description = "String - Join to Child - Select static text")]
            public List<string> StringPrimaryKeyJoinToSelectStaticText() => _context.StringPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => StaticText))
                .ToList();
            [Benchmark(Description = "Long(64) - Join to Child - Select child text")]
            public List<string?> LongPrimaryKeyJoinToSelectChildData() => _context.LongPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => y.Description))
                .ToList();
            [Benchmark(Description = "Long(64) - Join to Child - Select static text")]
            public List<string> LongPrimaryKeyJoinToSelectStaticText() => _context.LongPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => StaticText))
                .ToList();

        }
    }
}
