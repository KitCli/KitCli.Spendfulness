using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Reporting.Table;

public class TableCliCommandGenerator : ICliCommandGenerator<TableCliCommand>
{
    public CliCommand Generate(CliInstruction instruction)
    {
        return new TableCliCommand();
    }
}