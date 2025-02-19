using System.ComponentModel.DataAnnotations;

namespace GeolocationApp.Domain.DTOs
{
    public class CreateVisit
    {
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters.")]
        public string? Country { get; set; }
        
        [StringLength(4, ErrorMessage = "Emoji name cannot exceed 4 characters.")]
        public string? Emoji { get; set; }
        
        [StringLength(3, ErrorMessage = "Emoji name cannot exceed 3 characters.")]
        public string? Currency { get; set; }
        
        [StringLength(50, ErrorMessage = "Emoji name cannot exceed 4 characters.")]
        public string? CurrencyName { get; set; }
        public string? Symbol { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Ip { get; set; }
        public DateTime VisitDate { get; set; }
    }
}