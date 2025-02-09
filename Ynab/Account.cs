using Ynab.Aggregates;
using Ynab.Clients;
using Ynab.Responses.Accounts;
using Ynab.Sanitisers;

namespace Ynab;

public class Account(AccountsClient accountsClient, AccountResponse accountResponse)
{
    private readonly AccountsClient _accountsClient = accountsClient;
    public string Name => accountResponse.Name;
    public decimal Balance => MilliunitSanitiser.Calculate(accountResponse.Balance);
    public bool OnBudget => accountResponse.OnBudget;
    public bool Closed => accountResponse.Closed;
    public AccountBalanceAggregate ToAggregate() => new(Balance);
}