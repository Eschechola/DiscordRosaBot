using Refit;
using System;
using System.Threading.Tasks;
using RosaBot.Commands.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using RosaBot.Commands.Interfaces.Clients;
using System.Linq;
using RosaBot.Shared.Messages;
using RosaBot.Commands.Interfaces.Commands;
using System.Globalization;

namespace RosaBot.Commands.Commands
{
    public class QuotationCommand : Command, IQuotationCommand
    {
        private readonly IQuotationClient _quotationClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;

        public QuotationCommand(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiUrl = _configuration["APIs:Quotation"];
            _quotationClient = RestService.For<IQuotationClient>(_apiUrl);
        }

        public override async Task<string> ResultAsync(string commandValue)
        {   
            if (string.IsNullOrEmpty(commandValue))
                return BotMessages.QuotationInvalidMessage();

            string currency = commandValue;
            var quotation = await GetQuotationByCurrency(currency);

            return BotMessages.QuotationResultMessage(Convert.ToDouble(quotation.High, new CultureInfo("en-US")), _apiUrl, DateTime.Now); 
        }

        private async Task<Quotation> GetQuotationByCurrency(string currency)
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
                    break;

                default:
                    throw new Exception();
            }

            return quotations.FirstOrDefault();
        }
    }
}
