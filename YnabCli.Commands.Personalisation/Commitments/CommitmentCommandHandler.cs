using ConsoleTables;
using YnabCli.Commands.Handlers;

namespace YnabCli.Commands.Personalisation.Commitments;

public class CommitmentCommandHandler : ICommandHandler<CommitmentCommand>
{
    public Task<ConsoleTable> Handle(CommitmentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}