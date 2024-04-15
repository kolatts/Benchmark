using Benchmark.Cli.Binders;
using Benchmark.Data;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Benchmark.Cli.Commands.PrimaryKeys;

public class PrimaryKeyUpdateTests :BaseEntityFrameworkBenchmark
{

   
    public const string StaticText = "Updated by Update Test";

    [Benchmark(Description = "Byte(8) - Update")]
    public void ByteUpdate()
    {
        Context.BytePrimaryKeyEntities.Where(x => x.Description != StaticText)
            .Take(1)
            .ExecuteUpdate(x => x.SetProperty(entity => entity.Description, StaticText));
    }

    [Benchmark(Description = "Short(16) - Update")]
    public void ShortUpdate()
    {
        Context.ShortPrimaryKeyEntities.Where(x => x.Description != StaticText)
            .Take(1)
            .ExecuteUpdate(x => x.SetProperty(entity => entity.Description, StaticText));
    }

    [Benchmark(Description = "Int(32) - Update")]
    public void IntUpdate()
    {
        Context.IntPrimaryKeyEntities.Where(x => x.Description != StaticText)
            .Take(1)
            .ExecuteUpdate(x => x.SetProperty(entity => entity.Description, StaticText));
    }
    [Benchmark(Description = "Long(64) - Update")]
    public void LongUpdate()
    {
        Context.LongPrimaryKeyEntities.Where(x => x.Description != StaticText)
            .Take(1)
            .ExecuteUpdate(x => x.SetProperty(entity => entity.Description, StaticText));
    }

    [Benchmark(Description = "Guid - Update")]
    public void GuidUpdate()
    {
        Context.GuidPrimaryKeyEntities.Where(x => x.Description != StaticText)
            .Take(1)
            .ExecuteUpdate(x => x.SetProperty(entity => entity.Description, StaticText));
    }
    [Benchmark(Description = "String - Update")]
    public void StringUpdate()
    {
        Context.StringPrimaryKeyEntities.Where(x => x.Description != StaticText)
            .Take(1)
            .ExecuteUpdate(x => x.SetProperty(entity => entity.Description, StaticText));
    }
}