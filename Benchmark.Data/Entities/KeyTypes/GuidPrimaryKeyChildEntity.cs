namespace Benchmark.Data.Entities.KeyTypes;

public class GuidPrimaryKeyChildEntity : IPrimaryKeyEntity
{
    public Guid GuidPrimaryKeyChildEntityId { get; set; } = Guid.NewGuid();
    [StringLength(1000)]
    public string? Description { get; set; }
    public virtual GuidPrimaryKeyEntity Parent { get; set; } = null!;
    public Guid ParentId { get; set; }
}