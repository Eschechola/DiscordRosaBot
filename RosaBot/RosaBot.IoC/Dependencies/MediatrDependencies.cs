using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RosaBot.Commands.Handlers;
using RosaBot.Commands.Requests;
using RosaBot.Shared.Communication.Mediator;
using RosaBot.Shared.Communication.Mediator.Interfaces;

namespace RosaBot.IoC.Dependencies
{
    public static class MediatorDependencies
    {
        public static IServiceCollection AddMediatorHandler(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            return services;
        }

        public static IServiceCollection AddMediatorCustomHandlers(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetQuotationRequest, string>, QuotationHandler>();

            return services;
        }
    }
}
