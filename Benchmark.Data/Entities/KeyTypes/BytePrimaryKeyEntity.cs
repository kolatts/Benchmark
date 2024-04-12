namespace Benchmark.Data.Entities.KeyTypes;

public class BytePrimaryKeyEntity : IPrimaryKeyEntity<byte>
{
    public byte Id { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<BytePrimaryKeyChildEntity> Children { get; set; } = new();

}