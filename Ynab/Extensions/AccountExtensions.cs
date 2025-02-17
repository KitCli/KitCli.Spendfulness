namespace Ynab.Extensions;

public static class AccountExtensions
{
    public static IEnumerable<Account> FilterByType(
        this IEnumerable<Account> accounts, params AccountType[] types)
            => accounts.Where(account => types.Contains(account.Type));
}