using System.Net.Http.Headers;

namespace Ynab.Http;

public class YnabHttpClientBuilder
{
    private const string BaseUrl = "https://api.ynab.com/v1";
    private readonly IHttpClientFactory _httpClientFactory;
    private string? _bearerToken;

    public YnabHttpClientBuilder(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public YnabHttpClientBuilder WithBearerToken(string bearerToken)
    {
        _bearerToken = bearerToken;
        return this;
    }
    
    public HttpClient Build(string? parentPath = null, string? nextPath = null)
    {
        var httpClient = _httpClientFactory.CreateClient();

        httpClient.BaseAddress = new Uri($"{BaseUrl}/{parentPath}/{nextPath}");

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _bearerToken);
        
        return httpClient;
    }
}