namespace Benchmark.Data.Entities.KeyTypes;

public class GuidPrimaryKeyChildEntity : IPrimaryKeyChildEntity<Guid>
{
    public int Id { get; set; } 
    [StringLength(1000)]
    public string? Description { get; set; }
    public virtual GuidPrimaryKeyEntity Parent { get; set; } = null!;
    public Guid ParentId { get; set; }
}