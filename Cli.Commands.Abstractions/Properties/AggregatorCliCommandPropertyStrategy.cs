using Cli.Commands.Abstractions.Outcomes;

namespace Cli.Commands.Abstractions.Properties;

public class AggregatorCliCommandPropertyStrategy<TAggregate> : ICliCommandPropertyStrategy
{
    public bool CanCreate(CliCommandOutcome outcome)
    {
        return outcome is CliCommandAggregatorOutcome<TAggregate>;
    }

    public CliCommandProperty CreateProperty(CliCommandOutcome outcome)
    {
        if (outcome is CliCommandAggregatorOutcome<TAggregate> aggregateOutcome)
        {
            return new AggregateCliCommandProperty<TAggregate>(aggregateOutcome.Aggregator);
        }

        throw new InvalidOperationException("Outcome is not an aggregator outcome.");
    }
}