using YnabCli.Commands.User.Active;
using YnabCli.Commands.User.Create;
using YnabCli.Commands.User.Switch;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.User;

public class UserCommandGenerator : ICommandGenerator, ITypedCommandGenerator<UserCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument> arguments)
    {
        return subCommandName switch
        {
            UserCommand.SubCommandNames.Create => GenerateCreateCommand(arguments),
            UserCommand.SubCommandNames.Switch => GenerateSwitchCommand(arguments),
            UserCommand.SubCommandNames.Active => new UserActiveCommand(),
            _ => new UserCommand()
        };
    }

    private UserCreateCommand GenerateCreateCommand(List<InstructionArgument> arguments)
    {
        var userNameArgument = arguments.OfType<string>(UserCreateCommand.ArugmentNames.UserName);

        return new UserCreateCommand
        {
            UserName = userNameArgument?.ArgumentValue
        };
    }

    private UserSwitchCommand GenerateSwitchCommand(List<InstructionArgument> arguments)
    {
        var userNameArgument = arguments.OfType<string>(UserCreateCommand.ArugmentNames.UserName);

        return new UserSwitchCommand
        {
            UserName = userNameArgument?.ArgumentValue
        };
    }
}