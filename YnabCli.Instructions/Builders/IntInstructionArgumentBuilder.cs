using YnabCli.Instructions.Arguments;

namespace YnabCli.Instructions.Builders;

public class IntInstructionArgumentBuilder : NoDefaultInstructionArgumentBuilder, IInstructionArgumentBuilder
{
    public bool For(string? argumentValue) => argumentValue != null && int.TryParse(argumentValue, out _);

    public InstructionArgument Create(string argumentName, string? argumentValue)
    {
        var validArgumentValue = GetValidValue(argumentName, argumentValue);
        var parsedArgumentValue = int.Parse(validArgumentValue);
        return new TypedInstructionArgument<int>(argumentName, parsedArgumentValue);
    }
}