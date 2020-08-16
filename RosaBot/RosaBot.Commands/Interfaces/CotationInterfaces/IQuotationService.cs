using Refit;
using System.Threading.Tasks;
using RosaBot.Commands.Models;
using System.Collections.Generic;

namespace RosaBot.Commands.Interfaces.QuotationInterfaces
{
    public interface IQuotationService
    {
        [Get("/json/usd")]
        Task<List<Quotation>> GetDolarQuotationServiceAsync();

        [Get("/json/eur")]
        Task<List<Quotation>> GetEuroQuotationServiceAsync();

        [Get("/json/gbp")]
        Task<List<Quotation>> GetLibrasEsterlinasQuotationServiceAsync();

        [Get("/json/ars")]
        Task<List<Quotation>> GetPesosArgentinosQuotationServiceAsync();

        [Get("/json/btc")]
        Task<List<Quotation>> GetBitcoinQuotationServiceAsync();
    }
}
