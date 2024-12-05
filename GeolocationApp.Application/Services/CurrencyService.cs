using System.Net;
using GeolocationApp.Application.Configuration;
using GeolocationApp.Application.Exceptions;
using GeolocationApp.Application.Interfaces;
using GeolocationApp.Infrastructure.ExternalServices;
using GeolocationApp.Infrastructure.ExternalServices.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GeolocationApp.Application.Services
{
    public class CurrencyService(IExternalApiService apiService, IOptions<GeolocationSetting> geolocationSettings) : ICurrencyService
    {
        private List<Currency> _currencies = new();

        public async Task LoadCurrenciesAsync()
        {
            var content = await apiService.FetchDataAsync(geolocationSettings.Value.CurrencyApiUrl);
            var currencies = JsonConvert.DeserializeObject<Dictionary<string, Currency>>(content);
            if (currencies == null || currencies.Count == 0)
                throw new DataDeserializeException(HttpStatusCode.InternalServerError, "Currency API response could not be deserialized.");

            _currencies = currencies.Select(c => new Currency { Code = c.Key, Name = c.Value.Name, Symbol = c.Value.Symbol, }).ToList();
        }

        public List<Currency> GetAllCurrenciesAsync()
        {
            if (_currencies == null || !_currencies.Any())
                throw new LoadCurrenciesException(HttpStatusCode.InternalServerError, "Currencies could not be loaded.");

            return _currencies;
        }

        public Currency? GetCurrencyByCodeAsync(string code)
        {
            if (_currencies == null || !_currencies.Any())
                throw new LoadCurrenciesException(HttpStatusCode.InternalServerError, "Currencies could not be loaded.");

            return _currencies.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }
    }
}