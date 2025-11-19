using Cli.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using ConsoleTables;

namespace Cli.Commands.Abstractions;

public abstract class CliCommandHandler
{
    protected static CliCommandOutcome[] Compile(CliTable cliTable)
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
        
        return [new CliCommandTableOutcome(table)];
    }
    
    protected static CliCommandOutcome[] Compile<TAggregate>(CliAggregator<TAggregate> aggregator)
        => [new CliCommandAggregatorOutcome<TAggregate>(aggregator)];

    protected static CliCommandOutcome[] Compile(string message) => [new CliCommandOutputOutcome(message)];
}