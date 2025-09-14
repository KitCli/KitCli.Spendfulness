using System.Text.Json.Serialization;

namespace Ynab.Requests.Transactions;

public class UpdateTransactionRequest(IEnumerable<TransactionRequest> transactions)
{
    [JsonPropertyName("transactions")]
    public IEnumerable<TransactionRequest> Transactions { get; set; } = transactions;
}