namespace YnabProgressConsole.Compilation.SpareMoneyView;

public class SpareMoneyViewModel : ViewModel
{
    public const string SpareMoneyColumnName = "Spare Money";
    
    public static List<string> GetColumnNames() 
        => [SpareMoneyColumnName];
}