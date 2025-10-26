using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace Cli.Ynab.Commands.Reporting.MonthlySpending;

public class MonthlySpendingGenericCliCommandGenerator : ICliCommandGenerator<MonthlySpendingCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction)
    {
        var categoryIdArgument = instruction
            .Arguments
            .OfType<Guid>(MonthlySpendingCliCommand.ArgumentNames.CategoryId);

        return new MonthlySpendingCliCommand
        {
            CategoryId = categoryIdArgument?.ArgumentValue
        };
    }
}