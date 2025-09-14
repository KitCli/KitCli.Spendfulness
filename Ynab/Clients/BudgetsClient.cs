using Ynab.Connected;
using Ynab.Http;
using Ynab.Responses.Budgets;

namespace Ynab.Clients;

public class BudgetsClient(YnabHttpClientBuilder builder) : YnabApiClient
{
    public async Task<IEnumerable<ConnectedBudget>> GetBudgets()
    {
        var response = await Get<GetBudgetsResponseData>(string.Empty);
        return ConvertBudgetResponsesToWrappers(response.Data.Budgets);
    }

    private IEnumerable<ConnectedBudget> ConvertBudgetResponsesToWrappers(IEnumerable<BudgetResponse> budgetResponses)
    {
        foreach (var budgetResponse in budgetResponses)
        {
            var ynabBudgetApiPath = $"{YnabApiPath.Budgets}/{budgetResponse.Id}";
            
            var accountClient = new AccountClient(builder, ynabBudgetApiPath);
            var categoryClient = new CategoryClient(builder, ynabBudgetApiPath);
            var transactionClient = new TransactionClient(builder, ynabBudgetApiPath);
            var scheduledTransactionClient = new ScheduledTransactionClient(builder, ynabBudgetApiPath);

            yield return new ConnectedBudget(
                accountClient,
                categoryClient,
                transactionClient,
                scheduledTransactionClient,
                budgetResponse);
        }
    }

    protected override HttpClient GetHttpClient() => builder.Build(
        // is already http://ynab.api/v1/
        null,
        YnabApiPath.Budgets);
}