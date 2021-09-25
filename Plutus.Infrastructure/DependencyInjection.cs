using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plutus.Application.Common.Interfaces;
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
            services.AddDbContext<PlutusDbContext>(dbContextOptionsBuilder =>
                dbContextOptionsBuilder.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlServerDbContextOptionsBuilder => 
                        sqlServerDbContextOptionsBuilder.MigrationsAssembly("Plutus.Api")));

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ICurrentUserService, FakeCurrentUserService>();

            return services;
        }
    }
}