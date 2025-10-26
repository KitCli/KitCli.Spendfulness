using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Ynab.Commands.Reporting.YearlySpending;

public class YearlySpendingCliCommandGenerator : ICliCommandGenerator<YearlySpendingCliCommand>
{ 
    public ICliCommand Generate(CliInstruction instruction) => new YearlySpendingCliCommand();
}