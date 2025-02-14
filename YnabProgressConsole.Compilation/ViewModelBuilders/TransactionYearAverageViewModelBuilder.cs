using YnabProgressConsole.Compilation.Evaluators;
using YnabProgressConsole.Compilation.Formatters;
using YnabProgressConsole.Compilation.ViewModels;

namespace YnabProgressConsole.Compilation.ViewModelBuilders;

public class TransactionYearAverageViewModelBuilder 
    : ViewModelBuilder,
        IEvaluationViewModelBuilder<TransactionYearAverageEvaluator, IEnumerable<TransactionYearAverageAggregate>>
{
    private TransactionYearAverageEvaluator? _evaluator;
    
    public IEvaluationViewModelBuilder<TransactionYearAverageEvaluator, IEnumerable<TransactionYearAverageAggregate>> AddEvaluator(TransactionYearAverageEvaluator evaluator)
    {
        _evaluator = evaluator;
        return this;
    }

    public ViewModel Build()
    {
        if (_evaluator == null)
        {
            throw new InvalidOperationException("You must provide evaluator before calling Build()");
        }

        var transactionYearAverages = _evaluator.Evaluate();

        var rows = BuildRows(transactionYearAverages);

        return BuildViewModel(rows.ToList());
    }

    private IEnumerable<List<object>> BuildRows(IEnumerable<TransactionYearAverageAggregate> transactionYearAverages)
    {
        foreach (var transactionYearAverage in transactionYearAverages)
        {
            var displayableAverage = CurrencyDisplayFormatter.Format(transactionYearAverage.AverageAmount);
            var displayablePercentage = PercentageDisplayFormatter.Format(transactionYearAverage.PercentageChange);

            yield return
            [
                transactionYearAverage.Year,
                displayableAverage,
                displayablePercentage
            ];
        }
    }
}