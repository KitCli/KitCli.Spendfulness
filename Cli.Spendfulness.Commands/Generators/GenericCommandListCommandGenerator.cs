using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Spendfulness.Commands.Generators;

public class GenericCommandListCommandGenerator : ICommandGenerator<CliCommandListCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return new CliCommandListCliCommand();
    }
}