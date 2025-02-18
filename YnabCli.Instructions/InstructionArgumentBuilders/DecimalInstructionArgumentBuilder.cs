using YnabCli.Instructions.InstructionArguments;

namespace YnabCli.Instructions.InstructionArgumentBuilders;

public class DecimalInstructionArgumentBuilder : IInstructionArgumentBuilder
{
    public bool For(string argumentValue) => decimal.TryParse(argumentValue, out _);

    public InstructionArgument Create(string argumentName, string argumentValue)
    {
        var parsedArgumentValue = decimal.Parse(argumentValue);
        return new TypedInstructionArgument<decimal>(argumentName, parsedArgumentValue);
    }
}