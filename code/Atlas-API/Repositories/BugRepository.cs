using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Services;
using MongoDB.Driver;

namespace Atlas_API.Repositories
{
    public class BugRepository : IBaseRepository<Bug>
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<Bug> _collection;
        public BugRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<Bug>("Bugs");
        }

        public async Task<Bug> Create(Bug obj)
        {
            await _collection.InsertOneAsync(obj);
            return await Get(obj.Id);
        }

        public async Task Delete(string id)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new MongoException("Failed to delete");
            }
        }

        public async Task<Bug> Get(string id)
        {
            var result = await _collection.Find<Bug>(task => task.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Bug>> Get()
        {
            var allBugs = await _collection.FindAsync(_ => true);
            var allBugsAsList = allBugs.ToList();
            return allBugsAsList;
        }

        public async Task Update(string id, Bug obj)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, obj);
        }
    }
}