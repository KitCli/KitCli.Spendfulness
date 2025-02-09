using Ynab.Http;
using Ynab.Responses.Budgets;

namespace Ynab.Clients;

public class BudgetsClient(YnabHttpClientFactory ynabHttpClientFactory) : YnabApiClient
{
    private const string BudgetsApiPath = "budgets";

    public async Task<IEnumerable<Budget>> GetBudgets()
    {
        var response = await Get<GetBudgetsResponseData>(BudgetsApiPath);
        return ConvertBudgetResponsesToWrappers(response.Data.Budgets);
    }

    private IEnumerable<Budget> ConvertBudgetResponsesToWrappers(IEnumerable<BudgetResponse> budgetResponses)
    {
        foreach (var budgetResponse in budgetResponses)
        {
            var parentApiPath = $"{BudgetsApiPath}/{budgetResponse.Id}";
            
            var accounts = new AccountsClient(ynabHttpClientFactory, parentApiPath);
            var categories = new CategoriesClient(ynabHttpClientFactory, parentApiPath);
            var transactions = new TransactionsClient(ynabHttpClientFactory, parentApiPath);
            var scheduledTransactions = new ScheduledTransactionsClient(ynabHttpClientFactory, parentApiPath);

            yield return new Budget(
                accounts,
                categories,
                transactions,
                scheduledTransactions,
                budgetResponse);
        }
    }

    protected override HttpClient GetHttpClient() => ynabHttpClientFactory.Create();
}