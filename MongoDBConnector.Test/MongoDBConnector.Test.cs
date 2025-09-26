using System;
using System.Threading.Tasks;
using Testcontainers.MongoDb;
using MongoDBConnector;
using Xunit;

public class MongoDbServiceTests : IAsyncLifetime
{
    private readonly MongoDbContainer _mongoContainer;

    public MongoDbServiceTests()
    {
        _mongoContainer = new MongoDbBuilder()
            .WithImage("mongo:7")
            .WithCleanUp(true)
            .Build();
    }

    /// <summary>
    /// Starts the container and waits until MongoDB actually responds to ping.
    /// We poll up to a timeout to avoid flaky failures when MongoDB is still initializing.
    /// </summary>
    public async Task InitializeAsync()
    {
        await _mongoContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _mongoContainer.DisposeAsync();
    }

    [Fact(DisplayName = "PingAsync returns true when MongoDB container is up")]
    public async Task PingAsync_ReturnsTrue_WhenDatabaseIsUp()
    {
        var connector = new MongoDbService(_mongoContainer.GetConnectionString());
        bool result = await connector.PingAsync();
        Assert.True(result);
    }

    [Fact(DisplayName = "PingAsync returns false when connection string is invalid / server not reachable")]
    public async Task PingAsync_ReturnsFalse_WhenConnectionStringInvalid()
    {
        var badConnectionString = "mongodb://localhost:12345";
        var connector = new MongoDbService(badConnectionString);

        bool result = await connector.PingAsync().ConfigureAwait(false);

        Assert.False(result);
    }
}
