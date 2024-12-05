using GeolocationApp.Infrastructure.ExternalServices.Models;

namespace GeolocationApp.Application.Interfaces
{
    public interface IGeolocationService
    {
        Task<GeoLocationResponse> GetGeoLocationAsync();
    }
}