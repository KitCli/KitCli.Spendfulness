using Ynab.Http;
using Ynab.Responses.Transactions;

namespace Ynab.Clients;

public class TransactionsClient(YnabHttpClientFactory ynabHttpClientFactory, string parentApiPath)
    : YnabApiClient
{
    private const string TransactionsApiPath = "transactions";

    public async Task<IEnumerable<Transaction>> GetTransactions()
    {
        var response = await Get<GetTransactionsResponseData>(TransactionsApiPath);
        return response.Data.Transactions.Select(t => new Transaction(t));
    }
    
    protected override HttpClient GetHttpClient()
        => ynabHttpClientFactory.Create(parentApiPath,  TransactionsApiPath);
}

