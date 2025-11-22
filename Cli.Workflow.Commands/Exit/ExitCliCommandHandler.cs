using Cli.Commands.Abstractions.Handlers;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Outcomes.Final;
using Cli.Workflow.Abstractions;

namespace Cli.Workflow.Commands.Exit;

// TODO: Write unit tests.
public class ExitCliCommandHandler(ICliWorkflow cliWorkflow) : CliCommandHandler, ICliCommandHandler<ExitCliCommand>
{
    public Task<CliCommandOutcome[]> Handle(ExitCliCommand command, CancellationToken cancellationToken)
    {
        cliWorkflow.Stop();
        
        var outcome = new CliCommandOutputOutcome("Exiting CLI workflow.");
        return Task.FromResult<CliCommandOutcome[]>([outcome]);
    }
}