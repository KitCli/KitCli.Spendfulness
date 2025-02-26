using ConsoleTables;
using YnabCli.Commands.Factories;
using YnabCli.Commands.Handlers;
using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.ViewModelBuilders;

namespace YnabCli.Commands.Reporting.YearlySpending;

public class YearlySpendingCommandHandler : CommandHandler, ICommandHandler<YearlySpendingCommand>
{
    private readonly BudgetsClientFactory _budgetsesClientFactory;
    private readonly CategoryYearAverageViewModelBuilder _viewModelBuilder;

    public YearlySpendingCommandHandler(BudgetsClientFactory budgetsesClientFactory, CategoryYearAverageViewModelBuilder viewModelBuilder)
    {
        _budgetsesClientFactory = budgetsesClientFactory;
        _viewModelBuilder = viewModelBuilder;
    }

    public async Task<ConsoleTable> Handle(YearlySpendingCommand request, CancellationToken cancellationToken)
    {
        var budgetsClient = await _budgetsesClientFactory.Create();
        var budgets = await budgetsClient.GetBudgets();

        var budget = budgets.First();
        
        var transactions = await budget.GetTransactions();
        
        var aggregator = new CategoryYearAverageAggregator(transactions);

        var viewModel = _viewModelBuilder
            .AddAggregator(aggregator)
            .Build();

        return Compile(viewModel);
    }
}