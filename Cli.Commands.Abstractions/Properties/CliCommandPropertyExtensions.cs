namespace Cli.Commands.Abstractions.Properties;

public static class CliCommandPropertyExtensions
{
    public static CustomCliCommandProperty<TPropertyValueType>? OfType<TPropertyValueType>(
        this IEnumerable<CliCommandProperty> arguments, string propertyKey)
        where TPropertyValueType : class
        => arguments
            .Where(property => property.PropertyKey == propertyKey)
            .OfType<CustomCliCommandProperty<TPropertyValueType>>()
            .FirstOrDefault();
}