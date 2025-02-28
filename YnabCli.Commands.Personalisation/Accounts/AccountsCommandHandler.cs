using ConsoleTables;
using YnabCli.Commands.Exceptions;
using YnabCli.Commands.Handlers;

namespace YnabCli.Commands.Personalisation.Accounts;

public class AccountsCommandHandler : ICommandHandler<AccountsCommand>
{
    public Task<ConsoleTable> Handle(AccountsCommand request, CancellationToken cancellationToken)
    {
        throw new CommandException(
            CommandExceptionCode.NoBaseCommandFunctionality,
            "No functionality for Accounts base command");
    }
}