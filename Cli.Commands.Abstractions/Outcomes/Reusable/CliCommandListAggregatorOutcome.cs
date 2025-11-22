using Cli.Abstractions;
using Cli.Abstractions.Aggregators;

namespace Cli.Commands.Abstractions.Outcomes.Reusable;

public class CliCommandListAggregatorOutcome<TAggregate>(CliListAggregator<TAggregate> aggregator)
    : CliCommandOutcome(CliCommandOutcomeKind.Reusable)
{
    public CliListAggregator<TAggregate> Aggregator { get; } = aggregator;
}