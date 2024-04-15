using Benchmark.Cli.Binders;
using Benchmark.Data;
using Benchmark.Data.Entities.KeyTypes;
using BenchmarkDotNet.Attributes;

namespace Benchmark.Cli.Commands.PrimaryKeys;

public class PrimaryKeyInsertTests
{

    private BenchmarkDbContext _context;
    [GlobalSetup]
    public void Setup()
    {
        _context = BenchmarkDbContextBinder.GetDbContextFromEnvironmentVariables();
    }
    public const string StaticText = "Created By Insert Test";

    [Benchmark(Description = "Byte(8) - Insert with Child")]
    public void ByteInsertWithChild()
    {
        _context.BytePrimaryKeyEntities.Add(new BytePrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new BytePrimaryKeyChildEntity(description: StaticText)]
        });
        _context.SaveChanges();
    }

    [Benchmark(Description = "Short(16) - Insert with Child")]
    public void ShortInsertWithChild()
    {
        _context.ShortPrimaryKeyEntities.Add(new ShortPrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new ShortPrimaryKeyChildEntity { Description = StaticText }]
        });
        _context.SaveChanges();
    }

    [Benchmark(Description = "Int(32) - Insert with Child")]
    public void IntInsertWithChild()
    {
        _context.IntPrimaryKeyEntities.Add(new IntPrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new IntPrimaryKeyChildEntity { Description = StaticText }]
        });
        _context.SaveChanges();
    }
    [Benchmark(Description = "Long(64) - Insert with Child")]
    public void LongInsertWithChild()
    {
        _context.LongPrimaryKeyEntities.Add(new LongPrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new LongPrimaryKeyChildEntity { Description = StaticText }]
        });
        _context.SaveChanges();
    }

    [Benchmark(Description = "Guid - Insert with Child")]
    public void GuidInsertWithChild()
    {
        _context.GuidPrimaryKeyEntities.Add(new GuidPrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new GuidPrimaryKeyChildEntity { Description = StaticText }]
        });
        _context.SaveChanges();
    }
    [Benchmark(Description = "String - Insert with Child")]
    public void StringInsertWithChild()
    {
        _context.StringPrimaryKeyEntities.Add(new StringPrimaryKeyEntity()
        {
            Description = StaticText,
            Children = [new StringPrimaryKeyChildEntity { Description = StaticText }]
        });
        _context.SaveChanges();
    }
}