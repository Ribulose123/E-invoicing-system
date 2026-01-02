using E_invocing.Domin.Entities;
using E_invocing.Domin.InterFaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;


namespace E_invoicing.Infrastructure.Logic.Tax___Fx
{
    public class TaxService: ITaxService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        private readonly Dictionary<string, decimal> _fallBackRate = new Dictionary<string, decimal>()
        {
             { "NG", 0.075M },   
            { "GB", 0.20M },    
            { "US", 0.0M },    
            { "FR", 0.20M },
            { "DE", 0.19M }
        };

        public TaxService( HttpClient httpClient, IOptions<TaxApiOptions> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.BaseUrl;
            _apiKey = options.Value.AccessKey;
        }

        public async Task<decimal> GetTaxRateAsync(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))
                throw new ArgumentException("Countrty code is requried.");

            countryCode = countryCode.ToUpper();
            try
            {

                var response = await _httpClient.GetFromJsonAsync<VatLayerResponse>(
               $"{_baseUrl}/rates?country_code={countryCode}&access_key={_apiKey}");
                if (response != null && response.Rate.HasValue)
                    return response.Rate.Value / 100M;
            }
            catch { };
            return 0m;
        }

        private class VatLayerResponse
        {
            public decimal? Rate { get; set; }
        }
    }
}
