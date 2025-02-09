namespace Ynab.Aggregates;

public class YnabAggregation<TAggregation>(IEnumerable<TAggregation> aggregation)
    where TAggregation : class
{
    public IEnumerable<TAggregation> Aggregation { get; init; } = aggregation;
}