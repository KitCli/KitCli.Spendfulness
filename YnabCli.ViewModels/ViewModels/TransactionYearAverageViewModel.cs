using Cli.ViewModel.Abstractions;

namespace YnabCli.ViewModels.ViewModels;

public class TransactionYearAverageViewModel : CliTable
{
    public const string YearColumNName = "Yeear";
    public const string AverageAmountColumNName = "Average Amount";
    public const string PercentageIncreaseColumnNName = "% Increase";
    
    public static List<string> GetColumnNames() 
        => [YearColumNName, AverageAmountColumNName, PercentageIncreaseColumnNName];
}