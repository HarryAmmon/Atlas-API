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

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
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

        public Task Update(string id, KanBanColumn obj)
        {
            throw new System.NotImplementedException();
        }
    }
}