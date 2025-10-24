using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace Cli.Ynab.Commands.Reporting.MonthlySpending;

public class MonthlySpendingGenericCommandGenerator : ICommandGenerator<MonthlySpendingCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        var categoryIdArgument = arguments.OfType<Guid>(MonthlySpendingCliCommand.ArgumentNames.CategoryId);

        return new MonthlySpendingCliCommand
        {
            CategoryId = categoryIdArgument?.ArgumentValue
        };
    }
}