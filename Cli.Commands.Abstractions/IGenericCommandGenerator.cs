using Cli.Instructions.Abstractions;

namespace Cli.Commands.Abstractions;

public interface IGenericCommandGenerator
{
    // TODO: I wonder if there's any value in just pass the instruction through.
    ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments);
}