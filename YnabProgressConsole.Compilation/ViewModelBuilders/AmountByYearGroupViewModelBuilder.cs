using Ynab.Collections;
using YnabProgressConsole.Compilation.Calculators;
using YnabProgressConsole.Compilation.ViewModels;

namespace YnabProgressConsole.Compilation.ViewModelBuilders;

public class AmountByYearGroupViewModelBuilder 
    : ViewModelBuilder, IGroupViewModelBuilder<AmountByYear>
{
    private List<AmountByYear> _salaryIncreases = [];

    public IGroupViewModelBuilder<AmountByYear> AddGroups(IEnumerable<AmountByYear> groups)
    {
        _salaryIncreases = groups.ToList();
        return this;
    }

    public ViewModel Build()
    {
        var rows = BuildRows(_salaryIncreases);

        return new AmountByYearViewModel
        {
            Rows = rows,
            Columns = ColumnNames,
        };
    }

    private List<List<object>> BuildRows(List<AmountByYear> salaryIncreases)
    {
        // Can't increase from no basis, so first row is just a default.
        var firstSalaryIncrease  = salaryIncreases.First();
        var firstRow = BuildFirstRow(firstSalaryIncrease);
        var remainingRows = BuildRemainingRows(salaryIncreases);

        var indexedRows = remainingRows.ToList();
        indexedRows.Insert(0, firstRow);
        return indexedRows;
    }
    
    private List<object> BuildFirstRow(AmountByYear salaryIncrease) 
        => [salaryIncrease.Year, salaryIncrease.AverageAmount, "0%"];

    private IEnumerable<List<object>> BuildRemainingRows(List<AmountByYear> salaryIncreases)
    {
        // Offset from second salary increase so can be compared against first
        for (var i = 1; i < salaryIncreases.Count; i++)
        {
            var priorSalaryIncrease = salaryIncreases.ElementAt(i - 1);
            var currentSalaryIncrease = salaryIncreases.ElementAt(i);

            var percentageChange = PercentageCalculator.CalculateChange(
                priorSalaryIncrease.AverageAmount,
                currentSalaryIncrease.AverageAmount);

            var displayableAverageAmount = $"£{currentSalaryIncrease.AverageAmount}";
            var displayablePercentageChange = $"{percentageChange}%";

            yield return
            [
                currentSalaryIncrease.Year,
                displayableAverageAmount,
                displayablePercentageChange
            ];
        }
    }
}