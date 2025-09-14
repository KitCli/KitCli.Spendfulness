using System.Text.Json.Serialization;

namespace Ynab.Http;

public class YnabHttpErrorResponseContent
{
    [JsonPropertyName("error")]
    public required YnabHttpError Error { get; set; }
}