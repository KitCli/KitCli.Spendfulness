using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace YnabCli.Commands.Generators;

public class GenericCommandListCommandGenerator : ICommandGenerator<CommandListCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return new CommandListCommand();
    }
}