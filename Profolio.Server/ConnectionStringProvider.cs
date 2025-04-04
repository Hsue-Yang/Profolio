using Microsoft.Data.SqlClient;

namespace Profolio.Server
{
	public interface IConnectionStringProvider
    {
        string ConnStringMSSQL { get; }
        string ConnStringMSSQLReadOnly { get; }
    }
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private static string _connStringMSSQL;
        private static string _connStringMSSQLReadOnly;
        public string ConnStringMSSQL => _connStringMSSQL;
        public string ConnStringMSSQLReadOnly => _connStringMSSQLReadOnly;

        public ConnectionStringProvider(IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<Dictionary<string, string>>();
            if (connectionStrings.TryGetValue(("AZURE_SQL_CONNECTIONSTRING"), out _connStringMSSQL))
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connStringMSSQL)
                {
                    ApplicationIntent = ApplicationIntent.ReadOnly
                };
                _connStringMSSQLReadOnly = builder.ConnectionString;
            }
        }
    }
}