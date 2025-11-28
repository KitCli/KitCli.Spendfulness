using System.Text.Json.Serialization;
using Splitwise.Http;

namespace Splitwise.Clients;

public class SplitwiseUserResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
}

public class UserClient : SplitwiseApiClient
{
    private readonly SplitwiseHttpClientBuilder _splitwiseHttpClientBuilder;

    public UserClient(SplitwiseHttpClientBuilder splitwiseHttpClientBuilder)
    {
        _splitwiseHttpClientBuilder = splitwiseHttpClientBuilder;
    }
    
    public Task<SplitwiseUserResponse> GetCurrentUser()
        => Get<SplitwiseUserResponse>("get_current_user");

    protected override HttpClient GetHttpClient()
        => _splitwiseHttpClientBuilder.Build();
}