using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using Cli.Spendfulness.Commands.Personalisation.Users.Active;
using Cli.Spendfulness.Commands.Personalisation.Users.Create;
using Cli.Spendfulness.Commands.Personalisation.Users.Switch;

namespace Cli.Spendfulness.Commands.Personalisation.Users;

public class UserGenericCommandGenerator : ICommandGenerator<UserCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return subCommandName switch
        {
            UserCliCommand.SubCommandNames.Create => GenerateCreateCommand(arguments),
            UserCliCommand.SubCommandNames.Switch => GenerateSwitchCommand(arguments),
            UserCliCommand.SubCommandNames.Active => new UserActiveCliCommand(),
            _ => new UserCliCommand()
        };
    }

    private UserCreateCliCommand GenerateCreateCommand(List<ConsoleInstructionArgument> arguments)
    {
        var userNameArgument = arguments.OfRequiredType<string>(UserCreateCliCommand.ArugmentNames.UserName);

        return new UserCreateCliCommand
        {
            UserName = userNameArgument.ArgumentValue
        };
    }

    private UserSwitchCliCommand GenerateSwitchCommand(List<ConsoleInstructionArgument> arguments)
    {
        var userNameArgument = arguments.OfRequiredType<string>(UserCreateCliCommand.ArugmentNames.UserName);

        return new UserSwitchCliCommand
        {
            UserName = userNameArgument.ArgumentValue
        };
    }
}