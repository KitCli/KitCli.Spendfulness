namespace Cli.Abstractions.Aggregators;

public abstract class CliAggregator<TAggregation>
{
    public abstract TAggregation Aggregate();
}