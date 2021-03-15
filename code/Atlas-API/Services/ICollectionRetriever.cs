using System.Threading.Tasks;
using MongoDB.Driver;

namespace Atlas_API.Services
{
    public interface ICollectionRetriever<T>
    {
        IMongoCollection<T> GetCollection(string databaseName, string collectionName);
    }
}