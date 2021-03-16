using System.Threading.Tasks;
using MongoDB.Driver;

namespace Atlas_API.Factories
{
    public interface IMongoClientFactory
    {
        Task<MongoClient> CreateClient();
    }
}