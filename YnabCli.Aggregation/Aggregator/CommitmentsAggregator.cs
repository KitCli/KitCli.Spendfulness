using YnabCli.Database.Commitments;

namespace YnabCli.Aggregation.Aggregator;

public class CommitmentsAggregator(ICollection<Commitment> commitments)
    : Aggregator<IEnumerable<Commitment>>(commitments)
{
    protected override IEnumerable<Commitment> GenerateAggregate() => Commitments;
}