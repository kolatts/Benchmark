using System.ComponentModel.DataAnnotations.Schema;

namespace Benchmark.Data.Entities.KeyTypes;

public class BytePrimaryKeyEntity : IPrimaryKeyEntity<byte>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte Id { get; set; }
    [StringLength(1000)]
    public string? Description { get; set; }
    public List<BytePrimaryKeyChildEntity> Children { get; set; } = new();

}