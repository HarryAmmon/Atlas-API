using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Services;
using MongoDB.Driver;

namespace Atlas_API.Repositories
{
    public class KanBanColumnRepository : IBaseRepository<KanBanColumn>
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<KanBanColumn> _collection;
        public KanBanColumnRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<KanBanColumn>("KanBanColumns");
        }

        public async Task<KanBanColumn> Create(KanBanColumn obj)
        {
            if (obj.UserStoriesId == null)
            {
                obj.UserStoriesId = new List<string>();
            }
            await _collection.InsertOneAsync(obj);
            return await Get(obj.ColumnId);
        }

        public async Task Delete(string id)
        {
            var result = await _collection.DeleteOneAsync(x => x.ColumnId == id);
            if (result.DeletedCount == 0)
            {
                throw new MongoException("Failed to delete");
            }
        }

        public async Task<KanBanColumn> Get(string id)
        {
            var result = await _collection.FindAsync(x => x.ColumnId == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<KanBanColumn>> Get()
        {
            var result = await _collection.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task Update(string id, KanBanColumn obj)
        {
            var result = await _collection.ReplaceOneAsync(x => x.ColumnId == id, obj);
        }
    }
}