using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using YnabCli.Commands.Generators;

namespace YnabCli.Commands.Reporting.AverageYearlyPay;

public class AverageYearlyPayGenericCommandGenerator : ICommandGenerator<AverageYearlyPayCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument>? arguments)
    {
        return new AverageYearlyPayCommand();
    }
}