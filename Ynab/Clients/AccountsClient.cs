using Ynab.Responses.Accounts;

namespace Ynab.Clients;

public class AccountsClient : YnabApiClient
{
    private const string AccountsApiPath = "accounts";
    
    public AccountsClient(string parentApiPath, List<ApiRequestLog> requestLog) : base(requestLog)
    {
        HttpClient.BaseAddress = new Uri(parentApiPath);
    }

    public async Task<IEnumerable<Account>> GetAccounts()
    {
        var response = await Get<GetAccountsResponseData>(AccountsApiPath);
        return response.Data.Accounts.Select(a => new Account(this, a));
    }
}