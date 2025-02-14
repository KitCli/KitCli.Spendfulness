using Ynab.Sanitisers;
using YnabProgressConsole.Compilation.Aggregates;
using YnabProgressConsole.Compilation.Evaluators;
using YnabProgressConsole.Compilation.Extensions;
using YnabProgressConsole.Compilation.Formatters;

namespace YnabProgressConsole.Compilation.ViewModelBuilders;

public class TransactionMemoOccurrenceViewModelBuilder : 
    ViewModelBuilder<TransactionMemoOccurrenceEvaluator, IEnumerable<TransactionMemoOccurrenceAggregate>>
{
    private string? _payeeNameFilter;
    private int? _minimumOccurrencesFilter;
    
    public TransactionMemoOccurrenceViewModelBuilder AddPayeeNameFilter(string payeeNameFilter)
    {
        _payeeNameFilter = payeeNameFilter;
        return this;
    }

    public TransactionMemoOccurrenceViewModelBuilder AddMinimumOccurrencesFilter(int minimumOccurrences)
    {
        _minimumOccurrencesFilter = minimumOccurrences;
        return this;
    }
    
    protected override List<List<object>> BuildRows(TransactionMemoOccurrenceEvaluator evaluator)
    {
        var evaluatedOccurrences = evaluator.Evaluate();
        
         if (_payeeNameFilter != null)
         {
             evaluatedOccurrences = evaluatedOccurrences
                 .FilterByPayeeName(_payeeNameFilter);
         }

         if (_minimumOccurrencesFilter.HasValue)
         {
             evaluatedOccurrences = evaluatedOccurrences
                 .FilterByMinimumOccurrences(_minimumOccurrencesFilter.Value);
         }
         
         return evaluatedOccurrences
             .OrderBySortOrder(ViewModelSortOrder, aggregate => aggregate.MemoOccurrence)
             .Select(BuildMemoOccurrenceRow)
             .ToList();
    }
    
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