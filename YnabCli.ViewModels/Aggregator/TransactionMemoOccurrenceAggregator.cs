using Ynab;
using Ynab.Extensions;
using YnabCli.ViewModels.Aggregates;
using YnabCli.ViewModels.Extensions;
using YnabCli.ViewModels.ViewModels;

namespace YnabCli.ViewModels.Aggregator;

public class TransactionMemoOccurrenceAggregator(IEnumerable<Transaction> transactions)
    : ListAggregator<TransactionMemoOccurrenceAggregate>(transactions)
{
    protected override IEnumerable<TransactionMemoOccurrenceAggregate> ListAggregate() =>
        Transactions
            .FilterToSpending()
            .GroupByPayeeName()
            .GroupByMemoOccurence()
            .AggregateMemoOccurrences();
}

public abstract class ListAggregator<TAggregate> : Aggregator<IEnumerable<TAggregate>>
{
    private readonly List<Func<IEnumerable<TAggregate>, IEnumerable<TAggregate>>> _filterFunctions = [];
    private readonly List<Func<IEnumerable<TAggregate>, IEnumerable<TAggregate>>> _sortingFunctions = [];

    protected ListAggregator(IEnumerable<Transaction> transactions)
        : base(transactions)
    {
    }

    public override IEnumerable<TAggregate> Aggregate()
    {
        // TODO: Pre-aggregation filters
        
        var specificAggregation = ListAggregate();

        // TODO: Post-aggregation filters
        foreach (var filterFunction in _filterFunctions)
        {
            specificAggregation = filterFunction(specificAggregation);
        }
        
        foreach (var soringFunction in _sortingFunctions)
        {
            specificAggregation = soringFunction(specificAggregation);
        }
        
        return specificAggregation;
    }

    public void AddAggregationFilter(Func<IEnumerable<TAggregate>, IEnumerable<TAggregate>> filter) => _filterFunctions.Add(filter);
    public void AddAggregationSorter(Func<IEnumerable<TAggregate>, IEnumerable<TAggregate>> action) => _sortingFunctions.Add(action);

    protected abstract IEnumerable<TAggregate> ListAggregate();
}