using Cli.Commands.Abstractions.Outcomes;
using MediatR;

namespace Cli.Commands.Abstractions;

public interface ICommand : IRequest<CliCommandOutcome>
{
}