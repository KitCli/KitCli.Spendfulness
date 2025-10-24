using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Commands;

public class ExitCommandGenerator : ICommandGenerator<ExitCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return new ExitCliCommand();
    }
}