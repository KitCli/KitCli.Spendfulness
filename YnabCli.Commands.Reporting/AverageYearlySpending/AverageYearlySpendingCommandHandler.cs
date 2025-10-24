using Cli.Outcomes;
using ConsoleTables;
using Ynab.Extensions;
using YnabCli.Abstractions;
using YnabCli.Aggregation.Aggregator.ListAggregators;
using YnabCli.Commands.Handlers;
using YnabCli.Database;
using YnabCli.ViewModels.ViewModelBuilders;

namespace YnabCli.Commands.Reporting.AverageYearlySpending;

public class AverageYearlySpendingCommandHandler(ConfiguredBudgetClient configuredBudgetClient)
    : CommandHandler, ICommandHandler<AverageYearlySpendingCommand>
{
    public async Task<CliCommandOutcome> Handle(AverageYearlySpendingCommand request, CancellationToken cancellationToken)
    {
        var budget =  await configuredBudgetClient.GetDefaultBudget();
        
        var transactions = await budget.GetTransactions();

        var aggregator = new TransactionAverageAcrossYearAggregator(transactions)
            .BeforeAggregation(y => y.FilterToSpending())
            .BeforeAggregation(y => y.FilterToOutflow());
        
        var viewModel = new TransactionYearAverageViewModelBuilder()
            .WithAggregator(aggregator)
            .Build();
        
        return Compile(viewModel);
    }
}