using System.Text.Json.Serialization;

namespace Ynab.Requests.Transactions;

public class UpdateTransactionRequest
{
    [JsonPropertyName("transactions")]
    public required IEnumerable<TransactionRequest> Transactions { get; set; }
}