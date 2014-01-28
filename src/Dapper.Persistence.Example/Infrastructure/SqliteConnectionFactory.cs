using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Example.Infrastructure
{
    public class SqliteConnectionFactory
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly string _connectionString;

        public SqliteConnectionFactory(ConnectionStringSettings settings)
        {
            _providerFactory = DbProviderFactories.GetFactory(settings.ProviderName);
            _connectionString = settings.ConnectionString;
        }

        public IDbConnection Create()
        {
            var connection = _providerFactory.CreateConnection();
            connection.ConnectionString = _connectionString;

            return connection;
        }
    }
}