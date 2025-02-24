using Ynab.Http;
using Ynab.Responses.Budgets;

namespace Ynab.Clients;

public class BudgetsClient(YnabHttpClientBuilder ynabHttpClientBuilder) : YnabApiClient
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
            
            var accounts = new AccountsClient(ynabHttpClientBuilder, parentApiPath);
            var categories = new CategoriesClient(ynabHttpClientBuilder, parentApiPath);
            var transactions = new TransactionsClient(ynabHttpClientBuilder, parentApiPath);
            var scheduledTransactions = new ScheduledTransactionsClient(ynabHttpClientBuilder, parentApiPath);

            yield return new Budget(
                accounts,
                categories,
                transactions,
                scheduledTransactions,
                budgetResponse);
        }
    }

    protected override HttpClient GetHttpClient() => ynabHttpClientBuilder.Build();
}