using Benchmark.Data.Entities.KeyTypes;

namespace Benchmark.Data;

public class BenchmarkDbContext(DbContextOptions<BenchmarkDbContext> options) : DbContext(options)
{
    public DbSet<GuidPrimaryKeyEntity> GuidPrimaryKeyEntities { get; set; } = null!;
    public DbSet<GuidPrimaryKeyChildEntity> GuidPrimaryKeyChildEntities { get; set; } = null!;

    public DbSet<StringPrimaryKeyEntity> StringPrimaryKeyEntities { get; set; } = null!;
    public DbSet<StringPrimaryKeyChildEntity> StringPrimaryKeyChildEntities { get; set; } = null!;

    public DbSet<IntPrimaryKeyEntity> IntPrimaryKeyEntities { get; set; } = null!;

    public DbSet<IntPrimaryKeyChildEntity> IntPrimaryKeyChildEntities { get; set; } = null!;

    public DbSet<LongPrimaryKeyEntity> LongPrimaryKeyEntities { get; set; } = null!;
    public DbSet<LongPrimaryKeyChildEntity> LongPrimaryKeyChildEntities { get; set; } = null!;
    public  DbSet<ShortPrimaryKeyEntity> ShortPrimaryKeyEntities { get; set; } = null!;
    public DbSet<ShortPrimaryKeyChildEntity> ShortPrimaryKeyChildEntities { get; set; } = null!;
}