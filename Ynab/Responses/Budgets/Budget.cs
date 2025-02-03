using Ynab.Clients;
using Ynab.Extensions;

namespace Ynab.Responses.Budgets;

public class Budget
{
    private readonly AccountsClient _accountsClient;
    private readonly CategoriesClient _categoriesClient;
    private readonly TransactionsClient _transactionsClient;
    private readonly BudgetResponse _budget;


    public Budget(
        AccountsClient accountsClient,
        CategoriesClient categoriesClient,
        TransactionsClient transactionsClient,
        BudgetResponse budget)
    {
        _accountsClient = accountsClient;
        _categoriesClient = categoriesClient;
        _transactionsClient = transactionsClient;
        _budget = budget;
    }
    
    public Task<IEnumerable<Account>> GetAccounts()
        => _accountsClient.GetAccounts();

    public async Task<IEnumerable<Account>> GetCheckingAccounts()
    {
        var allAccounts = await _accountsClient.GetAccounts();
        return allAccounts.FilterToChecking();
    }

    public Task<IEnumerable<CategoryGroup>> GetCategoryGroups()
        => _categoriesClient.GetCategoryGroups();
    
    public Task<IEnumerable<Transaction>> GetTransactions()
        => _transactionsClient.GetTransactions();
}