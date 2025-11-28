using System.Net.Http.Headers;
using System.Text;

namespace Splitwise.Http;

public class SplitwiseHttpClientBuilder
{
    private const string BaseUrl = "https://secure.splitwise.com/api/v3.0";
    private readonly IHttpClientFactory _httpClientFactory;
    private string? _bearerToken;

    public SplitwiseHttpClientBuilder(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public SplitwiseHttpClientBuilder WithBearerToken(string bearerToken)
    {
        _bearerToken = bearerToken;
        return this;
    }
    
    public HttpClient Build(string? parentPath = null, string? nextPath = null)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var uriPath = BuilUriPath(parentPath, nextPath);

        httpClient.BaseAddress = new Uri(uriPath);

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _bearerToken);
        
        return httpClient;
    }
    
    private string BuilUriPath(string? parentPath, string? nextPath)
    {
        var uriPathBuilder = new StringBuilder();
        
        uriPathBuilder
            .Append(BaseUrl);

        if (!string.IsNullOrWhiteSpace(parentPath))
        {
            uriPathBuilder
                .Append("/")
                .Append(parentPath);
        }

        if (!string.IsNullOrWhiteSpace(nextPath))
        {
            uriPathBuilder
                .Append("/")
                .Append(nextPath);
        }
        
        uriPathBuilder
            .Append('/');

        return uriPathBuilder.ToString();
    }
}