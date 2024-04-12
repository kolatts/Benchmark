namespace Benchmark.Data.Entities.KeyTypes;

public class GuidPrimaryKeyEntity : IPrimaryKeyEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<GuidPrimaryKeyChildEntity> Children { get; set; } = new();
}