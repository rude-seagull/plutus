using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plutus.Application.Handlers.Transactions.Queries.GetTransactions;

namespace Plutus.Api.Controllers;

[Authorize]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("/api/accounts/{accountId:guid}/transactions")]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionController(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("", Name = "GetTransactions")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(object))]
    public async Task<ActionResult<object>> GetTransactionsAsync(
        [FromRoute] Guid accountId)
    {
        var transactions = await _mediator.Send(new GetTransactionsQuery(accountId));
        return Ok(transactions);
    }
}