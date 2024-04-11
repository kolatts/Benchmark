using System.CommandLine.Binding;
using Benchmark.Cli.Global;
using Benchmark.Data;
using Microsoft.EntityFrameworkCore;

namespace Benchmark.Cli.Binders
{
    /// <summary>
    /// A binder to provide the CLI with different alternate database contexts.
    /// </summary>
    public class BenchmarkDbContextBinder : BinderBase<BenchmarkDbContext>
    {
        protected override BenchmarkDbContext GetBoundValue(BindingContext bindingContext)
        {
            var database = bindingContext.ParseResult.GetValueForOption(DatabaseTypeOption.Value);
            var connectionString = bindingContext.ParseResult.GetValueForOption(ConnectionStringOption.Value);
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                Display.LogWarning("Database Type SQL server will be used because a connection string option was provided.");
                database = DatabaseTypes.SqlServer;
            }

            return new BenchmarkDbContext(GetDbContextOptions(database, connectionString).Options);
        }

        private static DbContextOptionsBuilder<BenchmarkDbContext> GetDbContextOptions(DatabaseTypes databaseType, string? connectionString)
        {
            var builder = new DbContextOptionsBuilder<BenchmarkDbContext>();
            switch (databaseType)
            {
                case DatabaseTypes.SqliteInMemory:
                    return builder.UseSqlite($"DataSource=file:{Guid.NewGuid()}?mode=memory&cache=shared");
                case DatabaseTypes.LocalDb:
                    return builder.UseSqlServer("Server=localhost;Database=Benchmark;Trusted_Connection=True;");
                case DatabaseTypes.SqlServer:
                    return builder.UseSqlServer(connectionString);
                default:
                    throw new ArgumentException($"Unknown database type {databaseType}");
            }
        }
    }
}
