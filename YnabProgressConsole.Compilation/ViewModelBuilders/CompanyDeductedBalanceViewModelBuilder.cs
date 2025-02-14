using YnabProgressConsole.Compilation.Evaluators;
using YnabProgressConsole.Compilation.Formatters;

namespace YnabProgressConsole.Compilation.ViewModelBuilders;

public class CompanyDeductedBalanceViewModelBuilder : ViewModelBuilder<CategoryDeductedBalanceEvaluator, decimal>
{
    protected override List<List<object>> BuildRows(CategoryDeductedBalanceEvaluator evaluator)
    {
        var spareMoney = evaluator.Evaluate();
        var displayable = CurrencyDisplayFormatter.Format(spareMoney);
    
        return
        [
            new List<object>
            {
                displayable
            }
        ];
    }
}