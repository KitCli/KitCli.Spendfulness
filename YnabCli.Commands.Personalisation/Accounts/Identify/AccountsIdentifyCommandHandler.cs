using Cli.Outcomes;
using ConsoleTables;
using Ynab.Extensions;
using YnabCli.Abstractions;
using YnabCli.Commands.Exceptions;
using YnabCli.Commands.Handlers;
using YnabCli.Database;
using YnabCli.Database.Accounts;

namespace YnabCli.Commands.Personalisation.Accounts.Identify;

public class AccountsIdentifyCommandHandler : CommandHandler, ICommandHandler<AccountsIdentifyCommand>
{
    private readonly YnabCliDb _db;
    private readonly ConfiguredBudgetClient _configuredBudgetClient;

    public AccountsIdentifyCommandHandler(YnabCliDb db, ConfiguredBudgetClient configuredBudgetClient)
    {
        _db = db;
        _configuredBudgetClient = configuredBudgetClient;
    }

    public async Task<CliCommandOutcome> Handle(AccountsIdentifyCommand command, CancellationToken cancellationToken)
    {
        var user = await _db.GetActiveUser();
        var accountTypes = await _db.GetAccountTypes();
        
        var budget = await _configuredBudgetClient.GetDefaultBudget();
        var accounts = await budget.GetAccounts();

        var account = accounts.Find(command.YnabAccountName);
        if (account == null)
        {
            throw new CommandException(
                CommandExceptionCode.DataWhenHandingNotFound,
                "Account not found");
        }
        
        var type = accountTypes.Find(command.CustomAccountTypeName);
        if (type == null)
        {
            throw new CommandException(
                CommandExceptionCode.DataWhenHandingNotFound,
                "Name of a custom account type not found");
        }

        var accountAccountType = user.AccountAttributes.Find(account.Id);
        if (accountAccountType != null)
        {
            accountAccountType.CustomAccountType = type;
        }

        var newAccountAccountType = new AccountAttributes
        {
            YnabAccountId = account.Id,
            CustomAccountType = type,
            User = user,
        };
        
        user.AccountAttributes.Add(newAccountAccountType);
        
        await _db.Save();
        
        return Compile($"Account {account.Name} identified as {type.Name}.");
    }
}