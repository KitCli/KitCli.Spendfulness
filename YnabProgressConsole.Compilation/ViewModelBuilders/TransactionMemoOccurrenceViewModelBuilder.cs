using Ynab.Sanitisers;
using YnabProgressConsole.Compilation.Aggregates;
using YnabProgressConsole.Compilation.Aggregator;
using YnabProgressConsole.Compilation.Extensions;
using YnabProgressConsole.Compilation.Formatters;

namespace YnabProgressConsole.Compilation.ViewModelBuilders;

public class TransactionMemoOccurrenceViewModelBuilder : 
    ViewModelBuilder<TransactionMemoOccurrenceAggregator, IEnumerable<TransactionMemoOccurrenceAggregate>>
{
    protected override List<List<object>> BuildRows(IEnumerable<TransactionMemoOccurrenceAggregate> aggregates)
        => aggregates
                // TODO: This is not the responsibility of the view model.
            .OrderBySortOrder(ViewModelSortOrder, aggregate => aggregate.MemoOccurrence)
            .Select(BuildMemoOccurrenceRow)
            .ToList();

    private List<object> BuildMemoOccurrenceRow(TransactionMemoOccurrenceAggregate aggregate)
    {
        var flowSanitisedAmount = TransactionFlowSanitiser.Sanitise(aggregate.AverageAmount);
        var displayableAverageAmount = CurrencyDisplayFormatter.Format(flowSanitisedAmount);

        return
        [
            aggregate.PayeeName,
            aggregate.Memo ?? string.Empty,
            aggregate.MemoOccurrence,
            displayableAverageAmount
        ];
    }
}