using Ynab;
using Ynab.Extensions;
using YnabCli.Aggregation.Aggregates;
using YnabCli.Aggregation.Extensions;

namespace YnabCli.Aggregation.Aggregator;

// TODO: This should be a list aggregator.

[Obsolete("Please do not use this method.")]
public class LegacyCategoryYearAverageAggregator(IEnumerable<Transaction> transactions)
    : Aggregator<IEnumerable<LegacyCategoryYearAverageAggregate>>(transactions)
{
    protected override IEnumerable<LegacyCategoryYearAverageAggregate> GenerateAggregate()
         => Transactions
                .GroupByCategory()
                .GroupByYear()
                .AggregateYearAverages();
}