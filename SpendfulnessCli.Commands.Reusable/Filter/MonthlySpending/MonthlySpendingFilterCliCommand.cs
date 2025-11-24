using Cli.Abstractions.Aggregators;
using SpendfulnessCli.Aggregation.Aggregates;

namespace SpendfulnessCli.Commands.Reusable.Filter.MonthlySpending;

public record MonthlySpendingFilterCliCommand : FilterCliCommand
{
    public static class ArgumentNames
    {
        public const string GreaterThan = "gt";
    }
    
    public MonthlySpendingFilterCliCommand(
        CliListAggregator<TransactionMonthTotalAggregate> aggregator,
        decimal? greaterThan)
    {
        Aggregator = aggregator;
        GreaterThan = greaterThan;
    }
    
    public CliListAggregator<TransactionMonthTotalAggregate> Aggregator { get; }
    
    public decimal? GreaterThan { get; }
}