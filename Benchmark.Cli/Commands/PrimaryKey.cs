using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benchmark.Cli.Binders;
using Benchmark.Data;
using Benchmark.Data.Entities.KeyTypes;
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
            var countArgument = new Argument<int>("count", () => 255, "Number of parent entities to seed.");
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
            var test = context.IntPrimaryKeyEntities.Count();
            Display.LogInformation(test.ToString());
            AnsiConsole.Progress().SimpleColumns().Start(progress =>
            {
                var seedTask = progress.AddTask("Seed entities (delete existing)");
                context.SeedPrimaryKeyEntities(true, count, seed, descriptionLength, childrenCount);
                seedTask.Complete();
                Display.LogInformation("Seed complete");
            });
            BenchmarkRunner.Run<PrimaryKeySelectTests>();
            AnsiConsole.Prompt(new ConfirmationPrompt("Press any enter when ready for next test."));
            BenchmarkRunner.Run<PrimaryKeyInsertTests>();
            if (context.DatabaseType != DatabaseTypes.SqlServer)
                context.Database.EnsureDeleted();
        }
        public class PrimaryKeySelectTests
        {

            private BenchmarkDbContext _context;
            [GlobalSetup]
            public void Setup()
            {
                _context = BenchmarkDbContextBinder.GetDbContextFromEnvironmentVariables();
            }
            public const string StaticText = "Static Text";
            [Benchmark(Description = "Byte(8) - Join to Child - Select child text")]
            public List<string?> BytePrimaryKeyJoinToSelectChildData() => _context.BytePrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => y.Description))
                .ToList();
            [Benchmark(Description = "Short(16) - Join to Child - Select child text")]
            public List<string?> ShortPrimaryKeyJoinToSelectChildData() => _context.ShortPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => y.Description))
                .ToList();
            [Benchmark(Description = "Int(32) - Join to Child - Select child text")]
            public List<string?> IntPrimaryKeyJoinToSelectChildData() => _context.IntPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => y.Description))
                .ToList();
            [Benchmark(Description = "Long(64) - Join to Child - Select child text")]
            public List<string?> LongPrimaryKeyJoinToSelectChildData() => _context.LongPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => y.Description))
                .ToList();
            [Benchmark(Description = "Guid - Join to Child - Select child text")]
            public List<string?> GuidPrimaryKeyJoinToSelectChildData() => _context.GuidPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => y.Description))
                .ToList();
            [Benchmark(Description = "String - Join to Child - Select child text")]
            public List<string?> StringPrimaryKeyJoinToSelectChildData() => _context.StringPrimaryKeyEntities
                .SelectMany(x => x.Children.Select(y => y.Description))
                .ToList();
        }

        public class PrimaryKeyInsertTests
        {

            private BenchmarkDbContext _context;
            [GlobalSetup]
            public void Setup()
            {
                _context = BenchmarkDbContextBinder.GetDbContextFromEnvironmentVariables();
            }
            public const string StaticText = "Static Text";

            [Benchmark(Description = "Byte(8) - Insert with Child")]
            public void ByteInsertWithChild()
            {
                _context.BytePrimaryKeyEntities.Add(new BytePrimaryKeyEntity()
                {
                    Description = StaticText,
                    Children = [new BytePrimaryKeyChildEntity(description: StaticText)]
                });
                _context.SaveChanges();
            }

            [Benchmark(Description = "Short(16) - Insert with Child")]
            public void ShortInsertWithChild()
            {
                _context.ShortPrimaryKeyEntities.Add(new ShortPrimaryKeyEntity()
                {
                    Description = StaticText,
                    Children = [new ShortPrimaryKeyChildEntity { Description = StaticText}]
                });
                _context.SaveChanges();
            }

            [Benchmark(Description = "Int(32) - Insert with Child")]
            public void IntInsertWithChild()
            {
                _context.IntPrimaryKeyEntities.Add(new IntPrimaryKeyEntity()
                {
                    Description = StaticText,
                    Children = [new IntPrimaryKeyChildEntity { Description = StaticText }]
                });
                _context.SaveChanges();
            }
            [Benchmark(Description = "Long(64) - Insert with Child")]
            public void LongInsertWithChild()
            {
                _context.LongPrimaryKeyEntities.Add(new LongPrimaryKeyEntity()
                {
                    Description = StaticText,
                    Children = [new LongPrimaryKeyChildEntity { Description = StaticText }]
                });
                _context.SaveChanges();
            }

            [Benchmark(Description = "Guid - Insert with Child")]
            public void GuidPrimaryKeyJoinToSelectChildData()
            {
                _context.GuidPrimaryKeyEntities.Add(new GuidPrimaryKeyEntity()
                {
                    Description = StaticText,
                    Children = [new GuidPrimaryKeyChildEntity { Description = StaticText }]
                });
                _context.SaveChanges();
            }
            [Benchmark(Description = "String - Join to Child - Select child text")]
            public void StringPrimaryKeyJoinToSelectChildData()
            {
                _context.StringPrimaryKeyEntities.Add(new StringPrimaryKeyEntity()
                {
                    Description = StaticText,
                    Children = [new StringPrimaryKeyChildEntity { Description = StaticText }]
                });
                _context.SaveChanges();
            }
        }

    }
}
