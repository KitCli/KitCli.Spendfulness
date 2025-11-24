using Cli.Commands.Abstractions.Filters;
using Cli.Commands.Abstractions.Handlers;
using Cli.Commands.Abstractions.Outcomes;
using SpendfulnessCli.Aggregation.Aggregates;

namespace SpendfulnessCli.Commands.Reusable.Filter.MonthlySpending;

public class MonthlySpendingFilterCliCommandHandler : CliCommandHandler, ICliCommandHandler<MonthlySpendingFilterCliCommand>
{
    public Task<CliCommandOutcome[]> Handle(MonthlySpendingFilterCliCommand command, CancellationToken cancellationToken)
    {
        var appliedFilters = new List<AppliedFilter>();
        
        if (command.GreaterThan.HasValue)
        {
            command
                .Aggregator
                .AfterAggregation(aggregates =>
                    aggregates.Where(aggregate => aggregate.TotalAmount >= command.GreaterThan));

            var appliedFilter = new ValuedAppliedFilter<decimal>(
                nameof(TransactionMonthTotalAggregate.TotalAmount),
                nameof(MonthlySpendingFilterCliCommand.GreaterThan),
                command.GreaterThan.Value);
            
            appliedFilters.Add(appliedFilter);
        }
        
        return AsyncOutcomeAs(appliedFilters);
    }
}