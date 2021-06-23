using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.Repositories
{
    public class BaseRepository
    {
        private IMongoDatabase db;

        public BaseRepository(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public async Task InsertAsync<T>(string collectionName, T record)
        {
            var collection = db.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(record);
        }

        public async Task<List<T>> GetAllAsync<T>(string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);

            var result = await collection.FindAsync(new BsonDocument());
            return result.ToList();
        }

        public async Task<T> GetByIdAsync<T>(string collectionName, Guid id)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);

            var result = await collection.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task UpsertAsync<T>(string collectionName, Guid id, T record)
        {
            var collection = db.GetCollection<T>(collectionName);

            await collection.ReplaceOneAsync(
                new BsonDocument("_id", id),
                record,
                new UpdateOptions {IsUpsert = true});
        }

        public async Task DeleteAsync<T>(string collectionName, Guid id)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);

            await collection.DeleteOneAsync(filter);
        }
    }
}