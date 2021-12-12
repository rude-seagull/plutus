using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plutus.Application.Handlers.Transactions;
using Plutus.Application.Handlers.Transactions.Commands.CreateTransaction;
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
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(IEnumerable<TransactionResponse>))]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetTransactionsAsync(
        [FromRoute] Guid accountId)
    {
        var transactions = await _mediator.Send(new GetTransactionsQuery(accountId));
        return Ok(transactions);
    }
    
    [HttpPost("", Name = "CreateTransaction")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(TransactionResponse))]
    public async Task<ActionResult<TransactionResponse>> CreateTransactionAsync(
        [FromRoute] Guid accountId, [FromQuery] decimal amount)
    {
        var transactions = await _mediator.Send(new CreateTransactionCommand(accountId, amount));
        return Ok(transactions);
    }
}