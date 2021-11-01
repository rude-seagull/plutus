using System.Collections.Generic;

namespace Plutus.Application.Users
{
    public record UserResponse(string UserName, string Email, IEnumerable<string> Roles, string Token);
}