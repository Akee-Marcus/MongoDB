using MongoDBConnector;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit;

public class PostgresConnectorTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer;

    public PostgresConnectorTests()
    {
        _postgresContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithCleanUp(true)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
    }

    [Fact(DisplayName = "PingAsync returns true when PostgreSQL container is up")]
    public async Task PingAsync_ReturnsTrue_WhenDatabaseIsUp()
    {
        var connector = new PostgresConnector(_postgresContainer.GetConnectionString());
        bool result = await connector.PingAsync();
        Assert.True(result);
    }

    [Fact(DisplayName = "PingAsync returns false when connection string is invalid / server not reachable")]
    public async Task PingAsync_ReturnsFalse_WhenConnectionStringInvalid()
    {
        var badConnectionString = "Host=localhost;Port=9999;Username=postgres;Password=bad;Database=testdb";
        var connector = new PostgresConnector(badConnectionString);
        bool result = await connector.PingAsync();
        Assert.False(result);
    }
}
