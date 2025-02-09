using Ynab.Collections;

namespace Ynab.Extensions;

public static class TransactionsByPayeeNameExtensions
{
    public static IEnumerable<TransactionsByMemoOccurrenceByPayeeName> GroupByMemoOccurence(
        this IEnumerable<TransactionsByPayeeName> transactionsByPayeeNames, int? minimumOccurences = 2)
    {
        foreach (var transactionsByPayeeName in transactionsByPayeeNames)
        {
            var memoOccurrenceGroups = transactionsByPayeeName.GroupByMemoOccurrence();

            if (minimumOccurences is not null)
            {
                memoOccurrenceGroups = memoOccurrenceGroups
                    .Where(memoOccurrenceGroup => memoOccurrenceGroup.MemoOccurence >= minimumOccurences);
            }
            
            memoOccurrenceGroups = memoOccurrenceGroups
                .OrderByDescending(memoOccurrenceGroup => memoOccurrenceGroup.MemoOccurence);

            yield return new TransactionsByMemoOccurrenceByPayeeName
            {
                PayeeName = transactionsByPayeeName.PayeeName,
                TransactionsByMemoOccurences = memoOccurrenceGroups
            };
        }
    }
}