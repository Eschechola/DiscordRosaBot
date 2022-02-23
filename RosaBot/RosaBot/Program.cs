using Microsoft.Extensions.Hosting;
using RosaBot.Application;
using RosaBot.Application.Extensions;

namespace RosaBot
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .RunAsync()
                .GetAwaiter()
                .GetResult();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
