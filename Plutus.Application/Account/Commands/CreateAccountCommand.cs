using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Plutus.Application.Account.Commands
{
    public class CreateAccountCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
    {
        public CreateAccountCommandHandler()
        {
            
        }
        
        public Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = Domain.Entities.Account.CreateInstance(
                request.Title, 
                request.Description, 
                request.Balance);
            
            throw new NotImplementedException();
        }
    }
}