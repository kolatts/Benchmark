namespace Benchmark.Data.Entities.KeyTypes;

public class StringPrimaryKeyEntity : IPrimaryKeyEntity
{
    public string StringPrimaryKeyEntityId { get; set; } = Guid.NewGuid().ToString();
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<StringPrimaryKeyChildEntity> Children { get; set; } = new();
}