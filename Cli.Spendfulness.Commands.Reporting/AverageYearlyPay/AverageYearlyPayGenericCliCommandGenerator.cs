using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Ynab.Commands.Reporting.AverageYearlyPay;

public class AverageYearlyPayGenericCliCommandGenerator : ICliCommandGenerator<AverageYearlyPayCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction)
    {
        return new AverageYearlyPayCliCommand();
    }
}