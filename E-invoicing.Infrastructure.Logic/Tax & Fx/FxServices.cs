using E_invocing.Domin.Entities;
using E_invocing.Domin.InterFaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace E_invoicing.Infrastructure.Logic.Tax___Fx
{
    public class FxServices : IFxServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public FxServices(HttpClient httpClient, IOptions<FxApiOptions> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.BaseUrl;
        }

        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            var response = await _httpClient
                .GetFromJsonAsync<FxApiResponse>($"{_baseUrl}/{fromCurrency.ToUpper()}");

            if (response == null ||
                !response.Rates.TryGetValue(toCurrency.ToUpper(), out decimal rate))
                throw new Exception($"FX rate not found: {fromCurrency} → {toCurrency}");

            return rate;
        }
    }

    public class FxApiResponse
    {
        public Dictionary<string, decimal> Rates { get; set; } = new();
    }
}
