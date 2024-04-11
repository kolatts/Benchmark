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
        public static void SeedPrimaryKeyEntities(this BenchmarkDbContext context, bool deleteExisting, int count, int seed = 1, int descriptionLength = 100, int numberOfChildren = 1)
        {
            if (deleteExisting)
                context.DeletePrimaryKeyEntities();
            var entities = GetEntities(count, seed, descriptionLength, numberOfChildren);
            context.AddRange(entities.Item1);
            context.AddRange(entities.Item2);
            context.AddRange(entities.Item3);
            //context.AddRange(entities.Item4); //todo there is some issues with shorts and sqlite to sort out.
            context.AddRange(entities.Item5);
            context.SaveChanges();
        }

        public static async Task SeedPrimaryKeyEntitiesAsync(this BenchmarkDbContext context, bool deleteExisting, int count, int seed = 1, int descriptionLength = 100, int numberOfChildren = 1)
        {
            if (deleteExisting)
                await context.DeletePrimaryKeyEntitiesAsync();
            var entities = GetEntities(count, seed, descriptionLength, numberOfChildren);
            context.AddRange(entities.Item1);
            context.AddRange(entities.Item2);
            context.AddRange(entities.Item3);
            //context.AddRange(entities.Item4); //todo there is some issues with shorts and sqlite to sort out.
            context.AddRange(entities.Item5);
            await context.SaveChangesAsync();
        }

        public static async Task DeletePrimaryKeyEntitiesAsync(this BenchmarkDbContext context)
        {
            await context.GuidPrimaryKeyEntities.ExecuteDeleteAsync();
            await context.StringPrimaryKeyEntities.ExecuteDeleteAsync();
            await context.IntPrimaryKeyEntities.ExecuteDeleteAsync();
            await context.LongPrimaryKeyEntities.ExecuteDeleteAsync();
        }


        public static void DeletePrimaryKeyEntities(this BenchmarkDbContext context)
        {
            context.GuidPrimaryKeyEntities.ExecuteDelete();
            context.StringPrimaryKeyEntities.ExecuteDelete();
            context.IntPrimaryKeyEntities.ExecuteDelete();
            context.LongPrimaryKeyEntities.ExecuteDelete();
        }

        private static PrimaryKeyEntities GetEntities(int count, int seed, int descriptionLength,
            int numberOfChildren)
        {
            //We'll use Bogus for just the int entities, and then copy the values to the other entities to create more of an apples-to-apples comparison
            var faker = new Faker<IntPrimaryKeyEntity>().UseSeed(seed)
                .RuleFor(x => x.Description, f => f.Random.AlphaNumeric(descriptionLength));
            var intEntities = faker.Generate(count);
            var intChildEntities = CopyFieldsInto<IntPrimaryKeyChildEntity>(intEntities);
            intEntities.ForEach(x =>
            {
                for (var i = 0; i < numberOfChildren; i++)
                {
                    x.Children.Add(new IntPrimaryKeyChildEntity() { Description = x.Description});
                }
            });
            //We could abstract this, but it's not worth it.
            var guidEntities = intEntities.CopyFieldsInto<GuidPrimaryKeyEntity>();
            for (var i = 0; i < count; i++)
            {
                guidEntities[i].Children = intEntities[i].Children.CopyFieldsInto<GuidPrimaryKeyChildEntity>();
            }
            var stringEntities = intEntities.CopyFieldsInto<StringPrimaryKeyEntity>();
            for (var i = 0; i < count; i++)
            {
                stringEntities[i].Children = intEntities[i].Children.CopyFieldsInto<StringPrimaryKeyChildEntity>();
            }
            var longEntities = intEntities.CopyFieldsInto<LongPrimaryKeyEntity>();
            for (var i = 0; i < count; i++)
            {
                longEntities[i].Children = intEntities[i].Children.CopyFieldsInto<LongPrimaryKeyChildEntity>();
            }
            //Ensure that we don't create more entities than can be stored for shorts, since these have a PK of type short.
            var shortCount = int.Min(count, short.MaxValue);
            var shortEntities = intEntities.CopyFieldsInto<ShortPrimaryKeyEntity>().Take(shortCount).ToList();
            for (var i = 0; i < shortCount; i++)
            {
                shortEntities[i].Children = intEntities[i].Children.CopyFieldsInto<ShortPrimaryKeyChildEntity>();
            }
            return (intEntities, guidEntities, stringEntities, shortEntities, longEntities);
        }

        private static List<TPrimaryKeyEntity> CopyFieldsInto<TPrimaryKeyEntity>(this IEnumerable<IPrimaryKeyEntity> copyFrom)
        where TPrimaryKeyEntity : IPrimaryKeyEntity, new()
        {
            return copyFrom.Select(x =>
                {

                    var instance = (TPrimaryKeyEntity)Activator.CreateInstance(typeof(TPrimaryKeyEntity))!;
                    instance.Description = x.Description;
                    return instance!;
                })
                .ToList();
        }
    }
}
