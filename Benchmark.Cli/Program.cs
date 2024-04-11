﻿using System.CommandLine.Parsing;
using Benchmark.Cli;
using Benchmark.Cli.Commands;
using Benchmark.Cli.Global;

var rootCommand = new RootCommand("Benchmark CLI") { Name = "bench" };
//Global Options
rootCommand.AddGlobalOption(DatabaseTypeOption.Value);
rootCommand.AddGlobalOption(ConnectionStringOption.Value);
//Commands
rootCommand.AddPrimaryKeyCommand();


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