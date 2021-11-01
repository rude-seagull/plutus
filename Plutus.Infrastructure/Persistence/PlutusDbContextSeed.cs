using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Plutus.Application.Common.Constants;
using Plutus.Infrastructure.Identity;

namespace Plutus.Infrastructure.Persistence
{
    public static class PlutusDbContextSeed
    {
        public static async Task SeedDefaultUsersAsync(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(IdentityConstants.Roles.Root));
            await roleManager.CreateAsync(new IdentityRole(IdentityConstants.Roles.User));
            
            var users = new List<ApplicationUser>
            { 
                new()
                {
                    UserName = "rudeseagull",
                    Email = "jun.capellen@gmail.com",
                    EmailConfirmed = true
                },
                new()
                {
                    UserName = "hydrobladee",
                    Email = "samuel.he@live.fr",
                    EmailConfirmed = true
                }
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, IdentityConstants.DefaultPassword);
                var createdUser = await userManager.FindByEmailAsync(user.Email);
                await userManager.AddToRoleAsync(createdUser, IdentityConstants.Roles.Root);
            }
        }
    }
}