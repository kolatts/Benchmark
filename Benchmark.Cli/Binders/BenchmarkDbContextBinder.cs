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
        /// <summary>
        /// By nature, the <see cref="BenchmarkDbContext"/> will be a singleton, primarily so the Benchmark classes can get a reference to it easily.
        /// </summary>
        public static BenchmarkDbContext? ContextInstance { get; private set; }
        public static Guid InMemoryDatabaseName { get; }= Guid.NewGuid();
        protected override BenchmarkDbContext GetBoundValue(BindingContext bindingContext)
        {
            var database = bindingContext.ParseResult.GetValueForOption(DatabaseTypeOption.Value);
            var connectionString = bindingContext.ParseResult.GetValueForOption(ConnectionStringOption.Value);
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                Display.LogWarning("Database Type SQL server will be used because a connection string option was provided.");
                database = DatabaseTypes.SqlServer;
            }

            AnsiConsole.Progress().SimpleColumns().Start(progress =>
            {
                var initializationTask = progress.AddTask("Initializing BenchmarkDbContext");
                ContextInstance = new BenchmarkDbContext(GetDbContextOptions(database, connectionString).Options);
                initializationTask.Complete();
                var connectionStringServerTask= progress.AddTask($"Server: {ContextInstance.Database.GetDbConnection().DataSource}");
                connectionStringServerTask.Complete();
                var connectionStringDatabaseTask =
                    progress.AddTask($"Database: {ContextInstance.Database.GetDbConnection().Database}");
                connectionStringDatabaseTask.Complete();
                var ensureCreatedTask = progress.AddTask($"Ensuring database is created ({database})");
                ContextInstance.Database.EnsureCreated();
                ensureCreatedTask.Complete();
            });
            return ContextInstance!;
        }


        public static DbContextOptionsBuilder<BenchmarkDbContext> GetDbContextOptions(DatabaseTypes databaseType, string? connectionString)
        {
            var builder = new DbContextOptionsBuilder<BenchmarkDbContext>();
            switch (databaseType)
            {
                case DatabaseTypes.SqliteInMemory:
                    return builder.UseSqlite($"DataSource=file:{InMemoryDatabaseName}?mode=memory&cache=shared");
                case DatabaseTypes.LocalDb:
                    return builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Benchmark;Trusted_Connection=True;");
                case DatabaseTypes.SqlServer:
                    return builder.UseSqlServer(connectionString);
                default:
                    throw new ArgumentException($"Unknown database type {databaseType}");
            }
        }
    }
}
