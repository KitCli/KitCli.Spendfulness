using System.Text.Json.Serialization;

namespace Ynab.Requests.Transactions;

public class TransactionRequest
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    
    [JsonPropertyName("account_id")]
    public required Guid AccountId { get; set; }
}