using Cli.Abstractions;
using Cli.Abstractions.Aggregators;

namespace Cli.Commands.Abstractions.Artefacts.Aggregator;

public class AggregatorCliCommandArtefact<TAggregate>(CliAggregator<TAggregate> value)
    : ValuedCliCommandArtefact<CliAggregator<TAggregate>>(value)
{
    
}