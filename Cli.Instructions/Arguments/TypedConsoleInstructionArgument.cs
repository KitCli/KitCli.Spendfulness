using Cli.Instructions.Abstractions;

namespace Cli.Instructions.Arguments;

public class TypedConsoleInstructionArgument<TArgumentValue>(string argumentName, TArgumentValue argumentValue)
    : ConsoleInstructionArgument(argumentName) where TArgumentValue : notnull
{
    public TArgumentValue ArgumentValue { get; set; } = argumentValue;
}