using YnabCli.Instructions.Arguments;

namespace YnabCli.Instructions.Builders;

public class DateOnlyInstructionArgumentBuilder : NoDefaultInstructionArgumentBuilder, IInstructionArgumentBuilder
{
    public bool For(string? argumentValue) => DateTime.TryParse(argumentValue, out _);

    public InstructionArgument Create(string argumentName, string? argumentValue)
    {
        var validArgumentValue = GetValidValue(argumentName, argumentValue);
        var argumentDate = DateTime.Parse(validArgumentValue);
        var argumentDateOnly = DateOnly.FromDateTime(argumentDate);

        return new TypedInstructionArgument<DateOnly>(argumentName, argumentDateOnly);
    }
}