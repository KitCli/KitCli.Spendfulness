using Ynab.Sanitisers;
using YnabCli.ViewModels.Aggregates;
using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.Extensions;
using YnabCli.ViewModels.Formatters;

namespace YnabCli.ViewModels.ViewModelBuilders;

public class TransactionMemoOccurrenceViewModelBuilder : 
    ViewModelBuilder<TransactionMemoOccurrenceAggregator, IEnumerable<TransactionMemoOccurrenceAggregate>>
{
    private int? _minimumOccurrences;
    
    public TransactionMemoOccurrenceViewModelBuilder AddMinimumOccurrencesFilter(int? minimumOccurrences)
    {
        _minimumOccurrences = minimumOccurrences;
        return this;
    }

    protected override List<List<object>> BuildRows(IEnumerable<TransactionMemoOccurrenceAggregate> aggregates)
    {
        if (_minimumOccurrences.HasValue)
        {
            aggregates = aggregates.FilterByMinimumOccurrences(_minimumOccurrences.Value);
        }
        
        return aggregates
            .OrderBySortOrder(ViewModelSortOrder, aggregate => aggregate.MemoOccurrence)
            .Select(BuildMemoOccurrenceRow)
            .ToList();
    }

    private List<object> BuildMemoOccurrenceRow(TransactionMemoOccurrenceAggregate aggregate)
    {
        var flowSanitisedAverageAmount = TransactionFlowSanitiser.Sanitise(aggregate.AverageAmount);
        var displayableAverageAmount = CurrencyDisplayFormatter.Format(flowSanitisedAverageAmount);
        
        var flowSanitisedTotalAmount = TransactionFlowSanitiser.Sanitise(aggregate.TotalAmount);
        var displayableTotalAmount = CurrencyDisplayFormatter.Format(flowSanitisedTotalAmount);

        return
        [
            aggregate.PayeeName,
            aggregate.Memo ?? string.Empty,
            aggregate.MemoOccurrence,
            displayableAverageAmount,
            displayableTotalAmount
        ];
    }
}