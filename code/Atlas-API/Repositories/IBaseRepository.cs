using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;

namespace Atlas_API.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> Create(T obj);
        Task<T> Get(string id);
        Task<IEnumerable<T>> Get();
        Task Update(T obj);
        Task Delete(string id);


    }
}