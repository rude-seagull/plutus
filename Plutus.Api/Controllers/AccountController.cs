using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plutus.Application.AccountHandlers.Commands;

namespace Plutus.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("/api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("", Name = "CreateAccount")]
        public async Task<ActionResult> CreateAccountAsync([FromBody] CreateAccountCommand command)
        {
            var accountId = await _mediator.Send(command);
            return Ok(accountId);
        }
    }
}