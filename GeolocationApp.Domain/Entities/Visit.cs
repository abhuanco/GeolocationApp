using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeolocationApp.Domain.Entities
{
    public class Visit: BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters.")]
        [Column(TypeName = "CHAR(100)")]
        public required string Country { get; set; }

        [StringLength(4, ErrorMessage = "Emoji name cannot exceed 4 characters.")]
        [Column(TypeName = "nvarchar(4)")]
        public string? Emoji { get; set; } = null!;

        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency name cannot exceed 3 characters.")]
        [Column(TypeName = "CHAR(3)")]
        public string? Currency { get; set; }
        
        [StringLength(50, ErrorMessage = "Currency name cannot exceed 50 characters.")]
        public string? CurrencyName { get; set; }
        
        [Column(TypeName = "CHAR(10)")]
        public string? Symbol { get; set; }
        
        public long? Latitude { get; set; }
        
        public long? Longitude { get; set; }
        public string? Ip { get; set; }
        
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime VisitDate { get; set; }
    }
}