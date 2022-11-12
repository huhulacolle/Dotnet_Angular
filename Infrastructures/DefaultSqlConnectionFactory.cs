using MySql.Data.MySqlClient;
using System.Data;

namespace Dotnet_Angular.Infrastructures
{
    public class DefaultSqlConnectionFactory
    {
        public string ConnectionString { get; } = null!;

        public DefaultSqlConnectionFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection Create()
        {
            return new MySqlConnection(ConnectionString);
        }

    }
}
