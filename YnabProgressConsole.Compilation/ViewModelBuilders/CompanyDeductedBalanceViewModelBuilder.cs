using YnabProgressConsole.Compilation.Evaluators;
using YnabProgressConsole.Compilation.Formatters;
using YnabProgressConsole.Compilation.ViewModels;

namespace YnabProgressConsole.Compilation.ViewModelBuilders;

public class CompanyDeductedBalanceEvaluator
    : ViewModelBuilder, IEvaluationViewModelBuilder<CategoryDeductedBalanceEvaluator, decimal>
{
    private CategoryDeductedBalanceEvaluator? _evaluator;
    
    public IEvaluationViewModelBuilder<CategoryDeductedBalanceEvaluator, decimal> AddEvaluator(CategoryDeductedBalanceEvaluator evaluator)
    {
        _evaluator = evaluator;
        return this;
    }
    
    public ViewModel Build()
    {
        if (_evaluator == null)
        {
            throw new InvalidOperationException("The evaluator must be set before calling this method.");
        }
        
        var spareMoney = _evaluator.Evaluate();
        var displayable = CurrencyDisplayFormatter.Format(spareMoney);

        var rows = new List<List<object>>
        {
            new()
            {
                displayable
            }
        };
        
        return BuildViewModel(rows);
    }
}