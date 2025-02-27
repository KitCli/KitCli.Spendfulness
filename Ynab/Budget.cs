using Ynab.Clients;
using Ynab.Responses.Budgets;

namespace Ynab;

public class Budget(
    AccountsClient accountsClient,
    CategoriesClient categoriesClient,
    TransactionsClient transactionsClient,
    ScheduledTransactionsClient scheduledTransactionsClient,
    BudgetResponse budget)
{
    public Guid Id => budget.Id;

    public Task<IEnumerable<Account>> GetAccounts()
        => accountsClient.GetAccounts();

    public Task<IEnumerable<CategoryGroup>> GetCategoryGroups()
        => categoriesClient.GetCategoryGroups();
    
    public Task<IEnumerable<Transaction>> GetTransactions()
        => transactionsClient.GetTransactions();
    
    public Task<IEnumerable<ScheduledTransaction>> GetScheduledTransactions()
        => scheduledTransactionsClient.GetScheduledTransactions();
}