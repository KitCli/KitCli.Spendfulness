using Ynab;

namespace YnabCli.Aggregation.Aggregator.AmountAggregators;

public class CategoryDeductedAmountAggregator(IEnumerable<Account> accounts, IEnumerable<CategoryGroup> categoryGroups)
    : Aggregator<decimal>(accounts, categoryGroups)
{
    protected override decimal GenerateAggregate()
    {
        var availableAccountBalance = Accounts.Sum(account => account.ClearedBalance);
        var assignedToCategoryGroups = CategoryGroups.Sum(cg => cg.Available);
        
        return availableAccountBalance - assignedToCategoryGroups;
    }
}
