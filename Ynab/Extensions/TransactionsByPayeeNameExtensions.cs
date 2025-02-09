using Ynab.Collections;

namespace Ynab.Extensions;

public static class TransactionsByPayeeNameExtensions
{
    public static IEnumerable<TransactionsByMemoOccurrenceByPayeeName> GroupByMemoOccurence(
        this IEnumerable<TransactionsByPayeeName> transactionsByPayeeNames, int minimumOccurrences = 0)
    {
        foreach (var transactionsByPayeeName in transactionsByPayeeNames)
        {
            var memoOccurrenceGroups = transactionsByPayeeName.
                GroupByMemoOccurrence()
                .Where(memoOccurrenceGroup => memoOccurrenceGroup.MemoOccurence >= minimumOccurrences)
                .OrderByDescending(memoOccurrenceGroup => memoOccurrenceGroup.MemoOccurence);

            yield return new TransactionsByMemoOccurrenceByPayeeName
            {
                PayeeName = transactionsByPayeeName.PayeeName,
                TransactionsByMemoOccurences = memoOccurrenceGroups
            };
        }
    }
}