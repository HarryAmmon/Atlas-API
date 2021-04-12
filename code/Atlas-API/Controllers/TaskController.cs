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
        private readonly IBaseRepository<AtlasTask> _taskRepo;
        private readonly IBaseRepository<UserStory> _storyRepo;
        public TaskController(IBaseRepository<AtlasTask> taskRepo, IBaseRepository<UserStory> storyRepo)
        {
            _taskRepo = taskRepo;
            _storyRepo = storyRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<AtlasTask>> Get()
        {
            return await _taskRepo.Get();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AtlasTask>> Get([FromRoute] string id)
        {
            var task = await _taskRepo.Get(id);
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
        [Route("{id}")]
        public async Task<ActionResult<AtlasTask>> Post(string id, [FromBody] AtlasTask task)
        {
            var userStory = await _storyRepo.Get(id);
            if (userStory == null)
            {
                return NotFound();
            }
            if (userStory.TasksId == null)
            {
                userStory.TasksId = new List<string>();
            }
            var result = await _taskRepo.Create(task);
            userStory.TasksId.Add(result.Id);
            await _storyRepo.Update(id, userStory);

            return CreatedAtAction("Post", result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _taskRepo.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            try
            {
                await _taskRepo.Delete(id);
                var stories = await _storyRepo.Get();
                foreach (var story in stories)
                {
                    if (story.TasksId.Contains(id))
                    {
                        story.TasksId.Remove(id);
                    }
                }
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
            if (await _taskRepo.Get(id) == null)
            {
                return NotFound();
            }
            try
            {
                if (await TryUpdateModelAsync(task))
                {
                    await _taskRepo.Update(id, task);
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