using Newtonsoft.Json;

namespace GeolocationApp.Infrastructure.ExternalServices.Models
{
    public class Currency
    {
        public required string Code { get; set; }
        
        [JsonProperty("name")]
        public required string Name { get; set; }
        
        [JsonProperty("symbol")]
        public required string Symbol { get; set; }
    }
}