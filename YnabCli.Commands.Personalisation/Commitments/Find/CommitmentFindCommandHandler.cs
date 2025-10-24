using ConsoleTables;
using YnabCli.Abstractions;
using YnabCli.Commands.Handlers;

namespace YnabCli.Commands.Personalisation.Commitments.Find;

public class CommitmentFindCommandHandler : CommandHandler, ICommandHandler<CommitmentFindCommand>
{
    public async Task<CliCommandOutcome> Handle(CommitmentFindCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(0, cancellationToken);
        return Compile("This is a message");
    }
}