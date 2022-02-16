using RosaBot.Commands.Requests;
using RosaBot.Services.Interfaces;
using RosaBot.Shared.Communication.Mediator.Interfaces;
using RosaBot.Shared.Extensions;
using RosaBot.Shared.Messages;

namespace RosaBot.Services.Services
{
    public class CommandService : ICommandService
    {
        private readonly IMediatorHandler _mediator;

        public CommandService(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> GetCommandResponseAsync(string command, string parammeter)
        {
            command.RemoveAccents();

            return command switch
            {
                "cotacao" or
                "cotaçao" 
                    => await _mediator.SendRequestAsync(new GetQuotationRequest { Parammeter = parammeter }),

                (_) => BotMessages.ErrorMessage()
            };
        }
    }
}
