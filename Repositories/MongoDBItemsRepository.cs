using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDBItemsRepository : IItemsRepository
    {
        private readonly IMongoCollection<Item> itemsCollection;
        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        public MongoDBItemsRepository(IMongoClient mongoClient)
        {

            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);

        }
        public async Task CreateItem(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existingitem => existingitem.Id, item.Id);
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}