using System.Net;
using System.Threading.Tasks;
using GeolocationApp.Application.Configuration;
using GeolocationApp.Application.Exceptions;
using GeolocationApp.Application.Interfaces;
using GeolocationApp.Application.Services;
using GeolocationApp.Infrastructure.ExternalServices;
using GeolocationApp.Infrastructure.ExternalServices.Models;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Microsoft.Extensions.Options;

namespace GeolocationApp.Tests
{
    public class GeolocationServiceTests
    {
        private readonly Mock<IExternalApiService> _apiServiceMock;
        private readonly Mock<IOptions<GeolocationSetting>> _geolocationSettingsMock;
        private readonly IGeolocationService _geolocationService;

        public GeolocationServiceTests()
        {
            _apiServiceMock = new Mock<IExternalApiService>();
            _geolocationSettingsMock = new Mock<IOptions<GeolocationSetting>>();
            _geolocationSettingsMock
                .Setup(settings => settings.Value)
                .Returns(new GeolocationSetting
                {
                    GeolocationApiUrl = "https://api.example.com/geolocation",
                    CurrencyApiUrl = "https://api.vatcomply.com/currencies"
                });

            _geolocationService = new GeolocationService(_apiServiceMock.Object, _geolocationSettingsMock.Object);
        }

        [Fact]
        public async Task GetGeoLocationAsync_ShouldReturnGeoLocationResponse_WhenApiResponseIsValid()
        {
            // Arrange
            var mockApiResponse = new GeoLocationResponse
            {
                Iso2 = "BO",
                Iso3 = "BOL",
                CountryCode = "BO",
                Name = "Bolivia",
                NumericCode = 68,
                PhoneCode = "591",
                Capital = "Sucre",
                Currency = "BOB",
                Tld = ".bo",
                Region = "Americas",
                Subregion = "South America",
                Latitude = -17.0,
                Longitude = -65.0,
                Emoji = "🇧🇴",
                Ip = "2800:cd0:7035:5600:3d45:fd8e:91ba:1871"
            };

            var mockJsonResponse = JsonConvert.SerializeObject(mockApiResponse);

            _apiServiceMock
                .Setup(api => api.FetchDataAsync(It.IsAny<string>()))
                .ReturnsAsync(mockJsonResponse);

            // Act
            var result = await _geolocationService.GetGeoLocationAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Bolivia", result.Name);
            Assert.Equal("BOB", result.Currency);
        }

        [Fact]
        public async Task GetGeoLocationAsync_ShouldThrowDataDeserializeException_WhenApiResponseIsInvalid()
        {
            // Arrange
            var invalidJsonResponse = "invalid json";

            _apiServiceMock
                .Setup(api => api.FetchDataAsync(It.IsAny<string>()))
                .ReturnsAsync(invalidJsonResponse);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DataDeserializeException>(
                () => _geolocationService.GetGeoLocationAsync()
            );

            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
            Assert.Contains("Geolocation API response could not be deserialized", exception.Message);
        }

        [Fact]
        public async Task GetGeoLocationAsync_ShouldThrowException_WhenApiServiceFails()
        {
            // Arrange
            _apiServiceMock
                .Setup(api => api.FetchDataAsync(It.IsAny<string>()))
                .ThrowsAsync(new HttpRequestException("API call failed"));

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(
                () => _geolocationService.GetGeoLocationAsync()
            );
        }
    }
}