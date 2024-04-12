namespace Benchmark.Data.Entities.KeyTypes;

public class LongPrimaryKeyEntity : IPrimaryKeyEntity<long>
{
    public long Id { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<LongPrimaryKeyChildEntity> Children { get; set; } = new();
}