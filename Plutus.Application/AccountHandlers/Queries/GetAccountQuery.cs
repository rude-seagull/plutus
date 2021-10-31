using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Plutus.Application.Common.Interfaces;
using Plutus.Domain.Entities;

namespace Plutus.Application.AccountHandlers.Queries
{
    public record GetAccountQuery(Guid AccountId) : IRequest<Account?>;

    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, Account?>
    {
        private readonly IPlutusDbContext _context;

        public GetAccountQueryHandler(IPlutusDbContext context)
        {
            _context = context;
        }
        
        public async Task<Account?> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken);
        }
    }
}