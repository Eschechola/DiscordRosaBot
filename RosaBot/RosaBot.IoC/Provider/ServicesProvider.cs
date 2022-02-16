using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RosaBot.Commands.Commands;
using RosaBot.Commands.Interfaces.Commands;

namespace RosaBot.IoC.Provider
{
    public static class ServicesProvider
    {
        public static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureService();

            return serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureService(this IServiceCollection services)
        {
            var configuration = GetConfigurationEnvironment();

            services.AddSingleton(configuration);
            services.AddSingleton<ILoggerFactory>((_) => LoggerFactory.Create(builder => builder.AddConsole()));

            services.AddSingleton<DiscordSocketClient>();
            services.AddScoped<IQuotationCommand, QuotationCommand>();
        }

        private static IConfiguration GetConfigurationEnvironment()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
