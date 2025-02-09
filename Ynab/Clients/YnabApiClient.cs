using System.Net.Http.Json;
using Ynab.Http;

namespace Ynab.Clients;

public class YnabApiClient
{
    protected virtual HttpClient GetHttpClient()
    {
        throw new Exception("No override");
    }

    protected async Task<YnabHttpResponseContent<TApiResponse>> Get<TApiResponse>(string url) where TApiResponse : class
    {
        var client = GetHttpClient();
        
        var response = await client.GetAsync(url);
        
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadFromJsonAsync<YnabHttpResponseContent<TApiResponse>>();
        if (responseContent is null)
        {
            throw new NullReferenceException("Response is null");
        }
        
        return responseContent;
    }
}