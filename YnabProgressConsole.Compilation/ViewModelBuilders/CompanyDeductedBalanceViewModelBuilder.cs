using YnabProgressConsole.Compilation.Evaluators;
using YnabProgressConsole.Compilation.Formatters;
using YnabProgressConsole.Compilation.ViewModels;

namespace YnabProgressConsole.Compilation.ViewModelBuilders;

public class CompanyDeductedBalanceEvaluator
    : ViewModelBuilder, IEvaluationViewModelBuilder<CategoryDeductedBalanceEvaluator, decimal>
{
    private CategoryDeductedBalanceEvaluator _evaluator = new();
    
    public IEvaluationViewModelBuilder<CategoryDeductedBalanceEvaluator, decimal> AddEvaluator(CategoryDeductedBalanceEvaluator evaluator)
    {
        _evaluator = evaluator;
        return this;
    }
    
    public ViewModel Build()
    {
        var spareMoney = _evaluator.Evaluate();
        var displayable = CurrencyDisplayFormatter.Format(spareMoney);
        
        return new ViewModel
        {
            ShowRowCount = ShowRowCount,
            Columns = ColumnNames,
            Rows =
            [
                new List<object>
                {
                    displayable
                }
            ]
        };
    }
}