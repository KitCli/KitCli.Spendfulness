using YnabCli.Database.Commitments;

namespace YnabCli.ViewModels.Aggregator;

public class CommitmentsAggregator(ICollection<Commitment> commitments)
    : Aggregator<IEnumerable<Commitment>>(commitments)
{
    public override IEnumerable<Commitment> Aggregate() => Commitments;
}