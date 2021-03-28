using System;
using System.Collections.Generic;
using Atlas_API.Entities;
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
            _collection = _context.GetCollection<UserStory>("UserStories");
        }

        public async Task<UserStory> Create(UserStory obj)
        {
            await _collection.InsertOneAsync(obj);
            var result = await _collection.FindAsync(x => x.Id == obj.Id);

            Console.WriteLine(obj.ToString());

            return result.First();
        }

        public async Task Delete(string id)
        {
            var result = await Get(id);
            result.Archived = true;
            await Update(id, result);
        }

        public async Task<IEnumerable<UserStory>> Get()
        {
            var allUserStories = await _collection.FindAsync(_ => true);
            var allUserStoriesAsList = allUserStories.ToList();

            return allUserStoriesAsList;
        }

        public async Task<UserStory> Get(string id)
        {
            var result = await _collection.Find<UserStory>(story => story.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task Update(string id, UserStory obj)
        {
            Console.WriteLine(obj.ToString());
            var result = await _collection.ReplaceOneAsync(story => story.Id == id, obj);
            if (result.ModifiedCount == 0)
            {
                Console.WriteLine("Throwing error");
                throw new NullReferenceException("No documents updated");
            }
            else
            {
                Console.WriteLine("Updated");
            }
        }
    }
}