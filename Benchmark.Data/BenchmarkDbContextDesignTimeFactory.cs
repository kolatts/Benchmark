using Microsoft.EntityFrameworkCore.Design;

namespace Benchmark.Data;
/// <summary>
/// Used by dotnet ef migrations commands.
/// </summary>
public class BenchmarkDbContextDesignTimeFactory : IDesignTimeDbContextFactory<BenchmarkDbContext>
{
    public BenchmarkDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<BenchmarkDbContext>();
        builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Benchmark;Trusted_Connection=True;");
        return new BenchmarkDbContext(builder.Options, DatabaseTypes.LocalDb);
    }
}