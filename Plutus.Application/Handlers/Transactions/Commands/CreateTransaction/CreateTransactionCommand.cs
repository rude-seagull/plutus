using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Plutus.Application.Common.Interfaces;
using Plutus.Application.Exceptions;

namespace Plutus.Application.Handlers.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(
    Guid AccountId,
    decimal Amount) : IRequest<TransactionResponse>;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionResponse>
{
    private readonly IPlutusDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateTransactionCommandHandler(
        IPlutusDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<TransactionResponse> Handle(
        CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var (accountId, amount) = request;

        var account = await _context.Accounts
            .Include(a => a.Transactions)
            .FirstOrDefaultAsync(
                a => a.Id == accountId && a.UserId == _currentUserService.UserId, 
                cancellationToken);

        if (account is null)
            throw new NotFoundException(nameof(account), request.AccountId);

        account.AddTransaction(amount);
        
        await _context.SaveChangesAsync(cancellationToken);

        return new TransactionResponse();
    }
}