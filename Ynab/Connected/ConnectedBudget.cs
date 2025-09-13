using Ynab.Clients;
using Ynab.Mappers;
using Ynab.Responses.Budgets;

namespace Ynab.Connected;

public class ConnectedBudget : Budget
{
    private readonly AccountsClient _accountsClient;
    private readonly CategoriesClient _categoriesClient;
    private readonly TransactionsClient _transactionsClient;
    private readonly ScheduledTransactionClient _scheduledTransactionsClient;

    public ConnectedBudget(
        AccountsClient accountsClient,
        CategoriesClient categoriesClient,
        TransactionsClient transactionsClient,
        ScheduledTransactionClient scheduledTransactionsClient,
        BudgetResponse budgetResponse) : base(budgetResponse)
    {
        _accountsClient = accountsClient;
        _categoriesClient = categoriesClient;
        _transactionsClient = transactionsClient;
        _scheduledTransactionsClient = scheduledTransactionsClient;
    }

    public Task<IEnumerable<Account>> GetAccounts() => _accountsClient.GetAccounts();
    public Task<ConnectedAccount> GetAccount(Guid id) => _accountsClient.GetAccount(id);
    public Task<IEnumerable<CategoryGroup>> GetCategoryGroups() => _categoriesClient.GetCategoryGroups();
    public Task<IEnumerable<Transaction>> GetTransactions() => _transactionsClient.GetTransactions();
    public Task<Transaction> GetTransaction(string id) => _transactionsClient.GetTransaction(id);
    public Task<ConnectedAccount> CreateAccount(NewAccount newAccount) => _accountsClient.CreateAccount(newAccount);
    
    public async Task MoveAccountTransactions(ConnectedAccount fromAccount, ConnectedAccount toAccount)
    {
        var transactionsTask = fromAccount.GetTransactions();
        var scheduledTransactionsTask = fromAccount.GetScheduledTransactions();
        
        await Task.WhenAll(transactionsTask, scheduledTransactionsTask);
        
        var transactionsToMove = transactionsTask
            .Result
            .Where(t => t.PayeeName != AutomatedPayeeNames.StartingBalance)
            .ToMovedTransactions(toAccount.Id);

        var scheduledTransactionsToMove = scheduledTransactionsTask
            .Result
            .ToMovedTransactions(toAccount.Id);

        var scheduledTransactionMoveTasks = scheduledTransactionsToMove
            .Select(movedScheduledTransaction =>
                _scheduledTransactionsClient.MoveTransaction(movedScheduledTransaction));
        
        await Task.WhenAll(scheduledTransactionMoveTasks);
        _  = await _transactionsClient.MoveTransactions(transactionsToMove);
    }
    
}