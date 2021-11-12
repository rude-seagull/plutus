using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Plutus.Application.Common.Interfaces;

namespace Plutus.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue("uid") ??
                                throw new ArgumentNullException(nameof(UserId));
    }
}