using Cli.Commands.Abstractions.Exceptions;
using Cli.Commands.Abstractions.Outcomes;

namespace Cli.Commands.Abstractions;

public abstract class NoCliCommandHandler<TCommand> : ICliCommandHandler<TCommand> where TCommand : ICliCommand
{
    public Task<CliCommandOutcome> Handle(TCommand request, CancellationToken cancellationToken)
    {
        // TODO: Don't hard code 'CliCommand!'
        var commandName = typeof(TCommand).Name.Replace("CliCommand", string.Empty);
        var outcome = new CliCommandOutputOutcome($"No functionality for {commandName} base command");
        return Task.FromResult<CliCommandOutcome>(outcome);
    }
}