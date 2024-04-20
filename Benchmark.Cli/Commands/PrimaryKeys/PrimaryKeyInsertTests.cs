using Benchmark.Cli.Binders;
using Benchmark.Data;
using Benchmark.Data.Entities.KeyTypes;
using BenchmarkDotNet.Attributes;

namespace Benchmark.Cli.Commands.PrimaryKeys;

[InProcess]
public class PrimaryKeyInsertTests : BaseEntityFrameworkBenchmark
{

   
    public const string StaticText = "Created By Insert Test";

    [Benchmark(Description = "Additional Warmup")]
    public List<string?> AdditionalWarmup() => Context.ShortPrimaryKeyEntities.Select(x => x.Description).Take(1)
        .Union(Context.IntPrimaryKeyEntities.Select(x => x.Description).Take(1))
        .Union(Context.LongPrimaryKeyEntities.Select(x => x.Description).Take(1))
        .Union(Context.GuidPrimaryKeyEntities.Select(x => x.Description).Take(1))
        .Union(Context.StringPrimaryKeyEntities.Select(x => x.Description).Take(1))
        .Union(Context.BytePrimaryKeyEntities.Select(x => x.Description).Take(1))
        .ToList();

    [Benchmark(Description = "Byte(8) - Insert with Child")]
    public void ByteInsertWithChild()
    {
        Context.BytePrimaryKeyEntities.Add(new BytePrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new BytePrimaryKeyChildEntity(description: StaticText)]
        });
        Context.SaveChanges();
    }

    [Benchmark(Description = "Short(16) - Insert with Child")]
    public void ShortInsertWithChild()
    {
        Context.ShortPrimaryKeyEntities.Add(new ShortPrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new ShortPrimaryKeyChildEntity { Description = StaticText }]
        });
        Context.SaveChanges();
    }

    [Benchmark(Description = "Int(32) - Insert with Child")]
    public void IntInsertWithChild()
    {
        Context.IntPrimaryKeyEntities.Add(new IntPrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new IntPrimaryKeyChildEntity { Description = StaticText }]
        });
        Context.SaveChanges();
    }
    [Benchmark(Description = "Long(64) - Insert with Child")]
    public void LongInsertWithChild()
    {
        Context.LongPrimaryKeyEntities.Add(new LongPrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new LongPrimaryKeyChildEntity { Description = StaticText }]
        });
        Context.SaveChanges();
    }

    [Benchmark(Description = "Guid - Insert with Child")]
    public void GuidInsertWithChild()
    {
        Context.GuidPrimaryKeyEntities.Add(new GuidPrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new GuidPrimaryKeyChildEntity { Description = StaticText }]
        });
        Context.SaveChanges();
    }
    [Benchmark(Description = "String - Insert with Child")]
    public void StringInsertWithChild()
    {
        Context.StringPrimaryKeyEntities.Add(new StringPrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new StringPrimaryKeyChildEntity { Description = StaticText }]
        });
        Context.SaveChanges();
    }
}