using Benchmark.Data.Entities.KeyTypes;
using Bogus;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
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
        public static void SeedPrimaryKeyEntities(this BenchmarkDbContext context, bool deleteExisting, int count,  int numberOfChildren = 1)
        {
            context.Database.SetCommandTimeout(600);
            if (deleteExisting)
                context.DeletePrimaryKeyEntities();
            var entities = GetEntities(count, numberOfChildren);
            Console.WriteLine("Created entities in memory. Starting inserts.");
            context.BulkInsert(entities.Item1, config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item1.SelectMany(x=>x.Children), config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item2, config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item2.SelectMany(x => x.Children), config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item3, config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item3.SelectMany(x => x.Children), config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item4, config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item4.SelectMany(x=>x.Children), config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item5, config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item5.SelectMany(x => x.Children), config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item6, config => config.BatchSize = 100000);
            context.BulkInsert(entities.Item6.SelectMany(x => x.Children), config => config.BatchSize = 100000);
        }


        public static void DeletePrimaryKeyEntities(this BenchmarkDbContext context)
        {
            context.ShortPrimaryKeyEntities.ExecuteDelete();
            context.ShortPrimaryKeyChildEntities.ExecuteDelete();
            context.GuidPrimaryKeyEntities.ExecuteDelete();
            context.GuidPrimaryKeyChildEntities.ExecuteDelete();
            context.StringPrimaryKeyEntities.ExecuteDelete();
            context.StringPrimaryKeyChildEntities.ExecuteDelete();
            context.IntPrimaryKeyEntities.ExecuteDelete();
            context.IntPrimaryKeyChildEntities.ExecuteDelete();
            context.LongPrimaryKeyEntities.ExecuteDelete();
            context.LongPrimaryKeyChildEntities.ExecuteDelete();
            context.BytePrimaryKeyEntities.ExecuteDelete();
            context.BytePrimaryKeyChildEntities.ExecuteDelete();
            context.ResetIdentity(nameof(context.BytePrimaryKeyEntities));
            context.ResetIdentity(nameof(context.BytePrimaryKeyChildEntities));
            context.ResetIdentity(nameof(context.ShortPrimaryKeyEntities));
            context.ResetIdentity(nameof(context.ShortPrimaryKeyChildEntities));
            context.ResetIdentity(nameof(context.IntPrimaryKeyEntities));
            context.ResetIdentity(nameof(context.IntPrimaryKeyChildEntities));
            context.ResetIdentity(nameof(context.LongPrimaryKeyEntities));
            context.ResetIdentity(nameof(context.LongPrimaryKeyChildEntities));
        }

        private static PrimaryKeyEntities GetEntities(int count, int numberOfChildren)
        {
            //We'll use Bogus for just the int entities, and then copy the values to the other entities to create more of an apples-to-apples comparison
            var childIncrement = 1;
            var intEntities = Enumerable.Range(0, count).Select(x =>
            {
                var entity =  new IntPrimaryKeyEntity()
                {
                    Description = Guid.NewGuid().ToString(),
                    Id = x + 1,
                };
                for (var i = 0; i < numberOfChildren; i++)
                {
                    entity.Children.Add(new() { Description = Guid.NewGuid().ToString(), ParentId = x + 1, Id = childIncrement,});
                    childIncrement++;
                }
                return entity;
            }).ToList();
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
            
            var shortEntities = count > short.MaxValue ? [] :  intEntities.CopyFieldsInto<ShortPrimaryKeyEntity>().Take(count).ToList();
            for (var i = 0; i < shortEntities.Count; i++)
            {
                shortEntities[i].Id = (short)intEntities[i].Id;
                shortEntities[i].Children = intEntities[i].Children.CopyFieldsIntoChildren<ShortPrimaryKeyChildEntity,short>();
                shortEntities[i].Children.ForEach(x=>x.ParentId = shortEntities[i].Id);
            }
            //Ensure that we don't create more entities than can be stored for bytes, since these have a PK of type byte.
            var byteEntities = count > byte.MaxValue ? [] : intEntities.CopyFieldsInto<BytePrimaryKeyEntity>().Take(count).ToList();
            for (var i = 0; i < byteEntities.Count; i++)
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
