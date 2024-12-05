namespace GeolocationApp.Api.Middleware
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; } = null!;
        public string ErrorId { get; set; } = null!;
    }
}