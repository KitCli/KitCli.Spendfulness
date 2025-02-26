using ConsoleTables;
using MediatR;

namespace YnabCli.Commands.Handlers;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, ConsoleTable> where TCommand : ICommand
{
    
}