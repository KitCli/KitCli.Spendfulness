using ConsoleTables;
using YnabCli.Commands.Factories;
using YnabCli.Commands.Handlers;
using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.ViewModelBuilders;

namespace YnabCli.Commands.Reporting.YearlySpending;

public class YearlySpendingCommandHandler : CommandHandler, ICommandHandler<YearlySpendingCommand>
{
    private readonly CommandBudgetGetter _budgetGetter;
    private readonly CategoryYearAverageViewModelBuilder _viewModelBuilder;

    public YearlySpendingCommandHandler(CommandBudgetGetter budgetGetter, CategoryYearAverageViewModelBuilder viewModelBuilder)
    {
        _budgetGetter = budgetGetter;
        _viewModelBuilder = viewModelBuilder;
    }

    public async Task<ConsoleTable> Handle(YearlySpendingCommand request, CancellationToken cancellationToken)
    {
        var budget =  await _budgetGetter.Get();
        
        var transactions = await budget.GetTransactions();
        
        var aggregator = new CategoryYearAverageAggregator(transactions);

        var viewModel = _viewModelBuilder
            .WithAggregator(aggregator)
            .Build();

        return Compile(viewModel);
    }
}