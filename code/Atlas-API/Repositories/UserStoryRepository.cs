using System;
using System.Collections.Generic;
using Atlas_API.Entities;
using Azure.Security.KeyVault.Secrets;
using MongoDB.Bson;
using MongoDB.Driver;
using Atlas_API.Services;
using System.Threading.Tasks;

namespace Atlas_API.Repositories
{
    public class UserStoryRepository : IBaseRepository<UserStory>
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<UserStory> _collection;
        public UserStoryRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<UserStory>("Atlas");
        }

        public async Task<UserStory> Create(UserStory obj)
        {
            await _collection.InsertOneAsync(obj);
            var result = await _collection.FindAsync(x => x.Id == obj.Id);

            Console.WriteLine(obj.ToString());

            return result.First();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserStory>> Get()
        {
            var allUserStories = await _collection.FindAsync(_ => true);
            var allUserStoriesAsList = allUserStories.ToList();

            return allUserStoriesAsList;
        }

        public Task<UserStory> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(UserStory obj)
        {
            throw new NotImplementedException();
        }
    }
}