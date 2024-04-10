using System.ComponentModel.DataAnnotations;

namespace Benchmark.Data.Entities.KeyTypes;

public class ShortPrimaryKeyEntity
{
    public short ShortPrimaryKeyEntityId { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<ShortPrimaryKeyChildEntity> Children { get; set; } = new();
}