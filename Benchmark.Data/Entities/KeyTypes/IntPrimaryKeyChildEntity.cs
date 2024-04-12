namespace Benchmark.Data.Entities.KeyTypes;

public class IntPrimaryKeyChildEntity : IPrimaryKeyChildEntity<int>
{
    public int Id { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public virtual IntPrimaryKeyEntity Parent { get; set; } = null!;
    public int ParentId { get; set; }
}