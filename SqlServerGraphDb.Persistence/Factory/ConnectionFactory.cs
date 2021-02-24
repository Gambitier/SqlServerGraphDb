using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace SqlServerGraphDb.Persistence.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _connectionStringKey = "DefaultConnectionString";

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(_connectionStringKey);
        }

        public SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
