using ConsoleTables;
using Ynab;
using YnabCli.Commands.Handlers;
using YnabCli.Commands.Organisation.MoveOnBudget;
using YnabCli.Database;

namespace YnabCli.Commands.Organisation.CopyOnBudget;

public class CopyOnBudgetCommandHandler(ConfiguredBudgetClient budgetClient) : CommandHandler, ICommandHandler<CopyOnBudgetCommand>
{
    public async Task<ConsoleTable> Handle(CopyOnBudgetCommand command, CancellationToken cancellationToken)
    {
        var budget = await budgetClient.GetDefaultBudget();

        var originalAccount = await budget.GetAccount(command.AccountId);

        ValidateAccountCanBeMoved(originalAccount);
        
        // TODO: Use old account name.
        var newAccount = new NewAccount($"[YnabCli Moved: {originalAccount.Name}]", AccountType.Checking, 0);

        var createdAccount =  await budget.CreateAccount(newAccount);
        
        await budget.MoveTransactions(originalAccount, createdAccount);

        return CompileMessage("Moved Account");
    }

    private void ValidateAccountCanBeMoved(Account account)
    {
        if (account.Closed)
        {
            // TODO: Migrate to use exceptions consistent with other handlers.
            throw new InvalidOperationException("Cannot move a closed account.");
        }

        if (account.OnBudget)
        {
            // TODO: Migrate to use exceptions consistent with other handlers.
            throw new InvalidOperationException("Account must be on budget to move.");
        }
    }
}