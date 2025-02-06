using System.Text.Json.Serialization;

namespace Ynab.Responses.ScheduledTransactions;

public class GetScheduledTransactionResponseData
{
    [JsonPropertyName("scheduled_transactions")]
    public required IEnumerable<ScheduledTransactionsResponse> ScheduledTransactions { get; set; }
}