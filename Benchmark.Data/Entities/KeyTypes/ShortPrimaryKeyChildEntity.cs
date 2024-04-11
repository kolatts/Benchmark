namespace Benchmark.Data.Entities.KeyTypes;

public class ShortPrimaryKeyChildEntity : IPrimaryKeyEntity
{
    public short ShortPrimaryKeyChildEntityId { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public virtual ShortPrimaryKeyEntity Parent { get; set; } = null!;
    public short ParentId { get; set; }
}