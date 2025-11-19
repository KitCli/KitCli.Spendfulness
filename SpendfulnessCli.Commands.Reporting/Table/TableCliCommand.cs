using Cli.Abstractions;
using Cli.Commands.Abstractions;
using SpendfulnessCli.Aggregation.Aggregates;

namespace SpendfulnessCli.Commands.Reporting.Table;

public record TableCliCommand : CliCommand
{
}
public record TransactionMonthTotalAggregateTableCliCommand : TableCliCommand
{
    public CliAggregator<IEnumerable<TransactionMonthTotalAggregate>> Aggregate { get; set; }
    
    public TransactionMonthTotalAggregateTableCliCommand(
        CliAggregator<IEnumerable<TransactionMonthTotalAggregate>> aggregate)
    {
        Aggregate = aggregate;
    }
}