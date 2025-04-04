namespace Profolio.Server
{
	public interface IMongoConnectionStringProvider
	{
		string ConnStringMongo { get; }
		string DatabaseName { get; }
	}
	public class MongoConnectionStringProvider : IMongoConnectionStringProvider
	{
		private readonly string _connStringMongo;
		private readonly string _databaseName;
		public string ConnStringMongo => _connStringMongo;
		public string DatabaseName => _databaseName;

		public MongoConnectionStringProvider(IConfiguration configuration)
		{
			var connectionStrings = configuration.GetSection("ConnectionStrings").Get<Dictionary<string, string>>();
			if (connectionStrings.TryGetValue(("MONGO_CONNECTIONSTRING"), out _connStringMongo))
			{
				_databaseName = configuration.GetValue("AppName", string.Empty);
			}
		}
	}
}