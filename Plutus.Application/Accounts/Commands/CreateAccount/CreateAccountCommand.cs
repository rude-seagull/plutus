using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Plutus.Application.Common.Interfaces;
using Plutus.Domain.Entities;

namespace Plutus.Application.Accounts.Commands.CreateAccount
{
    public record CreateAccountCommand(string Title, string Description, decimal Balance) : IRequest<AccountResponse>;

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountResponse>
    {
        private readonly IPlutusDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateAccountCommandHandler(
            IPlutusDbContext context, 
            IMapper mapper, 
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<AccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var (title, description, balance) = request;
            
            var newAccount = Account.CreateInstance(
                title,
                description,
                balance,
                _currentUserService.UserId);
            
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AccountResponse>(newAccount);
        }
    }
}