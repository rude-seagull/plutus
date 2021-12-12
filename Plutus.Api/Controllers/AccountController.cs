using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plutus.Application.Handlers.Accounts;
using Plutus.Application.Handlers.Accounts.Commands.CreateAccount;
using Plutus.Application.Handlers.Accounts.Queries.GetAccount;
using Plutus.Application.Handlers.Accounts.Queries.GetAccounts;

namespace Plutus.Api.Controllers;

[Authorize]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("/api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{accountId:guid}", Name = "GetAccount")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(AccountResponse))]
    public async Task<ActionResult<AccountResponse>> GetAccountAsync(
        [FromRoute] Guid accountId)
    {
        var account = await _mediator.Send(new GetAccountQuery(accountId));
        return account is null ? NotFound() : Ok(account);
    }

    [HttpGet("", Name = "GetAccounts")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(AccountResponse))]
    public async Task<ActionResult<AccountResponse>> GetAccountsAsync()
    {
        var accounts = await _mediator.Send(new GetAccountsQuery());
        return Ok(accounts);
    }

    [HttpPost("", Name = "CreateAccount")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<AccountResponse>))]
    public async Task<ActionResult<List<AccountResponse>>> CreateAccountAsync(
        [FromBody] CreateAccountCommand command)
    {
        var accountId = await _mediator.Send(command);
        return Ok(accountId);
    }
}