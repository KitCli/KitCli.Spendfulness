using System.Text.Json.Serialization;

namespace Ynab.Http;

public class YnabHttpResponseContent<TResponseData> where TResponseData : class
{
    [JsonPropertyName("data")]
    public required TResponseData Data { get; set; }
}