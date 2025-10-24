using Cli.Commands.Abstractions.Outcomes;
using MediatR;

namespace Cli.Commands.Abstractions;

public interface ICliCommandHandler<in TCommand> : IRequestHandler<TCommand, CliCommandOutcome> where TCommand : ICliCommand
{
}