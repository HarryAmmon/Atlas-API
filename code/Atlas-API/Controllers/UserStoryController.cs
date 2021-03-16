using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    }
}

