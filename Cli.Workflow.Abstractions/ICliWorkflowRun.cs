using Cli.Commands.Abstractions.Outcomes;

namespace Cli.Workflow.Abstractions;

public interface ICliWorkflowRun
{
    ICliWorkflowRunState State { get; }
    ValueTask<CliCommandOutcome[]> RespondToAsk(string? ask);
}