using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Plutus.Application.Common.Interfaces;

namespace Plutus.Application.Users.Queries;

public record AuthenticateQuery(
    string Email,
    string Password) : IRequest<UserResponse>;

public class AuthenticateQueryHandler : IRequestHandler<AuthenticateQuery, UserResponse>
{
    private readonly IIdentityService _identityService;

    public AuthenticateQueryHandler(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<UserResponse> Handle(
        AuthenticateQuery request,
        CancellationToken cancellationToken)
    {
        var (email, password) = request;
        return await _identityService.AuthenticateAsync(email, password);
    }
}