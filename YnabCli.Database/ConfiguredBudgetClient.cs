using Ynab.Clients;
using Ynab.Connected;
using Ynab.Http;
using YnabCli.Database.Users;

namespace YnabCli.Database;

public class ConfiguredBudgetClient(YnabCliDb db, YnabHttpClientBuilder httpClientBuilder)
{
    public async Task<ConnectedBudget> GetDefaultBudget()
    {
        var activeUser = await db.GetActiveUser();
        if (activeUser == null)
        {
            throw new YnabCliDbException(YnabCliDbExceptionCode.DataNotFound, "No active user found");
        }
        
        if (activeUser.YnabApiKey is null)
        {
            throw new YnabCliDbException(YnabCliDbExceptionCode.DataNotFound, $"No {nameof(User.YnabApiKey)} setting");
        }
        
        var builder = httpClientBuilder.WithBearerToken(activeUser.YnabApiKey);
        var budgetClient = new BudgetsClient(builder);
        
        var budgets = await budgetClient.GetBudgets();
        
        return activeUser.DefaultBudgetId is null
            ? budgets.First()
            : budgets.First(budget => budget.Id == activeUser.DefaultBudgetId);
    }
}