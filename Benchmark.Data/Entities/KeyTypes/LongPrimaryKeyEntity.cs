using System.ComponentModel.DataAnnotations;

namespace Benchmark.Data.Entities.KeyTypes;

public class LongPrimaryKeyEntity
{
    public long LongPrimaryKeyEntityId { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<LongPrimaryKeyChildEntity> Children { get; set; } = new();
}