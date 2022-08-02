using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Multitenant.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
