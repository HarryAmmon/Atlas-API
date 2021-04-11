using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Atlas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IBaseRepository<AtlasTask> _repo;
        public TaskController(IBaseRepository<AtlasTask> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<AtlasTask>> Get()
        {
            return await _repo.Get();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AtlasTask>> Get([FromRoute] string id)
        {
            var task = await _repo.Get(id);
            if (task == null)
            {
                return NotFound();
            }
            else
            {
                return task;
            }
        }

        [HttpPost]
        public async Task<ActionResult<AtlasTask>> Post(AtlasTask task)
        {
            var result = await _repo.Create(task);
            return CreatedAtAction("Post", result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _repo.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            try
            {
                await _repo.Delete(id);
                return StatusCode(202);
            }
            catch (MongoException)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Put(string id, AtlasTask task)
        {
            if (await _repo.Get(id) == null)
            {
                return NotFound();
            }
            try
            {
                if (await TryUpdateModelAsync(task))
                {
                    await _repo.Update(id, task);
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (NullReferenceException)
            {
                return StatusCode(500);
            }
        }

    }
}