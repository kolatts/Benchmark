using System.CommandLine.Binding;
using System.CommandLine;
using Benchmark.Data;

namespace Benchmark.Cli.Global
{
    public static class DatabaseOption
    {
        private static Option<DatabaseTypes> Initialize()
        {
            var option = new Option<DatabaseTypes>("--db", () => DatabaseTypes.SqliteInMemory, "The database to target");
            option.AddAlias("-d");
            return option;
        }

        public static Option<DatabaseTypes> Value { get; } = Initialize();

        public static DatabaseTypes GetGlobalEnvironmentOption(this BindingContext bindingContext)
        {
            return bindingContext.ParseResult.GetValueForOption(Value);
        }
    }
}
