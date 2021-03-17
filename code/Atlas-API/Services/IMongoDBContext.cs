using MongoDB.Driver;

namespace Atlas_API.Services
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}