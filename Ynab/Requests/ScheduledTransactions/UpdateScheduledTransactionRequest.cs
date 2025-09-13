using System.Text.Json.Serialization;

namespace Ynab.Requests.ScheduledTransactions;

public class UpdateScheduledTransactionRequest
{
    [JsonPropertyName("scheduled_transaction")]
    public required ScheduledTransactionRequest ScheduledTransaction { get; set; }
}