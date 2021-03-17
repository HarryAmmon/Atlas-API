using System;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Factories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Atlas_API.Services
{
    public class MongoDBContext : IMongoDBContext
    {

        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        public MongoDBContext(IMongoClientFactory client)
        {
            _client = client.CreateClient().Result;
            _db = _client.GetDatabase("Atlas");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("name can not be null or whitespace");
            }
            else
            {
                return _db.GetCollection<T>(name);
            }
        }
    }
}