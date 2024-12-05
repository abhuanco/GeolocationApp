using Newtonsoft.Json;

namespace GeolocationApp.Infrastructure.ExternalServices.Models
{
    public class GeoLocationResponse
    {
        [JsonProperty("iso2")] public string Iso2 { get; set; } = null!;

        [JsonProperty("iso3")] public string Iso3 { get; set; } = null!;
        [JsonProperty("country_code")] public string CountryCode { get; set; } = null!;

        [JsonProperty("name")] public string Name { get; set; } = null!;

        [JsonProperty("numeric_code")] public int NumericCode { get; set; } = 0;

        [JsonProperty("phone_code")] public string PhoneCode { get; set; } = null!;

        [JsonProperty("capital")] public string Capital { get; set; } = null!;

        [JsonProperty("currency")] public string Currency { get; set; } = null!;

        [JsonProperty("tld")] public string Tld { get; set; } = null!;

        [JsonProperty("region")] public string Region { get; set; } = null!;

        [JsonProperty("subregion")] public string Subregion { get; set; } = null!;

        [JsonProperty("latitude")] public double Latitude { get; set; } = 0;

        [JsonProperty("longitude")] public double Longitude { get; set; } = 0;

        [JsonProperty("emoji")] public string Emoji { get; set; } = null!;

        [JsonProperty("ip")] public string Ip { get; set; } = null!;
    }
}