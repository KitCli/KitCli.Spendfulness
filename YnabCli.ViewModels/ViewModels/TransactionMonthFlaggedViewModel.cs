using Cli.ViewModel.Abstractions;

namespace YnabCli.ViewModels.ViewModels;

public class TransactionMonthFlaggedViewModel : CliTable
{
    public const string MonthColumnName = "Month";

    public static List<string> GetColumnNames() => [MonthColumnName];
}