using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Services;
using MongoDB.Driver;

namespace Atlas_API.Repositories
{
    public class ColumnRepository : IBaseRepository<Column>
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<Column> _collection;
        public ColumnRepository(IMongoDBContext context)
        {
            _context = context;
            _collection = _context.GetCollection<Column>("Columns");
        }

        public async Task<Column> Create(Column obj)
        {
            await _collection.InsertOneAsync(obj);
            return await Get(obj.ColumnId);
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Column> Get(string id)
        {
            var result = await _collection.FindAsync(x => x.ColumnId == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Column>> Get()
        {
            var result = await _collection.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public Task Update(string id, Column obj)
        {
            throw new System.NotImplementedException();
        }
    }
}