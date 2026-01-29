using KitCli.Commands.Abstractions.Handlers;
using KitCli.Commands.Abstractions.Outcomes;

namespace SpendfulnessCli.Commands.Personalisation.Commitments.Find;

public class FindCommitmentCliCommandHandler : CliCommandHandler, ICliCommandHandler<FindCommitmentCliCommand>
{
    public async Task<CliCommandOutcome[]> Handle(FindCommitmentCliCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement FindCommitmentCliCommandHandler
        await Task.Delay(0, cancellationToken);
        return OutcomeAs("This is a message");
    }
}