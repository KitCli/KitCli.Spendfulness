using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace Cli.Instructions.Builders;

public class BoolCliInstructionArgumentBuilder : ICliInstructionArgumentBuilder
{
    public bool For(string? argumentValue) => true;

    public CliInstructionArgument Create(string argumentName, string? argumentValue)
    {
        if (bool.TryParse(argumentValue, out var argumentBool))
        {
            return new TypedCliInstructionArgument<bool>(argumentName, argumentBool);
        }
        
        return new TypedCliInstructionArgument<bool>(argumentName, true);
    }
}