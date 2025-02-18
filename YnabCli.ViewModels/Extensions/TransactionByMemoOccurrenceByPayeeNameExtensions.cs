using Ynab.Collections;
using YnabCli.ViewModels.Aggregates;

namespace YnabCli.ViewModels.Extensions;

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