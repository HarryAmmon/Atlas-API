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
    public class BugController : ControllerBase
    {
        private readonly IBaseRepository<Bug> _bugRepo;
        private readonly IBaseRepository<UserStory> _storyRepo;
        public BugController(IBaseRepository<Bug> bugRepo, IBaseRepository<UserStory> storyRepo)
        {
            _bugRepo = bugRepo;
            _storyRepo = storyRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<Bug>> Get()
        {
            return await _bugRepo.Get();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Bug>> Get([FromRoute] string id)
        {
            var bug = await _bugRepo.Get(id);
            if (bug == null)
            {
                return NotFound();
            }
            else
            {
                return bug;
            }
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<ActionResult<AtlasTask>> Post(string id, [FromBody] Bug obj)
        {
            var userStory = await _storyRepo.Get(id);
            if (userStory == null)
            {
                return NotFound();
            }
            if (userStory.BugsId == null)
            {
                userStory.BugsId = new List<string>();
            }
            var createResult = await _bugRepo.Create(obj);
            userStory.BugsId.Add(createResult.Id);
            await _storyRepo.Update(id, userStory);

            return CreatedAtAction("Post", createResult);
        }

        [HttpDelete]
        [Route("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _bugRepo.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            try
            {
                await _bugRepo.Delete(id);
                var stories = await _storyRepo.Get();
                foreach (var story in stories)
                {
                    if (story.BugsId.Contains(id))
                    {
                        story.BugsId.Remove(id);
                        await _storyRepo.Update(story.Id, story);
                    }
                }
                return StatusCode(202);
            }
            catch (MongoException)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("{id:length(24)}")]
        public async Task<ActionResult> Put(string id, Bug bug)
        {
            if (await _bugRepo.Get(id) == null)
            {
                return NotFound();
            }
            try
            {
                if (await TryUpdateModelAsync(bug))
                {
                    await _bugRepo.Update(id, bug);
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