using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Plutus.Application.Common.Interfaces;
using Plutus.Domain.Entities;

namespace Plutus.Application.Accounts.Queries.GetAccount;

public record GetAccountQuery(
    Guid AccountId) : IRequest<AccountResponse?>;

public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountResponse?>
{
    private readonly IPlutusDbContext _context;

    public GetAccountQueryHandler(
        IPlutusDbContext context)
    {
        _context = context;
    }

    public async Task<AccountResponse?> Handle(
        GetAccountQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .AsNoTracking()
            .ProjectToAccountResponse()
            .FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken);
    }
}