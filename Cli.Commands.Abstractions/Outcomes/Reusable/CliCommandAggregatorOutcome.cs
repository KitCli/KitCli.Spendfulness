using Cli.Abstractions;
using Cli.Abstractions.Aggregators;

namespace Cli.Commands.Abstractions.Outcomes.Reusable;

public class CliCommandAggregatorOutcome<TAggregate>(CliAggregator<TAggregate> aggregator)
    : CliCommandOutcome(CliCommandOutcomeKind.Reusable)
{
    public CliAggregator<TAggregate> Aggregator { get; } = aggregator;
}