using System;
using RosaBot.Commands;
using RosaBot.Commands.Commands;

namespace RosaBot.Application.Factory
{
    public static class CommandFactory
    {
        public static Command GetCommand(string commandValue)
        {
            switch (commandValue)
            {
                case "cotaçao":
                    return new QuotationCommand();

                case "cotação":
                   return new QuotationCommand();

                default:
                    throw new Exception();
            }
        }
    }
}
