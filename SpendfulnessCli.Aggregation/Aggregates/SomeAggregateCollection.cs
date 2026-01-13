namespace SpendfulnessCli.Aggregation.Aggregates;

public class SomeAggregateCollection
{
    public string CategoryGroupName { get; set; }
    public IEnumerable<SomeAggregate> Aggregates { get; set; }
}