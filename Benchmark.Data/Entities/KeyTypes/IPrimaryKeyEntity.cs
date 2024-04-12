namespace Benchmark.Data.Entities.KeyTypes
{
    public interface IDescriptionEntity
    {
        public string? Description { get; set; }

    }
    public interface IPrimaryKeyEntity<TKey> : IDescriptionEntity
    {
        public TKey Id { get; set; }

    }

    public interface IPrimaryKeyChildEntity<TKey> : IDescriptionEntity
    {
        public int Id { get; set; }

        public TKey ParentId { get; set; }
    }
}
