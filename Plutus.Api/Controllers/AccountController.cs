using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plutus.Application.Accounts.Commands.CreateAccount;
using Plutus.Application.Accounts.Queries.GetAccount;

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
        
        [HttpGet("{accountId:guid}", Name = "GetAccount")]
        public async Task<ActionResult> GetAccountAsync([FromRoute] Guid accountId)
        {
            var account = await _mediator.Send(new GetAccountQuery(accountId));
            return account is null ? NotFound() : Ok(account);
        }
        
        [HttpPost("", Name = "CreateAccount")]
        public async Task<ActionResult> CreateAccountAsync([FromBody] CreateAccountCommand command)
        {
            var accountId = await _mediator.Send(command);
            return Ok(accountId);
        }
    }
}