using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Benchmark.Cli.Commands.Linter.WarningTests;
using BenchmarkDotNet.Running;

namespace Benchmark.Cli.Commands.Linter
{
    public static class Linter
    {
        public enum Warnings
        {
            S6603,
            S6602,
            S6605,
            S6608,
            CA1829,
            CA1860

        }
        public static RootCommand AddLinter(this RootCommand rootCommand)
        {
            var warningArgument = new Argument<Warnings>("warning", () => Warnings.S6603, "The warning to test the performance of");
            var command = new Command("linter", "Tests performance implications of linter warnings") { warningArgument };
            command.SetHandler((warning) =>
            {
               BenchmarkRunner.Run(GetWarningTestType(warning));
            }, warningArgument);
            rootCommand.Add(command);
            return rootCommand;
        }

        private static Type GetWarningTestType(Warnings warning)
        {
            // Get all types that derive from WarningTest
            var warningTestTypes = Assembly.GetAssembly(typeof(WarningTest))?
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(WarningTest))).ToList() ?? [];

            // Iterate through the warningTestTypes and find the first instance with a matching Warning type
            foreach (var warningTestType in warningTestTypes)
            {
                var warningTestInstance = Activator.CreateInstance(warningTestType) as WarningTest;
                if (warningTestInstance.Warning == warning)
                {
                    return warningTestType;
                }
            }

            throw new NotSupportedException($"Warning of {warning} does not have a test implemented.");
        }
    }
}
