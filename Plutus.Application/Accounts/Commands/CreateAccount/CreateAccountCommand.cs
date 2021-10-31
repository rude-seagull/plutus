using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Plutus.Application.Common.Interfaces;
using Plutus.Domain.Entities;

namespace Plutus.Application.Accounts.Commands.CreateAccount
{
    public record CreateAccountCommand(string Title, string Description, decimal Balance) : IRequest<Guid>;

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
    {
        private readonly IPlutusDbContext _context;

        public CreateAccountCommandHandler(IPlutusDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var (title, description, balance) = request;
            
            var newAccount = Account.CreateInstance(
                title,
                description,
                balance);
            
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync(cancellationToken);
            return newAccount.Id;
        }
    }
}