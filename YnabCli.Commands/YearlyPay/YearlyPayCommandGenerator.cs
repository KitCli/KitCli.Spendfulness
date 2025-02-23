using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.YearlyPay;

public class YearlyPayCommandGenerator : ICommandGenerator, ITypedCommandGenerator<YearlyPayCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument>? arguments)
    {
        return new YearlyPayCommand();
    }
}