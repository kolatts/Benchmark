using System.CommandLine.Binding;
using Benchmark.Data;
using Microsoft.EntityFrameworkCore;

namespace Benchmark.Cli.Binders
{
    public class BenchmarkDbContextBinder : BinderBase<BenchmarkDbContext>
    {
        protected override BenchmarkDbContext GetBoundValue(BindingContext bindingContext)
        {
            var options  = new DbContextOptionsBuilder<BenchmarkDbContext>()
                .UseSqlServer("Server=localhost;Database=Benchmark;Trusted_Connection=True;")
                .Options;
            return new BenchmarkDbContext(options);
        }
    }
}
