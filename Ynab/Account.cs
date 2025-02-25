using Ynab.Responses.Accounts;
using Ynab.Sanitisers;

namespace Ynab;

public class Account(AccountResponse response)
{
    public AccountType Type => response.Type;
    public decimal ClearedBalance => MilliunitSanitiser.Calculate(response.ClearedBalance);
}