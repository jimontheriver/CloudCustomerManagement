using CustomerManagement.Library.Entities;
using Google.Cloud.Datastore.V1;
using System;
using System.Threading.Tasks;
using static Google.Cloud.Datastore.V1.PropertyOrder.Types;

namespace CustomerManagement.Library.Repositories
{
    //TODO - Logging
    public class CustomerDatastoreRepository : ICustomerRepository
    {
        private const string EntityKind = "customer";
        private readonly IDatastoreManager _datastoreManager;

        public CustomerDatastoreRepository(IDatastoreManager datastoreManager)
        {
            _datastoreManager = datastoreManager;
        }

        private void MapCustomerToEntity(Customer customer, Entity entity)
        {
            entity["sourceId"] = customer.SourceId;
            entity["name"] = customer.Name;
            entity["isDeleted"] = customer.IsDeleted;
            entity["created"] = customer.Created;
            entity["createdBy"] = customer.CreatedBy;
            entity["updated"] = customer.Updated;
            entity["updatedBy"] = customer.UpdatedBy;
            entity["industries"] = string.Join(",", customer.IndustryCodes);
        }


        private Customer MapEntityToCustomer(Entity entity)
        {
            return new Customer
            {
                SourceId = entity["sourceId"].IntegerValue,
                Name = entity["name"].StringValue,
                IsDeleted = entity["isDeleted"].BooleanValue,
                Created = entity["created"].TimestampValue.ToDateTimeOffset(),
                Updated = entity["updated"].TimestampValue.ToDateTimeOffset(),
                CreatedBy = entity["createdBy"].StringValue,
                UpdatedBy = entity["updatedBy"].StringValue,
                IndustryCodes = entity["industries"].StringValue.Split(',')
            };
        }

        public async Task<long> AddAsync(Customer customer)
        {
            var db = _datastoreManager.GetDatastore();
            KeyFactory keyFactory = db.CreateKeyFactory(EntityKind);

            Entity entity = new Entity
            {
                Key = keyFactory.CreateIncompleteKey()
            };

            MapCustomerToEntity(customer, entity);

            using (DatastoreTransaction transaction = await db.BeginTransactionAsync())
            {
                transaction.Insert(entity);
                CommitResponse commitResponse = await transaction.CommitAsync();
                Key insertedKey = commitResponse.MutationResults[0].Key;

                //TODO - Logging

                return insertedKey.Path[0].Id;
            }           
        }

        public async Task<Customer> GetAsync(long id)
        {
            DatastoreDb db = _datastoreManager.GetDatastore();

            var key = db.CreateKeyFactory(EntityKind).CreateKey(id);
            var entity = await db.LookupAsync(key);
            if (null != entity)
            {
                var customer = MapEntityToCustomer(entity);
                customer.Id = id;
                return customer;
            }
            return null;
        }

        public async Task<long> UpdateAsync(Customer customer)
        {
            var db = _datastoreManager.GetDatastore();
            using (var transaction = db.BeginTransaction())
            {
                var key = db.CreateKeyFactory(EntityKind).CreateKey(customer.Id);
                var entity = await db.LookupAsync(key);
                MapCustomerToEntity(customer, entity);
                transaction.Update(entity);
                await transaction.CommitAsync();
            }

            return customer.Id;
        }


        public async Task<long> DeleteAsync(long id)
        {
            var db = _datastoreManager.GetDatastore();
            using (var transaction = db.BeginTransaction())
            {
                var key = db.CreateKeyFactory(EntityKind).CreateKey(id);
                var entity = await db.LookupAsync(key);
                entity["isDeleted"] = true;
                transaction.Update(entity);
                await transaction.CommitAsync();
            }

            return id;
        }
    }
}
