using System.Data.SqlClient;

namespace SqlServerGraphDb.Persistence.Factory
{
    public interface IConnectionFactory
    {
        SqlConnection GetDatabaseConnection();
    }
}