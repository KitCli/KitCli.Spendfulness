using Ynab.Collections;

namespace SpendfulnessCli.Aggregation.Aggregates;

public class SomeAggregate
{
    public string CategoryName { get; set; }
    public IEnumerable<SplitTransactionsByYear> TransactionsByYears { get; set; }
}