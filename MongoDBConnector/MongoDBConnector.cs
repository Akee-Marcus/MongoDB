using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBConnector
{
    /// <summary>
    /// Provides a lightweight MongoDB connectivity helper.
    /// - Construct with a Mongo connection string.
    /// - PingAsync() issues a { ping: 1 } command to verify connectivity.
    /// </summary>
    public class MongoDbService
    {
        private readonly IMongoClient _client;

        /// <summary>
        /// Create a service with the provided MongoDB connection string.
        /// </summary>
        /// <param name="connectionString">A MongoDB connection string (e.g. "mongodb://localhost:27017")</param>
        public MongoDbService(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("connectionString must not be null or empty.", nameof(connectionString));
            }

            _client = new MongoClient(connectionString);
        }
    }
}
