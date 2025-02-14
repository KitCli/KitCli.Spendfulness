using Ynab.Collections;
using YnabProgressConsole.Compilation.Aggregates;

namespace YnabProgressConsole.Compilation.Extensions;

public static class TransactionByMemoOccurrenceByPayeeNameExtensions
{
    public static IEnumerable<TransactionMemoOccurrenceAggregate> AggregateMemoOccurrences(
        this IEnumerable<TransactionsByMemoOccurrenceByPayeeName> transactionGroups)
    {
        foreach (var transactionGroup in transactionGroups)
        {
            foreach (var transactionSubGroup in transactionGroup.TransactionsByMemoOccurrences)
            {
                var averageAmount = transactionSubGroup.Transactions.Average(o => o.Amount);
                
                var occurrence = new TransactionMemoOccurrenceAggregate(
                    transactionGroup.PayeeName,
                    transactionSubGroup.Memo,
                    transactionSubGroup.MemoOccurence,
                    averageAmount);
                
                yield return occurrence;
            }
        }
    }
}