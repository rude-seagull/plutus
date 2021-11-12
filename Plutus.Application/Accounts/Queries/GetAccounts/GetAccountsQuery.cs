using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Plutus.Application.Common.Interfaces;

namespace Plutus.Application.Accounts.Queries.GetAccounts
{
    public record GetAccountsQuery : IRequest<List<AccountResponse>>;
    
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountResponse>>
    {
        private readonly IPlutusDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetAccountsQueryHandler(
            IPlutusDbContext context, 
            IMapper mapper, 
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<List<AccountResponse>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .AsNoTracking()
                .Where(a => a.UserId == _currentUserService.UserId)
                .ProjectTo<AccountResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}