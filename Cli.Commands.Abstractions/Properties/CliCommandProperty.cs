namespace Cli.Commands.Abstractions.Properties;

public class CliCommandProperty(string propertyKey)
{
    public string PropertyKey { get; set; } = propertyKey;
}