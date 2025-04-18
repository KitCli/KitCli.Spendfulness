using Ynab;

namespace YnabCli.ViewModels.Aggregator.AmountAggregators;

public abstract class AmountAggregator(IEnumerable<Account> accounts, IEnumerable<CategoryGroup> categoryGroups)
    : Aggregator<decimal>(accounts, categoryGroups)
{
    public override decimal Aggregate() => AmountAggregate();
    
    protected abstract decimal AmountAggregate();
}