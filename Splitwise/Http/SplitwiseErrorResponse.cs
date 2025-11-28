using System.Text.Json.Serialization;

namespace Splitwise.Http;

public class SplitwiseErrorResponse
{
    [JsonPropertyName("error")]
    public string Error { get; set;}
}