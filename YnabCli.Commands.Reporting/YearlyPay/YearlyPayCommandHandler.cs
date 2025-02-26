using ConsoleTables;
using Ynab.Clients;
using YnabCli.Commands.Factories;
using YnabCli.Commands.Handlers;
using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.ViewModelBuilders;
using YnabCli.ViewModels.ViewModels;

namespace YnabCli.Commands.Reporting.YearlyPay;

public class YearlyPayCommandHandler : CommandHandler, ICommandHandler<YearlyPayCommand>
{
    private readonly BudgetsClientFactory _budgetsClientFactory;
    private readonly TransactionYearAverageViewModelBuilder _averageViewModelBuilder;

    public YearlyPayCommandHandler(BudgetsClientFactory budgetsClientFactory, TransactionYearAverageViewModelBuilder averageViewModelBuilder)
    {
        _budgetsClientFactory = budgetsClientFactory;
        _averageViewModelBuilder = averageViewModelBuilder;
    }

    public async Task<ConsoleTable> Handle(YearlyPayCommand request, CancellationToken cancellationToken)
    {
        var budgetsClient = await _budgetsClientFactory.Create();
        var budgets = await budgetsClient.GetBudgets();
        
        var budget =  budgets.First();
        
        var transactions = await budget.GetTransactions();
        
        var aggregator = new TransactionYearAverageAggregator(transactions);

        var viewModel = _averageViewModelBuilder
            .AddAggregator(aggregator)
            .AddColumnNames(TransactionYearAverageViewModel.GetColumnNames())
            .AddRowCount(false)
            .Build();

        return Compile(viewModel);
    }
}
