using Cli.Abstractions;
using Cli.Commands.Abstractions.Properties.CommandRan;

namespace Cli.Commands.Abstractions.Properties;

public static class CliCommandPropertyExtensions
{
    public static bool LastCommandRanWas<TCliCommand>(this List<CliCommandProperty> properties)
        where TCliCommand : CliCommand
    {
        var lastCommandRanProperty = properties.OfType<CliCommandRanProperty>().LastOrDefault();
        if (lastCommandRanProperty == null)
        { 
            return false;
        }
        
        return lastCommandRanProperty.RanCommand is TCliCommand;
    }
    
    public static List<ListAggregatorCliCommandProperty<TAggregate>> OfListAggregatorType<TAggregate>(
        this List<CliCommandProperty> properties)
        => properties.OfType<ListAggregatorCliCommandProperty<TAggregate>>().ToList();

    public static CliListAggregator<TAggregate>? GetListAggregator<TAggregate>(
        this List<CliCommandProperty> properties)
        => properties
            .OfListAggregatorType<TAggregate>()
            .FirstOrDefault()
            ?.Value;
}