using Benchmark.Cli.Binders;
using Benchmark.Data;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Benchmark.Cli.Commands.PrimaryKeys;

public class PrimaryKeyDeleteTests : BaseEntityFrameworkBenchmark
{


    [Benchmark(Description = "Byte(8) - Delete with Child")]
    public void ByteDeleteWithChild()
    {
        Context.BytePrimaryKeyEntities.Take(1).ExecuteDelete();
    }

    [Benchmark(Description = "Short(16) - Delete with Child")]
    public void ShortDeleteWithChild()
    {
        Context.ShortPrimaryKeyEntities.Take(1).ExecuteDelete();

    }

    [Benchmark(Description = "Int(32) - Delete with Child")]
    public void IntDeleteWithChild()
    {
        Context.IntPrimaryKeyEntities.Take(1).ExecuteDelete();

    }
    [Benchmark(Description = "Long(64) - Delete with Child")]
    public void LongDeleteWithChild()
    {
        Context.LongPrimaryKeyEntities.Take(1).ExecuteDelete();

    }

    [Benchmark(Description = "Guid - Delete with Child")]
    public void GuidDeleteWithChild()
    {
        Context.GuidPrimaryKeyEntities.Take(1).ExecuteDelete();

    }
    [Benchmark(Description = "String - Delete with Child")]
    public void StringDeleteWithChild()
    {
        Context.StringPrimaryKeyEntities.Take(1).ExecuteDelete();

    }
}