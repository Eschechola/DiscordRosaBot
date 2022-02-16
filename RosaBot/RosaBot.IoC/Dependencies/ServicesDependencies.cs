using Microsoft.Extensions.DependencyInjection;
using RosaBot.Services.Interfaces;
using RosaBot.Services.Services;

namespace RosaBot.IoC.Dependencies
{
    public static class ServicesDependencies
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandService, CommandService>();

            return services;
        }
    }
}
