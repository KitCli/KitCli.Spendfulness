using Ynab;

namespace YnabCli.ViewModels.Aggregator;

public class CategoryDeductedAmountAggregator(IEnumerable<Account> accounts, IEnumerable<CategoryGroup> categoryGroups)
    : AmountAggregator(accounts, categoryGroups)
{
    protected override decimal AmountAggregate()
    {
        var availableAccountBalance = Accounts.Sum(account => account.ClearedBalance);
        var assignedToCategoryGroups = CategoryGroups.Sum(cg => cg.Available);
        
        return availableAccountBalance - assignedToCategoryGroups;
    }
}
