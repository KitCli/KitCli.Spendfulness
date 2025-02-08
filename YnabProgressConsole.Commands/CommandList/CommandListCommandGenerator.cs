using YnabProgressConsole.Instructions.InstructionArguments;

namespace YnabProgressConsole.Commands.CommandList;

public class CommandListCommandGenerator : ICommandGenerator
{
    public ICommand Generate(List<InstructionArgument> arguments)
    {
        return new CommandListCommand();
    }
}