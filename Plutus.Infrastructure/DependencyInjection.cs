using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plutus.Application.Common.Interfaces;
using Plutus.Infrastructure.Identity;
using Plutus.Infrastructure.Options;
using Plutus.Infrastructure.Persistence;
using Plutus.Infrastructure.Services;

namespace Plutus.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<TokenSecurityOptions>(configuration.GetSection(TokenSecurityOptions.TokenSecurity));
            
            services.AddDbContext<PlutusDbContext>(dbContextOptionsBuilder =>
                dbContextOptionsBuilder.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(typeof(PlutusDbContext).Assembly.FullName);
                        optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }));

            services.AddScoped<IPlutusDbContext>(provider => provider.GetService<PlutusDbContext>()!);
            
            services
                .AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PlutusDbContext>();
            
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}