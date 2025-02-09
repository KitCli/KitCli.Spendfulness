using Ynab.Http;
using Ynab.Responses.ScheduledTransactions;

namespace Ynab.Clients;

public class ScheduledTransactionsClient(YnabHttpClientFactory ynabHttpClientFactory, string parentApiPath)
    : YnabApiClient
{
    private const string ScheduledTransactionsPath = "scheduled_transactions";

    public async Task<IEnumerable<ScheduledTransaction>> GetScheduledTransactions()
    {
        var response = await Get<GetScheduledTransactionResponseData>(ScheduledTransactionsPath);
        return response.Data.ScheduledTransactions.Select(st => new ScheduledTransaction(this, st));
    }
    
    protected override HttpClient GetHttpClient()
        => ynabHttpClientFactory.Create(parentApiPath, ScheduledTransactionsPath);
}