using Cli.Ynab.CliTables.Formatters;
using Cli.Ynab.CliTables.ViewModels;
using YnabCli.Aggregation.Aggregates;

namespace Cli.Ynab.CliTables.ViewModelBuilders;

public class TransactionYearAverageCliTableBuilder : CliTableBuilder<IEnumerable<TransactionYearAverageAggregate>>
{
    protected override List<string> BuildColumnNames(IEnumerable<TransactionYearAverageAggregate> evaluation)
        => TransactionYearAverageViewModel.GetColumnNames();

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