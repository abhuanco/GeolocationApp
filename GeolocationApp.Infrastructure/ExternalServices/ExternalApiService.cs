using GeolocationApp.Infrastructure.Exceptions;

namespace GeolocationApp.Infrastructure.ExternalServices;

public class ExternalApiService(HttpClient httpClient) : IExternalApiService
{

    public async Task<string> FetchDataAsync(string url)
    {
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            throw new ExternalApiServiceException(response.StatusCode, $"Error fetching data from url: {url}");
        
        return await response.Content.ReadAsStringAsync();
    }
}