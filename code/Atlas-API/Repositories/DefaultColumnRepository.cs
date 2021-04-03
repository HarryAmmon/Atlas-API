using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Services;
using MongoDB.Driver;

namespace Atlas_API.Repositories
{
    public class DefaultColumnRepository : IBaseRepository<DefaultColumn>
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<DefaultColumn> _collection;

        public DefaultColumnRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<DefaultColumn>("DefaultColumns");
        }

        public async Task<DefaultColumn> Create(DefaultColumn obj)
        {
            await _collection.InsertOneAsync(obj);
            return await Get(obj.ColumnId);
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DefaultColumn> Get(string id)
        {
            var result = await _collection.FindAsync(x => x.ColumnId == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DefaultColumn>> Get()
        {
            var allDefaultColumns = await _collection.FindAsync(_ => true);
            return await allDefaultColumns.ToListAsync();
        }

        public Task Update(string id, DefaultColumn obj)
        {
            throw new System.NotImplementedException();
        }
    }
}