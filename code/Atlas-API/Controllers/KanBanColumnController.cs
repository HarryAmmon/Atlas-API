using System;
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
        public async Task<ActionResult> Put([FromRoute] string destinationId, [FromBody] KanBanColumnPutRequest body)
        {
            var sourceColumn = await _repo.Get(body.Source.DroppableId);
            if (sourceColumn == null)
            {
                return NotFound();
            }
            var cardId = sourceColumn.UserStoriesId[body.Source.Index];
            sourceColumn.UserStoriesId.Remove(cardId);

            if (body.Source.DroppableId == body.Destination.DroppableId)
            {
                sourceColumn.UserStoriesId.Insert(body.Destination.Index, cardId);
                await _repo.Update(sourceColumn.ColumnId, sourceColumn);
            }
            else
            {
                var destinationColumn = await _repo.Get(body.Destination.DroppableId);
                if (destinationColumn == null)
                {
                    return NotFound();
                }
                destinationColumn.UserStoriesId.Insert(body.Destination.Index, cardId);
                var updateSourceColumn = _repo.Update(sourceColumn.ColumnId, sourceColumn);
                var updateDestinationColumn = _repo.Update(destinationColumn.ColumnId, destinationColumn);

                Task.WaitAll(updateSourceColumn, updateDestinationColumn);
            }
            return NoContent();
        }
    }
}