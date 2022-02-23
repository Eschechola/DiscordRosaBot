using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace RosaBot.Application.Extensions
{
    public static class HostBuilderExtensions
    {
        private const string ConfigureServicesMethodName = "ConfigureServices";
        public static IHostBuilder UseStartup<TStartup>(this IHostBuilder hostBuilder)
            where TStartup : class
        {
            hostBuilder.ConfigureServices((context, serviceCollection) =>
            {
                var cfgServicesMethod = typeof(TStartup).GetMethod(
                    ConfigureServicesMethodName,
                    new Type[] { typeof(IServiceCollection) });

                var hasConfigCtor = typeof(TStartup).GetConstructor(
                    new Type[] { typeof(IConfiguration) }) != null;

                var startUpObj = hasConfigCtor ?
                    (TStartup)Activator.CreateInstance(typeof(TStartup), context.Configuration) :
                    (TStartup)Activator.CreateInstance(typeof(TStartup), null);

                cfgServicesMethod?.Invoke(startUpObj, new object[] { serviceCollection });
            });

            // chain the response
            return hostBuilder;
        }
    }
}
