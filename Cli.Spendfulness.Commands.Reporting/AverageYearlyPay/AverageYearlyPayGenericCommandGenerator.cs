using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Ynab.Commands.Reporting.AverageYearlyPay;

public class AverageYearlyPayGenericCommandGenerator : ICommandGenerator<AverageYearlyPayCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument>? arguments)
    {
        return new AverageYearlyPayCliCommand();
    }
}