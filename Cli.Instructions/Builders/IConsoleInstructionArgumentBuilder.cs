using Cli.Instructions.Abstractions;

namespace Cli.Instructions.Builders;

public interface IConsoleInstructionArgumentBuilder
{
    bool For(string? argumentValue);

    ConsoleInstructionArgument Create(string argumentName, string? argumentValue);
}
