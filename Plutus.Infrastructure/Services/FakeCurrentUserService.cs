using Plutus.Application.Common.Interfaces;

namespace Plutus.Infrastructure.Services
{
    public class FakeCurrentUserService : ICurrentUserService
    {
        public string UserId => string.Empty;
    }
}