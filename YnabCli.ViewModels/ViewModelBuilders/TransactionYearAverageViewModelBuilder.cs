using YnabCli.ViewModels.Aggregates;
using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.Formatters;

namespace YnabCli.ViewModels.ViewModelBuilders;

public class TransactionYearAverageViewModelBuilder :
    ViewModelBuilder<TransactionYearAverageAggregator, IEnumerable<TransactionYearAverageAggregate>>
{
    protected override List<List<object>> BuildRows(IEnumerable<TransactionYearAverageAggregate> aggregates)
    {
        var rows = BuildMultipleRows(aggregates);

        return rows.ToList();
    }
    
    private IEnumerable<List<object>> BuildMultipleRows(IEnumerable<TransactionYearAverageAggregate> transactionYearAverages)
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