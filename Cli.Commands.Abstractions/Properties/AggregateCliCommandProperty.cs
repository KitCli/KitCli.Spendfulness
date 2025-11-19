using Cli.Abstractions;

namespace Cli.Commands.Abstractions.Properties;

public record AggregateCliCommandProperty<TAggregate>(CliAggregator<TAggregate> Value) 
    : ValuedCliCommandProperty<CliAggregator<TAggregate>>(Value);