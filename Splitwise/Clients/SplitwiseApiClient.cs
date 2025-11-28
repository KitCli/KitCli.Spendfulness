using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Splitwise.Http;
using Ynab.Converters;

namespace Splitwise.Clients;

public abstract class SplitwiseApiClient
{
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true,
        Converters = { 
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            new IgnoreEmptyStringNullableEnumConverter()
        }
    };

    protected abstract HttpClient GetHttpClient();

    protected async Task<TApiResponse> Get<TApiResponse>(string path)
        where TApiResponse : class
    {
        var client = GetHttpClient();
        var response = await client.GetAsync(path);
        await EnsureSuccessfulResponse(response);
        return await ReadResponseContent<TApiResponse>(response);
    }

    protected async Task<TApiResponse> Post<TApiRequest, TApiResponse>(string path, TApiRequest payload)
        where TApiRequest : class where TApiResponse : class
    {
        var client = GetHttpClient();
        var response = await client.PostAsJsonAsync(path, payload, _jsonOptions);
        await EnsureSuccessfulResponse(response);
        return await ReadResponseContent<TApiResponse>(response);
    }

    protected async Task<TApiResponse> Patch<TApiRequest, TApiResponse>(string path, TApiRequest payload) 
        where TApiRequest : class where TApiResponse : class
    {
        var client = GetHttpClient();
        var response = await client.PatchAsJsonAsync(path, payload, _jsonOptions);
        await EnsureSuccessfulResponse(response);
        return await ReadResponseContent<TApiResponse>(response);
    }

    protected async Task<TApiResponse> Put<TApiRequest, TApiResponse>(string path, TApiRequest payload)
        where TApiRequest : class where TApiResponse : class
    {
        var client = GetHttpClient();        
        var response = await client.PutAsJsonAsync(path, payload, _jsonOptions);
        await EnsureSuccessfulResponse(response);
        return await ReadResponseContent<TApiResponse>(response);
    }
    
    private async Task EnsureSuccessfulResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var erorrResponseContent = await response.Content.ReadFromJsonAsync<SplitwiseErrorResponse>(_jsonOptions);
            if (erorrResponseContent is null)
            {
                throw new Exception( $"No error response on {response.RequestMessage?.RequestUri}");
            }
            
            throw new Exception(
                $"Splitwise API Error {erorrResponseContent.Error}");
        }
    }

    private async Task<TApiResponse> ReadResponseContent<TApiResponse>(HttpResponseMessage response)
        where TApiResponse : class
    {
        var o = await response.Content.ReadAsStringAsync();
        
        var responseContent = await response.Content.ReadFromJsonAsync<TApiResponse>(_jsonOptions);
        if (responseContent is null)
        {
            // TODO: Better exception.
            throw new Exception($"No response on {response.RequestMessage?.RequestUri}");
        }
        return responseContent;
    }
}