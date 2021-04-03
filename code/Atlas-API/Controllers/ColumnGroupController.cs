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
        private readonly IBaseRepository<ColumnGroup> _columnGroupRepo;
        private readonly IBaseRepository<Column> _columnRepo;

        public ColumnGroupController(IBaseRepository<ColumnGroup> columnGroupRepo, IBaseRepository<Column> columnRepo)
        {
            _columnGroupRepo = columnGroupRepo;
            _columnRepo = columnRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<ColumnGroup>> Get()
        {
            return await _columnGroupRepo.Get();
        }

        [HttpPost]
        public async Task<ActionResult> Post(ColumnGroup group)
        {
            var groupResult = await _columnGroupRepo.Create(group);

            var column1 = new Column()
            {
                Title = "Done",
                GroupId = groupResult.GroupId,
            };
            var column2 = new Column()
            {
                Title = "Doing",
                GroupId = groupResult.GroupId,
            };

            var column1Result = _columnRepo.Create(column1);
            var column2Result = _columnRepo.Create(column2);

            return CreatedAtAction("Post", new object[] { groupResult, await column1Result, await column2Result });
        }
    }
}