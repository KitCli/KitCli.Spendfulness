using Ynab.Aggregates;

namespace Ynab.Extensions;

public static class AccountExtensions
{
    public static IEnumerable<Account> FilterToChecking(
        this IEnumerable<Account> accounts)
            => accounts.Where(account => account is { OnBudget: true, Closed: false });

    public static YnabAggregation<AccountBalanceAggregate> AggregateByBalance(this IEnumerable<Account> accounts)
    {
        var aggregation = accounts.Select(account => account.ToAggregate());
        return new YnabAggregation<AccountBalanceAggregate>(aggregation);
    }
}