using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Properties;
using Cli.Instructions.Abstractions;
using Spendfulness.Database;
using SpendfulnessCli.Aggregation.Aggregates;
using SpendfulnessCli.Aggregation.Aggregator;
using SpendfulnessCli.Aggregation.Aggregator.ListAggregators;
using SpendfulnessCli.CliTables.ViewModelBuilders;
using Ynab.Extensions;

namespace SpendfulnessCli.Commands.Reporting.MonthlySpending;

// TODO: Write unit tests.
public class MonthlySpendingCliCommandHandler: CliCommandHandler, ICliCommandHandler<MonthlySpendingCliCommand>
{
    private readonly SpendfulnessBudgetClient _spendfulnessBudgetClient;

    public MonthlySpendingCliCommandHandler(SpendfulnessBudgetClient spendfulnessBudgetClient)
    {
        _spendfulnessBudgetClient = spendfulnessBudgetClient;
    }

    public async Task<CliCommandOutcome> Handle(MonthlySpendingCliCommand cliCommand, CancellationToken cancellationToken)
    {
        var aggregator = await PrepareAggregator(cliCommand);

        var viewModel = new TransactionMonthChangeCliTableBuilder()
            .WithAggregator(aggregator)
            .Build();
        
        return Compile(viewModel);
    }

    private async Task<TransactionMonthTotalYnabAggregator> PrepareAggregator(MonthlySpendingCliCommand cliCommand)
    {
        var budget = await _spendfulnessBudgetClient.GetDefaultBudget();

        var transactions = await budget.GetTransactions();

        var aggregator = new TransactionMonthTotalYnabAggregator(transactions);

        if (cliCommand.CategoryId.HasValue)
        {
            aggregator.BeforeAggregation(o => o.FilterToCategories(cliCommand.CategoryId.Value));
        }
        
        // TODO: Find a better way to pass this around.
        cliCommand.Properties.Add(new AggregatorCommandProperty<TransactionMonthTotalAggregate>(aggregator));
        
        return aggregator;
    }
}

public class AggregatorCommandProperty<TAggregate>(ListYnabAggregator<TAggregate> aggregator)
    : CustomCliCommandProperty<ListYnabAggregator<TAggregate>>(typeof(TAggregate).Name, aggregator)
{
    
}

public record YnabFilterMonthlySpendingCliCommand : CliCommand
{
    
}

public class YnabFilterMonthlySpendingCliCommandGenerator : ICliCommandGenerator<YnabFilterMonthlySpendingCliCommand>
{
    public CliCommand Generate(CliInstruction instruction)
    {
        return new YnabFilterMonthlySpendingCliCommand();
    }
}

public class YnabFilterMonthlySpendingCliCommandHandler : CliCommandHandler, ICliCommandHandler<YnabFilterMonthlySpendingCliCommand>
{
    public Task<CliCommandOutcome> Handle(YnabFilterMonthlySpendingCliCommand command, CancellationToken cancellationToken)
    {
        var x = command
            .Properties
            .OfType<AggregatorCommandProperty<TransactionMonthTotalAggregate>>()
            .First();

        x.PropertyValue.AfterAggregation(y => y.Take(2));
        
        return Task.FromResult<CliCommandOutcome>(new CliCommandNothingOutcome());
    }
}
