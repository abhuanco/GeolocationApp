using System.Net;
using GeolocationApp.Application.Configuration;
using GeolocationApp.Application.Exceptions;
using GeolocationApp.Application.Interfaces;
using GeolocationApp.Infrastructure.ExternalServices;
using GeolocationApp.Infrastructure.ExternalServices.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GeolocationApp.Application.Services
{
    public class GeolocationService(IExternalApiService apiService, IOptions<GeolocationSetting> geolocationSettings, ILogger<GeolocationService> logger): IGeolocationService
    {
        public async Task<GeoLocationResponse> GetGeoLocationAsync()
        {
            var response = await apiService.FetchDataAsync(geolocationSettings.Value.GeolocationApiUrl);

            try
            {
                var geoLocationResponse = JsonConvert.DeserializeObject<GeoLocationResponse>(response);

                if (geoLocationResponse == null)
                {
                    throw new JsonSerializationException("Deserialization resulted in a null object.");
                }

                return geoLocationResponse;
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, $"Deserialization resulted in a null object.");
                throw new DataDeserializeException(HttpStatusCode.InternalServerError, $"Geolocation API response could not be deserialized.");
            }
        }

        
    }
}