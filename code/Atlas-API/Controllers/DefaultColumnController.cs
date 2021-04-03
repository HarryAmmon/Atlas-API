using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultColumnController : ControllerBase
    {
        private readonly IBaseRepository<DefaultColumn> _defaultColumnRepo;

        public DefaultColumnController(IBaseRepository<DefaultColumn> defaultColumnRepo)
        {
            _defaultColumnRepo = defaultColumnRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<DefaultColumn>> Get()
        {
            return await _defaultColumnRepo.Get();
        }
    }
}