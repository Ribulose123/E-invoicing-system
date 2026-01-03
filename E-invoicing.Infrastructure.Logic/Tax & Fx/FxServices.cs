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
            if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
                throw new ArgumentException("Both fromCurrency and toCurrency are required.");

            fromCurrency = fromCurrency.ToUpper();
            toCurrency = toCurrency.ToUpper();

            // Call the API
            var response = await _httpClient.GetFromJsonAsync<FxApiResponse>($"{_baseUrl}/{fromCurrency}");

            if (response == null || !response.Rates.TryGetValue(toCurrency, out decimal rate))
                throw new Exception($"Exchange rate from {fromCurrency} to {toCurrency} not found.");

            return rate;
        }

        public class FxApiResponse
        {
            public string Base { get; set; } = string.Empty;
            public Dictionary<string, decimal> Rates { get; set; } = new();
        }
    }
}
