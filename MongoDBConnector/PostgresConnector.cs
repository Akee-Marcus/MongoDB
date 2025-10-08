using System;
using System.Threading.Tasks;
using Npgsql;

namespace MongoDBConnector
{
    public class PostgresConnector : IDBConnector
    {
        private readonly string _connectionString;

        public PostgresConnector(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("connectionString must not be null or empty.", nameof(connectionString));

            _connectionString = connectionString;
        }

        public async Task<bool> PingAsync()
        {
            try
            {
                await using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand("SELECT 1", connection);
                await command.ExecuteScalarAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
