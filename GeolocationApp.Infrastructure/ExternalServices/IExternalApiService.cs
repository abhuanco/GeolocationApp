namespace GeolocationApp.Infrastructure.ExternalServices
{
    public interface IExternalApiService
    {
        Task<string> FetchDataAsync(string url);
    }
}