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
        private readonly IBaseRepository<KanBanColumn> _kanBanColumnRepo;
        public UserStoryController(IBaseRepository<UserStory> repo, IBaseRepository<KanBanColumn> kanBanColumnRepo)
        {
            _repo = repo;
            _kanBanColumnRepo = kanBanColumnRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<UserStory>> Get()
        {
            return await _repo.Get();
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserStory userStory)
        {
            // Create user story
            var result = await _repo.Create(userStory);
            // Get list of all default columns
            var defaultColumns = await _kanBanColumnRepo.Get();
            foreach (var column in defaultColumns)
            {
                if (column.Title == "Backlog")
                {
                    column.UserStoriesId.Add(result.Id);
                    await _kanBanColumnRepo.Update(column.ColumnId, column);
                }
            }
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
                var allDefaultColumns = await _kanBanColumnRepo.Get();
                foreach (var column in allDefaultColumns)
                {
                    foreach (var userStoryId in column.UserStoriesId)
                    {
                        if (userStoryId == story.Id)
                        {
                            column.UserStoriesId.Remove(story.Id);
                            await _kanBanColumnRepo.Update(column.ColumnId, column);
                            await AddToArchive(story.Id);
                            break;
                        }
                    }
                }
                return StatusCode(202);
            }

        }

        private async Task AddToArchive(string id)
        {
            var allDefaultColumns = await _kanBanColumnRepo.Get();
            foreach (var column in allDefaultColumns)
            {
                if (column.Title == "Archived")
                {
                    column.UserStoriesId.Add(id);
                    await _kanBanColumnRepo.Update(column.ColumnId, column);
                }
            }
        }
    }
}