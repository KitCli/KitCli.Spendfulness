using Cli.Instructions.Abstractions;

namespace Cli.Instructions.Arguments;

public class TypedCliInstructionArgument<TArgumentValue>(string argumentName, TArgumentValue argumentValue)
    : CliInstructionArgument(argumentName) where TArgumentValue : notnull
{
    public TArgumentValue ArgumentValue { get; set; } = argumentValue;
}