namespace Benchmark.Data.Entities.KeyTypes;

public class StringPrimaryKeyChildEntity : IPrimaryKeyChildEntity<string>
{
    public int Id { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public virtual StringPrimaryKeyEntity Parent { get; set; } = null!;
    public string ParentId { get; set; } = null!;
}