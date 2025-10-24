using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using YnabCli.Commands.Generators;

namespace YnabCli.Commands.Reporting.YearlySpending;

public class YearlySpendingCommandGenerator : ICommandGenerator<YearlySpendingCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments) => new YearlySpendingCommand();
}