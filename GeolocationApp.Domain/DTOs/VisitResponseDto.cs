namespace GeolocationApp.Domain.DTOs
{
    public class VisitResponseDto
    {
        public Guid Id { get; set; }
        public required string Country { get; set; }
        public string? Emoji { get; set; }
        public string? Currency { get; set; }
        public string? CurrencyName { get; set; }
        public string? Symbol { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Ip { get; set; }
        public DateTime VisitDate { get; set; }
    }
}