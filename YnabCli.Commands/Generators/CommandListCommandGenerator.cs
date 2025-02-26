using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Generators;

public class CommandListCommandGenerator : ICommandGenerator, ITypedCommandGenerator<CommandListCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument> arguments)
    {
        return new CommandListCommand();
    }
}