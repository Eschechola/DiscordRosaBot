using MediatR;
using RosaBot.Shared.Communication.Requests;

namespace RosaBot.Shared.Communication.Mediator.Interfaces
{
    public interface IMediatorHandler
    {
        Task<dynamic> SendRequestAsync<T>(T request)
            where T : Request;
    }
}
