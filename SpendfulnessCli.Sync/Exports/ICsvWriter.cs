using SpendfulnessCli.Aggregation.Aggregator;

namespace SpendfulnessCli.Sync.Exports;

public interface ICsvWriter<TCsvRow> where TCsvRow : class, new()
{
    public Task Write(YnabListAggregator<TCsvRow> aggregator);
}