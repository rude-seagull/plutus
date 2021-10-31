using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Plutus.Application.Common.Interfaces;
using Plutus.Domain.Entities;

namespace Plutus.Application.AccountHandlers.Commands
{
    public class CreateAccountCommand : IRequest<Guid>
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Balance { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
    {
        private readonly IPlutusDbContext _context;

        public CreateAccountCommandHandler(IPlutusDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var newAccount = Account.CreateInstance(
                request.Title,
                request.Description,
                request.Balance);

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync(cancellationToken);
            return newAccount.Id;
        }
    }
}