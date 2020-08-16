using Refit;
using System;
using ESCHENet.Configuration;
using System.Threading.Tasks;
using RosaBot.Commands.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using RosaBot.Commands.Interfaces.QuotationInterfaces;

namespace RosaBot.Commands.Commands
{
    public class QuotationCommand : Command
    {
        private readonly IQuotationService _quotationService;
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;

        public QuotationCommand()
        {
            _configuration = SettingsInjection.Configuration;
            _apiUrl = _configuration["API:Quotation"];
            _quotationService = RestService.For<IQuotationService>(_apiUrl);
        }

        public override string ReturnMessage(string commandValue)
        {
            if (string.IsNullOrEmpty(commandValue))
                return "Use @}cotaçao <moeda>";

            var quotation = QuotationFactory(commandValue).Result[0];

            var message = String.Format(
                "A cotação dessa moeda infeliz está: R${0:0.00}\nFonte: {1}\nCotação do dia: {2}",
                quotation.High,
                _apiUrl,
                DateTime.Now.ToString());

            return message;
        }

        private async Task<List<Quotation>> QuotationFactory(string quotationCommand)
        {
            switch (quotationCommand.ToLower())
            {
                case "dólar":
                    return await _quotationService.GetDolarQuotationServiceAsync();

                case "dolar":
                    return await _quotationService.GetDolarQuotationServiceAsync();

                case "euro":
                    return await _quotationService.GetEuroQuotationServiceAsync();

                case "libra":
                    return await _quotationService.GetLibrasEsterlinasQuotationServiceAsync();

                case "pesos":
                    return await _quotationService.GetPesosArgentinosQuotationServiceAsync();

                case "bitcoin":
                    return await _quotationService.GetBitcoinQuotationServiceAsync();

                default:
                    throw new Exception();
            }
        }
    }
}
