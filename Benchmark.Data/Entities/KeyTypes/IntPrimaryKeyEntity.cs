using System.ComponentModel.DataAnnotations;

namespace Benchmark.Data.Entities.KeyTypes;

public class IntPrimaryKeyEntity
{
    public int IntPrimaryKeyEntityId { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<IntPrimaryKeyChildEntity> Children { get; set; } = new();
}