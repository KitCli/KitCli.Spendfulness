using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Commands;

public class ExitCommandGenerator : ICommandGenerator<ExitCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return new ExitCommand();
    }
}