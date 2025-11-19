using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using SpendfulnessCli.Aggregation.Aggregates;

namespace SpendfulnessCli.Commands.Reporting.Table;

public class TableCliCommandHandler : ICliCommandHandler<TableCliCommand>
{
    public Task<CliCommandOutcome> Handle(TableCliCommand request, CancellationToken cancellationToken)
    {
        var outcome =  new CliCommandNothingOutcome();
        return Task.FromResult<CliCommandOutcome>(outcome);
    }
}

public class TransactionMonthTotalAggregateTableCliCommandHandler : ICliCommandHandler<TransactionMonthTotalAggregateTableCliCommand>
{
    public Task<CliCommandOutcome> Handle(TransactionMonthTotalAggregateTableCliCommand request, CancellationToken cancellationToken)
    {
        var outcome = new CliCommandOutputOutcome("Transaction Month Total Aggregate Table");
        return Task.FromResult<CliCommandOutcome>(outcome);
    }
}