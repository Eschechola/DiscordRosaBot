using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace RosaBot.IoC.Dependencies
{
    public static class DiscordDependencies
    {
        public static IServiceCollection AddDiscordClient(this IServiceCollection services)
        {
            services.AddScoped<DiscordSocketClient>();

            return services;
        }
    }
}
