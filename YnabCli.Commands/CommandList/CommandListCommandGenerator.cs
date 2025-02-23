using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.CommandList;

public class CommandListCommandGenerator : ICommandGenerator, ITypedCommandGenerator<CommandListCommand>
{
    public ICommand Generate(List<InstructionArgument> arguments)
    {
        return new CommandListCommand();
    }
}