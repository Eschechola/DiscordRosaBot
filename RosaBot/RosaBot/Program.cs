using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RosaBot.Application.Events;
using RosaBot.Application.Worker;
using RosaBot.IoC.Dependencies;

namespace RosaBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureServices(services => 
                {
                    services
                    .AddHostedService<BotWorker>()
                    .AddScoped<BotEvents>()
                    .AddDiscordClient()
                    .AddServices()
                    .AddMediatR(typeof(Program))
                    .AddMediatorHandler()
                    .AddMediatorCustomHandlers();
                })
                .Build()
                .RunAsync()
                .GetAwaiter()
                .GetResult();
        }
    }
}
