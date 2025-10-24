namespace Cli.Ynab.CliTables.Formatters;

public static class PercentageDisplayFormatter
{
    public static string Format(decimal percentage) => $"{percentage}%";
}