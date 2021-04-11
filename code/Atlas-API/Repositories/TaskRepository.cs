using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Services;
using MongoDB.Driver;

namespace Atlas_API.Repositories
{
    public class TaskRepository : IBaseRepository<AtlasTask>
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<AtlasTask> _collection;

        public TaskRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<AtlasTask>("Tasks");
        }
        public Task<AtlasTask> Create(AtlasTask obj)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AtlasTask> Get(string id)
        {
            var result = await _collection.Find<AtlasTask>(task => task.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<AtlasTask>> Get()
        {
            var allTasks = await _collection.FindAsync(_ => true);
            var allTasksAsList = allTasks.ToList();
            return allTasksAsList;
        }


        public Task Update(string id, AtlasTask obj)
        {
            throw new System.NotImplementedException();
        }
    }
}