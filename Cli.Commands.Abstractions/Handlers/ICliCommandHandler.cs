using Cli.Commands.Abstractions.Outcomes;
using MediatR;

namespace Cli.Commands.Abstractions.Handlers;

public interface ICliCommandHandler<in TCommand> : IRequestHandler<TCommand, CliCommandOutcome[]> where TCommand : CliCommand
{
}