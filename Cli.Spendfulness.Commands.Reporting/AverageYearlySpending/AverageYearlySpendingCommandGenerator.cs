using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Ynab.Commands.Reporting.AverageYearlySpending;

public class AverageYearlySpendingCommandGenerator : ICommandGenerator<AverageYearlySpendingCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction)
    {
        return new AverageYearlySpendingCliCommand();
    }
}