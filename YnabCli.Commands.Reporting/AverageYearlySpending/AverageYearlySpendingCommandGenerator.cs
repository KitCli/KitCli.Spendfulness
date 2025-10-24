using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using YnabCli.Commands.Generators;

namespace YnabCli.Commands.Reporting.AverageYearlySpending;

public class AverageYearlySpendingCommandGenerator : ICommandGenerator<AverageYearlySpendingCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return new AverageYearlySpendingCommand();
    }
}