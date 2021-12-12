using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Plutus.Application.Common.Interfaces;

namespace Plutus.Application.Handlers.Transactions.Queries.GetTransactions;

public record GetTransactionsQuery(
    Guid AccountId) : IRequest<List<TransactionResponse>>;

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionResponse>>
{
    private readonly IPlutusDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetTransactionsQueryHandler(
        IPlutusDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<TransactionResponse>> Handle(
        GetTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        // TODO: Expose transactions as a DbSet ? Or maybe load all transaction then map ? 
        // Check query plan anyway
        return await _context.Accounts
            .AsNoTracking()
            .Where(a => a.UserId == _currentUserService.UserId && a.Id == request.AccountId)
            .Select(a => a.Transactions.Select(t => new TransactionResponse()).ToList())
            .FirstOrDefaultAsync(cancellationToken) ?? new List<TransactionResponse>();
    }
}