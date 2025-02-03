namespace Ynab.Extensions;

public static class AccountExtensions
{
    public static IEnumerable<Account> FilterToChecking(
        this IEnumerable<Account> accounts)
            => accounts.Where(x => x is { OnBudget: true, Closed: false });
}