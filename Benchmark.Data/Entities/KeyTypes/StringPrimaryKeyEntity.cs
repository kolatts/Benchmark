namespace Benchmark.Data.Entities.KeyTypes;

public class StringPrimaryKeyEntity : IPrimaryKeyEntity<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<StringPrimaryKeyChildEntity> Children { get; set; } = new();
}