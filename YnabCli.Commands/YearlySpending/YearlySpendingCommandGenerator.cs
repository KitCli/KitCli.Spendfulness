using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.YearlySpending;

public class YearlySpendingCommandGenerator : ICommandGenerator, ITypedCommandGenerator<YearlySpendingCommand>
{
    public ICommand Generate(List<InstructionArgument> arguments) => new YearlySpendingCommand();
}