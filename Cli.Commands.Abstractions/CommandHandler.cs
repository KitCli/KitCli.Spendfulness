using Cli.Outcomes;
using ConsoleTables;

namespace Cli.Commands.Abstractions;

// TODO: Rename to CliCommandHandler.
public abstract class CommandHandler
{
    protected static CliCommandTableOutcome Compile(ViewModel.Abstractions.CliTable cliTable)
    {
        var table = new ConsoleTable
        {
            Options =
            {
                EnableCount = cliTable.ShowRowCount
            }
        };

        table.AddColumn(cliTable.Columns.ToArray());
       
        foreach (var row in cliTable.Rows)
            table.AddRow(row.ToArray());
        
        return new CliCommandTableOutcome(table);
    }

    protected static CliCommandOutputOutcome Compile(string message) => new(message);
}