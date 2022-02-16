using MediatR;
using RosaBot.Shared.Communication.Requests;

namespace RosaBot.Commands.Requests
{
    public class GetQuotationRequest : Request, IRequest<string> 
    {
        public string Parammeter { get; set; }
    }
}
