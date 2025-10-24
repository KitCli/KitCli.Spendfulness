using ConsoleTables;
using MediatR;
using YnabCli.Abstractions;

namespace YnabCli.Commands.Handlers;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, CliCommandOutcome> where TCommand : ICommand
{
    
}