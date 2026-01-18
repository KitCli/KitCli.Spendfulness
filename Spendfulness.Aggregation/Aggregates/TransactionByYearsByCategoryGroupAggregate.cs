namespace Spendfulness.Aggregation.Aggregates;

public record TransactionByYearsByCategoryGroupAggregate(
    string CategoryGroupName, 
    IEnumerable<TransactionByYearsByCategoryAggregate> CategoryAggregates);