using ConsoleTables;
using MediatR;

namespace YnabCli.Commands;

public interface ICommand : IRequest<ConsoleTable>
{
}