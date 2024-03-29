﻿using Refit;
using RosaBot.Domain.Entities;

namespace RosaBot.Infrastructure.ExternalServices.Interfaces.Clients
{
    public interface IQuotationClient
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
