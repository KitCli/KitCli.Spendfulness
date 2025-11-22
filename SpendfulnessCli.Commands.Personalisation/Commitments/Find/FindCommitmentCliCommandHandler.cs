using Cli.Commands.Abstractions.Handlers;
using Cli.Commands.Abstractions.Outcomes;

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