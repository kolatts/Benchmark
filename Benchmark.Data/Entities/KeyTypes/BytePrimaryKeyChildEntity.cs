namespace Benchmark.Data.Entities.KeyTypes;

public class BytePrimaryKeyChildEntity : IPrimaryKeyChildEntity<byte>
{
    public BytePrimaryKeyChildEntity()
    {
    }

    public BytePrimaryKeyChildEntity(string? description)
    {
        Description = description;
    }

    public int Id { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }

    public virtual BytePrimaryKeyEntity Parent { get; set; } = null!;
    public byte ParentId { get; set; }

}