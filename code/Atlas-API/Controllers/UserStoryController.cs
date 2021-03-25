using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Atlas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserStoryController : ControllerBase
    {

        private readonly IBaseRepository<UserStory> _repo;

        public UserStoryController(IBaseRepository<UserStory> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<UserStory>> Get()
        {
            return await _repo.Get();
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserStory userStory)
        {
            Console.WriteLine("Post");
            var result = await _repo.Create(userStory);
            return CreatedAtAction("Post", result);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Put(string id, UserStory userStory)
        {
            var story = await _repo.Get(id);

            if (story == null)
            {
                return NotFound();
            }
            try
            {

                if (await TryUpdateModelAsync(userStory))
                {
                    await _repo.Update(id, userStory);
                    return NoContent();
                }
                else
                {
                    Console.WriteLine("could not update");
                    return BadRequest();
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Failed in exception");
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var story = await _repo.Get(id);
            if (story == null)
            {
                return NotFound();
            }
            else
            {
                await _repo.Delete(id);
                return StatusCode(202);
            }

        }
    }
}