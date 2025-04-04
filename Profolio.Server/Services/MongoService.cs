using MongoDB.Driver;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Services
{
	public class MongoService : IMongoService
	{
		private readonly IMongoConnectionStringProvider _mongoConn;

		public MongoService(IMongoConnectionStringProvider mongoConn)
		{
			_mongoConn = mongoConn;
		}

		private IMongoCollection<T> GetCollection<T>(string collectionName)
		{
			var mongoClient = new MongoClient(_mongoConn.ConnStringMongo);
			var database = mongoClient.GetDatabase(_mongoConn.DatabaseName);
			return database.GetCollection<T>(collectionName);
		}

		public async Task DeleteManyAsync<T>(string collectionName, FilterDefinition<T> filter)
		{
			var collection = GetCollection<T>(collectionName);
			await collection.DeleteManyAsync(filter);
		}

		public async Task DeleteOneAsync<T>(string collectionName, FilterDefinition<T> filter)
		{
			var collection = GetCollection<T>(collectionName);
			await collection.DeleteOneAsync(filter);
		}

		public async Task<List<T>> FindManyAsync<T>(string collectionName, FilterDefinition<T> filter)
		{
			var collection = GetCollection<T>(collectionName);
			return await collection.Find(filter).ToListAsync();
		}

		public async Task<T> FindOneAsync<T>(string collectionName, FilterDefinition<T> filter)
		{
			var collection = GetCollection<T>(collectionName);
			return await collection.Find(filter).FirstOrDefaultAsync();
		}

		public async Task InsertManyAsync<T>(string collectionName, IEnumerable<T> documents)
		{
			var collection = GetCollection<T>(collectionName);
			await collection.InsertManyAsync(documents);
		}

		public async Task InsertOneAsync<T>(string collectionName, T document)
		{
			var collection = GetCollection<T>(collectionName);
			await collection.InsertOneAsync(document);
		}

		public async Task UpdateManyAsync<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update)
		{
			var collection = GetCollection<T>(collectionName);
			await collection.UpdateManyAsync(filter, update);
		}

		public async Task UpdateOneAsync<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update)
		{
			var collection = GetCollection<T>(collectionName);
			await collection.UpdateOneAsync(filter, update);
		}
	}
}