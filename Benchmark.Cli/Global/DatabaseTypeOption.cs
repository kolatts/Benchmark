using System.CommandLine.Binding;
using Benchmark.Data;

namespace Benchmark.Cli.Global
{
    public static class DatabaseTypeOption
    {
        private static Option<DatabaseTypes> Initialize()
        {
            var option = new Option<DatabaseTypes>("--db", () => DatabaseTypes.SqlServerContainer, "The database type to target.");
            option.AddAlias("-d");
            return option;
        }

        public static Option<DatabaseTypes> Value { get; } = Initialize();

        public static DatabaseTypes GetGlobalDatabaseTypeOption(this BindingContext bindingContext)
        {
            return bindingContext.ParseResult.GetValueForOption(Value);
        }
    }

}
