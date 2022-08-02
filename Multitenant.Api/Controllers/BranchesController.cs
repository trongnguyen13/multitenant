using Microsoft.AspNetCore.Mvc;
using Multitenant.Application.Branches.Commands;
using Multitenant.Application.Branches.Queries;

namespace Multitenant.Api.Controllers
{
    public class BranchesController : ApiController
    {
        public BranchesController()
        {

        }

        [HttpPost("create")]
        public async Task<ActionResult<bool>> Create([FromBody] CreateBranchCommand createBranchCommand)
        {
            TryValidateModel(createBranchCommand);
            var response = await Mediator.Send(createBranchCommand);

            return new OkObjectResult(response); ;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<AllBranchModel>>> GetAll()
        {
            return await Mediator.Send(new GetBranchesQuery());
        }
    }
}
