using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using Benchmark.Cli;

var rootCommand = new RootCommand("Benchmark CLI") { Name = "benchmark" };
var parser = new CommandLineBuilder(rootCommand)
    .UseDefaults()
    .AddMiddleware(async (context, next) =>
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Display.LogError(ex.ToString());
            context.ExitCode = 1;
        }
    }).Build();
return await parser.InvokeAsync(args);