using YnabCli.Commands.User.Create;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.User;

public class UserCommandGenerator : ICommandGenerator, ITypedCommandGenerator<UserCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument> arguments)
    {
        if (subCommandName == UserCommand.SubCommandNames.Create)
        {
            var userNameArgument = arguments.OfType<string>(UserCreateCommand.ArugmentNames.UserName);

            return new UserCreateCommand
            {
                UserName = userNameArgument?.ArgumentValue
            };
        }
        
        return new UserCommand();
    }
}