using System.ComponentModel.DataAnnotations.Schema;

namespace Benchmark.Data.Entities.KeyTypes;

public class ShortPrimaryKeyEntity : IPrimaryKeyEntity<short>
{
    //EF Core will not make these SQL Identity fields by default
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<ShortPrimaryKeyChildEntity> Children { get; set; } = new();
}