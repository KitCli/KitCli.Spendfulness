using YnabProgressConsole.Compilation.Aggregator;
using YnabProgressConsole.Compilation.Formatters;

namespace YnabProgressConsole.Compilation.ViewModelBuilders;

public class CompanyDeductedBalanceViewModelBuilder : ViewModelBuilder<CategoryDeductedBalanceAggregator, decimal>
{
    protected override List<List<object>> BuildRows(decimal aggregates)
    {
        var displayable = CurrencyDisplayFormatter.Format(aggregates);
    
        return
        [
            new List<object>
            {
                displayable
            }
        ];
    }
}