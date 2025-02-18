namespace YnabCli.ViewModels.Formatters;

public static class PercentageDisplayFormatter
{
    public static string Format(decimal percentage) => $"{percentage}%";
}