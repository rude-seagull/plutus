using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Plutus.Infrastructure.Identity;

namespace Plutus.Infrastructure.Persistence.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void EnsureDatabaseCreated(
        this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var services = serviceScope.ServiceProvider;
        var dbContext = services.GetRequiredService<PlutusDbContext>();

        dbContext.Database.EnsureCreated();
    }

    public static async Task SeedIdentityDataAsync(
        this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var services = serviceScope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await PlutusDbContextSeed.SeedDefaultUsersAsync(userManager, roleManager);
    }
}