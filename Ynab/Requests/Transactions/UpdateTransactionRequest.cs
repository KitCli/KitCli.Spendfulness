using System.Text.Json.Serialization;

namespace Ynab.Requests.Transactions;

public class UpdateTransactionRequest
{
    [JsonPropertyName("transactions")]
    public IEnumerable<TransactionRequest> Transactions { get; set; }
}