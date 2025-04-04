using MongoDB.Driver;

namespace Profolio.Server.Services.Interfaces
{
	public interface IMongoService
	{
		Task InsertOneAsync<T>(string collectionName, T document);
		Task InsertManyAsync<T>(string collectionName, IEnumerable<T> documents);
		Task<T> FindOneAsync<T>(string collectionName, FilterDefinition<T> filter);
		Task<List<T>> FindManyAsync<T>(string collectionName, FilterDefinition<T> filter);
		Task UpdateOneAsync<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update);
		Task UpdateManyAsync<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update);
		Task DeleteOneAsync<T>(string collectionName, FilterDefinition<T> filter);
		Task DeleteManyAsync<T>(string collectionName, FilterDefinition<T> filter);
	}
}