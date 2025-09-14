using Ynab.Connected;
using Ynab.Http;
using Ynab.Mappers;
using Ynab.Responses.Accounts;

namespace Ynab.Clients;

public class AccountClient(YnabHttpClientBuilder builder, string ynabBudgetApiPath) : YnabApiClient
{
    public async Task<IEnumerable<Account>> GetAccounts()
    {
        // TODO: Can i do thid without string.Empty
        var response = await Get<GetAccountsResponseData>(string.Empty);
        return response.Data.Accounts.Select(a => new Account(a));
    }
    
    public async Task<ConnectedAccount> GetAccount(Guid accountId)
    {
        var response = await Get<GetAccountResponseData>($"{accountId}");
        return ConvertAccountResponseToConnectedAccount(response.Data.Account);
    }
    
    public async Task<ConnectedAccount> CreateAccount(NewAccount newAccount)
    {
        var request = new CreateAccountRequest(newAccount.ToAccountRequest());
        // TODO: Can i do thid without string.Empty
        var response = await Post<CreateAccountRequest, CreateAccountResponse>(string.Empty, request);
        return ConvertAccountResponseToConnectedAccount(response.Data.Account);
    }
    
    private ConnectedAccount ConvertAccountResponseToConnectedAccount(AccountResponse accountResponse)
    {
        var transactionClient = new TransactionClient(builder, ynabBudgetApiPath);
        var scheduledTransactionClient = new ScheduledTransactionClient(builder, ynabBudgetApiPath);
        return new ConnectedAccount(transactionClient, scheduledTransactionClient, accountResponse);
    }
    
    protected override HttpClient GetHttpClient() => builder.Build(
        // e.g. budgets/{budgetId}/
        ynabBudgetApiPath,
        YnabApiPath.Accounts);
}