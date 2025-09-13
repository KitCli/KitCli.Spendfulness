using System.Text.Json.Serialization;

namespace Ynab.Requests.Transactions;

public class TransactionRequest
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("account_id")]
    public Guid AccountId { get; set; }
}