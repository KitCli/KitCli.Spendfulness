
using YnabCli.Instructions.Arguments;

namespace YnabCli.Instructions.Builders;

public class StringInstructionArgumentBuilder : NoDefaultInstructionArgumentBuilder, IInstructionArgumentBuilder
{
    public bool For(string? argumentValue)
    {
        if (argumentValue == null) return false;
        
        var characters = argumentValue.ToCharArray();
        var nonWhiteSpacecharacters = characters.Where(c => !char.IsWhiteSpace(c));
        return nonWhiteSpacecharacters.Any(char.IsLetter);
    }

    public InstructionArgument Create(string argumentName, string? argumentValue)
    {
        var validArgumentValue = GetValidValue(argumentName, argumentValue);
        return new TypedInstructionArgument<string>(argumentName, validArgumentValue);
    }
}