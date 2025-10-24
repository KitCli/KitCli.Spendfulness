using Cli.Outcomes;
using ConsoleTables;
using YnabCli.Abstractions;
using YnabCli.ViewModels.ViewModels;

namespace YnabCli.Commands.Handlers;

// TODO: Rename to CliCommandHandler.
public abstract class CommandHandler
{
    protected static CliCommandTableOutcome Compile(ViewModel viewModel)
    {
        var table = new ConsoleTable
        {
            Options =
            {
                EnableCount = viewModel.ShowRowCount
            }
        };

        table.AddColumn(viewModel.Columns.ToArray());
       
        foreach (var row in viewModel.Rows)
            table.AddRow(row.ToArray());
        
        return new CliCommandTableOutcome(table);
    }

    protected static CliCommandOutputOutcome Compile(string message) => new(message);
}