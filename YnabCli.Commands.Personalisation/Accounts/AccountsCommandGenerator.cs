using YnabCli.Commands.Generators;
using YnabCli.Commands.Personalisation.Accounts.Identify;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Personalisation.Accounts;

public class AccountsCommandGenerator : ICommandGenerator<AccountsCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument> arguments)
        => subCommandName switch
        {
            AccountsCommand.SubCommandNames.Identify => GenerateIdentifyCommand(arguments),
            _ => new AccountsCommand()
        };

    private  AccountsIdentifyCommand GenerateIdentifyCommand(List<InstructionArgument> arguments)
    {
        var nameArgument = arguments.OfRequiredType<string>(AccountsIdentifyCommand.ArgumentNames.Name);
        var typeArgument = arguments.OfRequiredType<string>(AccountsIdentifyCommand.ArgumentNames.Type);

        return new AccountsIdentifyCommand(nameArgument.ArgumentValue, typeArgument.ArgumentValue);
    }
}