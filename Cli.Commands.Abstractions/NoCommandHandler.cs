using Cli.Commands.Abstractions.Exceptions;
using Cli.Commands.Abstractions.Outcomes;

namespace Cli.Commands.Abstractions;

public abstract class NoCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
{
    public Task<CliCommandOutcome> Handle(TCommand request, CancellationToken cancellationToken)
    {
        throw new CliCommandException(
            CliCommandExceptionCode.NoCommandFunctionality,
            $"No functionality for {nameof(TCommand)} base command");
    }
}