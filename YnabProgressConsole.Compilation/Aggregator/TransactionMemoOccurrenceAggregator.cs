using Ynab;
using Ynab.Extensions;
using YnabProgressConsole.Compilation.Aggregates;
using YnabProgressConsole.Compilation.Extensions;

namespace YnabProgressConsole.Compilation.Aggregator;

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