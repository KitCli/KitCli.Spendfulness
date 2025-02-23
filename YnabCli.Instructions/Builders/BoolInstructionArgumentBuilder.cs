using YnabCli.Instructions.Arguments;

namespace YnabCli.Instructions.Builders;

public class BoolInstructionArgumentBuilder : IInstructionArgumentBuilder
{
    public bool For(string? argumentValue) => true;

    public InstructionArgument Create(string argumentName, string? argumentValue)
    {
        if (bool.TryParse(argumentValue, out var argumentBool))
        {
            return new TypedInstructionArgument<bool>(argumentName, argumentBool);
        }
        
        return new TypedInstructionArgument<bool>(argumentName, true);
    }
}