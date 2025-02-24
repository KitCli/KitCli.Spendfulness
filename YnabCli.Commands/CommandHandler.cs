using ConsoleTables;
using YnabCli.ViewModels.ViewModels;

namespace YnabCli.Commands;

public abstract class CommandHandler
{
    protected static ConsoleTable Compile(ViewModel viewModel)
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
        
        return table;
    }

    protected static ConsoleTable CompileMessage(string message)
    {
        return new ConsoleTable
        {
            Columns = { message },
            Options =
            {
                EnableCount = false
            }
        };
    }
}