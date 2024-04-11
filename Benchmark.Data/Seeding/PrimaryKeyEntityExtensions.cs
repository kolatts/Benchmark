using Benchmark.Data.Entities.KeyTypes;
using Bogus;
using PrimaryKeyEntities = (
    System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.IntPrimaryKeyEntity>,
    System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.GuidPrimaryKeyEntity>,
    System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.StringPrimaryKeyEntity>,
    System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.ShortPrimaryKeyEntity>,
    System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.LongPrimaryKeyEntity>
    );

namespace Benchmark.Data.Seeding
{
    public static class PrimaryKeyEntityExtensions
    {
        public static async Task SeedPrimaryKeyEntitiesAsync(this BenchmarkDbContext context, int count, bool deleteExisting, int seed = 1, int descriptionLength = 100, int numberOfChildren = 1)
        {
            if (deleteExisting)
                await context.DeletePrimaryKeyEntitiesAsync();
        
        }

        private static PrimaryKeyEntities GetEntities(int count, bool deleteExisting, int seed, int descriptionLength,
            int numberOfChildren)
        {
            //We'll use Bogus for just the int entities, and then copy the values to the other entities to create more of an apples-to-apples comparison
            var faker = new Faker<IntPrimaryKeyEntity>().UseSeed(seed)
                .RuleFor(x => x.Description, f => f.Random.AlphaNumeric(descriptionLength));
            var intEntities = faker.Generate(count);
            var intChildEntities = Create<IntPrimaryKeyChildEntity>(intEntities);
            intEntities.ForEach(x =>
            {
                for (var i = 0; i < numberOfChildren; i++)
                {
                    x.Children.Add(intChildEntities[i]);
                }
            });
            //We could abstract this, but it's not worth it.
            var guidEntities = intEntities.Create<GuidPrimaryKeyEntity>();
            for (var i = 0; i < count; i++)
            {
                guidEntities[i].Children = intEntities[i].Children.Create<GuidPrimaryKeyChildEntity>();
            }
            var stringEntities = intEntities.Create<StringPrimaryKeyEntity>();
            for (var i = 0; i < count; i++)
            {
                stringEntities[i].Children = intEntities[i].Children.Create<StringPrimaryKeyChildEntity>();
            }
            var shortEntities = intEntities.Create<ShortPrimaryKeyEntity>();
            for (var i = 0; i < count; i++)
            {
                shortEntities[i].Children = intEntities[i].Children.Create<ShortPrimaryKeyChildEntity>();
            }
            var longEntities = intEntities.Create<LongPrimaryKeyEntity>();
            for (var i = 0; i < count; i++)
            {
                longEntities[i].Children = intEntities[i].Children.Create<LongPrimaryKeyChildEntity>();
            }
            return (intEntities, guidEntities, stringEntities, shortEntities, longEntities);
        }
        public static async Task DeletePrimaryKeyEntitiesAsync(this BenchmarkDbContext context)
        {
            await context.GuidPrimaryKeyEntities.ExecuteDeleteAsync();
            await context.StringPrimaryKeyEntities.ExecuteDeleteAsync();
            await context.IntPrimaryKeyEntities.ExecuteDeleteAsync();
            await context.LongPrimaryKeyEntities.ExecuteDeleteAsync();
        }
        public static void SeedPrimaryKeyEntities(this BenchmarkDbContext context, long count, bool deleteExisting)
        {
            if (deleteExisting)
                context.DeletePrimaryKeyEntities();
        }

        public static void DeletePrimaryKeyEntities(this BenchmarkDbContext context)
        {
            context.GuidPrimaryKeyEntities.ExecuteDelete();
            context.StringPrimaryKeyEntities.ExecuteDelete();
            context.IntPrimaryKeyEntities.ExecuteDelete();
            context.LongPrimaryKeyEntities.ExecuteDelete();
        }

        private static List<TPrimaryKeyEntity> Create<TPrimaryKeyEntity>(this IEnumerable<IPrimaryKeyEntity> copyFrom)
        where TPrimaryKeyEntity : IPrimaryKeyEntity, new()
        {
            return copyFrom.Select(x =>
                {

                    var instance = (TPrimaryKeyEntity)Activator.CreateInstance(typeof(TPrimaryKeyEntity), x)!;
                    instance.Description = x.Description;
                    return instance!;
                })
                .ToList();
        }
    }
}
