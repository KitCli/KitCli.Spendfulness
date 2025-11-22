using Cli.Commands.Abstractions.Handlers;
using Cli.Commands.Abstractions.Outcomes;

namespace Cli.Workflow.Commands.MissingOutcomes;

// TODO: Write uit tests.
public class MissingOutcomesCliCommandHandler : CliCommandHandler, ICliCommandHandler<MissingOutcomesCliCommand>
{
    private const string Message = "The following prerequisite outcomes were not returned from previous commands:";
    
    public Task<CliCommandOutcome[]> Handle(MissingOutcomesCliCommand command, CancellationToken cancellationToken)
    {
        var missingOutcomeList = string.Join(", ", command.MissingOutcomeNames);
        return AsyncOutcomeAs($"{Message} {missingOutcomeList}");
    }
}