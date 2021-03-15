using System.Collections.Generic;
using Atlas_API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserStoryController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<UserStory> Get()
        {
            return new List<UserStory>()
                {
                    new UserStory("3211", "User Story 1", 2),
                    new UserStory("1111", "User Story 2", 6, "Some long description", "Some acceptance criteria")
                };
        }
    }
}