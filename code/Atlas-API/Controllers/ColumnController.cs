using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColumnController : ControllerBase
    {
        private readonly IBaseRepository<Column> _repo;

        public ColumnController(IBaseRepository<Column> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<Column>> Get()
        {
            return await _repo.Get();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Column column)
        {
            var result = await _repo.Create(column);
            return CreatedAtAction("Post", result);
        }
    }
}