using Ynab.Requests.ScheduledTransactions;

namespace Ynab.Mappers;

public static class ScheduledTransactionRequestMapping
{
    public static ScheduledTransactionRequest ToScheduledTransactionRequest(
        this MovedScheduledTransaction movedScheduledTransaction)
            => new ScheduledTransactionRequest(
                movedScheduledTransaction.Id,
                movedScheduledTransaction.AccountId);
}