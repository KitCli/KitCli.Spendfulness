using Ynab;
using Ynab.Collections;
using Ynab.Extensions;
using YnabCli.Aggregation.Aggregates;

namespace YnabCli.Aggregation.Aggregator;

// TODO: This should be a list aggregator.

[Obsolete("Please do not use this method.")]
public class LegacyCategoryYearAverageAggregator(IEnumerable<Transaction> transactions)
    : Aggregator<IEnumerable<LegacyCategoryYearAverageAggregate>>(transactions)
{
    protected override IEnumerable<LegacyCategoryYearAverageAggregate> GenerateAggregate()
    {
        var transactionGroups = Transactions
            .GroupByCategory()
            .GroupByYear();
        
        return MapToAggregate(transactionGroups);
    }

    // TODO: I wonder if this could be an extension...
    private IEnumerable<LegacyCategoryYearAverageAggregate> MapToAggregate(
        IEnumerable<TransactionsByYearByCategory> transactionGroups)
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