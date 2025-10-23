using Cli.Instructions.Arguments;
using YnabCli.Commands.Generators;

namespace YnabCli.Commands.Reporting.AverageYearlySpending;

public class AverageYearlySpendingCommandGenerator : ICommandGenerator<AverageYearlySpendingCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return new AverageYearlySpendingCommand();
    }
}