using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Ynab.Commands.Reporting.YearlySpending;

public class YearlySpendingCommandGenerator : ICommandGenerator<YearlySpendingCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments) => new YearlySpendingCliCommand();
}