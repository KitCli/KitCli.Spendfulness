using System.Text.Json.Serialization;

namespace Ynab.Responses.Accounts;

public record AccountResponse
{
    [JsonPropertyName("type")]
    public AccountType Type { get; set; }
    
    [JsonPropertyName("cleared_balance")]
    public required int ClearedBalance { get; set; }
}