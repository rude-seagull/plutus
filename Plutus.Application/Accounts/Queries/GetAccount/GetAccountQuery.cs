using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Plutus.Application.Common.Interfaces;

namespace Plutus.Application.Accounts.Queries.GetAccount
{
    public record GetAccountQuery(Guid AccountId) : IRequest<AccountResponse?>;

    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountResponse?>
    {
        private readonly IPlutusDbContext _context;
        private readonly IMapper _mapper;

        public GetAccountQueryHandler(IPlutusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<AccountResponse?> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .AsNoTracking()
                .ProjectTo<AccountResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken);
        }
    }
}