using Cli.Commands.Abstractions.Outcomes;
using MediatR;

namespace Cli.Commands.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, CliCommandOutcome> where TCommand : ICommand
{
}