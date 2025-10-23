using Cli.Instructions.Arguments;
using YnabCli.Commands.Generators;

namespace YnabCli.Commands.Reporting.AverageYearlyPay;

public class AverageYearlyPayGenericCommandGenerator : ICommandGenerator<AverageYearlyPayCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument>? arguments)
    {
        return new AverageYearlyPayCommand();
    }
}