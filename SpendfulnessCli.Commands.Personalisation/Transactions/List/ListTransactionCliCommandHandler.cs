using Cli.Commands.Abstractions.Handlers;
using Cli.Commands.Abstractions.Outcomes;
using Spendfulness.Database;
using SpendfulnessCli.Aggregation.Aggregator.ListAggregators;
using SpendfulnessCli.CliTables.ViewModelBuilders;
using Ynab.Extensions;

namespace SpendfulnessCli.Commands.Personalisation.Transactions.List;

public class ListTransactionCliCommandHandler(SpendfulnessBudgetClient spendfulnessBudgetClient)
    : CliCommandHandler, ICliCommandHandler<ListTransactionCliCommand>
{
    public async Task<CliCommandOutcome[]> Handle(ListTransactionCliCommand transactionCliCommand, CancellationToken cancellationToken)
    {
        var budget = await spendfulnessBudgetClient.GetDefaultBudget();

        var transactions = await budget.GetTransactions();

        var aggregator = new TransactionYnabListAggregator(transactions);

        if (transactionCliCommand.PayeeName is not null)
        {
            aggregator.BeforeAggregation(transaction
                => transaction.FilterToPayeeNames([transactionCliCommand.PayeeName]));
        }
        
        aggregator.AfterAggregation(aggregates
            => aggregates.OrderByDescending(x => x.Occured));
        
        var viewModel = new TransactionsCliTableBuilder()
            .WithAggregator(aggregator)
            .Build();

        return OutcomeAs(viewModel);
    }
}