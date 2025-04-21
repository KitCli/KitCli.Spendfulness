using ConsoleTables;
using YnabCli.Commands.Handlers;

namespace YnabCli.Commands.Personalisation.Accounts.Create;

public class AccountCreateCommandHandler : CommandHandler, ICommandHandler<AccountsCreateCommand>
{
    public async Task<ConsoleTable> Handle(AccountsCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}