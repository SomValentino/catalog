using System;
using System.Collections.Generic;
using catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace catalog.Repositories
{
    public class MongoDBItemRepository : IItemRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private IMongoCollection<Item> itemsCollection;
        private FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDBItemRepository(IMongoClient mongoDBClient)
        {
            var database = mongoDBClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid Id)
        {
            var filter = filterBuilder.Eq(existingitem => existingitem.Id, Id);

            itemsCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid Id)
        {
            var filter = filterBuilder.Eq(item => item.Id, Id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList(); ;
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existingitem => existingitem.Id, item.Id);

            itemsCollection.ReplaceOne(filter, item);
        }
    }
}
