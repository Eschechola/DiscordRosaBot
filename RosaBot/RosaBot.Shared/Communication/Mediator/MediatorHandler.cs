using MediatR;
using RosaBot.Shared.Communication.Mediator.Interfaces;
using RosaBot.Shared.Communication.Requests;

namespace RosaBot.Shared.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<dynamic> SendRequestAsync<T>(T request) 
            where T : Request
             => await _mediator.Send(request);
    }
}
