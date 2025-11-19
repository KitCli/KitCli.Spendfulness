using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;

namespace SpendfulnessCli.Commands.Reporting.Table;

public class TransactionMonthTotalAggregateTableCliCommandHandler : ICliCommandHandler<TransactionMonthTotalAggregateTableCliCommand>
{
    public Task<CliCommandOutcome[]> Handle(TransactionMonthTotalAggregateTableCliCommand request, CancellationToken cancellationToken)
    {
        var outcome = new CliCommandOutputOutcome("Transaction Month Total Aggregate Table");
        return Task.FromResult<CliCommandOutcome[]>([outcome]);
    }
}