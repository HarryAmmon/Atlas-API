using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Factories;
using MongoDB.Driver;

namespace Atlas_API.Services
{
    public class UserStoryCollectionRetriever : ICollectionRetriever<UserStory>
    {

        private readonly MongoClient _client;
        public UserStoryCollectionRetriever(IMongoClientFactory client)
        {
            _client = client.CreateClient().Result;
        }
        public IMongoCollection<UserStory> GetCollection(string databaseName, string collectionName)
        {
            var database = _client.GetDatabase(databaseName);
            var collection = database.GetCollection<UserStory>(collectionName);
            return collection;
        }
    }
}