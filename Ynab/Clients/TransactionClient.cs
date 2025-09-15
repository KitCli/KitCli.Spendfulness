using Ynab.Http;
using Ynab.Mappers;
using Ynab.Requests.Transactions;
using Ynab.Responses.Transactions;

namespace Ynab.Clients;

public class TransactionClient(YnabHttpClientBuilder builder, string ynabBudgetApiPath) : YnabApiClient
{
    public async Task<IEnumerable<Transaction>> GetAll()
    {
        var response = await Get<GetTransactionsResponse>(string.Empty);
        return response.Data.Transactions.Select(transaction => new Transaction(transaction));
    }

    public async Task<Transaction> Get(string transactionId)
    {
        var response = await Get<GetTransactionResponse>($"{transactionId}");
        return new Transaction(response.Data.Transaction);
    }

    public async Task<IEnumerable<Transaction>> Move(IEnumerable<MovedTransaction> movedTransactions)
    {
        var request = new UpdateTransactionRequest(movedTransactions.ToTransactionRequests());
        var response = await Patch<UpdateTransactionRequest, GetTransactionsResponse>(string.Empty, request);
        return response.Data.Transactions.Select(transaction => new Transaction(transaction));
    }
    
    protected override HttpClient GetHttpClient() => builder.Build(ynabBudgetApiPath,  YnabApiPath.Transactions);
}

