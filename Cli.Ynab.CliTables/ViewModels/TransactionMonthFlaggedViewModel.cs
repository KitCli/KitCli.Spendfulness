using Cli.ViewModel.Abstractions;

namespace Cli.Ynab.CliTables.ViewModels;

public class TransactionMonthFlaggedViewModel : CliTable
{
    public const string MonthColumnName = "Month";

    public static List<string> GetColumnNames() => [MonthColumnName];
}