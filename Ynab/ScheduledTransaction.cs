using Ynab.Clients;
using Ynab.Responses.ScheduledTransactions;
using Ynab.Sanitisers;

namespace Ynab;

public class ScheduledTransaction
{
    private readonly ScheduledTransactionsClient _scheduledTransactionsClient;
    private readonly ScheduledTransactionsResponse _scheduledTransactionsResponse;

    public decimal Amount => MilliunitSanitiser.Calculate(_scheduledTransactionsResponse.Amount);
    public DateTime NextOccurence => _scheduledTransactionsResponse.NextOccurence;

    public ScheduledTransaction(
        ScheduledTransactionsClient scheduledTransactionsClient,
        ScheduledTransactionsResponse scheduledTransactionsResponse)
    {
        _scheduledTransactionsClient = scheduledTransactionsClient;
        _scheduledTransactionsResponse = scheduledTransactionsResponse;
    }
}