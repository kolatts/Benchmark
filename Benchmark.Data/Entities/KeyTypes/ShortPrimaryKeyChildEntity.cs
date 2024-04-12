namespace Benchmark.Data.Entities.KeyTypes;

public class ShortPrimaryKeyChildEntity : IPrimaryKeyChildEntity<short>
{
    public int Id { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public virtual ShortPrimaryKeyEntity Parent { get; set; } = null!;
    public short ParentId { get; set; }
}