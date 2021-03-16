using System.Collections.Generic;
using Atlas_API.Entities;
using Atlas_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserStoryController : ControllerBase
    {

        private readonly IUserStoryRepository _repo;

        public UserStoryController(IUserStoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<UserStory> Get()
        {
            return _repo.GetAll();
        }
    }
}

