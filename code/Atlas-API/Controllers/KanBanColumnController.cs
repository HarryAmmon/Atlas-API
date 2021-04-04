using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KanBanColumnController : ControllerBase
    {
        private readonly IBaseRepository<KanBanColumn> _repo;

        public KanBanColumnController(IBaseRepository<KanBanColumn> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<KanBanColumn>> Get()
        {
            return await _repo.Get();
        }

        [HttpPost]
        public async Task<ActionResult> Post(KanBanColumn column)
        {
            var result = await _repo.Create(column);
            return CreatedAtAction("Post", result);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Put(string cardId, CardMovement source, CardMovement destination)
        {
            if (source.ColumnId == destination.ColumnId)
            {
                // THE CARD IS IN A DIFFERENT POSTION IN THE SAME COLUMN
                var columnToUpdate
            }
            else
            {
                // THE CARD IS IN A DIFFERENT COLUMN
            }
        }
    }
}