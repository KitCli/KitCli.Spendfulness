using Ynab;

namespace YnabCli.ViewModels.Aggregator;

public class CategoryDeductedBalanceAggregator(IEnumerable<Account> accounts, IEnumerable<CategoryGroup> categoryGroups)
    : Aggregator<decimal>(accounts, categoryGroups)
{
    public override decimal Aggregate()
    {
        var availableAccountBalance = Accounts.Sum(account => account.ClearedBalance);
        var assignedToCategoryGroups = CategoryGroups.Sum(cg => cg.Available);
        
        return availableAccountBalance - assignedToCategoryGroups;
    }
}
