using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Services;
using MongoDB.Driver;

namespace Atlas_API.Repositories
{
    public class ColumnGroupRepository : IBaseRepository<ColumnGroup>
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<ColumnGroup> _collection;
        public ColumnGroupRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<ColumnGroup>("ColumnGroups");
        }

        public async Task<ColumnGroup> Create(ColumnGroup obj)
        {
            await _collection.InsertOneAsync(obj);
            return await Get(obj.GroupId);
        }

        public async Task Delete(string id)
        {
            var result = await _collection.DeleteOneAsync(x => x.GroupId == id);
            if (result.DeletedCount == 0)
            {
                throw new MongoException("Failed to delete");
            }
        }

        public async Task<ColumnGroup> Get(string id)
        {
            var result = await _collection.FindAsync(x => x.GroupId == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ColumnGroup>> Get()
        {
            var allColumnGroups = await _collection.FindAsync(_ => true);
            return allColumnGroups.ToList();
        }

        public async Task Update(string id, ColumnGroup obj)
        {
            var result = await _collection.ReplaceOneAsync(column => column.GroupId == id, obj);
        }
    }
}