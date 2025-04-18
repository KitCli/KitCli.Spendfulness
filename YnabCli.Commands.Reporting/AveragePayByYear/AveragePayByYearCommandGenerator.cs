using YnabCli.Commands.Generators;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Reporting.AveragePayByYear;

public class AveragePayByYearCommandGenerator : ICommandGenerator, ITypedCommandGenerator<AveragePayByYearCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument>? arguments)
    {
        return new AveragePayByYearCommand();
    }
}