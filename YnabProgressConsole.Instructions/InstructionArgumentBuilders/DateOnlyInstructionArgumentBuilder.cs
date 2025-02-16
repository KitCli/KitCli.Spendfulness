using YnabProgressConsole.Instructions.InstructionArguments;

namespace YnabProgressConsole.Instructions.InstructionArgumentBuilders;

public class DateOnlyInstructionArgumentBuilder : IInstructionArgumentBuilder
{
    public bool For(string argumentValue)
        => DateTime.TryParse(argumentValue, out _);

    public InstructionArgument Create(string argumentName, string argumentValue)
    {
        var argumentDate = DateTime.Parse(argumentValue);
        var argumentDateOnly = DateOnly.FromDateTime(argumentDate);

        return new TypedInstructionArgument<DateOnly>(argumentName, argumentDateOnly);
    }
}