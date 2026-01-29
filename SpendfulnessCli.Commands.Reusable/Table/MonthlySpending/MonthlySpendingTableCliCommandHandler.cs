using KitCli.Commands.Abstractions.Handlers;
using KitCli.Commands.Abstractions.Outcomes;
using KitCli.Commands.Abstractions.Outcomes.Final;
using SpendfulnessCli.CliTables.ViewModelBuilders;

namespace SpendfulnessCli.Commands.Reusable.Table.MonthlySpending;

public class MonthlySpendingTableCliCommandHandler : ICliCommandHandler<MonthlySpendingTableCliCommand>
{
    public Task<CliCommandOutcome[]> Handle(MonthlySpendingTableCliCommand command, CancellationToken cancellationToken)
    {
        var table = new TransactionMonthChangeCliTableBuilder()
            .WithAggregator(command.Aggregator)
            .Build();

        var outcome = new CliCommandTableOutcome(table);
        
        return Task.FromResult<CliCommandOutcome[]>([outcome]);
    }
}