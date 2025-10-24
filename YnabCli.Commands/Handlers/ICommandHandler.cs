using Cli.Commands.Abstractions;
using Cli.Outcomes;
using MediatR;

namespace YnabCli.Commands.Handlers;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, CliCommandOutcome> where TCommand : ICommand
{
    
}