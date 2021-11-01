using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Plutus.Application.Common.Interfaces;
using Plutus.Application.Exceptions;
using Plutus.Application.Users;
using Plutus.Infrastructure.Identity;
using Plutus.Infrastructure.Options;

namespace Plutus.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenSecurityOptions _tokenOptions;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IOptions<TokenSecurityOptions> tokenOptions)
        {
            _userManager = userManager;
            _tokenOptions = tokenOptions.Value;
        }
        
        public async Task<UserResponse> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new NotFoundException(nameof(email), email);

            if (!await _userManager.CheckPasswordAsync(user, password))
                throw new InvalidPasswordException();
            
            var roles = await _userManager.GetRolesAsync(user);
            var token = await CreateTokenAsync(user, roles);
            
            return new UserResponse(
                user.UserName, 
                user.Email, 
                roles, 
                new JwtSecurityTokenHandler().WriteToken(token));
        }

        private async Task<JwtSecurityToken> CreateTokenAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id)
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                _tokenOptions.Issuer,
                _tokenOptions.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenOptions.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}