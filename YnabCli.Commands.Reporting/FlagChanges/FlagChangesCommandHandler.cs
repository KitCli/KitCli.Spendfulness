using ConsoleTables;
using Ynab;
using Ynab.Extensions;
using YnabCli.Aggregation.Aggregator;
using YnabCli.Commands.Handlers;
using YnabCli.Database;
using YnabCli.ViewModels.ViewModelBuilders;

namespace YnabCli.Commands.Reporting.FlagChanges;

public class FlagChangesCommandHandler(ConfiguredBudgetClient configuredBudgetClient)
    : CommandHandler, ICommandHandler<FlagChangesCommand>
{
    public async Task<ConsoleTable> Handle(FlagChangesCommand command, CancellationToken cancellationToken)
    {
        var budget = await configuredBudgetClient.GetDefaultBudget();
        
        var categoryGroups = await budget.GetCategoryGroups();
        var transactions = await budget.GetTransactions();

        var castedTransactions = transactions.Cast<Transaction>();

        if (command.From.HasValue)
        {
            castedTransactions = castedTransactions
                .FilterFrom(command.From.Value);
        }

        if (command.To.HasValue)
        {
            castedTransactions = castedTransactions.FilterTo(command.To.Value);
        }
        
        var aggregator = new TransactionMonthFlaggedAggregator(categoryGroups, castedTransactions);
        
        var viewModel = new TransactionMonthFlaggedViewModelBuilder()
            .WithAggregator(aggregator)
            .Build();
        
        return Compile(viewModel);
    }
}