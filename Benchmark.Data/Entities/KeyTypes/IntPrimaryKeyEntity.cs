namespace Benchmark.Data.Entities.KeyTypes;

public class IntPrimaryKeyEntity : IPrimaryKeyEntity<int>
{
    public int Id { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<IntPrimaryKeyChildEntity> Children { get; set; } = new();
}