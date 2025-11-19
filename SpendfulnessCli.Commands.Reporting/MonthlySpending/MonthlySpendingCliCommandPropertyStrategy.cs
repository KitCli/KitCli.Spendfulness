using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Properties;
using SpendfulnessCli.Aggregation.Aggregates;

namespace SpendfulnessCli.Commands.Reporting.MonthlySpending;


public abstract class AggregatorCliCommandPropertyStrategy<TAggregate> : ICliCommandPropertyStrategy
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

// TODO: Can I achieve this via reflection.
public class MonthlySpendingCliCommandPropertyStrategy 
    : AggregatorCliCommandPropertyStrategy<IEnumerable<TransactionMonthTotalAggregate>>
{
}