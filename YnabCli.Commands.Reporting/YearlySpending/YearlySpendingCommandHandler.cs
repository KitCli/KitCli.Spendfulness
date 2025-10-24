using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Ynab.CliTables.ViewModelBuilders;
using ConsoleTables;
using Ynab.Extensions;
using YnabCli.Aggregation.Aggregator;
using YnabCli.Commands.Handlers;
using YnabCli.Database;

namespace YnabCli.Commands.Reporting.YearlySpending;

public class YearlySpendingCommandHandler(ConfiguredBudgetClient budgetClient)
    : CommandHandler, ICommandHandler<YearlySpendingCommand>
{
    public async Task<CliCommandOutcome> Handle(YearlySpendingCommand request, CancellationToken cancellationToken)
    {
        var budget =  await budgetClient.GetDefaultBudget();
        
        var transactions = await budget.GetTransactions();

        var aggregator = new CategoryYearAverageYnabAggregator(transactions)
            .BeforeAggregation(t => t.FilterOutTransfers())
            .BeforeAggregation(t => t.FilterOutAutomations());

        var viewModel = new CategoryYearAverageCliTableBuilder()
            .WithAggregator(aggregator)
            .Build();

        return Compile(viewModel);
    }
}