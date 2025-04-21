using YnabCli.Commands.Generators;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Reporting.AverageYearlyPay;

public class AverageYearlyPayGenericCommandGenerator : ICommandGenerator<AverageYearlyPayCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument>? arguments)
    {
        return new AverageYearlyPayCommand();
    }
}