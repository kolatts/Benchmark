using Benchmark.Data.Entities.KeyTypes;
using Bogus;
using EFCore.BulkExtensions;
using PrimaryKeyEntities = (
     System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.IntPrimaryKeyEntity>,
    System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.GuidPrimaryKeyEntity>,
    System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.StringPrimaryKeyEntity>,
    System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.ShortPrimaryKeyEntity>,
    System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.LongPrimaryKeyEntity>,
    System.Collections.Generic.List<Benchmark.Data.Entities.KeyTypes.BytePrimaryKeyEntity>

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
            context.BulkInsert(entities.Item1);
            context.BulkInsert(entities.Item1.SelectMany(x=>x.Children));
            context.BulkInsert(entities.Item2);
            context.BulkInsert(entities.Item2.SelectMany(x => x.Children));
            context.BulkInsert(entities.Item3);
            context.BulkInsert(entities.Item3.SelectMany(x => x.Children));
            context.BulkInsert(entities.Item4);
            context.BulkInsert(entities.Item4.SelectMany(x=>x.Children));
            context.BulkInsert(entities.Item5);
            context.BulkInsert(entities.Item5.SelectMany(x => x.Children));
            context.BulkInsert(entities.Item6);
            context.BulkInsert(entities.Item6.SelectMany(x => x.Children));
        }


        public static void DeletePrimaryKeyEntities(this BenchmarkDbContext context)
        {
            context.GuidPrimaryKeyEntities.ExecuteDelete();
            context.StringPrimaryKeyEntities.ExecuteDelete();
            context.IntPrimaryKeyEntities.ExecuteDelete();
            context.LongPrimaryKeyEntities.ExecuteDelete();
            context.BytePrimaryKeyEntities.ExecuteDelete();
        }

        private static PrimaryKeyEntities GetEntities(int count, int seed, int descriptionLength,
            int numberOfChildren)
        {
            //We'll use Bogus for just the int entities, and then copy the values to the other entities to create more of an apples-to-apples comparison
            var faker = new Faker<IntPrimaryKeyEntity>().UseSeed(seed)
                .RuleFor(x => x.Description, f => f.Random.AlphaNumeric(descriptionLength));
            var intEntities = faker.Generate(count);
            var childIdIncrement = 1;
            intEntities.ForEach(x =>
            {
                x.Id = intEntities.IndexOf(x) + 1;
                for (var i = 0; i < numberOfChildren; i++)
                {
                    x.Children.Add(new IntPrimaryKeyChildEntity() { Description = x.Description, ParentId = x.Id, Id = childIdIncrement});
                    childIdIncrement++;
                }
            });
            //We could abstract this, but it's not worth it.
            var guidEntities = intEntities.CopyFieldsInto<GuidPrimaryKeyEntity>();
            for (var i = 0; i < count; i++)
            {
                guidEntities[i].Children = intEntities[i].Children.CopyFieldsIntoChildren<GuidPrimaryKeyChildEntity, Guid>();
                guidEntities[i].Children.ForEach(x=>x.ParentId = guidEntities[i].Id);
            }
            var stringEntities = intEntities.CopyFieldsInto<StringPrimaryKeyEntity>();
            for (var i = 0; i < count; i++)
            {
                stringEntities[i].Children = intEntities[i].Children.CopyFieldsIntoChildren<StringPrimaryKeyChildEntity, string>();
                stringEntities[i].Children.ForEach(x=>x.ParentId = stringEntities[i].Id);
            }
            var longEntities = intEntities.CopyFieldsInto<LongPrimaryKeyEntity>();
            for (var i = 0; i < count; i++)
            {
                longEntities[i].Id = intEntities[i].Id;
                longEntities[i].Children = intEntities[i].Children.CopyFieldsIntoChildren<LongPrimaryKeyChildEntity,long>();
                longEntities[i].Children.ForEach(x=>x.ParentId = longEntities[i].Id);
            }
            //Ensure that we don't create more entities than can be stored for shorts, since these have a PK of type short.
            var shortCount = int.Min(count, short.MaxValue);
            var shortEntities = intEntities.CopyFieldsInto<ShortPrimaryKeyEntity>().Take(shortCount).ToList();
            for (var i = 0; i < shortCount; i++)
            {
                shortEntities[i].Id = (short)intEntities[i].Id;
                shortEntities[i].Children = intEntities[i].Children.CopyFieldsIntoChildren<ShortPrimaryKeyChildEntity,short>();
                shortEntities[i].Children.ForEach(x=>x.ParentId = shortEntities[i].Id);
            }
            //Ensure that we don't create more entities than can be stored for bytes, since these have a PK of type byte.
            var byteCount = int.Min(count, byte.MaxValue);
            var byteEntities = intEntities.CopyFieldsInto<BytePrimaryKeyEntity>().Take(byteCount).ToList();
            for (var i = 0; i < byteCount; i++)
            {
                byteEntities[i].Id = (byte)intEntities[i].Id;
                byteEntities[i].Children = intEntities[i].Children.CopyFieldsIntoChildren<BytePrimaryKeyChildEntity,byte>();
                byteEntities[i].Children.ForEach(x => x.ParentId = byteEntities[i].Id);
            }
            return (intEntities, guidEntities, stringEntities, shortEntities, longEntities,byteEntities);
        }
        private static List<TDescriptionEntity> CopyFieldsInto<TDescriptionEntity>(this IEnumerable<IDescriptionEntity> copyFrom)
        where TDescriptionEntity: IDescriptionEntity, new()
        {
            return copyFrom.Select(x =>
                {

                    var instance = (TDescriptionEntity)Activator.CreateInstance(typeof(TDescriptionEntity))!;
                    instance.Description = x.Description;
                    return instance!;
                })
                .ToList();
        }
        private static List<TPrimaryKeyChildEntity> CopyFieldsIntoChildren<TPrimaryKeyChildEntity,TKey>(this IEnumerable<IPrimaryKeyChildEntity<int>> copyFrom)
            where TPrimaryKeyChildEntity : IPrimaryKeyChildEntity<TKey>, new()
        {
            return copyFrom.Select(x =>
                {

                    var instance = (TPrimaryKeyChildEntity)Activator.CreateInstance(typeof(TPrimaryKeyChildEntity))!;
                    instance.Id = x.Id;
                    instance.Description = x.Description;
                    return instance!;
                })
                .ToList();
        }
    }
}
