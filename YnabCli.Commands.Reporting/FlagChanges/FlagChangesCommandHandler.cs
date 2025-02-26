using ConsoleTables;
using Ynab.Clients;
using Ynab.Extensions;
using YnabCli.Commands.Factories;
using YnabCli.Commands.Handlers;
using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.ViewModelBuilders;
using YnabCli.ViewModels.ViewModels;

namespace YnabCli.Commands.Reporting.FlagChanges;

public class FlagChangesCommandHandler : CommandHandler, ICommandHandler<FlagChangesCommand>
{
    private readonly BudgetsClientFactory _budgetsClientFactory;
    private readonly TransactionMonthFlaggedViewModelBuilder _viewModelBuilder;

    public FlagChangesCommandHandler(
        BudgetsClientFactory budgetsClientFactory,
        TransactionMonthFlaggedViewModelBuilder viewModelBuilder)
    {
        _budgetsClientFactory = budgetsClientFactory;
        _viewModelBuilder = viewModelBuilder;
    }

    public async Task<ConsoleTable> Handle(FlagChangesCommand command, CancellationToken cancellationToken)
    {
        var budgetsClient = await _budgetsClientFactory.Create();
        var budgets = await budgetsClient.GetBudgets();
        var budget = budgets.First();
        
        var categoryGroups = await budget.GetCategoryGroups();
        var transactions = await budget.GetTransactions();

        if (command.From.HasValue)
        {
            transactions = transactions.FilterFrom(command.From.Value);
        }

        if (command.To.HasValue)
        {
            transactions = transactions.FilterTo(command.To.Value);
        }
        
        var aggregator = new TransactionMonthFlaggedAggregator(categoryGroups, transactions);
        
        var viewModel = _viewModelBuilder
            .AddAggregator(aggregator)
            .AddColumnNames(TransactionMonthFlaggedViewModel.GetColumnNames())
            .Build();
        
        return Compile(viewModel);
    }
}