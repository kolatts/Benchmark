using System.CommandLine.Binding;

namespace Benchmark.Cli.Global;

public static class ConnectionStringOption
{
    private static Option<string> Initialize()
    {
        var option = new Option<string>("--connection-string",  "The SQL connection string to use.");
        option.AddAlias("-c");
        return option;
    }

    public static Option<string> Value { get; } = Initialize();

    public static string? GetGlobalDatabaseTypeOption(this BindingContext bindingContext)
    {
        return bindingContext.ParseResult.GetValueForOption(Value);
    }
}