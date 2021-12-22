using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plutus.Application.Common.Behaviours;

namespace Plutus.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            return services;
        }
    }
}