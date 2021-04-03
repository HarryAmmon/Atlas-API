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
        private readonly IBaseRepository<DefaultColumn> _defaultColumnRepo;
        private readonly IBaseRepository<KanBanColumn> _kanBanColumnRepo;
        public UserStoryController(IBaseRepository<UserStory> repo, IBaseRepository<DefaultColumn> defaultColumnRepo, IBaseRepository<KanBanColumn> kanBanColumnRepo)
        {
            _repo = repo;
            _defaultColumnRepo = defaultColumnRepo;
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
            var defaultColumns = await _defaultColumnRepo.Get();
            foreach (var column in defaultColumns)
            {
                if (column.Title == "Backlog")
                {
                    column.UserStoriesId.Add(result.Id);
                    await _defaultColumnRepo.Update(column.ColumnId, column);
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
            // check if the story exists

            // search kanban columns first
            // remove from kanban column and add to archived column

            // search default columns
            // remove from default column and add to archived column




            var story = await _repo.Get(id);
            if (story == null)
            {
                return NotFound();
            }
            else
            {
                await _repo.Delete(id);
                var allDefaultColumns = await _defaultColumnRepo.Get();
                bool deleted = false;
                foreach (var column in allDefaultColumns)
                {
                    foreach (var userStoryId in column.UserStoriesId)
                    {
                        if (userStoryId == story.Id)
                        {
                            column.UserStoriesId.Remove(story.Id);
                            await _defaultColumnRepo.Update(column.ColumnId, column);
                            await AddToArchive(story.Id);
                            deleted = true;
                            break;
                        }
                    }
                }
                if (!deleted)
                {
                    var allKanBanColumns = await _kanBanColumnRepo.Get();
                    foreach (var column in allKanBanColumns)
                    {
                        foreach (var userStoryId in column.UserStoriesId)
                        {
                            if (userStoryId == story.Id)
                            {
                                column.UserStoriesId.Remove(story.Id);
                                await _kanBanColumnRepo.Update(column.ColumnId, column);
                                await AddToArchive(story.Id);
                                deleted = true;
                            }
                        }
                    }
                }
                return StatusCode(202);
            }

        }

        private async Task AddToArchive(string id)
        {
            var allDefaultColumns = await _defaultColumnRepo.Get();
            foreach (var column in allDefaultColumns)
            {
                if (column.Title == "Archived")
                {
                    column.UserStoriesId.Add(id);
                    await _defaultColumnRepo.Update(column.ColumnId, column);
                }
            }
        }
    }
}