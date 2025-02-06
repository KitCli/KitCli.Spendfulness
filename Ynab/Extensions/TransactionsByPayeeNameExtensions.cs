using System.Transactions;
using Ynab.Collections;

namespace Ynab.Extensions;

public static class TransactionsByPayeeNameExtensions
{
    public static IEnumerable<TransactionsByMemoOccurrenceByPayeeName> GroupByMemoOccurence(
        this IEnumerable<TransactionsByPayeeName> transactionsByPayeeNames)
    {
        foreach (var transactionsByPayeeName in transactionsByPayeeNames)
        {
            var memoOccurences = transactionsByPayeeName
                .Transactions
                .GroupBy(t => t.Memo)
                .Select(grouping => new TransactionsByMemoOccurence
                {
                    Memo = grouping.Key,
                    MemoOccurence = grouping.Count(),
                    // All transactions with that memo
                    Transactions = grouping.ToList()
                })
                // Highest memo occurence first is logical.
                .OrderByDescending(t => t.MemoOccurence)
                // TODO: Can this be passed in?
                .Take(10);

            yield return new TransactionsByMemoOccurrenceByPayeeName
            {
                PayeeName = transactionsByPayeeName.PayeeName,
                TransactionsByMemoOccurences = memoOccurences
            };
        }
    }
}