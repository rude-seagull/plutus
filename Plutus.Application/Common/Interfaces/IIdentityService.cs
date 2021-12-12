using System.Threading.Tasks;
using Plutus.Application.Users;

namespace Plutus.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<UserResponse> AuthenticateAsync(
        string email,
        string password);
}