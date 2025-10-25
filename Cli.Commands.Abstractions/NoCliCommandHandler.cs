using Cli.Commands.Abstractions.Exceptions;
using Cli.Commands.Abstractions.Outcomes;

namespace Cli.Commands.Abstractions;

public abstract class NoCliCommandHandler<TCommand> : ICliCommandHandler<TCommand> where TCommand : ICliCommand
{
    public Task<CliCommandOutcome> Handle(TCommand request, CancellationToken cancellationToken)
    {
        throw new CliCommandException(
            CliCommandExceptionCode.NoCommandFunctionality,
            $"No functionality for {nameof(TCommand)} base command");
    }
}