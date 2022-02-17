using Refit;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using RosaBot.Shared.Messages;
using System.Globalization;
using MediatR;
using RosaBot.Commands.Requests;
using System.Threading;
using RosaBot.Domain.Entities;
using RosaBot.Infrastructure.ExternalServices.Interfaces.Clients;

namespace RosaBot.Commands.Handlers
{
    public class QuotationHandler : IRequestHandler<GetQuotationRequest, string>
    {
        private readonly IQuotationClient _quotationClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;

        public QuotationHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiUrl = _configuration["APIs:Quotation"];
            _quotationClient = RestService.For<IQuotationClient>(_apiUrl);
        }

        public async Task<string> Handle(GetQuotationRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Parammeter))
                return BotMessages.QuotationInvalidMessage();

            string currency = request.Parammeter;
            var quotation = await GetQuotationByCurrencyAsync(currency);

            return BotMessages.QuotationResultMessage(
                Convert.ToDouble(quotation.High, new CultureInfo("en-US")),
                _apiUrl,
                DateTime.Now);
        }

        private async Task<Quotation> GetQuotationByCurrencyAsync(string currency)
        {
            List<Quotation> quotations;

            switch (currency.ToLower())
            {
                case "dólar":
                    quotations = await _quotationClient.GetDolarQuotationServiceAsync();
                    break;

                case "dolar":
                    quotations = await _quotationClient.GetDolarQuotationServiceAsync();
                    break;

                case "euro":
                    quotations = await _quotationClient.GetEuroQuotationServiceAsync();
                    break;

                case "libra":
                    quotations = await _quotationClient.GetLibrasEsterlinasQuotationServiceAsync();
                    break;

                case "pesos":
                    quotations = await _quotationClient.GetPesosArgentinosQuotationServiceAsync();
                    break;

                case "bitcoin":
                    quotations = await _quotationClient.GetBitcoinQuotationServiceAsync();
                    
                    string highQuotation = quotations.FirstOrDefault().High.Replace(".", "");
                    quotations[0].SetHighQuotation(highQuotation);
                    break;

                default:
                    throw new Exception();
            }

            return quotations.FirstOrDefault();
        }
    }
}
