using System.Threading.Tasks;

namespace MongoDBConnector
{
    public interface IDBConnector
    {
        Task<bool> PingAsync();
    }
}
