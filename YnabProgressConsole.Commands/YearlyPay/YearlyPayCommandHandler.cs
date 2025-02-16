using ConsoleTables;
using Ynab.Clients;
using YnabProgressConsole.Compilation.Aggregator;
using YnabProgressConsole.Compilation.ViewModelBuilders;
using YnabProgressConsole.Compilation.ViewModels;

namespace YnabProgressConsole.Commands.YearlyPay;

public class YearlyPayCommandHandler : CommandHandler, ICommandHandler<YearlyPayCommand>
{
    private readonly BudgetsClient _budgetsClient;
    private readonly TransactionYearAverageViewModelBuilder _averageViewModelBuilder;

    public YearlyPayCommandHandler(
        BudgetsClient budgetsClient,
        TransactionYearAverageViewModelBuilder averageViewModelBuilder)
    {
        _budgetsClient = budgetsClient;
        _averageViewModelBuilder = averageViewModelBuilder;
    }

    public async Task<ConsoleTable> Handle(YearlyPayCommand request, CancellationToken cancellationToken)
    {
        var budgets = await _budgetsClient.GetBudgets();
        
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
