using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Ynab.Commands.Reporting.AverageYearlySpending;

public class AverageYearlySpendingCliCommandGenerator : ICliCommandGenerator<AverageYearlySpendingCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction)
    {
        return new AverageYearlySpendingCliCommand();
    }
}