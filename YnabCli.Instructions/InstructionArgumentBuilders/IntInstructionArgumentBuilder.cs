using YnabCli.Instructions.InstructionArguments;

namespace YnabCli.Instructions.InstructionArgumentBuilders;

public class IntInstructionArgumentBuilder : NoDefaultInstructionArgumentBuilder, IInstructionArgumentBuilder
{
    public bool For(string? argumentValue)
    {
        if (argumentValue == null) return false;
        
        var characters = argumentValue.ToCharArray();
        return characters.All(char.IsNumber);
    }

    public InstructionArgument Create(string argumentName, string? argumentValue)
    {
        var validArgumentValue = GetValidValue(argumentName, argumentValue);
        var parsedArgumentValue = int.Parse(validArgumentValue);
        return new TypedInstructionArgument<int>(argumentName, parsedArgumentValue);
    }
}