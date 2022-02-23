using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RosaBot.Application.Events;
using RosaBot.Application.Worker;
using RosaBot.IoC.Dependencies;

namespace RosaBot.Application
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables()
                .AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHostedService<BotWorker>()
                .AddScoped<BotEvents>()
                .AddDiscordClient()
                .AddServices()
                .AddMediatR(typeof(Program))
                .AddMediatorHandler()
                .AddMediatorCustomHandlers();
        }
    }
}
