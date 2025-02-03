using Ynab.Calculators;
using Ynab.Clients;
using Ynab.Responses.Accounts;

namespace Ynab;

public class Account
{
    private readonly AccountsClient _accountsClient;
    private readonly AccountResponse _accountResponse;
    
    public decimal Balance => MilliunitCalculator.Calculate(_accountResponse.Balance);
    public bool OnBudget => _accountResponse.OnBudget;
    public bool Closed => _accountResponse.Closed;
    
    public Account(AccountsClient accountsClient, AccountResponse accountResponse)
    {
        _accountsClient = accountsClient;
        _accountResponse = accountResponse;
    }
}