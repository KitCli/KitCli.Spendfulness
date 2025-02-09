using Ynab.Clients;
using Ynab.Responses.ScheduledTransactions;
using Ynab.Sanitisers;

namespace Ynab;

public class ScheduledTransaction(
    ScheduledTransactionsClient scheduledTransactionsClient,
    ScheduledTransactionsResponse scheduledTransactionsResponse)
{
    private readonly ScheduledTransactionsClient _scheduledTransactionsClient = scheduledTransactionsClient;

    public decimal Amount => MilliunitSanitiser.Calculate(scheduledTransactionsResponse.Amount);
    public DateTime NextOccurence => scheduledTransactionsResponse.NextOccurence;
}