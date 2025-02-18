using YnabCli.Instructions.InstructionArguments;

namespace YnabCli.Commands.YearlyPay;

public class YearlyPayCommandGenerator : ICommandGenerator, ITypedCommandGenerator<YearlyPayCommand>
{
    public ICommand Generate(List<InstructionArgument> arguments)
    {
        return new YearlyPayCommand();
    }
}