using YnabCli.Instructions.Arguments;

namespace YnabCli.Instructions.Builders;

public class DecimalInstructionArgumentBuilder : NoDefaultInstructionArgumentBuilder, IInstructionArgumentBuilder
{
    public bool For(string? argumentValue) => decimal.TryParse(argumentValue, out _);

    public InstructionArgument Create(string argumentName, string? argumentValue)
    {
        var validArgumentValue = GetValidValue(argumentName, argumentValue);
        var parsedArgumentValue = decimal.Parse(validArgumentValue);
        return new TypedInstructionArgument<decimal>(argumentName, parsedArgumentValue);
    }
}