namespace Benchmark.Data.Entities.KeyTypes;

public class LongPrimaryKeyChildEntity : IPrimaryKeyChildEntity<long>
{
    public int Id { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public virtual LongPrimaryKeyEntity Parent { get; set; } = null!;
    public long ParentId { get; set; }
}