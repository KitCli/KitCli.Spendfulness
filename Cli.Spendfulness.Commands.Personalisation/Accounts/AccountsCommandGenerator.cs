using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using Cli.Spendfulness.Commands.Personalisation.Accounts.Identify;

namespace Cli.Spendfulness.Commands.Personalisation.Accounts;

public class AccountsCommandGenerator : ICommandGenerator<AccountsCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
        => subCommandName switch
        {
            AccountsCliCommand.SubCommandNames.Identify => GenerateIdentifyCommand(arguments),
            _ => new AccountsCliCommand()
        };

    private  AccountsIdentifyCliCommand GenerateIdentifyCommand(List<ConsoleInstructionArgument> arguments)
    {
        var nameArgument = arguments.OfRequiredType<string>(AccountsIdentifyCliCommand.ArgumentNames.Name);
        var typeArgument = arguments.OfRequiredType<string>(AccountsIdentifyCliCommand.ArgumentNames.Type);

        return new AccountsIdentifyCliCommand(nameArgument.ArgumentValue, typeArgument.ArgumentValue);
    }
}