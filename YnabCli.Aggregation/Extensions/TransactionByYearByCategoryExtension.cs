using Ynab.Collections;
using YnabCli.Aggregation.Aggregates;

namespace YnabCli.Aggregation.Extensions;

public static class TransactionByYearByCategoryExtension
{
    public static IEnumerable<LegacyCategoryYearAverageAggregate> AggregateYearAverages(
        this IEnumerable<TransactionsByYearByCategory> transactionGroups)
    {
        foreach (var transactionGroup in transactionGroups)
        {
            var averageAmountByYears = transactionGroup
                .TransactionsByYear
                .ToDictionary(
                    transactionByYear => transactionByYear.Year,
                    transactionByYear => transactionByYear.Transactions.Average(t => t.Amount));

            yield return new LegacyCategoryYearAverageAggregate(transactionGroup.CategoryName, averageAmountByYears);
        }
    }
}