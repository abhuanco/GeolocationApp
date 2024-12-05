namespace GeolocationApp.Application.DTOs
{
    public class PagedResponse<T>
    {
        public required IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}