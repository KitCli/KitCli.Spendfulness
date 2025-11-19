using Cli.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using ConsoleTables;

namespace Cli.Commands.Abstractions;

public abstract class CliCommandHandler
{
    protected static CliCommandOutcome[] Compile(CliTable cliTable) => [new CliCommandTableOutcome(cliTable)];

    protected static CliCommandOutcome[] Compile<TAggregate>(CliAggregator<TAggregate> aggregator)
        => [new CliCommandAggregatorOutcome<TAggregate>(aggregator)];

    protected static CliCommandOutcome[] Compile(string message) => [new CliCommandOutputOutcome(message)];
}