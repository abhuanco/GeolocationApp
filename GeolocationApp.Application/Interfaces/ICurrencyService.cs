using GeolocationApp.Infrastructure.ExternalServices.Models;

namespace GeolocationApp.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task LoadCurrenciesAsync();
        List<Currency> GetAllCurrenciesAsync();
        Currency? GetCurrencyByCodeAsync(string code);
    }
}