using YnabCli.Commands.Generators;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Reporting.YearlySpending;

public class YearlySpendingGenericCommandGenerator : ICommandGenerator<YearlySpendingCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument> arguments) => new YearlySpendingCommand();
}