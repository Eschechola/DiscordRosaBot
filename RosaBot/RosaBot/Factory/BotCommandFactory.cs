using System;
using Microsoft.Extensions.DependencyInjection;
using RosaBot.Commands;
using RosaBot.Commands.Commands;
using RosaBot.Commands.Interfaces.Commands;
using RosaBot.IoC.Provider;

namespace RosaBot.Application.Factory
{
    public static class BotCommandFactory
    {
        private static IServiceProvider _serviceProvider;

        static BotCommandFactory()
        {
            _serviceProvider = ServicesProvider.GetServiceProvider();
        }

        public static ICommand GetCommand(string commandValue)
        {
            switch (commandValue)
            {
                case "cotaçao":
                    return _serviceProvider.GetService<IQuotationCommand>();

                case "cotação":
                    return _serviceProvider.GetService<IQuotationCommand>();

                default:
                    throw new Exception();
            }
        }
    }
}
