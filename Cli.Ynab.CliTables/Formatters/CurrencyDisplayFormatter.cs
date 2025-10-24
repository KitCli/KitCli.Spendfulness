namespace Cli.Ynab.CliTables.Formatters;

public static class CurrencyDisplayFormatter
{
    public static string Format(decimal currencyValue) => $"{currencyValue:C}";
}