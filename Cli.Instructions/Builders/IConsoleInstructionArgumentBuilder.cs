using Cli.Instructions.Arguments;

namespace Cli.Instructions.Builders;

public interface IConsoleInstructionArgumentBuilder
{
    bool For(string? argumentValue);

    ConsoleInstructionArgument Create(string argumentName, string? argumentValue);
}
