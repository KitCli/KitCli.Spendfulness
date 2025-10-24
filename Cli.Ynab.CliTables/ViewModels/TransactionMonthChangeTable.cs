using Cli.ViewModel.Abstractions;

namespace Cli.Ynab.CliTables.ViewModels;

public class TransactionMonthChangeTable : CliTable
{
    public const string MonthColumnName = "Yeear";
    public const string TotalAmountColumnName = "Total Amount";
    public const string PercentageChangeColumnName = "% Change";
    
    public static List<string> GetColumnNames() 
        => [MonthColumnName, TotalAmountColumnName, PercentageChangeColumnName];
}