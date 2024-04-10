using System.ComponentModel.DataAnnotations;

namespace Benchmark.Data.Entities.KeyTypes;

public class StringPrimaryKeyChildEntity
{
    public string StringPrimaryKeyChildEntityId { get; set; } = Guid.NewGuid().ToString();
    [StringLength(1000)]
    public string? Description { get; set; }
    public virtual StringPrimaryKeyEntity Parent { get; set; } = null!;
    public string ParentId { get; set; } = null!;
}