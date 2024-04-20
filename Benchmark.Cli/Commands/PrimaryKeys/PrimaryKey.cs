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

namespace Benchmark.Cli.Commands.PrimaryKeys
{
    public static class PrimaryKeyCommands
    {
        public static RootCommand AddPrimaryKeyCommand(this RootCommand rootCommand)
        {
            var countArgument = new Argument<int>("count", () => 1000, "Number of parent entities to seed.");

            var childrenCount =
                new Option<int>("--childrenCount", () => 1, "Number of children to seed with each parent.");
            var command = new Command("primary-keys",
                "Runs tests to evaluate performance of different primary key types.")
            {
                countArgument, childrenCount,
            };
            command.SetHandler(RunPrimaryKeyTest, new BenchmarkDbContextBinder(), countArgument, childrenCount);
            rootCommand.Add(command);
            return rootCommand;
        }

        public static void RunPrimaryKeyTest(BenchmarkDbContext context, int count,
            int childrenCount)
        {
            var parameterTable = new Table() { Title = new TableTitle("Parameters") };
            parameterTable.AddColumns("Name", "Value");
            parameterTable.AddRow("Count", count.ToString());
            parameterTable.AddRow("Children Count", childrenCount.ToString());
            AnsiConsole.Write(parameterTable);
            AnsiConsole.Progress().SimpleColumns().Start(progress =>
            {
                var seedTask = progress.AddTask("Seed entities (delete existing)");
                context.SeedPrimaryKeyEntities(true, count, childrenCount);
                seedTask.Complete();
                Display.LogInformation("Seed complete");
            });
            BenchmarkClassHelper.RunBenchmarkMethods<PrimaryKeyDeleteTests>();
            AnsiConsole.Prompt(new ConfirmationPrompt("Press any enter when ready for next test."));
            BenchmarkClassHelper.RunBenchmarkMethods<PrimaryKeyInsertTests>();
            AnsiConsole.Prompt(new ConfirmationPrompt("Press any enter when ready for next test."));
            BenchmarkClassHelper.RunBenchmarkMethods<PrimaryKeySelectTests>();
            AnsiConsole.Prompt(new ConfirmationPrompt("Press any enter when ready for next test."));
            BenchmarkClassHelper.RunBenchmarkMethods<PrimaryKeyUpdateTests>();


            if (context.DatabaseType != DatabaseTypes.SqlServer)
                context.Database.EnsureDeleted();
        }



    }


}
