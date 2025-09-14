using Ynab.Http;
using Ynab.Requests.ScheduledTransactions;
using Ynab.Responses.ScheduledTransactions;

namespace Ynab.Clients;

public class ScheduledTransactionClient(YnabHttpClientBuilder builder, string ynabBudgetApiPath) : YnabApiClient
{
    public async Task<IEnumerable<ScheduledTransaction>> GetAll()
    {
        var response = await Get<GetScheduledTransactionsResponseData>(string.Empty);
        return response.Data.ScheduledTransactions.Select(st => new ScheduledTransaction(st));
    }

    public async Task<ScheduledTransaction> MoveTransaction(MovedScheduledTransaction movedScheduledTransaction)
    {
        var requestUrl = $"{movedScheduledTransaction.Id}";
        
        // TODO: Some kind of mapper.
        var request = new UpdateScheduledTransactionRequest
        {
            ScheduledTransaction = new ScheduledTransactionRequest
            {
                Id = movedScheduledTransaction.Id,
                AccountId = movedScheduledTransaction.AccountId
            }
        };
         
        var response = await Put<UpdateScheduledTransactionRequest, GetScheduledTransactionResponseData>(requestUrl, request);
        return new ScheduledTransaction(response.Data.ScheduledTransaction);
    }
    
    protected override HttpClient GetHttpClient() => builder.Build(ynabBudgetApiPath,  YnabApiPath.ScheduledTransactions);
}
