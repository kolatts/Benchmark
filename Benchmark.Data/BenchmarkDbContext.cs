using Benchmark.Data.Entities;
using Benchmark.Data.Entities.KeyTypes;

namespace Benchmark.Data;

public class BenchmarkDbContext(DbContextOptions<BenchmarkDbContext> options, DatabaseTypes databaseType) : DbContext(options)
{
    public DatabaseTypes DatabaseType { get; } = databaseType;

    public DbSet<WarmupEntity> WarmupEntities { get; set; }
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

    public DbSet<BytePrimaryKeyEntity> BytePrimaryKeyEntities { get; set; } = null!;
    public DbSet<BytePrimaryKeyChildEntity> BytePrimaryKeyChildEntities { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WarmupEntity>().HasData(Enumerable.Range(1, 1000).Select(x => new WarmupEntity() {Id = x}));
    }
}