namespace Benchmark.Data.Entities.KeyTypes;

public class LongPrimaryKeyChildEntity : IPrimaryKeyEntity
{
    public long LongPrimaryKeyChildEntityId { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public virtual LongPrimaryKeyEntity Parent { get; set; } = null!;
    public long ParentId { get; set; }
}