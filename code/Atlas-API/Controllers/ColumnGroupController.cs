using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColumnGroupController : ControllerBase
    {
        private readonly IBaseRepository<ColumnGroup> _repo;

        public ColumnGroupController(IBaseRepository<ColumnGroup> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<ColumnGroup>> Get()
        {
            return await _repo.Get();
        }

        [HttpPost]
        public async Task<ActionResult> Post(ColumnGroup group)
        {
            Console.WriteLine($"request body: {group.ToString()}");
            var result = await _repo.Create(group);
            Console.WriteLine($"result: {result.ToString()}");
            return CreatedAtAction("Post", result);
        }
    }
}