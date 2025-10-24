using MediatR;
using YnabCli.Abstractions;

namespace YnabCli.Commands;

public interface ICommand : IRequest<CliCommandOutcome>
{
}