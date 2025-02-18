using Ynab;
using Ynab.Extensions;
using YnabCli.ViewModels.Aggregates;
using YnabCli.ViewModels.Extensions;

namespace YnabCli.ViewModels.Aggregator;

public class TransactionMemoOccurrenceAggregator(IEnumerable<Transaction> transactions)
    : Aggregator<IEnumerable<TransactionMemoOccurrenceAggregate>>(transactions)
{
    public override IEnumerable<TransactionMemoOccurrenceAggregate> Aggregate() =>
        Transactions
            .FilterToSpending()
            .GroupByPayeeName()
            .GroupByMemoOccurence()
            .AggregateMemoOccurrences();
}