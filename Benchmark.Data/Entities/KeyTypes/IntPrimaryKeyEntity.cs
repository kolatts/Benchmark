namespace Benchmark.Data.Entities.KeyTypes;

public class IntPrimaryKeyEntity : IPrimaryKeyEntity
{
    public int IntPrimaryKeyEntityId { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<IntPrimaryKeyChildEntity> Children { get; set; } = new();
}