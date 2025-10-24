using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace Cli.Instructions.Builders;

public class DateOnlyConsoleInstructionArgumentBuilder : NoDefaultInstructionArgumentBuilder, IConsoleInstructionArgumentBuilder
{
    public bool For(string? argumentValue) => DateTime.TryParse(argumentValue, out _);

    public ConsoleInstructionArgument Create(string argumentName, string? argumentValue)
    {
        var validArgumentValue = GetValidValue(argumentName, argumentValue);
        var argumentDate = DateTime.Parse(validArgumentValue);
        var argumentDateOnly = DateOnly.FromDateTime(argumentDate);

        return new TypedConsoleInstructionArgument<DateOnly>(argumentName, argumentDateOnly);
    }
}