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
    private readonly CommandBudgetGetter _budgetGetter;
    private readonly TransactionMonthFlaggedViewModelBuilder _viewModelBuilder;

    public FlagChangesCommandHandler(
        CommandBudgetGetter budgetGetter,
        TransactionMonthFlaggedViewModelBuilder viewModelBuilder)
    {
        _budgetGetter = budgetGetter;
        _viewModelBuilder = viewModelBuilder;
    }

    public async Task<ConsoleTable> Handle(FlagChangesCommand command, CancellationToken cancellationToken)
    {
        var budget = await _budgetGetter.Get();
        
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