namespace Cli.Commands.Abstractions.Properties;

public class CustomCliCommandProperty<TPropertyValue>(string propertyKey, TPropertyValue propertyValue) : CliCommandProperty(propertyKey)
    where TPropertyValue : class
{
    public TPropertyValue PropertyValue { get; set; } = propertyValue;
}