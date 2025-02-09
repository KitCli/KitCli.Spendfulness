using Ynab.Http;
using Ynab.Responses.Accounts;

namespace Ynab.Clients;

public class AccountsClient(YnabHttpClientFactory ynabHttpClientFactory, string parentApiPath)
    : YnabApiClient
{
    private const string AccountsApiPath = "accounts";

    public async Task<IEnumerable<Account>> GetAccounts()
    {
        var response = await Get<GetAccountsResponseData>(string.Empty);
        return response.Data.Accounts.Select(a => new Account(this, a));
    }
    
    protected override HttpClient GetHttpClient() => 
        ynabHttpClientFactory.Create(parentApiPath, AccountsApiPath);
}